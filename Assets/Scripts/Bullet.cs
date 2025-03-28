using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed *= (transform.rotation.y>0.0f ? -1 : 1);
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, 0);
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Bullet collided with: " + other.gameObject.name);
        if (other.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.collider.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
        if (other.collider.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
        
    }
}
