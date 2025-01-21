using UnityEngine;
using UnityEngine.SceneManagement;

public class taptocontinue : MonoBehaviour
{
    [SerializeField] private string targetScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayyGame()
    {
        SceneManager.LoadSceneAsync(targetScene);
    }
}
