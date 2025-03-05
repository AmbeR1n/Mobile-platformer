using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 pos;

    private void Awake()
    {
        if (!player) player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        pos = player.position;
        pos.z = -10;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
        
}
