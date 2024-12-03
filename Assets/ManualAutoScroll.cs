using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManualAutoScroll : MonoBehaviour
{
    public RectTransform imageContainer; // Reference to the container holding the images
    public float scrollSpeed = 100f; // Speed of scrolling
    public float stopDelay = 2f; // Time before stopping
    private bool isScrolling = true;

    void Start()
    {
        // Start the auto-scrolling
        StartCoroutine(ScrollAndStop());
    }

    IEnumerator ScrollAndStop()
    {
        float elapsedTime = 0f;

        while (isScrolling)
        {
            // Move the container upwards
            imageContainer.anchoredPosition -= new Vector2(0, scrollSpeed * Time.deltaTime);

            // Loop the container position if it moves beyond bounds
            if (imageContainer.anchoredPosition.y <= -imageContainer.sizeDelta.y / 2)
            {
                imageContainer.anchoredPosition = new Vector2(imageContainer.anchoredPosition.x, 0);
            }

            elapsedTime += Time.deltaTime;

            // Stop scrolling after the delay
            if (elapsedTime >= stopDelay)
            {
                isScrolling = false;
                yield return StopAtRandomImage();
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(2);
            }

            yield return null;
        }
    }

    IEnumerator StopAtRandomImage()
    {
        // Determine a random image to stop at
        int totalImages = imageContainer.childCount;
        int randomIndex = Random.Range(0, totalImages);

        // Calculate the target position based on the image's index
        RectTransform targetImage = imageContainer.GetChild(randomIndex) as RectTransform;
        float targetPositionY = -targetImage.anchoredPosition.y;

        // Smoothly move the container to the target position
        while (Mathf.Abs(imageContainer.anchoredPosition.y - targetPositionY) > 0.1f)
        {
            imageContainer.anchoredPosition = Vector2.Lerp(
                imageContainer.anchoredPosition,
                new Vector2(imageContainer.anchoredPosition.x, targetPositionY),
                Time.deltaTime * scrollSpeed / 10
            );
            yield return null;
        }

        // Snap to the final position
        imageContainer.anchoredPosition = new Vector2(imageContainer.anchoredPosition.x, targetPositionY);
    }
}
