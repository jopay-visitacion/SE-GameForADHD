using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject dirtPrefab;
    public GameObject cloudPrefab;
    private Rigidbody2D rb;

    int[] listNum = { -2, -1, 0, 1, 2 };
    int[] pos = { -1, 1};
    int num;
    Vector2 startPos;
    Vector2 nextPos;

    public float spawnInterval = 0.5f;
    private float timer;
    public int tileCount = 0;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();

        do
        {
            num = Random.Range(0, listNum.Length);
        }
        while (listNum[num] == 0);
        startPos = new Vector2(listNum[num], 0);
        Instantiate(dirtPrefab, startPos, Quaternion.identity);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && tileCount < 10)
        {
            TileGen();
            tileCount++;
            timer = 0;
        }

    }

    void TileGen()
    {
        int randomNum = Random.Range(0, 2);
        Vector2 pos = startPos;

        if (randomNum == 0)
        {
            pos.x = (pos.x > -2) ? pos.x - 2 : pos.x + 2;
        }
        else
        {
            pos.x = (pos.x < 2) ? pos.x + 2 : pos.x - 2;
        }

        pos.y += 1.5f;

        Instantiate(dirtPrefab, pos, Quaternion.identity);
        startPos = pos; // update for next spawn
    }
}
