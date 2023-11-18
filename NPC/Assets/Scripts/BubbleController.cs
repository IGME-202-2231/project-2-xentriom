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
    [Range(0f, 1f)] public float destructionRate = 0.06f;

    private Vector2 camSize;

    void Start()
    {
        camSize.y = Camera.main.orthographicSize;
        camSize.x = camSize.y * Camera.main.aspect;

        StartCoroutine(SpawnBubbles());
    }

    IEnumerator SpawnBubbles()
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

                StartCoroutine(MoveUp(bubble, bubbleSpeed));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    SpriteRenderer GetRandomBubbleRenderer()
    {
        SpriteRenderer[] bubbleRenderers = { smallBubbleRenderer, mediumBubbleRenderer, largeBubbleRenderer };
        int randomIndex = Random.Range(0, 3);
        return bubbleRenderers[randomIndex];
    }

    string GetRandomSortingLayer()
    {
        string[] sortingLayers = { "Farground", "Foreground", "Midground" };
        int randomIndex = Random.Range(0, sortingLayers.Length);
        return sortingLayers[randomIndex];
    }

    IEnumerator MoveUp(GameObject bubble, float speed)
    {
        float destructionChance = 0f;

        while (true)
        {
            bubble.transform.Translate(Vector2.up * speed * Time.deltaTime);

            destructionChance += destructionRate * bubble.transform.position.y;

            if (Random.value < destructionChance)
            {
                Destroy(bubble);
                yield break;
            }

            yield return null;
        }
    }
}