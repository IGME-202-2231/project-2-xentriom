using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> bubblePrefab;

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
                SortingLayerInfo sortingLayer = GetRandomSortingLayer();

                Vector3 spawnPosition = new Vector3(
                    Random.Range(-camSize.x, camSize.x),
                    Random.Range(-camSize.y + -camSize.y, -camSize.y),
                    sortingLayer.Z - 0.1f);

                SpriteRenderer bubbleRenderer = bubblePrefab[Random.Range(0, 3)];
                SpriteRenderer bubble = Instantiate(bubbleRenderer, spawnPosition, Quaternion.identity);
                bubble.sortingLayerName = sortingLayer.Layer.ToString();

                ApplyDarkTint(bubble, sortingLayer.Layer);
                StartCoroutine(MoveUp(bubble.gameObject, bubbleSpeed, 0.75f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private SortingLayerInfo GetRandomSortingLayer()
    {
        EnvLayers[] sortingLayers = {
            EnvLayers.Background,
            EnvLayers.Farground,
            EnvLayers.Midground,
            EnvLayers.Foreground};
        float[] zValues = { 15, 10, 5, 0 };

        int randomIndex = Random.Range(0, sortingLayers.Length);

        SortingLayerInfo sortingLayerInfo = new SortingLayerInfo(
            sortingLayers[randomIndex],
            zValues[randomIndex]);

        return sortingLayerInfo;
    }

    private void ApplyDarkTint(SpriteRenderer bubble, EnvLayers sortingLayerName)
    {
        Color tint;
        float intensity;

        switch (sortingLayerName)
        {
            case EnvLayers.Background:
                intensity = 0.2f;
                tint = new Color(
                    bubble.color.r * intensity,
                    bubble.color.g * intensity,
                    bubble.color.b * intensity,
                    0.9f);
                break;
            case EnvLayers.Farground:
                intensity = 0.7f;
                tint = new Color(
                    bubble.color.r * intensity,
                    bubble.color.g * intensity,
                    bubble.color.b * intensity,
                    0.9f);
                break;
            case EnvLayers.Midground:
                intensity = 0.8f;
                tint = new Color(
                    bubble.color.r * intensity,
                    bubble.color.g * intensity,
                    bubble.color.b * intensity,
                    0.9f);
                break;
            default:
                intensity = 0.9f;
                tint = new Color(
                    bubble.color.r * intensity,
                    bubble.color.g * intensity,
                    bubble.color.b * intensity,
                    0.9f);
                break;
        }

        bubble.color = tint;
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