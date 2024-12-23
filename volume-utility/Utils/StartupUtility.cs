using IWshRuntimeLibrary;
using System.Reflection;

namespace volume_utility.Utils
{
    /// <summary>
    /// スタートアップ起動ユーティリティ
    /// </summary>
    internal class StartupUtility
    {
        /// <summary>
        /// スタートアップに登録
        /// </summary>
        public static void Create()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string appPath = asm.Location;
            string shortcutPath = MakeShortcutPath(asm);

            // アプリケーションをスタートアップに登録
            CreateShortcut(shortcutPath, appPath);
        }
        /// <summary>
        /// スタートアップから削除
        /// </summary>
        public static void Delete()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string shortcutPath = MakeShortcutPath(asm);
            // アプリケーションをスタートアップから削除
             DeleteShortcut(shortcutPath);
        }

        /// <summary>
        /// ショートカットのパスを作成
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        private static string MakeShortcutPath(Assembly asm)
        {
            string appName = asm.GetName().Name ?? "VolumeUtility";
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, $"{appName}.lnk");
            return shortcutPath;
        }

        /// <summary>
        /// ショートカットを作成
        /// </summary>
        /// <param name="shortcutPath"></param>
        /// <param name="targetPath"></param>
        private static void CreateShortcut(string shortcutPath, string targetPath)
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }

        /// <summary>
        /// ショートカットを削除
        /// </summary>
        /// <param name="shortcutPath"></param>
        private static void DeleteShortcut(string shortcutPath)
        {
            if (System.IO.File.Exists(shortcutPath))
            {
                System.IO.File.Delete(shortcutPath);
            }
        }
    }
}
