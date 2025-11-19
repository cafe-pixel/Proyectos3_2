using UnityEngine;

public class Player : MonoBehaviour
{
    private float x;
    private float y;
    static public int vidaPlayer = 5;
    private int dash = 0;

    void Start()
    {
        GetComponent<Enemie>();
    }
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        this.gameObject.transform.position += new Vector3(x, y, 0).normalized * (Time.deltaTime * 4);
    }

    public void PlayerRestarVida()
    {
        vidaPlayer -= Enemie.ataque;
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
        if (CompareTag("Enemie"))
        {
            Enemie.EnemieRestarVida();
        }
        if ( IInteractable)
        //cuando colisione con un objeto de ataque enemigo RestarVida() a menos que use un parry
        //si lo hace con algo guay SumarVida()
        
        //si lo hace con un asset movible lo puede tomar para tirarlo
    }
}
