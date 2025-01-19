using UnityEngine;

public class Key : MonoBehaviour
{
    public bool hasKey = false;

    private void OnMouseDown()
    {
        if (!hasKey)
        {
            hasKey = true;
            Debug.Log("Key picked up!");
        }
    }
}
