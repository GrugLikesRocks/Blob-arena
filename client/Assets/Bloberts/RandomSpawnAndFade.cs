using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawnAndFade : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign your UI prefab in the inspector
    public RectTransform spawnPosition; // Assign a RectTransform to dictate spawn position
    private GameObject spawnedObject;
    private float transparency = 1f;
    private Coroutine fadeRoutine;

    public float smallTime = 5f;
    public float bigTime = 15f;

    void Start()
    {
        fadeRoutine = StartCoroutine(SpawnAndFadeRoutine());
    }

    IEnumerator SpawnAndFadeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(smallTime, bigTime)); // Wait for a random time between 5 and 20 seconds
            SpawnAndFadePrefab();
        }
    }

    private void SpawnAndFadePrefab()
    {
        spawnedObject = Instantiate(prefabToSpawn) as GameObject;
        SetupPrefabTransform();
        ResetTransparency();
        StartCoroutine(FadeOutRoutine());
    }

    private void SetupPrefabTransform()
    {
        RectTransform rectTransform = spawnedObject.GetComponent<RectTransform>();
        if (rectTransform != null && spawnPosition != null)
        {
            // Set the parent and position as before
            rectTransform.SetParent(spawnPosition.parent, false);
            rectTransform.anchoredPosition = spawnPosition.anchoredPosition;
            rectTransform.sizeDelta = spawnPosition.sizeDelta;
            rectTransform.anchorMin = spawnPosition.anchorMin;
            rectTransform.anchorMax = spawnPosition.anchorMax;
            rectTransform.pivot = spawnPosition.pivot;
        }
        else
        {
            spawnedObject.transform.position = spawnPosition.position;
            spawnedObject.transform.SetParent(transform, false);
        }
    }

    private void ResetTransparency()
    {
        RawImage rawImage = spawnedObject.GetComponentInChildren<RawImage>();
        TMP_Text textComponent = spawnedObject.GetComponentInChildren<TMP_Text>();

        transparency = 1f;
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, transparency);
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, transparency);
    }

    IEnumerator FadeOutRoutine()
    {
        RawImage rawImage = spawnedObject.GetComponentInChildren<RawImage>();
        TMP_Text textComponent = spawnedObject.GetComponentInChildren<TMP_Text>();
        float fadeDuration = 10f;
        while (transparency > 0f)
        {
            transparency -= Time.deltaTime / fadeDuration;
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, transparency);
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, transparency);
            yield return null;
        }

        Destroy(spawnedObject);
    }

    // New method to instantly show the prefab and restart the coroutine
    public void ShowPrefabInstantly()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        if (spawnedObject != null) Destroy(spawnedObject); // Destroy existing prefab instance if it exists

        spawnedObject = Instantiate(prefabToSpawn) as GameObject;
        SetupPrefabTransform();
        ResetTransparency(); // Instantly show the prefab by resetting its transparency

        fadeRoutine = StartCoroutine(SpawnAndFadeRoutine()); // Restart the coroutine
    }
}
