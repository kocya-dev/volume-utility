using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Windows.Forms;
using volume_utility.Controller;
using volume_utility.Properties;
using volume_utility.Utils;
using volume_utility.View;

namespace volume_utility
{
    public partial class Main : Form
    {
        /// <summary>
        /// �{�����[������N���X
        /// </summary>
        private VolumeController _volumeController;
        /// <summary>
        /// UI�X�V�p�̃R���e�L�X�g
        /// </summary>
        private SynchronizationContext? _context = null;
        /// <summary>
        /// �h���b�O�\�N���X
        /// </summary>
        private Draggable _draggable;
        /// <summary>
        /// �E�B���h�E�̕\�����
        /// </summary>
        private bool _isWindowVisible = false;
        /// <summary>
        /// �X�^�[�g�A�b�v�t���O
        /// </summary>
        private bool _isStartup = false;

        public Main()
        {
            InitializeComponent();

            NativeMethods.EnableRoundWindowStyle(Handle);

            _draggable = new Draggable(this);
            _volumeController = new VolumeController(VolumeChangedCallback);

            LoadSettings();
        }
        /// <summary>
        /// �t�H�[�����[�h���̏���
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Debug.Assert(SynchronizationContext.Current != null);

            // UI�Ɋւ������X�V
            _context = SynchronizationContext.Current;
            _trackBarVolume.Value = (int)_volumeController.CurrentVolume;
            _toolStripMenuItemVisible.Checked = _isWindowVisible;
            _toolStripMenuItemStartup.Checked = _isStartup;
            UpdateCurrentMuteStatus(_volumeController.IsMute);
            UpdateCurrentVolumeText();
            UpdateWindowVisibility();
            NativeMethods.StartHook(Handle);
            base.OnLoad(e);
        }

        /// <summary>
        /// �t�H�[���N���[�Y��̏���
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
        /// �t�H�[�����T�C�Y���̏���
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
        /// �t�H�[���̃E�B���h�E���b�Z�[�W����
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_HOOK_ACTIVATE)
            {
                Debug.WriteLine($"WndProc: {m.Msg} {m.WParam} {m.LParam}");
                Process? p = NativeMethods.getProcess(m.LParam);
                if (p != null)
                {
                    Debug.WriteLine($"{p.ProcessName} {p.MainModule?.FileVersionInfo.ProductName}");//�A�v���P�[�V�����̖��O���o�Ă��܂�
                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// �ŏ��l�R���g���[���̒l�ύX���̏���
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
        /// �ő�l�R���g���[���̒l�ύX���̏���
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
        /// �g���b�N�o�[��UI�X�V�����ǂ���
        /// </summary>
        private bool isChagingVolume = false;
        /// <summary>
        /// �g���b�N�o�[�̒l�ύX���̏���
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
        /// �{�����[���ύX���̃R�[���o�b�N
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
        /// �~���[�g�`�F�b�N�{�b�N�X�̒l�ύX���̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBoxMute_CheckedChanged(object sender, EventArgs e)
        {
            _volumeController.IsMute = _checkBoxMute.Checked;
        }

        /// <summary>
        /// �I�����j���[�̃N���b�N���̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// ���݂̃{�����[���e�L�X�g���X�V����
        /// </summary>
        private void UpdateCurrentVolumeText()
        {
            _labelCurrentVolume.Text = $"{_trackBarVolume.Value} / 100";
        }

        /// <summary>
        /// ���݂̃~���[�g��ԕ\�����X�V����
        /// </summary>
        /// <param name="isMute"></param>
        private void UpdateCurrentMuteStatus(bool isMute)
        {
            _checkBoxMute.Checked = isMute;
        }

        /// <summary>
        /// �ݒ胁�j���[�̃N���b�N���̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _toolStripMenuItemConfig_Click(object sender, EventArgs e)
        {
            OpenConfigDialog();
        }

        /// <summary>
        /// �\�����j���[�̃N���b�N���̏���
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
        /// �X�^�[�g�A�b�v���j���[�̃N���b�N���̏���
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
        /// �ݒ�{�^���N���b�N���̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonConfig_Click(object sender, EventArgs e)
        {
            OpenConfigDialog();
        }
        /// <summary>
        /// �ʒm�A�C�R���̃_�u���N���b�N���̏���
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
        /// �ݒ�̓ǂݍ���
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
        }

        /// <summary>
        /// �ݒ�̕ۑ�
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.MinValue = (int)_numericUpDownMin.Value;
            Settings.Default.MaxValue = (int)_numericUpDownMax.Value;
            Settings.Default.Opacity = Opacity;
            Settings.Default.Visible = _isWindowVisible;
            Settings.Default.Startup = _isStartup;
            Settings.Default.Save();
        }
        /// <summary>
        /// �ݒ�_�C�A���O���J��
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
        /// �E�B���h�E�̕\����Ԃ��X�V����
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
        /// �X�^�[�g�A�b�v�V���[�g�J�b�g�̍X�V
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
