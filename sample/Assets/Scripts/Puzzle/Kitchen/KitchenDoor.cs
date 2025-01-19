using UnityEngine; 

public class KitchenDoor : MonoBehaviour
{
    private bool isUnlocked = false;

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Kitchen Door is now unlocked!");
        // Add animation or logic to open the door
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUnlocked && other.CompareTag("Player"))
        {
            // Logic to open or let the player pass
            Debug.Log("Player has entered the unlocked door.");
        }
    }
}
