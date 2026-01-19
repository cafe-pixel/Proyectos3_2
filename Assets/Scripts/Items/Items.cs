using UnityEngine;

public class Items : MonoBehaviour, IReciveDamage
{
    private SpriteRenderer sp;
    private BoxCollider2D bc;
    private GameObject player;
    [SerializeField] private TipoItem tipoItem;
    //al tomarse debe desenablarse tanto el box collider como el sprite renderer
    //al entrar en contacto con el jugador le hacen cosas depende de que tipo de item sea
    //tiene q tener musica (como todos)
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
    
    public enum TipoItem
    {
        BebidaEnergetica, BebidaEnergetica2, RefrescoDeUva, PatatasPicantes, Poki
    }
    public void Damage(float damage)
    {
        sp.enabled = false;
        bc.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.TryGetComponent<IReciboObjeto>(out IReciboObjeto reciveItem))
            {
                reciveItem.AplicarEfecto(tipoItem);
                Destroy(gameObject);
                
            }
        }
        
    }
}
