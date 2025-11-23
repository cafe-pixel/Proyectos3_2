using System;
using UnityEngine;

public class FloorDetection : Player
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsFloorDetected { get; set; } = false;
    
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Suelo"))
            {
                IsFloorDetected = true;
            }
        }
    
}
