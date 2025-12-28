using UnityEngine;

public class RotatorEnemy : Enemy
{
    private float velocidad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0,0,1) * (Time.deltaTime * velocidad));
    }
}
