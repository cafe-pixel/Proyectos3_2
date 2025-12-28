using UnityEngine;

public class Player : MonoBehaviour, IReciboObjeto, IReciveDamage
{
    //Input
    private float xInput;
    private float yInput;
    [SerializeField] private KeyCode jump = KeyCode.Space;

    [SerializeField] private KeyCode interact = KeyCode.E;
    [SerializeField] private KeyCode dash = KeyCode.R;
    [SerializeField] private KeyCode attack = KeyCode.Z;

    //Movement
    new Vector3 initialPosition;
    [SerializeField] private float velocidad = 1f;

    //Dash
    [SerializeField] private int dashh = 2;
    [SerializeField] private float dashMaxTimer;
    private float dashTimer;


    //Jump

    private float jumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float grav;

    //Ataque
    [SerializeField] private float attackMaxTimer;
    private float attackTimer;

    //Stats
    private int vidaPlayer = 5; //los items que suben esto son permanentes y acumulativos
    public float velocidadPlayer = 1f; //los items que suben el resto son temporales y acumulativos
    public float defensaPlayer = 1f;
    public float ataquePlayer = 1f;



    //Fisicas
    private Rigidbody2D rb;


    //Referencias
    private FloorDetection floor;
    [SerializeField] private Canva canvas;


    //States
    private string state = "move";


    //Sprites
    private SpriteRenderer sr;




    private void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        floor = GetComponentInChildren<FloorDetection>();
        sr = GetComponent<SpriteRenderer>();

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
                    dashTimer = dashMaxTimer;
                    rb.AddForce(new Vector2(xInput * dashh, 0), ForceMode2D.Impulse);
                    state = "dash";
                }

                if (Input.GetKeyUp(attack))
                {
                    attackTimer = attackMaxTimer;
                    //fijar la dirección del personaje
                    state = "attack";
                }


                break;

            case "jump":

                Jump();

                break;

            case "dash":
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0f)
                {
                    state = "move";
                }


                break;

            case "attack":
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
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

    private void PlayerSumarVida()
    {
        if (vidaPlayer != 5)
        {
            vidaPlayer++;
        }
    }

    private void Muerto()
    {
        sr.enabled = false;
        canvas.CanvasAppear("Hola");
        //Pantalla de Jugar Otra Vez

    }


    public void AplicarEfecto(Items.TipoItem item)
    {
        switch (item)
        {
            case Items.TipoItem.BebidaEnergetica:
                PlayerSumarVida();
                break;
            case Items.TipoItem.BebidaEnergetica2:
                PlayerSumarVida2();
                break;
            case Items.TipoItem.PatatasPicantes:
                PlayerSubirAtaque();
                break;
            case Items.TipoItem.Poki:
                PlayerSubirDefensa();
                break;
            case Items.TipoItem.RefrescoDeUva:
                PlayerSubirVelocidad();
                break;
        }




    }

    private void PlayerSubirAtaque()
    {
        throw new System.NotImplementedException();
    }

    private void PlayerSubirDefensa()
    {
        throw new System.NotImplementedException();
    }

    private void PlayerSubirVelocidad()
    {
        throw new System.NotImplementedException();
    }

    private void PlayerSumarVida2()
    {
        throw new System.NotImplementedException();
    }
}

