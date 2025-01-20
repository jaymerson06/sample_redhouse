using UnityEngine;

public class SceneMusicChanger : MonoBehaviour
{
    public AudioClip newSceneMusic;

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic(newSceneMusic);
    }
}