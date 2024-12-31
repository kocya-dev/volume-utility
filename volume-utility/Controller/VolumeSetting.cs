namespace volume_utility.Controller
{
    public class VolumeSetting
    {
        /// <summary>
        /// プロセス名
        /// </summary>
        public string ProcessName { get; private set; }
        /// <summary>
        /// アプリケーション名
        /// </summary>
        public string ApplicationName { get; private set; }
        /// <summary>
        /// ボリューム
        /// </summary>
        public int Volume { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="applicationName"></param>
        /// <param name="volume"></param>
        public VolumeSetting(string processName, string applicationName, int volume)
        {
            ProcessName = processName;
            ApplicationName = applicationName;
            Volume = volume;
        }
    }
}
