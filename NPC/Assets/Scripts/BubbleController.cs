using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer smallBubbleRenderer;
    [SerializeField] private SpriteRenderer mediumBubbleRenderer;
    [SerializeField] private SpriteRenderer largeBubbleRenderer;

    [Range(1, 5)] public int minNumberOfBubbles = 1;
    [Range(6, 10)] public int maxNumberOfBubbles = 9;
    [Range(1, 5)] public int minSpawnInterval = 2;
    [Range(6, 10)] public int maxSpawnInterval = 8;
    [Range(0f, 10f)] public float bubbleSpeed = 1.5f;

    private Vector2 camSize;

    void Start()
    {
        camSize.y = Camera.main.orthographicSize;
        camSize.x = camSize.y * Camera.main.aspect;

        StartCoroutine(SpawnBubbles());
    }

    private IEnumerator SpawnBubbles()
    {
        while (true)
        {
            float numberOfBubbles = Random.Range(minNumberOfBubbles, maxNumberOfBubbles);
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            for (int i = 0; i < numberOfBubbles; i++)
            {
                Vector2 spawnPosition = new Vector2(
                    Random.Range(-camSize.x, camSize.x),
                    Random.Range(-camSize.y + -camSize.y, -camSize.y));

                SpriteRenderer bubbleRenderer = GetRandomBubbleRenderer();
                GameObject bubble = new GameObject("Bubble");

                bubble.transform.position = spawnPosition;
                bubble.AddComponent<SpriteRenderer>().sprite = bubbleRenderer.sprite;
                bubble.transform.localScale = bubbleRenderer.transform.localScale;

                string sortingLayerName = GetRandomSortingLayer();
                bubble.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;

                ApplyDarkTint(bubble, sortingLayerName);

                StartCoroutine(MoveUp(bubble, bubbleSpeed, 0.75f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private SpriteRenderer GetRandomBubbleRenderer()
    {
        SpriteRenderer[] bubbleRenderers = {
            smallBubbleRenderer,
            mediumBubbleRenderer,
            largeBubbleRenderer };
        int randomIndex = Random.Range(0, 3);
        return bubbleRenderers[randomIndex];
    }

    private string GetRandomSortingLayer()
    {
        string[] sortingLayers = { "Farground", "Midground", "Foreground" };
        int randomIndex = Random.Range(0, sortingLayers.Length);
        return sortingLayers[randomIndex];
    }

    private void ApplyDarkTint(GameObject bubble, string sortingLayerName)
    {
        Color tint = new Color(255f, 255f, 255f, 0.8f);
        if (sortingLayerName == "Midground")
        {
            tint = new Color(138f, 138f, 138f, 0.5f);
        }
        else if (sortingLayerName == "Farground")
        {
            tint = new Color(0f, 0f, 0f, 0.2f);
        }

        bubble.GetComponent<SpriteRenderer>().color = tint;
    }

    private IEnumerator MoveUp(GameObject bubble, float speed, float swayIntensity)
    {
        float destructionChance = 0f;

        while (true)
        {
            float randomValue = Random.value;
            if (randomValue < 0.2f)
            {
                float rightSway = Mathf.Sin(Time.time * 2f) * swayIntensity;

                bubble.transform.Translate(new Vector2(rightSway, 1f) * speed * Time.deltaTime);
            }
            else if (randomValue >= 0.2f && randomValue < 0.4f)
            {
                float leftsway = Mathf.Sin(Time.time * 2f) * -swayIntensity;

                bubble.transform.Translate(new Vector2(leftsway, 1f) * speed * Time.deltaTime);
            }
            else
            {
                bubble.transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            destructionChance += 0.15f * bubble.transform.position.y + Random.Range(0f, 0.8f);
            if (Random.value < destructionChance + Random.Range(0f, 0.06f))
            {
                Destroy(bubble);
                yield break;
            }

            yield return null;
        }
    }
}