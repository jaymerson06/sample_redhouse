using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject forwardButton;
    [SerializeField] GameObject toggleButton;     // The button to toggle the book
    [SerializeField] GameObject bookGameObject;   // The GameObject containing the book

    int index = -1;
    bool rotate = false;
    bool isBookOpen = false;

    private void Start()
    {
        // Ensure the book is closed by default
        bookGameObject.SetActive(false);
        InitialState();

        // Add listener for the toggle button
        if (toggleButton != null)
        {
            UnityEngine.UI.Button btn = toggleButton.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(ToggleBookVisibility);
            }
        }
    }

    public void InitialState()
    {
        // Reset all pages to their initial state
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }

        index = -1; // Start with no pages turned
        pages[0].SetAsLastSibling(); // Ensure the first page is on top
        backButton.SetActive(false); // Disable back button
        forwardButton.SetActive(true); // Enable forward button
    }

    public void ToggleBookVisibility()
    {
        isBookOpen = !isBookOpen;
        bookGameObject.SetActive(isBookOpen);

        if (isBookOpen)
        {
            InitialState(); // Reset book to its initial state when opened
        }
    }

    public void RotateForward()
    {
        if (rotate || index + 1 >= pages.Count) return;

        index++;
        float angle = 180; // Rotate forward by 180 degrees
        ForwardButtonActions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void ForwardButtonActions()
    {
        if (!backButton.activeInHierarchy)
        {
            backButton.SetActive(true); // Enable back button when moving forward
        }

        if (index == pages.Count - 1)
        {
            forwardButton.SetActive(false); // Disable forward button if on the last page
        }
    }

    public void RotateBack()
    {
        if (rotate || index - 1 < 0) return;

        float angle = 0; // Rotate back to 0 degrees
        pages[index].SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }

    public void BackButtonActions()
    {
        if (!forwardButton.activeInHierarchy)
        {
            forwardButton.SetActive(true); // Enable forward button when moving back
        }

        if (index - 1 < 0)
        {
            backButton.SetActive(false); // Disable back button if on the first page
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;

        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
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
}