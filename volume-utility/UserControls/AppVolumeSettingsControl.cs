using volume_utility.Controller;

namespace volume_utility.UserControls
{
    public partial class AppVolumeSettingsControl : UserControl
    {
        /// <summary>
        /// ボリュームコントローラ
        /// </summary>
        private VolumeController? _controller;

        /// <summary>
        /// ボリューム設定
        /// </summary>
        public VolumeSetting Setting { get; private set; }
        /// <summary>
        /// アクティブかどうか
        /// </summary>
        public bool IsActive
        {
            get
            {
                return BackColor != Color.FromKnownColor(KnownColor.Control);
            }
            set
            {
                BackColor = value ? Color.FromKnownColor(KnownColor.GradientInactiveCaption) : Color.FromKnownColor(KnownColor.Control);
            }
        }

        /// <summary>
        /// 削除ボタンクリック時のイベント
        /// </summary>
        public event EventHandler RemoveRequested = delegate { };

        /// <summary>
        /// コンストラクタ(デザイナ用)
        /// </summary>
        public AppVolumeSettingsControl()
        {
            InitializeComponent();
            Setting = new VolumeSetting(string.Empty, string.Empty, 0);
            _controller = null;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="setting"></param>
        public AppVolumeSettingsControl(VolumeController controller, VolumeSetting setting)
        {
            InitializeComponent();
            _controller = controller;
            Setting = setting;
            UpdateSettings();
        }

        /// <summary>
        /// ボリュームを更新する
        /// </summary>
        /// <param name="volume"></param>
        public void UpdateVolume(int volume)
        {
            Setting.Volume = volume;
            _trackBar.Value = Setting.Volume;
            _labelVolume.Text = _trackBar.Value.ToString();
        }
        /// <summary>
        /// プロセス名を更新する
        /// </summary>
        private void UpdateSettings()
        {
            _trackBar.Value = Setting.Volume;
            _labelVolume.Text = _trackBar.Value.ToString();

            _labelProcessName.Text = Setting.ProcessName;
            _labelAppName.Text = Setting.ApplicationName;
        }
        /// <summary>
        /// トラックバーの値が変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_controller == null) return;
            if (_controller.IsChanging) return;

            float nextValue = _controller.GetNextVolume(_trackBar.Value);
            if ((int)nextValue != _trackBar.Value)
            {
                _trackBar.Value = (int)nextValue;
                return;
            }
            _labelVolume.Text = _trackBar.Value.ToString();
            Setting.Volume = _trackBar.Value;
            if (IsActive)
            {
                _controller.CurrentVolume = Setting.Volume;
            }
        }
        /// <summary>
        /// 削除ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveRequested(this, EventArgs.Empty);
        }
    }
}
