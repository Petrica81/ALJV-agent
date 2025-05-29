using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public MovePaddleAgent _agent;
    public SpriteRenderer _background;
    public BrickSpawner _brickSpawner;
    public int x;
    private int _badStreak = 0;
    private int _streak = 0;

    private void Update()
    {
        x = _brickSpawner.BricksNumber;
        if (_brickSpawner.BricksNumber <= 0)
        {
            _background.color = Color.green;
            _agent.AddReward(30f);
            _agent.EndEpisode();
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _brickSpawner = _agent._brickHolder.GetComponent<BrickSpawner>();
        // Inițial, mingea pleacă într-o direcție random
        Vector2 initialDirection = new Vector2(0, 1).normalized;
        rb.linearVelocity = initialDirection * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Brick"))
        {
            _brickSpawner.BricksNumber -= 1;
            _streak = +1;
            _badStreak = 0;
            collision.gameObject.SetActive(false);
            _agent.AddReward(2f + _streak);
        }
        else if (collision.gameObject.tag.Equals("Paddle"))
        {
            _streak = 0;
            _agent.AddReward(1f);
            _badStreak += 1;

            float ballX = transform.position.x;
            float paddleX = collision.transform.position.x;
            float paddleWidth = collision.collider.bounds.size.x;

            float hitFactor = (ballX - paddleX) / (paddleWidth / 2);

            Vector2 newDir = new Vector2(hitFactor, 1).normalized;

            float speed = rb.linearVelocity.magnitude;
            rb.linearVelocity = newDir * speed;
        }
        else
        {
            _agent.AddReward(-1f);
            _badStreak += 1;
        }

        if(_badStreak >= 7)
        {
            _background.color = Color.red;
            _agent.AddReward(-30f);
            _agent.EndEpisode();
        }

        rb.linearVelocity = new Vector2(0f, -0.1f) + rb.linearVelocity.normalized * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Pit"))
        {
            _agent.AddReward(-30f);
            _background.color = Color.red;
            _agent.EndEpisode();
        }
    }

    public void ResetBall()
    {
        transform.localPosition = new Vector3(0, -4.3f, 0);
        rb.linearVelocity = Vector2.zero;
        _badStreak = 0;
        _streak = 0;
        // Inițial, mingea pleacă într-o direcție random
        Vector2 initialDirection = new Vector2(Random.Range(-1f, 1f), 1).normalized;
        rb.linearVelocity = initialDirection * speed;
    }
}
