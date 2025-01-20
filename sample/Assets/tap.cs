using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToContinue : MonoBehaviour
{
    public void OnTap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
