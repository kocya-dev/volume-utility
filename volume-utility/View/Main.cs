using NAudio.CoreAudioApi;
using System.Diagnostics;
using volume_utility.Controller;
using volume_utility.Properties;
using volume_utility.UserControls;
using volume_utility.Utils;
using volume_utility.View;
using System.Resources;
using System.Globalization;

namespace volume_utility
{
    public partial class Main : Form
    {
        /// <summary>
        /// ボリューム操作クラス
        /// </summary>
        private readonly VolumeController _volumeController;
        /// <summary>
        /// アプリケーションごとのボリューム設定
        /// </summary>
        private readonly ApplicationVolumeSettings _appVolumeSettings = new ApplicationVolumeSettings();
        /// <summary>
        /// UI更新用のコンテキスト
        /// </summary>
        private SynchronizationContext? _context = null;
        /// <summary>
        /// ドラッグ可能クラス
        /// </summary>
        private Draggable _draggable;
        /// <summary>
        /// ウィンドウの表示状態
        /// </summary>
        private bool _isWindowVisible = false;
        /// <summary>
        /// スタートアップフラグ
        /// </summary>
        private bool _isStartup = false;

        public Main()
        {
            InitializeComponent();

            NativeMethods.EnableRoundWindowStyle(Handle);

            _draggable = new Draggable(_panel, this);
            _volumeController = new VolumeController(VolumeChangedCallback);

            LoadSettings();
        }
        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Debug.Assert(SynchronizationContext.Current != null);

            // UIに関わる情報を更新
            _context = SynchronizationContext.Current;
            _trackBarVolume.Value = (int)_volumeController.CurrentVolume;
            _toolStripMenuItemVisible.Checked = _isWindowVisible;
            _toolStripMenuItemStartup.Checked = _isStartup;
            UpdateCurrentMuteStatus(_volumeController.IsMute);
            UpdateCurrentVolumeText();
            UpdateWindowVisibility();
            _appVolumeSettings.VolumeSettings.ForEach(AddAppSettingControl);
            NativeMethods.StartHook(Handle);
            base.OnLoad(e);
        }

        /// <summary>
        /// フォームクローズ後の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            NativeMethods.EndHook();
            SaveSettings();

