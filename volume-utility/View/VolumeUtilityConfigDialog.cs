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
            NativeMethods.EnableRoundWindowStyle(Handle);

            _draggable = new Draggable(this);
        }

        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Opacity = OpacityValue;
            if (Owner != null)
            {
                Screen? screen = Screen.PrimaryScreen;
                Point rightBottom = new Point(screen == null ? 0 : screen.WorkingArea.Right, screen == null ? 0 : screen.WorkingArea.Bottom);
                Size a= new Size(Width, Height);
                // ディスプレイの表示範囲からはみ出る場合は内側に表示されるよう補正する
                // 非表示中はX座標=0。この場合もウィンドウ右下に出したいのではみ出た扱いにする。
                int x = (Owner.Location.X + Width > rightBottom.X || Owner.Location.X <= 0)
                    ? rightBottom.X - Width
                    : Owner.Location.X;
                int y = (Owner.Location.Y + Height > rightBottom.Y || Owner.Location.Y <= 0)
                    ? rightBottom .Y - Height
                    : Owner.Location.Y + Owner.Size.Height;
                Location = new Point(x, y);
            }
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
