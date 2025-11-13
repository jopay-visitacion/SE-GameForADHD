using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject dirtPrefab;
    public GameObject cloudPrefab;

    int[] listNum = { -2, -1, 0, 1, 2 };
    Vector2 startPos;

    public float spawnInterval = 0.5f;
    private float timer;

    private void Start()
    {
        int num = Random.Range(0, listNum.Length);
        startPos = new Vector2(listNum[num], 0);
        Instantiate(dirtPrefab, startPos, Quaternion.identity);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            TileGen();
            timer = 0;
        }
    }

    void TileGen()
    {
        int randomNum = Random.Range(0, 2);
        Vector2 pos = startPos;

        if (randomNum == 0)
        {
            pos.x = (pos.x > -2) ? pos.x - 1 : pos.x + 1;
        }
        else
        {
            pos.x = (pos.x < 2) ? pos.x + 1 : pos.x - 1;
        }

        pos.y += 1;

        Instantiate(dirtPrefab, pos, Quaternion.identity);
        startPos = pos; // update for next spawn
    }
}
