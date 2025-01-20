using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSetup : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    void Start()
    {
        // Create a new RenderTexture
        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);

        // Assign it to the VideoPlayer's targetTexture
        videoPlayer.targetTexture = renderTexture;

        // Assign the RenderTexture to the RawImage's texture
        rawImage.texture = renderTexture;

        // Start the video
        videoPlayer.Play();
    }
}
