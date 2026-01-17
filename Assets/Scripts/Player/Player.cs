using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IReciboObjeto, IReciveDamage
{
    [Header("Inputs")] 
    private float xInput;
    private float yInput;
    [SerializeField] private KeyCode jump = KeyCode.Space; //en los combos aparece como un 3
    [SerializeField] private KeyCode dash = KeyCode.R;
    
    
    //ATTACKS
    [Header("Attack-Area")] 
    [SerializeField] private Transform attackPoint;

    [SerializeField] private LayerMask layerAttackable;

    private bool canGiveDamage;
    
    [Header("Combos")] 
    //lo que voy a hacer es que cuando presione una tecla se manda un numero y se guarda en una lista, si recibe solo uno tira los normales y si no busca uno que sume eso
    [SerializeField] private float comboMaxTimer = 0.2f;
    private float comboTimer;
    private List<int> comboInputs = new List<int>();
    
    
    [Header("Headbutt")] 
    [SerializeField] private float headbuttDamage;
    [SerializeField] private float maxHeadbuttTimer;
    private float headbuttTimer;
    
    [Header("SwingSwing")] 
    [SerializeField] private float swingDamage;
    [SerializeField] private float maxSwingTimer;
    private float swingTimer;
    
    [Header("ElbowStrike")] 
    [SerializeField] private float elbowStrikeDamage;
    [SerializeField] private float maxElbowStrikeTimer;
    private float elbowStrikeTimer;
    
    [Header("SoftAttack")] 
    [SerializeField] private int softAttack = 0; //boton izq
    [SerializeField] private float suaveMaxTimer = 0.41f;
    private float suaveTimer;
    
    [Header("HardAttack")] 
    [SerializeField] private int hardAttack = 1;
    [SerializeField] private float hardMaxTimer = 1.75f;
    private float hardTimer;
    
    [Header("Movement")] 
    new Vector3 initialPosition;
    [SerializeField] private float velocidad = 1f;

    //Dash (en los combos aparece como un 2)
    [SerializeField] private int dashForce = 2;
    [SerializeField] private float dashMaxTimer;
    private float dashTimer;
    

    [Header("Jump")] 
    //Jump
    private float jumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float grav;
    
    
    [Header("Parry")] 
    //Parry - bloquea todo el daño por un segundo -
    [SerializeField] private KeyCode parry = KeyCode.E; //se le hace referencia con el numero 4
    private float parryTimer;
    [SerializeField] private float parryMaxTimer = 1f;
    private bool isParrying;

    

    [Header("PlayerStats")] 
    [SerializeField] private float vidaPlayer = 1; //los items que suben esto son permanentes y acumulativos
    [SerializeField] public float velocidadPlayer = 1f; //los items que suben el resto son temporales y acumulativos
    [SerializeField] public float defensaPlayer = 1f;
    [SerializeField] public float ataquePlayer = 1f;
    [SerializeField] private float vidaMaxPlayer = 100;



    //Physiques
    private Rigidbody2D rb;


    [Header("References")] 
    private FloorDetection floor;
    [SerializeField] private Canva canvasEnd;
   


    //States
    private string state = "move";


    //Sprites 
    private SpriteRenderer sr;
    
    //Coroutines 
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
                
                #region Combos
                
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

                if (Input.GetMouseButton(hardAttack)&&canGiveDamage) //instaura el tiempo que va a tardar
                {
                    ((IList)comboInputs).Add(1); //CD
                    comboTimer = comboMaxTimer;
                }

                if (Input.GetMouseButtonDown(softAttack)&&canGiveDamage)
                {
                    ((IList)comboInputs).Add(0); //CI
                    comboTimer = comboMaxTimer;
                    
                }

                if (Input.GetKeyDown(parry))
                {
                    ((IList)comboInputs).Add(4); //Parry
                    comboTimer = comboMaxTimer;
                }
    
                #endregion

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
                
                if (suaveTimer <= 0)
                {
                    state = "move";
                    suaveTimer = suaveMaxTimer;
                    canGiveDamage = true;
                }
                        
                
            
                break;
            
            case "hardAttack":
            
                hardTimer -=  Time.deltaTime;
                
                if (hardTimer <= 0)
                {
                    state = "move";
                    hardTimer = hardMaxTimer;
                    canGiveDamage = true;
                    
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
            
            case "headbutt":
                headbuttTimer -= Time.deltaTime;
                if (headbuttTimer <= 0f)
                {
                    state = "move";
                    canGiveDamage = true;
                }
                break;
            
            case "swing":
                swingTimer -= Time.deltaTime;
                if (swingTimer <= 0f)
                {
                    state = "move";
                    canGiveDamage = true;
                }
                break;
            
            case "elbow":
                elbowStrikeTimer -= Time.deltaTime;
                if (swingTimer <= 0f)
                {
                    state = "move";
                    canGiveDamage=true;
                }
                break;
                

            

        }

    }

    private void ResolverCombo()//comprueba si es un ataque básico o un combo
    {
        if (comboInputs.Count == 1)//si solo hay una tecla pulsada
        {
            if (comboInputs[0] == 0) SoftAttack();
            if (comboInputs[0]==1) HardAttack();
            if (comboInputs[0]==2) Dash();
            if (comboInputs[0]==3) PreJump();
            if (comboInputs[0]==4) Parry();
        }
        else ResolverComboEspecial();
    }

    private void ResolverComboEspecial()
    {
        if (comboInputs.Count == 2)
        {
            if (comboInputs[0] == 0 && comboInputs[1] == 0) JabCodo();
            if (comboInputs[0] == 1 && comboInputs[1] == 1) SwingSwing();
          
        }

        if (comboInputs.Count == 3)
        {
           
            if (comboInputs[0] == 1 && comboInputs[1] == 1 && comboInputs[2] == 1) Cabezazo();
        }
    }
    
    
    // --EFECTUAR EL ATAQUE --
    
    public void Attack(float damage) //toma el daño del ataque seleccionado
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(attackPoint.position, 1,layerAttackable); //desde el punto de ataque con un radio de uno golpea solo a los enemigos e items

        foreach (var c in col)
        {
            if (c.TryGetComponent<IReciveDamage>(out IReciveDamage enemy))
            {
                enemy.Damage(damage);
            }
        }
    }
    
    // --ATAQUES ESPECIALES--

    private void Cabezazo()
    {
        Attack(headbuttDamage);
        headbuttTimer = maxHeadbuttTimer;
        state = "headbutt";

        //falta ponerle la animacion
    }


    private void SwingSwing()
    {
        Attack(swingDamage);
        swingTimer = maxSwingTimer;
        state = "swing";
        
        //falta animacion
    }
    
    
    private void JabCodo()
    {
        Attack(elbowStrikeDamage);
        elbowStrikeTimer = maxElbowStrikeTimer;
        state = "elbow";
        
        // falta animacion
    }

    private void HardAttack()
    {
        hardTimer = hardMaxTimer;
        canGiveDamage = false;
        state = "hardAttack";
    }

    private void SoftAttack()
    {
        suaveTimer = suaveMaxTimer;
        canGiveDamage = false;
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

