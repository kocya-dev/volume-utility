using NAudio.CoreAudioApi;
using System.Diagnostics;
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
        private Draggable _draggable;

        public Main()
        {
            InitializeComponent();

            var attribute = NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = NativeMethods.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            NativeMethods.DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            _draggable = new Draggable(this);
            _volumeController = new VolumeController(VolumeChangedCallback);
        }

        /// <summary>
        /// �t�H�[�����[�h���̏���
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Debug.Assert(SynchronizationContext.Current != null);
            _context = SynchronizationContext.Current;
            _volumeController.MinVolume = (float)_numericUpDownMin.Value;
            _volumeController.MaxVolume = (float)_numericUpDownMax.Value;
            _trackBarVolume.Value = (int)_volumeController.CurrentVolume;
            UpdateCurrentMuteStatus(_volumeController.IsMute);
            UpdateCurrentVolumeText();

            base.OnLoad(e);
        }

        /// <summary>
        /// �t�H�[���N���[�Y��̏���
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _volumeController.Dispose();
            _draggable.Dispose();
            base.OnFormClosed(e);
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
            using(var dialog = new VolumeUtilityConfigDialog())
            {
                dialog.OpacityValue = Opacity;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Opacity = dialog.OpacityValue;
                }
            }
        }
    }
}
