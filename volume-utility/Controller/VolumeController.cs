using NAudio.CoreAudioApi;
using System.Diagnostics;

namespace volume_utility.Controller
{
    /// <summary>
    /// ボリューム操作クラス
    /// </summary>
    public class VolumeController : IDisposable
    {
        private MMDeviceEnumerator _deviceEnumerator;
        private MMDevice _device;
        private bool _isDisposed = false;

        /// <summary>
        /// ボリューム変更時のコールバック
        /// float: 最小・最大値に丸められた調整後のボリューム
        /// AudioVolumeNotificationData: ボリューム変更時の情報
        /// </summary>
        private Action<float, AudioVolumeNotificationData> _volumeChangedCallback;

        /// <summary>
        /// ボリューム変更中かどうか
        /// </summary>
        public bool IsChanging { get; set; }
        /// <summary>
        /// 最小値
        /// </summary>
        public float MinVolume { get; set; }
        /// <summary>
        /// 最大値
        /// </summary>
        public float MaxVolume { get; set; }
        /// <summary>
        /// 現在のボリューム
        /// </summary>
        public float CurrentVolume
        {
            get { return (int)ConvertToTrackBarValue(_device.AudioEndpointVolume.MasterVolumeLevelScalar); }
            set
            {
                IsChanging = true;
                _device.AudioEndpointVolume.MasterVolumeLevelScalar = ConvertoVolumeValue(value);
                IsChanging = false;
            }
        }
        /// <summary>
        /// ミュート状態
        /// </summary>
        public bool IsMute
        {
            get { return _device.AudioEndpointVolume.Mute; }
            set { _device.AudioEndpointVolume.Mute = value; }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="volumeChangedCallback"></param>
        public VolumeController(Action<float, AudioVolumeNotificationData> volumeChangedCallback)
        {
            _deviceEnumerator = new MMDeviceEnumerator();
            _device = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _device.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
            _volumeChangedCallback = volumeChangedCallback;
        }
        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) { return; }

            _isDisposed = true;
            _device.AudioEndpointVolume.OnVolumeNotification -= AudioEndpointVolume_OnVolumeNotification;
            _device.Dispose();
            _deviceEnumerator.Dispose();
        }
        /// <summary>
        /// 最小・最大値で補正された次のボリュームを取得する
        /// </summary>
        /// <param name="currentVolume"></param>
        /// <returns></returns>
        public float GetNextVolume(float currentVolume)
        {
            return Math.Min(MaxVolume, Math.Max(MinVolume, currentVolume));
        }
        /// <summary>
        /// ボリューム通知時のイベントハンドラ
        /// </summary>
        /// <param name="data"></param>
        private void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            float notificationVolume = ConvertToTrackBarValue(data.MasterVolume);
            int limitedVolume = (int)GetNextVolume(notificationVolume);
            int current = (int)CurrentVolume;

            if (current != limitedVolume)
            {
                Debug.WriteLine($"MasterVolume: {notificationVolume}, limitedValue: {limitedVolume}, current: {current}");
                CurrentVolume = limitedVolume;
            }
            _volumeChangedCallback(limitedVolume, data);

        }

        /// <summary>
        /// トラックバーの値をNAudioのボリュームに変換する
        /// トラックバーの値は0～100の範囲であるため、NAudioのボリュームを0～100に変換する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static float ConvertToTrackBarValue(float value)
        {
            return (float)Math.Round(value * 100);
        }
        /// <summary>
        /// NAudioのボリュームをトラックバーの値に変換する
        /// NAudioのボリュームは0.0～1.0の範囲であるため、トラックバーの値を0.0～1.0に変換する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static float ConvertoVolumeValue(float value)
        {
            return value / 100f;
        }
    }
}