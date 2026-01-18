using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
    
    [SerializeField] private Transform patrolPath;//si no le indicas valor a una variable en rosa son null.
    private List<Vector3> patrolPoints = new List<Vector3>(); //Constructor.
    [SerializeField] private float speed;
    private int currentIndex = 0; //saber por qué punto voy
    private void Awake()//guardas la posicion inicial de cada punto se pone en el awwake pq es tuyo
    {
        foreach (Transform child in patrolPath)//toma los hijos en la clase
        {
            patrolPoints.Add(child.position);
        }
    }

    public void Patrol()
    {
        //transform se mueve en una direccion y el move towards se mueve a una posicion
        //si no pone rb es sin fisicas
        //debes sobreescribir tu posicion
        transform.position= Vector3.MoveTowards(transform.position/*donde estoy*/, patrolPoints[currentIndex]/*a donde voy*/, Time.deltaTime/*velocidad multiplicada por el tiempo*/);
        if (transform.position == patrolPoints[currentIndex])//has llegado al objetivo
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        currentIndex = (currentIndex + 1) % patrolPoints.Count; //operador modulo [aritmetica modular] hasta donde puedes contar

       

        if (transform.position.x > patrolPoints[currentIndex].x)//mira si mi posicion es superior a la del punto de patrulla, mi objetivo está a la izquierda
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        
        //ternario
        //transform.eulerAngles = transform.position.x > patrolPoints[currentIndex].x ? new Vector3(0,180,0) : Vector3.zero; //mira si mi posicion es superior a la del punto de patrulla, mi objetivo está a la izquierda
        //le da un valor a transform.euler en base de una condicion, primera te pregunta y te dice loque hace si es si y si es no te pone : y despues lo q hace cuando es no
        //solo se usa cuando haya un if else
    }
    
}
