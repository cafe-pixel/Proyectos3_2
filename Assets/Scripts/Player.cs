using UnityEngine;

public class Player : MonoBehaviour
{
    private float x;
    private float y;
    private float force = 3;
    static public int vidaPlayer = 5;
    private int dash = 0;
    new Vector3 initialPosition;
    private Rigidbody2D rb;
    private bool canAttack = false;
    private bool canInteract;
    private Enemy enemigo;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        enemigo = GetComponent<RotatorEnemy>();
    }
    void Update()
    {
        Jump();
        Interact();
        Attack();
    }

    private void Interact()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IInteractable.Interact();
            }
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            //la tecla que voy a meter es un placeholder de ataque
            if (Input.GetKeyDown(KeyCode.Q))
            {
                enemigo.EnemyRestarVida(1);
            }
            
        }
        
    }

    void Jump()
    {
        
    }

    void FixedUpdate()//solo los fisicos acumulables
    {
        x = Input.GetAxis("Horizontal");//toda variable q declares en un sitio es local
        y = Input.GetAxis("Vertical");
        
        rb.AddForce(new Vector3(x, 0, y).normalized * force, ForceMode2D.Force);
    }

    public void PlayerRestarVida()
    {
        vidaPlayer -= enemigo.Ataque;
        //aqui falta pensar en la defensa
        if (vidaPlayer == 0)
        {
            Muerto();
        }
    }

    public void PlayerSumarVida()
    {
        if (vidaPlayer != 5)
        {
            vidaPlayer++;
        }
    }

    private void Muerto()
    {
        //Pantalla de Jugar Otra Vez
        
    }

    private void Dash()
    {
        //multiplicar la velocidad del jugador durante 3 segundos y apagarla
        //--dash;
        //tengo que ver lo de las corrutinas
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            canAttack = true;
        }

        if (col.TryGetComponent<IInteractable>(out var interactable))
        {
            canInteract = true;
        }
        //cuando colisione con un objeto de ataque enemigo RestarVida() a menos que use un parry
        //si lo hace con algo guay SumarVida()
        
        //si lo hace con un asset movible lo puede tomar para tirarlo
    }
}
