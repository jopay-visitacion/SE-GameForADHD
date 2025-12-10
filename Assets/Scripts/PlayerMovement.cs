using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;

    public float stepX = 4f;
    public float stepY = 1.5f;

    public float cameraSmoothSpeed = 5f;
    private float targetCamY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        targetCamY = cam.transform.position.y;  // Start at current position
    }

    private void Update()
    {
        Vector2 pos = rb.position;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 newPos = new Vector2(pos.x - stepX, pos.y + stepY);
            rb.MovePosition(newPos);

            targetCamY = newPos.y;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector2 newPos = new Vector2(pos.x + stepX, pos.y + stepY);
            rb.MovePosition(newPos);

            targetCamY = newPos.y;
        }
    }

    private void LateUpdate()
    {
        // Smooth the camera
        Vector3 camPos = cam.transform.position;
        camPos.y = Mathf.Lerp(camPos.y, targetCamY, cameraSmoothSpeed * Time.deltaTime);
        cam.transform.position = camPos;
    }
}
