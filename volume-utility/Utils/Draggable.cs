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
        private Control _operationControl;
        /// <summary>
        /// 移動対象のコントロール
        /// </summary>
        private Control _moveTargetControl;
        /// <summary>
        /// マウスの位置
        /// </summary>
        private Point _mousePoint;

        private bool _disposed = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="operationControl"></param>
        /// <param name="moveTargetControl"></param>
        public Draggable(Control operationControl, Control? moveTargetControl = null)
        {
            _operationControl = operationControl;
            _moveTargetControl = moveTargetControl ?? operationControl;
            _operationControl.MouseDown += _control_MouseDown;
            _operationControl.MouseMove += _control_MouseMove;
        }
        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            _operationControl.MouseDown -= _control_MouseDown;
            _operationControl.MouseMove -= _control_MouseMove;
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
                _moveTargetControl.Left += e.X - _mousePoint.X;
                _moveTargetControl.Top += e.Y - _mousePoint.Y;
            }
        }

    }
}
