using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class MenuVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip desktopClip;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = Path.Combine(
            Application.streamingAssetsPath,
            "title screen.mp4"
        );
#else
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = desktopClip;
#endif

        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.errorReceived += OnVideoError;

        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        player.Play();
    }

    private void OnVideoError(VideoPlayer player, string message)
    {
        Debug.LogError("Video error: " + message);
    }

    private void OnDestroy()
    {
        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.errorReceived -= OnVideoError;
    }
}