            _volumeController.Dispose();
            _draggable.Dispose();
            base.OnFormClosed(e);
        }

        /// <summary>
        /// フォームリサイズ時の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
            base.OnResize(e);
        }

        /// <summary>
        /// フォームのウィンドウメッセージ処理
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_HOOK_ACTIVATE)
            {
                Debug.WriteLine($"WndProc: {m.Msg} {m.WParam} {m.LParam}");
                Process? p = NativeMethods.getProcess(m.LParam);
                if (p != null && p.ProcessName != Process.GetCurrentProcess().ProcessName)
                {
                    Debug.WriteLine($"Setting: {p.ProcessName} {p.MainModule?.FileVersionInfo.ProductName}");
                    // ウィンドウ切り替えに伴うボリューム更新
                    var setting = _appVolumeSettings.Find(p.ProcessName, p.MainModule?.FileVersionInfo.ProductName ?? string.Empty);
                    _volumeController.CurrentVolume = (setting != null)
                        ? setting.Volume // アクティブなプロセスに対応する設定が存在していればボリュームを適用する
                        : _trackBarVolume.Value; // アクティブなプロセスに対応する設定が存在していなければマスターボリュームを適用する

                    // 設定の選択状態を更新する
                    foreach (var contrl in _flowLayoutPanelSettings.Controls)
                    {
                        AppVolumeSettingsControl? c = contrl as AppVolumeSettingsControl;
                        if (c != null)
                        {
                            c.IsActive = (setting != null) 
                                && c.Setting.ProcessName == setting.ProcessName 
                                && c.Setting.ApplicationName == setting.ApplicationName;
                        }
                    }
                    // マスターボリューム設定有効状態の更新
                    _trackBarVolume.Enabled = setting == null;
                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 最小値コントロールの値変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _numericUpDownMin_ValueChanged(object sender, EventArgs e)
        {
            _volumeController.MinVolume = (float)_numericUpDownMin.Value;
            _numericUpDownMax.Minimum = _numericUpDownMin.Value;
            _trackBarVolume.Value = Math.Max(_trackBarVolume.Value, (int)_volumeController.MinVolume);
        }

        /// <summary>
        /// 最大値コントロールの値変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _numericUpDownMax_ValueChanged(object sender, EventArgs e)
        {
            _volumeController.MaxVolume = (float)_numericUpDownMax.Value;
            _numericUpDownMin.Maximum = _numericUpDownMax.Value;
            _trackBarVolume.Value = Math.Min(_trackBarVolume.Value, (int)_volumeController.MaxVolume);
        }

        /// <summary>
        /// トラックバーの値変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _trackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            if (_volumeController.IsChanging) { return; }

            float nextValue = _volumeController.GetNextVolume(_trackBarVolume.Value);
            if ((int)nextValue != _trackBarVolume.Value)
            {
                _trackBarVolume.Value = (int)nextValue;
                return;
            }
            _labelCurrentVolume.Text = $"{_trackBarVolume.Value} / 100";
            Debug.WriteLine($"_trackBarVolume_ValueChanged set: {_trackBarVolume.Value}, next: {nextValue}");
            _volumeController.CurrentVolume = nextValue;
        }
        /// <summary>
        /// ボリューム変更時のコールバック
        /// </summary>
        /// <param name="nextValue"></param>
        /// <param name="data"></param>
        private void VolumeChangedCallback(float nextValue, AudioVolumeNotificationData data)
        {
            _context?.Post((state) =>
            {
                _volumeController.IsChanging = true;
                Debug.WriteLine($"VolumeChangedCallback next: {nextValue}, muted: {data.Muted}");
                var control = FindActiveSettingControl();
                if (control != null)
                {
                    // アクティブな設定が存在する場合は設定を更新する
                    control.UpdateVolume((int)nextValue);
                }
                else
                {
                    // アクティブな設定が存在しない場合はメインのトラックバーの値を更新する
                    _trackBarVolume.Value = (int)nextValue;
                    UpdateCurrentVolumeText();
                    UpdateCurrentMuteStatus(data.Muted);
                }
                _volumeController.IsChanging = false;
            }, null);
        }

        /// <summary>
        /// ミュートチェックボックスの値変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBoxMute_CheckedChanged(object sender, EventArgs e)
        {
            _volumeController.IsMute = _checkBoxMute.Checked;
        }

        /// <summary>
        /// 終了メニューのクリック時の処理
        /// </summary>
        /// <param="sender"></param>
        /// <param name="e"></param>
        private void _toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 現在のボリュームテキストを更新する
        /// </summary>
        private void UpdateCurrentVolumeText()
        {
            _labelCurrentVolume.Text = $"{_trackBarVolume.Value} / 100";
        }

        /// <summary>
        /// 現在のミュート状態表示を更新する
        /// </summary>
        /// <param name="isMute"></param>
        private void UpdateCurrentMuteStatus(bool isMute)
        {
            _checkBoxMute.Checked = isMute;
        }

        /// <summary>
        /// 設定メニューのクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _toolStripMenuItemConfig_Click(object sender, EventArgs e)
        {
            OpenConfigDialog();
        }

        /// <summary>
        /// 表示メニューのクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _toolStripMenuItemVisible_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemVisible.Checked = !_toolStripMenuItemVisible.Checked;
            _isWindowVisible = _toolStripMenuItemVisible.Checked;
            UpdateWindowVisibility();
        }
        /// <summary>
        /// スタートアップメニューのクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _toolStripMenuItemStartup_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemStartup.Checked = !_toolStripMenuItemStartup.Checked;
            _isStartup = _toolStripMenuItemStartup.Checked;
            UpdateStartupShortcut();
        }

        /// <summary>
        /// 設定ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonConfig_Click(object sender, EventArgs e)
        {
            OpenConfigDialog();
        }
        /// <summary>
        /// 追加ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonAdd_Click(object sender, EventArgs e)
        {
            OpenAddAppDialog();
        }

        /// <summary>
        /// 通知アイコンのダブルクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _isWindowVisible = true;
            _toolStripMenuItemVisible.Checked = true;
            UpdateWindowVisibility();
        }
        /// <summary>
        /// 設定の読み込み
        /// </summary>
        private void LoadSettings()
        {
            _numericUpDownMin.Value = Settings.Default.MinValue;
            _numericUpDownMax.Value = Settings.Default.MaxValue;
            _volumeController.MinVolume = Settings.Default.MinValue;
            _volumeController.MaxVolume = Settings.Default.MaxValue;
            Opacity = Settings.Default.Opacity;
            _isWindowVisible = Settings.Default.Visible;
            _isStartup = Settings.Default.Startup;
            _appVolumeSettings.Import(Settings.Default.AppSettings);
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.MinValue = (int)_numericUpDownMin.Value;
            Settings.Default.MaxValue = (int)_numericUpDownMax.Value;
            Settings.Default.Opacity = Opacity;
            Settings.Default.Visible = _isWindowVisible;
            Settings.Default.Startup = _isStartup;
            Settings.Default.AppSettings = _appVolumeSettings.Export();
            Settings.Default.Save();
        }
        /// <summary>
        /// 設定ダイアログを開く
        /// </summary>
        private void OpenConfigDialog()
        {
            using (var dialog = new VolumeUtilityConfigDialog())
            {
                dialog.OpacityValue = Opacity;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Opacity = dialog.OpacityValue;
                }
            }
        }
        /// <summary>
        /// アプリ追加ダイアログを開く
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OpenAddAppDialog()
        {
            using (var dialog = new ProcessSelectionDialog())
            {
                dialog.Opacity = Opacity;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var setting = new VolumeSetting(dialog.ProcessName, dialog.ApplicationName, _trackBarVolume.Value);
                    Debug.WriteLine($"{dialog.ProcessName} {dialog.ApplicationName}");
                    _appVolumeSettings.Add(setting);
                    AddAppSettingControl(setting);
                }
            }
        }
        /// <summary>
        /// アプリ設定コントロールを追加する
        /// </summary>
        /// <param name="setting"></param>

        private void AddAppSettingControl(VolumeSetting setting)
        {
            AppVolumeSettingsControl control = new AppVolumeSettingsControl(_volumeController, setting);
            control.RemoveRequested += Control_RemoveRequested;
            _flowLayoutPanelSettings.Controls.Add(control);
        }
        /// <summary>
        /// 設定コントロールの削除リクエスト時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Control_RemoveRequested(object? sender, EventArgs e)
        {
            var control = sender as AppVolumeSettingsControl;
            if (control != null)
            {
                control.RemoveRequested -= Control_RemoveRequested;
                _flowLayoutPanelSettings.Controls.Remove(control);
                _appVolumeSettings.Remove(control.Setting);
            }
        }

        /// <summary>
        /// アクティブな設定コントロールを検索する
        /// </summary>
        /// <returns></returns>
        private AppVolumeSettingsControl? FindActiveSettingControl()
        {
            foreach (var contrl in _flowLayoutPanelSettings.Controls)
            {
                AppVolumeSettingsControl? c = contrl as AppVolumeSettingsControl;
                if (c != null && c.IsActive)
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// ウィンドウの表示状態を更新する
        /// </summary>
        private void UpdateWindowVisibility()
        {
            if (_isWindowVisible)
            {
                Show();
                WindowState = FormWindowState.Normal;
                Activate();
            }
            else
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        /// <summary>
        /// スタートアップショートカットの更新
        /// </summary>
        private void UpdateStartupShortcut()
        {
            if (_isStartup)
            {
                StartupUtility.Create();
            }
            else
            {
                StartupUtility.Delete();
            }
        }
    }
}
