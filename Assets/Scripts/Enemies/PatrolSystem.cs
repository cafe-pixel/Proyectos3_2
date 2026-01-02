using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
    [SerializeField] private Transform patrolSystem;
    private List<Vector3> patrolPoints = new List<Vector3>(); //constructor
    [SerializeField] private float speed; //para ver la velocidad
    private int currentIndex = 0; //para saber por que punto voy
    
    
   

    private void Awake() //guardas la posicion inicial de los puntos en el Awake
    {
        foreach (Transform child in patrolSystem) 
        {
            patrolPoints.Add(child.position); //añades los puntos que has puesto en el inspector en la lista
        }
    }

    

    public void Patrol()
    {
        //move towards se mueve a una posicion
        transform.position = Vector3.MoveTowards(transform.position/*origen*/, patrolPoints[currentIndex]/*destino*/, speed * Time.deltaTime/*velocidad*/);

        if (transform.position == patrolPoints[currentIndex]) //si ha llegado a su destino
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        currentIndex = (currentIndex + 1) % patrolPoints.Count; //pasa al siguiente numero dentro del modulo
        
        
        //para girar al personaje y que mire donde debe
        if (transform.position.x >
            patrolPoints[currentIndex]
                .x) //si el enemigo en x es mayor al numero de puntos instaurados en X, esta a la derecha y ahora debe ir hacia la izquierda
        {
            transform.eulerAngles = new Vector3(0, 180, 0); //gira al gameObject en Y
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0); //si no esta a la derecha está a la izquierda
        }
    }
    
}
