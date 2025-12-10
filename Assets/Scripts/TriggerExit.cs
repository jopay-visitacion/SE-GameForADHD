using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    private TileGenerator _tile;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            Destroy(collision.gameObject);
            _tile.tileCount--;
        }
    }
}
