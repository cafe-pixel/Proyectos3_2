using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IReciboObjeto
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

    private bool canGiveDamage = true;
    
    [Header("Combos")] 
    //lo que voy a hacer es que cuando presione una tecla se manda un numero y se guarda en una lista, si recibe solo uno tira los normales y si no busca uno que sume eso
    [SerializeField] private float comboMaxTimer = 0.2f;
    private float comboTimer;
    private List<int> comboInputs = new List<int>();
    private bool comboActivated;
    
    
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
    private int softAttackNumber = 0;
    
    [Header("HardAttack")] 
    [SerializeField] private int hardAttack = 1;
    [SerializeField] private float hardMaxTimer = 1.75f;
    private float hardTimer;
    private int hardAttackNumber = 0;

    [Header("Knockback")] 
    [SerializeField] private float maxKnockBackForce;
    [SerializeField] private float knockBackForceX; //fuerza de empuje en horizontal
    private float knockBackForce;
    
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
    public bool isParrying;

    

    [Header("PlayerStats")] 
    [SerializeField] public float velocidadPlayer = 1f; //los items que suben el resto son temporales y acumulativos
    [SerializeField] public float defensaPlayer = 1f;
    [SerializeField] public float ataquePlayer = 1f;
    



    //Physiques
    private Rigidbody2D rb;


    [Header("References")] 
    private FloorDetection floor;
    [SerializeField] private Canva canvasEnd;
    //Sprites 
    private SpriteRenderer sr;
    private Animator anim;


    //States
    private string state = "move";


    
    
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
        anim = GetComponent<Animator>();
        
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
                comboActivated = false;
            }
        }
        
        if (Input.GetMouseButtonDown(hardAttack)&& canGiveDamage) // --LOS INPUTS DE COMBO SE RECOGEN EN CUALQUIER ESTADO--
        {
            Debug.Log("tomo input");
            ((IList)comboInputs).Add(1); //CD
            if (!comboActivated) comboTimer = comboMaxTimer;
            Debug.Log("Activo combo");
            comboActivated = true;
        }

        if (Input.GetMouseButtonDown(softAttack)&&canGiveDamage)
        {
            ((IList)comboInputs).Add(0); //CI
            if (!comboActivated) comboTimer = comboMaxTimer;
            comboActivated = true;
                    
        }

        switch (state)
        {
            case "move":

                Movement();
                
                #region Combos
                
                if (Input.GetKeyDown(jump))
                {
                    ((IList)comboInputs).Add(3); //Jump
                    if (!comboActivated) comboTimer = comboMaxTimer;
                    comboActivated = true;
                }

                if (Input.GetKeyDown(dash))
                {
                    ((IList)comboInputs).Add(2); //Dash
                    if (!comboActivated) comboTimer = comboMaxTimer;
                    comboActivated = true;
                }

                

                if (Input.GetKeyDown(parry))
                {
                    ((IList)comboInputs).Add(4); //Parry
                    if (!comboActivated) comboTimer = comboMaxTimer;
                    comboActivated = true;
                }
    
                #endregion

                break;
            
            case "parrystate":
                parryTimer -= Time.deltaTime;
                if (parryTimer <= 0)
                {
                    isParrying = false;
                    state = "move";
                }
                break;
            
            case "softAttack":
                
                rb.linearVelocity = Vector2.zero;
                
                suaveTimer -=  Time.deltaTime;
                
                if (suaveTimer <= 0)
                {
                    softAttackNumber = 0;
                    anim.SetInteger("softCombo", softAttackNumber);
                    
                    state = "move";
                    
                    canGiveDamage = true;
                }
                        
                if (Input.GetMouseButtonDown(softAttack)&&canGiveDamage)
                {
                    SoftAttack();
                    ((IList)comboInputs).Add(0); //CI
                    comboTimer = comboMaxTimer;
                }
            
                break;
            
            case "hardAttack":

                rb.linearVelocity = Vector2.zero;

                hardTimer -=  Time.deltaTime;
                Debug.Log("Estoy restando tiempo");
                
                if (hardTimer <= 0)
                {
                    Debug.Log("El timer es 0");
                    hardAttackNumber = 0;
                    anim.SetInteger("hardComb", hardAttackNumber);
                    Debug.Log("Mando animacion");
                    
                    
                    state = "move";
                    
                    canGiveDamage = true;
                    
                }
                
                if (Input.GetMouseButtonDown(hardAttack)&&canGiveDamage) //instaura el tiempo que va a tardar
                {
                    HardAttack();
                    ((IList)comboInputs).Add(1); //CD
                    comboTimer = comboMaxTimer;
                }
                        
                
            
                break;

            case "jabSwingJump":
                rb.linearVelocity = Vector2.zero;

                hardTimer -= Time.deltaTime;
                
                if (hardTimer<= 0)
                {
                    

                    state = "move";

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
            
            case "knockBack":
                knockBackForce -= grav * Time.deltaTime;

                rb.linearVelocity = new Vector2(-1* knockBackForceX, knockBackForce);

                if (knockBackForce <= -maxKnockBackForce)
                {
                    rb.linearVelocity = new Vector2(-1*knockBackForceX, 0);
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
            if (comboInputs[0]==1) HardAttack();
            if (comboInputs[0]==2) Dash();
            if (comboInputs[0]==3) PreJump();
            if (comboInputs[0]==4) Parry();
        }
        else ResolverComboEspecial();
    }

    private void ResolverComboEspecial()
    {
        

        if (comboInputs.Contains(0)&&comboInputs.Contains(1))
        {
           JabSwingSalto();
        }
        
        if (comboInputs.Contains(3)&&comboInputs.Contains(1))
        {
            HitInJump();
        }
        
        if (comboInputs.Contains(4)&&comboInputs.Contains(0))
        {
            BlockEnemy();
        }
    }

    private void BlockEnemy()
    {
        //ur por hacer
    }

    private void HitInJump()
    {
        //prepara el salto como para jump y lo ejecuta
        jumpForce = maxJumpForce;
        anim.SetTrigger("hitInJump");
        state = "jump";
    }

    private void JabSwingSalto()
    {
        anim.SetTrigger("softHardComb");
        hardTimer = hardMaxTimer;
        Debug.Log("Soy la nueva anim");

        state = "hardAttack";
    }


    // --EFECTUAR EL ATAQUE --
    
    public void Attack(float damage) //toma el daño del ataque seleccionado
    {
        Debug.Log("te ataco xd");
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

    

    private void KnockBackPropio() //le llama desde el animator
    {
        Debug.Log("intento hacer el knocback");
        knockBackForce = maxKnockBackForce;
        state = "knockBack";
    }
    
    

    private void HardAttack()
    {
        
        if (hardAttackNumber < 4)
        {
            Debug.Log(3);
            hardAttackNumber++;
            anim.SetInteger("hardComb", hardAttackNumber);
            hardTimer = hardMaxTimer;

            state = "hardAttack";
        }

        else
        {
            hardAttackNumber = 0;
            anim.SetInteger("hardComb", hardAttackNumber);
            state = "move";
        }
        
        //canGiveDamage = false;
        state = "hardAttack";
    }

    private void SoftAttack()
    {
        if (softAttackNumber < 4)
        {
            Debug.Log(2);
            softAttackNumber++;
            anim.SetInteger("softCombo", softAttackNumber);
            suaveTimer = suaveMaxTimer;
            //canGiveDamage = false;
            state = "softAttack";
        }
        else
        {
            softAttackNumber = 0;
            anim.SetInteger("softCombo", softAttackNumber);
            state = "move";
        }
    }

    private void Dash()
    {
        dashTimer = dashMaxTimer;
        rb.AddForce(new Vector2(xInput * dashForce, 0), ForceMode2D.Impulse);
        anim.SetTrigger("dash");
        state = "dash";
    }

    private void Movement()
    {
        xInput = Input.GetAxisRaw("Horizontal"); //toda variable q declares en un sitio es local
        yInput = Input.GetAxisRaw("Vertical");

        if (xInput != 0 || yInput != 0)
        {
            //Se mueve
            anim.SetBool("isWalking", true);
            if (xInput > -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            //Quieto parao
            anim.SetBool("isWalking", false);
        }
        
        Vector3 move = new Vector3(xInput, yInput, 0).normalized * (velocidad * velocidadPlayer);
        rb.linearVelocity = move;
    }

    private void Parry()
    {
        parryTimer = parryMaxTimer;
        isParrying = true;
        anim.SetTrigger("parry");
        state = "parrystate";
    }

    private void PreJump()
    {
        jumpForce = maxJumpForce;
        anim.SetTrigger("jump");
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

    


    


    public void AplicarEfecto(Items.TipoItem item)
    {
        switch (item)
        {
            
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
