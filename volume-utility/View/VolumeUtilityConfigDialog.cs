using volume_utility.Utils;

namespace volume_utility.View
{
    public partial class VolumeUtilityConfigDialog : Form
    {
        /// <summary>
        /// ドラッグ可能クラス
        /// </summary>
        private Draggable _draggable;

        /// <summary>
        /// 不透過度の値
        /// </summary>
        public double OpacityValue
        {
            get { return (double)_trackBarOpacity.Value / 100; }
            set { _trackBarOpacity.Value = (int)(Math.Round(value * 100)); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VolumeUtilityConfigDialog()
        {
            InitializeComponent();
            var attribute = NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = NativeMethods.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            NativeMethods.DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));

            _draggable = new Draggable(this);
        }

        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Opacity = OpacityValue;
            base.OnLoad(e);
        }

        /// <summary>
        /// フォームクローズ後の処理
        /// </summary>
        /// <param name="e"></param>
        override protected void OnFormClosed(FormClosedEventArgs e)
        {
            _draggable.Dispose();
            base.OnFormClosed(e);
        }

        /// <summary>
        /// 透明度トラックバーの値変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _trackBarOpacity_ValueChanged(object sender, EventArgs e)
        {
            _labelCurrentOpacity.Text = _trackBarOpacity.Value.ToString();
            Opacity = OpacityValue;
        }

        /// <summary>
        /// OKボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// キャンセルボタンクリック時の処理 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
