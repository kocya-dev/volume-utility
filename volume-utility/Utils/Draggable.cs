namespace volume_utility.Utils
{
    /// <summary>
    /// コントロールをドラッグ可能にするユーティリティ
    /// </summary>
    internal class Draggable : IDisposable
    {
        /// <summary>
        /// 操作対象のコントロール
        /// </summary>
        private Control _control;
        /// <summary>
        /// マウスの位置
        /// </summary>
        private Point _mousePoint;

        private bool _disposed = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="control"></param>
        public Draggable(Control control)
        {
            _control = control;
            _control.MouseDown += _control_MouseDown;
            _control.MouseMove += _control_MouseMove;
        }
        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            _control.MouseDown -= _control_MouseDown;
            _control.MouseMove -= _control_MouseMove;
        }
        /// <summary>
        /// マウスダウン時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _control_MouseDown(object? sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                _mousePoint = new Point(e.X, e.Y);
            }
        }
        /// <summary>
        /// マウスムーブ時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _control_MouseMove(object? sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                _control.Left += e.X - _mousePoint.X;
                _control.Top += e.Y - _mousePoint.Y;
            }
        }

    }
}
