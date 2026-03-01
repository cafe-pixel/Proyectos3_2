using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HugoPlayerMovement : MonoBehaviour
{
    [Header("Player Movement Stats")]
    [SerializeField] private float playerSpeed;
    
    [Header("Player Jump Stats")]
    [SerializeField] public float jumpHeight; 
    [SerializeField] public float jumpTime;
    private float jumpTimer = 0f;
    private Rigidbody2D rb;
    private bool isJumping = false;
    
    //Inputs
    private float xInput;
    private float yInput;
    private KeyCode jump = KeyCode.Space;
    private KeyCode dash = KeyCode.R;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        xInput = Input.GetAxisRaw("Horizontal"); 
        yInput = Input.GetAxisRaw("Vertical"); 
        
        Vector3 movementDirection  = new Vector3 (xInput, yInput, 0f).normalized; 
        
        transform.Translate(movementDirection * (playerSpeed * Time.deltaTime), Space.World);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)  // Detectar el inicio del salto
        {
            isJumping = true;
            jumpTimer = 0f;  // Resetear el temporizador del salto
        }

        if (isJumping)
        {
            // Simulamos el salto cambiando la posición en el eje Y
            jumpTimer += Time.deltaTime;  // Aumentar el temporizador del salto

            // Calcular el desplazamiento en Y basado en el tiempo
            float jumpProgress = jumpTimer / jumpTime;
            float height = Mathf.Lerp(0, jumpHeight, jumpProgress);  // Interpolamos para obtener la altura deseada

            rb.MovePosition(new Vector2(rb.position.x, rb.position.y + height));  // Mover el personaje

            if (jumpProgress >= 1f)  // Cuando el salto ha terminado
            {
                isJumping = false;
            }
        }
    }
    
}
