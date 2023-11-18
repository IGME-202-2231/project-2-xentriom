using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer smallBubbleRenderer;
    [SerializeField] private SpriteRenderer mediumBubbleRenderer;
    [SerializeField] private SpriteRenderer largeBubbleRenderer;

    [Range(1, 5)] public int minNumberOfBubbles = 1;
    [Range(6, 10)] public int maxNumberOfBubbles = 10;
    [Range(1, 5)] public int minSpawnInterval = 2;
    [Range(6, 10)] public int maxSpawnInterval = 10;
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
                    Random.Range(-camSize.y + -camSize.y, -(camSize.y / 2)));

                SpriteRenderer bubbleRenderer = GetRandomBubbleRenderer();
                GameObject bubble = new GameObject("Bubble");

                bubble.transform.position = spawnPosition;
                bubble.AddComponent<SpriteRenderer>().sprite = bubbleRenderer.sprite;
                bubble.transform.localScale = bubbleRenderer.transform.localScale;

                string sortingLayerName = GetRandomSortingLayer();
                bubble.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;

                StartCoroutine(MoveUp(bubble, bubbleSpeed, 0.2f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private SpriteRenderer GetRandomBubbleRenderer()
    {
        SpriteRenderer[] bubbleRenderers = { smallBubbleRenderer, mediumBubbleRenderer, largeBubbleRenderer };
        int randomIndex = Random.Range(0, 3);
        return bubbleRenderers[randomIndex];
    }

    private string GetRandomSortingLayer()
    {
        string[] sortingLayers = { "Farground", "Foreground", "Midground" };
        int randomIndex = Random.Range(0, sortingLayers.Length);
        return sortingLayers[randomIndex];
    }

    private IEnumerator MoveUp(GameObject bubble, float speed)
    {
        float destructionChance = 0f;

        while (true)
        {
            bubble.transform.Translate(Vector2.up * speed * Time.deltaTime);

            destructionChance += 0.1f * bubble.transform.position.y;

            if (Random.value < destructionChance + Random.Range(0f, 0.06f))
            {
                Destroy(bubble);
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator MoveUp(GameObject bubble, float speed, float swayIntensity)
    {
        float destructionChance = 0f;

        while (true)
        {
            bool shouldSway = Random.value < 0.5f;

            if (shouldSway)
            {
                float horizontalSway = Mathf.Sin(Time.time * 2f) * swayIntensity;

                bubble.transform.Translate(new Vector2(horizontalSway, 1f) * speed * Time.deltaTime);
            }
            else
            {
                bubble.transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            destructionChance += 0.1f * bubble.transform.position.y;

            if (Random.value < destructionChance + Random.Range(0f, 0.06f))
            {
                Destroy(bubble);
                yield break;
            }

            yield return null;
        }
    }

}