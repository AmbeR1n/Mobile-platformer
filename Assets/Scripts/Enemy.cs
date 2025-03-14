using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    private Rigidbody2D rb;
    [SerializeField] private Transform currentPoint;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointA.transform) rb.linearVelocity = new Vector2(-speed, 0);
        if(currentPoint == pointB.transform) rb.linearVelocity = new Vector2(speed, 0);
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
            sprite.flipX = !sprite.flipX;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            sprite.flipX = !sprite.flipX;
        }
    }
}
