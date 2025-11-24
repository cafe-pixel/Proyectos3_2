using UnityEngine;

public class Player : MonoBehaviour, IReciveItem
{
    //Input
    private float xInput;
    private float yInput;
    [SerializeField] private KeyCode jump = KeyCode.Space;
    [SerializeField] private KeyCode attack = KeyCode.Q;
    [SerializeField] private KeyCode interact = KeyCode.E;
    [SerializeField] private KeyCode dash = KeyCode.R;
    
    //Movement
    new Vector3 initialPosition;
    [SerializeField] private float velocidad = 1f;
    
    //Dash
    [SerializeField] private int dashh = 2;
    [SerializeField] private float maxTimer;
    private float timer;
    
    
    //Jump
    
    private float jumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float grav;
    
    //Vida
    static public int vidaPlayer = 5;
    
    //Fisicas
    private Rigidbody2D rb;
    
    
    //Referencias
    private FloorDetection floor;
    
    
    //States
    private string state = "move";
    


    private void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        floor = GetComponentInChildren<FloorDetection>();
    }

    private void Update()
    {

        switch (state)
        {
            case "move":
                
                Movement();

                if (Input.GetKeyDown(jump))
                {
                    jumpForce = maxJumpForce;
                    state = "jump";
                }

                if (Input.GetKeyDown(dash))
                {
                    timer = maxTimer;
                    rb.AddForce(new Vector2(xInput  * dashh, 0),  ForceMode2D.Impulse);
                    state = "dash";
                }

                    
                break;

            case "jump":

                Jump();

                break;
            
            case "dash":
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    state = "move";
                }
                
                
                break;
                
        }

    }

    private void Movement()
    {
        xInput = Input.GetAxisRaw("Horizontal"); //toda variable q declares en un sitio es local
        yInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(xInput, yInput, 0).normalized * velocidad;
        rb.linearVelocity = move;
    }

    



   
    

    private void Jump()
    {
        jumpForce -= grav * Time.deltaTime;
                
        rb.linearVelocity = new Vector2(xInput * velocidad, jumpForce);

        if (jumpForce <= -maxJumpForce)
        {
            rb.linearVelocity = new Vector2(0, 0);
            state = "move";
        }

    }

    private void FixedUpdate() //solo los fisicos acumulables
        {







        }

        public void PlayerRestarVida()
        {
            vidaPlayer--;
            //aqui falta pensar en la defensa
            if (vidaPlayer == 0)
            {
                Muerto();
            }
        }

        private static void PlayerSumarVida()
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

        
        public static void AplicarEfecto(string item)
        {
            if (item == "vida")
            {
                PlayerSumarVida();
            }
            else if (item == "velocidad")
            {
                //PlayerSumarVelocidad();
            }
            else if (item == "ataque")
            {
                //PlayerSumarAtaque();
            }
            else if (item == "defensa")
            {
                //PlayerSumarDefensa();
            }
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<IGiveDamage>(out IGiveDamage giveDamage))
            {
                if (col.CompareTag("Enemy"))
                {
                    PlayerRestarVida();
                }
                giveDamage.Damage();
            }

            //borro la interacción pq se interactua a base de hostias
            
            

            //si lo hace con un asset movible lo puede tomar para tirarlo
        }
        
    }

