using UnityEngine;

public class Player : MonoBehaviour
{
    private float x;
    private float y;
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        this.gameObject.transform.position += new Vector3(x, y, 0).normalized * (Time.deltaTime * 4);
    }
}
