using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public float speed = 40f;
    public Transform wallLeft;  // Referință la peretele stâng
    public Transform wallRight; // Referință la peretele drept
    private Rigidbody2D rb;
    private float paddleHalfWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Calculăm jumătatea lățimii paddle-ului pentru a nu intra în pereți
        paddleHalfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }
    public void ResetPaddle()
    {
        transform.localPosition = new Vector3(0, -4.75f, 0);
        rb.linearVelocity = Vector2.zero;
    }
    public void Move(Vector2 movement)
    {
        Vector2 newPosition = (Vector2)transform.localPosition + movement * speed * Time.deltaTime;

        // Obținem limitele pereților
        float leftLimit = wallLeft.localPosition.x + paddleHalfWidth + 0.5f;
        float rightLimit = wallRight.localPosition.x - paddleHalfWidth - 0.5f;

        // Aplicăm limitele pentru a nu depăși pereții
        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);

        // Aplicăm noua poziție
        rb.linearVelocity = new Vector2((newPosition.x - transform.localPosition.x) / Time.deltaTime, 0);
    }
}
