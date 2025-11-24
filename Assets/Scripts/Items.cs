using UnityEngine;

public class Items : MonoBehaviour, IGiveDamage
{
    private SpriteRenderer sp;
    private BoxCollider2D bc;
    //al tomarse debe desenablarse tanto el box collider como el sprite renderer
    //al entrar en contacto con el jugador le hacen cosas depende de que tipo de item sea
    //tiene q tener musica (como todos)
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
    public void Damage()
    {
        sp.enabled = false;
        bc.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
        }
    }
}
