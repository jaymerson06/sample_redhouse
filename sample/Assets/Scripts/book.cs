using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    [SerializeField] GameObject gameObjectToToggle; // Reference to the GameObject to toggle
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject forwardButton;
    [SerializeField] GameObject itemButton; // Reference to the Item button

    int index = -1;
    bool rotate = false;
    bool isBookOpen = false;

    private void Start()
    {
        backButton.SetActive(false);
        gameObjectToToggle.SetActive(false); // Ensure the book is initially hidden

        // Add listener for the Item button
        if (itemButton != null)
        {
            UnityEngine.UI.Button btn = itemButton.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(ToggleBookVisibility);
            }
        }
    }

    public void ToggleBookVisibility()
    {
        isBookOpen = !isBookOpen;
        gameObjectToToggle.SetActive(isBookOpen);

        if (isBookOpen)
        {
            // Reset to first page when the book is opened
            index = 0;
            backButton.SetActive(false);
            forwardButton.SetActive(true);
            ResetPagesRotation();
        }
        else
        {
            // Reset pages when the book is closed
            ResetPagesRotation();
        }
    }

    public void RotateForward()
    {
        if (rotate || index + 1 >= pages.Count) return;
        index++;
        float angle = 180; // Rotate forward
        ForwardButtonActions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void RotateBack()
    {
        if (rotate || index - 1 < 0) return;
        float angle = 0; // Rotate back
        BackButtonActions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, false));
    }

    public void ForwardButtonActions()
    {
        if (!backButton.activeInHierarchy)
        {
            backButton.SetActive(true);
        }
        if (index == pages.Count - 1)
        {
            forwardButton.SetActive(false);
        }
    }

    public void BackButtonActions()
    {
        if (!forwardButton.activeInHierarchy)
        {
            forwardButton.SetActive(true);
        }
        if (index - 1 == -1)
        {
            backButton.SetActive(false); // Hide back button on the first page
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        rotate = true;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

        while (true)
        {
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value);
            float angleDifference = Quaternion.Angle(pages[index].rotation, targetRotation);

            if (angleDifference < 0.1f)
            {
                if (!forward && index > 0)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }

    private void ResetPagesRotation()
    {
        foreach (var page in pages)
        {
            page.rotation = Quaternion.identity;
        }
    }
}
