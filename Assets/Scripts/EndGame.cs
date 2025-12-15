using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject triggerPrefab;
    public PlayerMovement player;
    public Canvas canvas;

    private void Start()
    {
        if (canvas == null)
        {
            Debug.Log("Canvas not assigned in EndGame script.", this);
            return;
        }

        canvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerPrefab == null) return;

        if (collision.CompareTag("Player"))
        {
            if (canvas != null) canvas.enabled = true;
        }
    }
}
