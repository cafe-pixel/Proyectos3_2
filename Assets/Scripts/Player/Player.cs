using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IReciboObjeto, IReciveDamage
{
    //Input
    private float xInput;
    private float yInput;
    [SerializeField] private KeyCode jump = KeyCode.Space; //en los combos aparece como un 3
    [SerializeField] private KeyCode dash = KeyCode.R;
    
    //Combs
    //lo que voy a hacer es que cuando presione una tecla se manda un numero y se guarda en una lista, si recibe solo uno tira los normales y si no busca uno que sume eso
    [SerializeField] private float comboMaxTimer = 0.2f;
    private float comboTimer;
    private List<int> comboInputs = new List<int>();
    
    
    
    //Movement
    new Vector3 initialPosition;
    [SerializeField] private float velocidad = 1f;

    //Dash (en los combos aparece como un 2)
    [SerializeField] private int dashForce = 2;
    [SerializeField] private float dashMaxTimer;
    private float dashTimer;
    
    //Attacks
        //soft
    [SerializeField] private int softAttack = 0; //boton izq
    [SerializeField] private float suaveMaxTimer = 0.41f;
    private float suaveTimer;
    internal bool canGiveDamage1 = false;
    
        //hard
    [SerializeField] private int hardAttack = 1;
    [SerializeField] private float hardMaxTimer = 1.75f;
    private float hardTimer;
    internal bool canGiveDamage2 = false;


    //Jump
    private float jumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float grav;
    
    //Parry - bloquea todo el daño por un segundo -
    [SerializeField] private KeyCode parry = KeyCode.E; //se le hace referencia con el numero 4
    private float parryTimer;
    [SerializeField] private float parryMaxTimer = 1f;
    private bool isParrying;

    

    //Stats
    private float vidaPlayer = 1; //los items que suben esto son permanentes y acumulativos
    public float velocidadPlayer = 1f; //los items que suben el resto son temporales y acumulativos
    public float defensaPlayer = 1f;
    public float ataquePlayer = 1f;
    private float vidaMaxPlayer = 100;



    //Fisicas
    private Rigidbody2D rb;


    //Referencias
    private FloorDetection floor;
    [SerializeField] private Canva canvasEnd;
    


    //States
    private string state = "move";


    //Sprites
    private SpriteRenderer sr;
    
    //Corrutinas
    private bool attackFuncionando = false;
    private bool defenseFuncionando = false;
    private bool velocidadFuncionando = false;
    




    private void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        floor = GetComponentInChildren<FloorDetection>();
        sr = GetComponent<SpriteRenderer>();
        vidaPlayer = vidaMaxPlayer;
        comboTimer = comboMaxTimer;
        

    }

    private void Update()
    {

        if (((ICollection)comboInputs).Count > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                ResolverCombo();
                comboInputs.Clear();
            }
        }

        switch (state)
        {
            case "move":

                Movement();
                
                
                if (Input.GetKeyDown(jump))
                {
                    ((IList)comboInputs).Add(3); //Jump
                    comboTimer = comboMaxTimer;
                }

                if (Input.GetKeyDown(dash))
                {
                    ((IList)comboInputs).Add(2); //Dash
                    comboTimer = comboMaxTimer;
                }

                if (Input.GetMouseButton(hardAttack)) //instaura el tiempo que va a tardar
                {
                    ((IList)comboInputs).Add(1); //CD
                    comboTimer = comboMaxTimer;
                }

                if (Input.GetMouseButtonDown(softAttack))
                {
                    ((IList)comboInputs).Add(0); //CI
                    comboTimer = comboMaxTimer;
                    
                }

                if (Input.GetKeyDown(parry))
                {
                    ((IList)comboInputs).Add(4); //Parry
                    comboTimer = comboMaxTimer;
                }


                break;
            
            case "parrystate":
                parryTimer -= Time.deltaTime;
                if (parryTimer <= 0)
                {
                    isParrying = false;
                    state = "parrystate";
                }
                break;
            
            case "softAttack":
            
                suaveTimer -=  Time.deltaTime;
                canGiveDamage1 = false;
                if (suaveTimer <= 0)
                {
                    state = "move";
                    suaveTimer = suaveMaxTimer;
                    
                }
                        
                
            
                break;
            
            case "hardAttack":
            
                hardTimer -=  Time.deltaTime;
                canGiveDamage2 = false;
                if (hardTimer <= 0)
                {
                    state = "move";
                    hardTimer = hardMaxTimer;
                    
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

            

        }

    }

    private void ResolverCombo()//comprueba si es un ataque básico o un combo
    {
        if (comboInputs.Count == 1)//si solo hay una tecla pulsada
        {
            if (comboInputs[0] == 0) SoftAttack();
            else HardAttack();
        }
        else ResolverComboEspecial();
    }

    private void ResolverComboEspecial()
    {
        if (comboInputs.Count == 2)
        {
            if (comboInputs[0] == 0 && comboInputs[1] == 0) JabCodo();
            if (comboInputs[0] == 1 && comboInputs[1] == 1) SwingSwing();
            if (comboInputs[0] == 3 && comboInputs[1] == 1) HitInJump();
            if (comboInputs[0] == 3 && comboInputs[1] == 0) HitInKnee();
        }

        if (comboInputs.Count == 3)
        {
            if (comboInputs[0] == 0 && comboInputs[1] == 0 && comboInputs[2] == 0) PatadaAlta();
            if (comboInputs[0] == 0 && comboInputs[1] == 0 && comboInputs[2] == 1) JabSwingSalto();
            if (comboInputs[0] == 1 && comboInputs[1] == 1 && comboInputs[2] == 1) Cabezazo();
        }
    }

    private void HardAttack()
    {
        hardTimer = hardMaxTimer;
        canGiveDamage2 = true;
        state = "hardAttack";
    }

    private void SoftAttack()
    {
        suaveTimer = suaveMaxTimer;
        canGiveDamage1 = true;
        state = "softAttack";
    }

    private void Dash()
    {
        dashTimer = dashMaxTimer;
        rb.AddForce(new Vector2(xInput * dashForce, 0), ForceMode2D.Impulse);
        state = "dash";
    }

    private void Movement()
    {
        xInput = Input.GetAxisRaw("Horizontal"); //toda variable q declares en un sitio es local
        yInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(xInput, yInput, 0).normalized * (velocidad * velocidadPlayer);
        rb.linearVelocity = move;
    }

    private void Parry()
    {
        parryTimer = parryMaxTimer;
        isParrying = true;
        state = "parrystate";
    }

    private void PreJump()
    {
        jumpForce = maxJumpForce;
        state = "jump";
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

    public void Damage(float damage) //daño recibido menos la defensa
    {
        if (!isParrying)
        {
            float damageFinal = damage -  defensaPlayer;
            PlayerRestarVida(damageFinal);
        }
        
    }


    public void PlayerRestarVida(float damage) //le resta el daño total
    {
        vidaPlayer -= damage;
        
        if (vidaPlayer == 0)
        {
            Muerto();
        }
    }

    private void PlayerSumarVida() //suma vida si es que puede (bebida1)
    {
        if (!Mathf.Approximately(vidaPlayer, vidaMaxPlayer))
        {
            vidaPlayer += 25;
            if (vidaPlayer > 100)
            {
                vidaPlayer = 100;
            }
        }
        else
        {
            Debug.Log("Vida suficiente");
        }
    }

    private void Muerto() //hacer el canvas
    {
        sr.enabled = false;
        canvasEnd.CanvasAppear("Hola");
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

    private void PlayerSubirAtaque() //le añade 0.35 de fuerza durante 3 mins
    {
        if (!attackFuncionando)
        {
            attackFuncionando = true;
            StartCoroutine(Ataque());
            
        }
    }

    private void PlayerSubirDefensa() //le añade 0.35 de deefensa durante 1 mins
    {
        if (!defenseFuncionando)
        {
            defenseFuncionando = true;
            StartCoroutine(Defensa());

        }
    }

    private void PlayerSubirVelocidad() //le añade 0.35 de fuerza durante 2.5 mins
    {
        if (!velocidadFuncionando)
        {
            velocidadFuncionando = true;
            StartCoroutine(Velocidad());
        }
    }

    private void PlayerSumarVida2() //le suma la bebida2
    {
        if (!Mathf.Approximately(vidaPlayer, vidaMaxPlayer))
        {
            vidaPlayer += 50;
            if (vidaPlayer > 100)
            {
                vidaPlayer = 100;
            }
        }
        else
        {
            Debug.Log("Vida suficiente");
        }
    }

    private IEnumerator Defensa()
    {
       
            defensaPlayer += 0.35f;
            yield return new WaitForSeconds(60f);
            defensaPlayer -= 0.35f;
            defenseFuncionando = false;
       
        
    }

    private IEnumerator Velocidad()
    {
       
            velocidadPlayer += 0.35f;
            yield return new WaitForSeconds(180f);
            velocidadPlayer -= 0.35f;
            velocidadFuncionando = false;
       
    }

    private IEnumerator Ataque()
    {
        
            ataquePlayer += 0.35f;
            yield  return new WaitForSeconds(150f);
            ataquePlayer -= 0.35f;
        
    }
    
}

