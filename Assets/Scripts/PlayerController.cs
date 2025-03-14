using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int lives;
    [SerializeField] private int coins;
    [SerializeField] private int jumpForce;
    public TMP_Text livesText;
    public TMP_Text coinsText;
    [SerializeField] private bool isGrounded;
    private bool isAlive;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float cooldownEnemy;
    [SerializeField] private float cooldownRespawn;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Vector2 spawnPoint;

    public int Lives { get => lives; set {lives = value; livesText.text = $"Health: {lives}";} }
    public int Coins { get => coins; set {coins = value; coinsText.text = $"Coins: {coins}";} }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isAlive = true;
        spawnPoint = transform.position;
        livesText.text = $"Health: {lives}";
        coinsText.text = $"Coins: {coins}";
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

        if (isAlive && joystick.Vertical >= 0.9f)
        {
            Jump();
        }
        if (isAlive && Lives <= 0)
        {
            isAlive = false;
            sprite.enabled = false;
            cooldownRespawn = 3f;
        }
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    public void Jump()
    {
        if (isAlive && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Player collided with: " + other.gameObject.name);
        if (other.collider.CompareTag("Enemy") && cooldownEnemy <= 0)
        {
            Lives--;
            cooldownEnemy = 5f;
            
        }
        if (other.collider.CompareTag("Border"))
        {
            Lives--;
            isAlive = false;
            sprite.enabled = false;
            cooldownRespawn = 3f;
            transform.position = spawnPoint;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player triggered: " + other.gameObject.name);
        
        if (other.GetComponent<BoxCollider2D>().CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            Coins++;
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
        if (Lives <= 0) Lives = 3;
        transform.position = spawnPoint;
    }

    public void ButtonClick()
    {
        Debug.Log("Button clicked");
    }
}
