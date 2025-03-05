using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Box Collider Triggered by: " + other.gameObject.name);
    }
}

