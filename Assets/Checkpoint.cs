using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckpointDetector respawn = other.GetComponent<PlayerCheckpointDetector>();

            if (respawn != null)
            {
                respawn.SetCheckpoint(transform.position);
            }
        }
    }
}

