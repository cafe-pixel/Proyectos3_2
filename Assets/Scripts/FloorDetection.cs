using System;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsFloorDetected { get; set; } = false;
    
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                IsFloorDetected = true;
            }
        }
    
}
