using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
 
public class FakeLoading : MonoBehaviour
{
    [Header("UI Elements")]
    public Image fillImage;            // Drag your fill image here
    public TextMeshProUGUI loadingText; // Drag your loading text here (optional)

    [Header("Loading Settings")]
    public float loadingTime = 5f;     // Total fake loading time

    private void Start()
    {
        StartCoroutine(PerformFakeLoading());
    }

    private IEnumerator PerformFakeLoading()
    {
        float elapsedTime = 0f;
        int dotCount = 0;

        while (elapsedTime < loadingTime)
        {
            // Update the fill amount of the image
            fillImage.fillAmount = elapsedTime / loadingTime;

            // Update loading text with dots
            dotCount = (dotCount + 1) % 4; // Cycle through 0, 1, 2, 3
            if (loadingText != null)
            {
                loadingText.text = "Loading" + new string('.', dotCount);
            }

            // Wait for 0.5 seconds for the dots animation
            yield return new WaitForSeconds(0.5f);

            // Increment elapsed time
            elapsedTime += 0.5f;
        }

        // Complete the loading
        fillImage.fillAmount = 1f;
        if (loadingText != null)
        {
            SceneManager.LoadScene(1);
        }
    }
}
