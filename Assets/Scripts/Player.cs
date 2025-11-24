using UnityEngine;

public class Player : MonoBehaviour
{
    private float x;
    private float y;
    private float force = 10f;
    static public int vidaPlayer = 5;
    private int dash = 0;
    new Vector3 initialPosition;
    private Rigidbody2D rb;
    private bool canAttack = false;
    private bool canInteract;
    private bool isJumping;
    private Enemy enemigo;
    private FloorDetection floor;
    private float sueloActual;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float velocidad = 1f;


    private void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        enemigo = GetComponent<RotatorEnemy>();
        floor = GetComponentInChildren<FloorDetection>();
    }

    private void Update()
    {

        Movement();
        Jump();
        //Interact();
        //Attack();

    }

    private void Movement()
    {
        x = Input.GetAxisRaw("Horizontal"); //toda variable q declares en un sitio es local

        // Siempre que toque el suelo puede moverse en vertical, sino no 
        if (floor.IsFloorDetected && !isJumping)
        {
            y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            y = 0;
        }


        Vector3 move = new Vector3(x, y, 0).normalized * velocidad;
        rb.linearVelocity = move;
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

    private void Jump()
    {
        //se está sumando cada poco pero mas o menos guarda la posicion
        //lo hace en 3 clicks
        //baja antes el floordetection ¿pq tiene un componente q el otro no?
        //voy a usar mates :c, tengo que crear un punto máximo y cuando llegue a él tiene que comenzar a restar
        //no lo he logrado, lo dejo comentado
        //dice que el ssalto es muy caro
        if (floor.IsFloorDetected && Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {

            /* rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
             isJumping = true;
             Debug.Log("0");

         }

         if (isJumping)
         {
             floor.IsFloorDetected = false;
             this.rb.gravityScale = 20;
             Debug.Log(rb.gravityScale);
             Debug.Log("1");
         }

         if (transform.position.y <= sueloActual)
         {
             sueloActual = transform.position.y;
             isJumping = false;
             floor.IsFloorDetected = true;
             rb.gravityScale = 0;
             Debug.Log("2");
         }*/
            rb.AddForceY(jumpForce);
            rb.gravityScale = 2;
            isJumping = true;
            floor.IsFloorDetected = false;
            Debug.Log("0");
            if (rb.position.y >= sueloActual + jumpForce)
            {
                this.rb.gravityScale = 20;

            }


            if (transform.position.y <= sueloActual)
            {

                sueloActual = transform.position.y;
                isJumping = false;
                floor.IsFloorDetected = true;
                rb.gravityScale = 0;
                Debug.Log("2");
                Debug.Log(rb.gravityScale);
            }


        }
    }

    private void FixedUpdate() //solo los fisicos acumulables
        {







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


        private void OnTriggerEnter2D(Collider2D col)
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

