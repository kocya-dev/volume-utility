

using System.Diagnostics;
using System.Text.Json;

namespace volume_utility.Controller
{
    internal class ApplicationVolumeSettings
    {
        public List<VolumeSetting> VolumeSettings { get; private set; } =  new List<VolumeSetting>();

        public ApplicationVolumeSettings()
        {

        }
        /// <summary>
        /// ボリューム設定を追加する
        /// </summary>
        /// <param name="volumeSetting"></param>
        public void Add(VolumeSetting volumeSetting)
        {
            VolumeSettings.Add(volumeSetting);
        }
        /// <summary>
        /// ボリューム設定を削除する
        /// </summary>
        /// <param name="volumeSetting"></param>
        public void Remove(VolumeSetting volumeSetting)
        {
            VolumeSettings.Remove(volumeSetting);
        }
        /// <summary>
        /// ボリューム設定を更新する
        /// </summary>
        /// <param name="volumeSetting"></param>
        public void Update(VolumeSetting volumeSetting)
        {
            var index = VolumeSettings.FindIndex(x => x.ProcessName == volumeSetting.ProcessName && x.ApplicationName == volumeSetting.ApplicationName);
            if (index != -1)
            {
                VolumeSettings[index] = volumeSetting;
            }
        }
        /// <summary>
        /// ボリューム設定を取得する
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public VolumeSetting? Find(string processName, string applicationName)
        {
            return VolumeSettings.Find(x => x.ProcessName == processName && x.ApplicationName == applicationName);
        }
        /// <summary>
        /// ボリューム設定をインポートする
        /// </summary>
        /// <param name="data"></param>
        public void Import(string data)
        {
            try
            {
                var settings = JsonSerializer.Deserialize<List<VolumeSetting>>(data);
                if (settings != null)
                {
                    VolumeSettings = settings;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// ボリューム設定をエクスポートする
        /// </summary>
        /// <returns></returns>
        public string Export()
        {
            return JsonSerializer.Serialize(VolumeSettings);
        }
    }
}
