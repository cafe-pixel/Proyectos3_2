using UnityEngine;

public class PlayerCheckpointDetector : MonoBehaviour
{
    public Vector3 checkpointPosition;
    
    void Start()
    {
        checkpointPosition = transform.position;
    }
    
    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
        Debug.Log("Checkpoint guardado en: " + newCheckpoint);
    }
    
}
