using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int lives;
    [SerializeField] private int jumpForce;

    [SerializeField] private bool isGrounded;
    private bool isAlive;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float cooldownEnemy;
    [SerializeField] private float cooldownRespawn;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Vector2 spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isAlive = true;
        spawnPoint = transform.position;
    }
    void Update()
    {
        if (cooldownRespawn > 0)
        {
            cooldownRespawn -= Time.deltaTime;
            if (cooldownRespawn <= 0)
            {
                Respawn();
            }
        }
        if (cooldownEnemy > 0)
        {
            cooldownEnemy -= Time.deltaTime;
        }
        if (isAlive && joystick.Horizontal != 0)
        {
            Run();
        }

        if (isAlive && isGrounded && joystick.Vertical > 0)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Player collided with: " + other.gameObject.name);
        if (other.collider.CompareTag("Enemy") && cooldownEnemy <= 0)
        {
            lives--;
            cooldownEnemy = 5f;
            
        }
        if (other.collider.CompareTag("Border"))
        {
            lives--;
            isAlive = false;
            sprite.enabled = false;
            cooldownRespawn = 3f;
            transform.position = spawnPoint;
        }
        
        if (lives <= 0)
        {
            isAlive = false;
            sprite.enabled = false;
            cooldownRespawn = 3f;
        }
    }
    private void Run()
    {
        float moveInput = joystick.Horizontal;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        if (moveInput > 0)
        {
            sprite.flipX = false;
        }
        else if (moveInput < 0)
        {
            sprite.flipX = true;
        }
    }
    
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircleAll(transform.position, 0.03f).Length > 1 & (rb.linearVelocity.y == 0);
    }
    
    private void Respawn()
    {
        isAlive = true;
        sprite.enabled = true;
        if (lives <= 0) lives = 3;
        transform.position = spawnPoint;
    }
}
