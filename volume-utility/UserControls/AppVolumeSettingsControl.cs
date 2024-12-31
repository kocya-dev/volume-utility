using volume_utility.Controller;

namespace volume_utility.UserControls
{
    public partial class AppVolumeSettingsControl : UserControl
    {
        public VolumeSetting? Setting { get; private set; }

        public AppVolumeSettingsControl()
        {
            InitializeComponent();
        }

        public AppVolumeSettingsControl(VolumeSetting? setting = null)
        {
            InitializeComponent();
            Setting = setting;
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            _trackBar.Value = (int)(Setting?.Volume ?? 0);
            _labelVolume.Text = _trackBar.Value.ToString();

            _labelProcessName.Text = Setting?.ProcessName ?? string.Empty;
            _labelAppName.Text = Setting?.ApplicationName ?? string.Empty;
        }
    }
}
