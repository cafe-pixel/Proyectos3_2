using UnityEngine;

public class Enemie : MonoBehaviour
{
    public static int vidaEnemie;
    private int defensa = 1;
    public static int ataque = 1;
    public static void EnemieSumarVida()
    {
        vidaEnemie++;
    }
    
    public static void EnemieRestarVida()
    {
        vidaEnemie--;
    }
    
    
}
