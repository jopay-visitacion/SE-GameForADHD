using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject triggerPrefab;
    public PlayerMovement player;
    public TextMeshProUGUI ui;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerPrefab == null) return;

        if (player != null && player.CompareTag("Player"))
        {
            ui.canvas.enabled = true;
            Time.timeScale = 0f;
        }
    }
}
