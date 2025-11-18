using UnityEngine;

public class Player : MonoBehaviour
{
    private float x;
    private float y;
    private int vida = 5;
    private int dash = 0;
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        this.gameObject.transform.position += new Vector3(x, y, 0).normalized * (Time.deltaTime * 4);
    }

    public void RestarVida()
    {
        vida--;
        if (vida == 0)
        {
            Muerto();
        }
    }

    public void SumarVida()
    {
        if (vida != 5)
        {
            vida++;
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
    }


    private void OnTriggerEnter(Collider col)
    {
        //cuando colisione con un objeto de ataque enemigo RestarVida() a menos que use un parry que entonces gana un Dash
        //si lo hace con algo guay SumarVida()
        
        //si lo hace con un asset movible lo puede tomar para tirarlo
    }
}
