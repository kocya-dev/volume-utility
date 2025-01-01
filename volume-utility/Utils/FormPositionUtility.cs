namespace volume_utility.Utils
{
    internal static class FormPositionUtility
    {
        public static Point AdjustWindowPosition(Form? owner, Form current)
        {
            if (owner == null)
            {
                return current.Location;
            }

            Screen? screen = Screen.PrimaryScreen;
            Point rightBottom = new Point(screen == null ? 0 : screen.WorkingArea.Right, screen == null ? 0 : screen.WorkingArea.Bottom);

            // ディスプレイの表示範囲からはみ出る場合は内側に表示されるよう補正する
            // 非表示中はX座標=0。この場合もウィンドウ右下に出したいのではみ出た扱いにする。
            int x = (owner.Location.X + current.Width > rightBottom.X || owner.Location.X <= 0)
                ? rightBottom.X - current.Width
                : owner.Location.X;
            int y = (owner.Location.Y + current.Height > rightBottom.Y || owner.Location.Y <= 0)
                ? rightBottom.Y - current.Height
                : owner.Location.Y + owner.Size.Height;
            return new Point(x, y);
        }
    }
}
