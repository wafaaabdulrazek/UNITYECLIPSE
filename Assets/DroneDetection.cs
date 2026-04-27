using UnityEngine;

public class DroneDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED BY DRONE");
        }
    }
}