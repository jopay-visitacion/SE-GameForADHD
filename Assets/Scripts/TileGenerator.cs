using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject dirtPrefab;
    public GameObject cloudPrefab;
    private Rigidbody2D rb;

    int[] listNum = { -2, 2 };
    int num;
    Vector2 startPos;
    private List<GameObject> tilePos = new List<GameObject>();

    [Header("Spawn Settings")]
    public float spawnInterval = 0.5f;
    private float timer;
    public int tileCount = 0;
    private int maxTiles = 10;

    [Header("Camera Target")]
    public Camera mainCamera;
    public float destroyMargin = 1f;

    [Header("End Trigger")]
    public Transform endTrigger;

    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        rb = GetComponentInChildren<Rigidbody2D>();
        num = Random.Range(0, listNum.Length);

        startPos = new Vector2(listNum[num], -1);

        if (dirtPrefab != null)
        {
            GameObject tile = Instantiate(dirtPrefab, startPos, Quaternion.identity);
            tilePos.Add(tile);
            tileCount++;
        }
            
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && tileCount < maxTiles)
        {
            TileGen();
            timer = 0;
        }

        RemoveTileOffScreen();
        EndTriggerFollow();
    }

    void TileGen()
    {
        if (dirtPrefab == null) return;

        int randomNum = Random.Range(0, 2);
        Vector2 pos = startPos;

        if (randomNum == 0)
        {
            pos.x = (pos.x > -6) ? pos.x - 2 : pos.x + 2;
        }
        else
        {
            pos.x = (pos.x < 6) ? pos.x + 2 : pos.x - 2;
        }

        pos.y += 3;

        GameObject tile = Instantiate(dirtPrefab, pos, Quaternion.identity);
        tilePos.Add(tile);
        tileCount = tilePos.Count;
        startPos = pos; // update for next spawn
    }

    void EndTriggerFollow()
    {
        Vector2 trigger = endTrigger.transform.position;
        trigger.y = tilePos[0].transform.position.y - 4;
        endTrigger.transform.position = trigger;
    }

    void RemoveTileOffScreen()
    {
        if (mainCamera == null || tilePos.Count == 0) return;

        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 cameraPos = mainCamera.transform.position;

        float leftBound = cameraPos.x - cameraWidth * 0.5f - destroyMargin;
        float rightBound = cameraPos.x + cameraWidth * 0.5f + destroyMargin;
        float bottomBound = cameraPos.y - cameraHeight * 0.5f - destroyMargin;

        int removedCount = 0;

        for (int i = 0; i < tilePos.Count; i++)
        {
            GameObject tile = tilePos[i];
            if (tile == null)
            {
                tilePos.RemoveAt(i);
                removedCount++;
                continue;
            }

            Vector3 p = tile.transform.position;
            if (p.x < leftBound || p.x > rightBound || p.y < bottomBound)
            {
                Destroy(tile);
                tilePos.RemoveAt(i);
                removedCount++;
            }
        }

        if (removedCount > 0)
        {
            tileCount = Mathf.Max(0, tileCount - removedCount);

            for (int i = 0; i < removedCount; i++)
            {
                if (tileCount < 10)
                {
                    TileGen();
                }
            }
        }
    }
}
