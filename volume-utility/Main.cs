using NAudio.CoreAudioApi;
using System.Diagnostics;
using volume_utility.Properties;
using volume_utility.Utils;
using volume_utility.View;

namespace volume_utility
{
    public partial class Main : Form
    {
        /// <summary>
        /// ボリューム操作クラス
        /// </summary>
        private VolumeController _volumeController;
        /// <summary>
        /// UI更新用のコンテキスト
        /// </summary>
        private SynchronizationContext? _context = null;
        private Draggable _draggable;

        public Main()
        {
            InitializeComponent();

            var attribute = NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = NativeMethods.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            NativeMethods.DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));

            _draggable = new Draggable(this);
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
            UpdateCurrentMuteStatus(_volumeController.IsMute);
            UpdateCurrentVolumeText();

            base.OnLoad(e);
        }

        /// <summary>
        /// フォームクローズ後の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SaveSettings();

            _volumeController.Dispose();
            _draggable.Dispose();
            base.OnFormClosed(e);
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
        /// トラックバーのUI更新中かどうか
        /// </summary>
        private bool isChagingVolume = false;
        /// <summary>
        /// トラックバーの値変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _trackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            if (isChagingVolume) return;

            isChagingVolume = true;
            float nextValue = _volumeController.GetNextVolume(_trackBarVolume.Value);
            if ((int)nextValue != _trackBarVolume.Value)
            {
                _trackBarVolume.Value = (int)nextValue;
            }
            Debug.WriteLine($"_trackBarVolume_ValueChanged: {_trackBarVolume.Value}, next: {nextValue}");
            _volumeController.CurrentVolume = nextValue;
            isChagingVolume = false;
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
                isChagingVolume = true;
                Debug.WriteLine($"VolumeChangedCallback next: {nextValue}, muted: {data.Muted}");
                _trackBarVolume.Value = (int)nextValue;
                UpdateCurrentVolumeText();
                UpdateCurrentMuteStatus(data.Muted);
                isChagingVolume = false;
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
        /// <param name="sender"></param>
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
        /// 設定ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonConfig_Click(object sender, EventArgs e)
        {
            OpenConfigDialog();
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
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.MinValue = (int)_numericUpDownMin.Value;
            Settings.Default.MaxValue = (int)_numericUpDownMax.Value;
            Settings.Default.Opacity = Opacity;
            Settings.Default.Save();
        }

    }
}
