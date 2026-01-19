using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("General")]
    public float attackCooldown = 2f;
    public float returnSpeed = 6f;
    public float chargeSpeed = 12f;

    [Header("Referencias")]
    public Transform player;
    public Rigidbody2D rb;
    [SerializeField] private Animator bossMovementAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Spawning")]
    public GameObject bikerEnemyPrefab;
    public Transform spawnTop;
    public Transform spawnBottom;

    [Header("Bottle Attack")]
    public GameObject bottlePrefab;
    public Transform bottleSpawnPoint;

    private Vector2 startPosition;
    private bool isAttacking = false;
    private bool isCharging = false;
    private bool forceReturn = false;

    void Start()
    {
        startPosition = transform.position;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(BossLoop());
    }
    
    void Update()
    {
        if (rb.linearVelocity.x > 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.linearVelocity.x <= 0f)
        {
            spriteRenderer.flipX = false;
        }
    }
    

    IEnumerator BossLoop()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (!isAttacking)
            {
                int attackIndex = Random.Range(0, 3);

                switch (attackIndex)
                {
                    case 0:
                        yield return StartCoroutine(ChargeAttack());
                        break;
                    case 1:
                        yield return StartCoroutine(SummonBikers());
                        break;
                    case 2:
                        yield return StartCoroutine(BottleAttack());
                        break;
                }

                yield return StartCoroutine(ReturnToStart());
                yield return new WaitForSeconds(attackCooldown);
            }
            
            yield return null;
        }
    }

    //ATAQUE 1: CARGA
    IEnumerator ChargeAttack()
    {
        isAttacking = true;
        bossMovementAnimator.SetBool("dashAttack", true);
        bossMovementAnimator.SetBool("returnPosition", false);
        isCharging = true;
        forceReturn = false;

        rb.bodyType = RigidbodyType2D.Kinematic;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * chargeSpeed;

        while (isCharging && IsOnScreen())
        {
            yield return null;
        }

        // Detener totalmente
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;

        isCharging = false;
        isAttacking = false;
        bossMovementAnimator.SetBool("dashAttack", false);
    }

    bool IsOnScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPos.x > -0.2f && viewportPos.x < 1.2f;
    }

    //ATAQUE 2: INVOCAR BIKERS
    IEnumerator SummonBikers()
    {
        isAttacking = true;

        Instantiate(bikerEnemyPrefab, spawnTop.position, Quaternion.identity);
        Instantiate(bikerEnemyPrefab, spawnBottom.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    //ATAQUE 3: BOTELLA
    IEnumerator BottleAttack()
    {
        isAttacking = true;
        bossMovementAnimator.SetBool("molotovAttack", true);

        GameObject bottle = Instantiate(
            bottlePrefab,
            bottleSpawnPoint.position,
            Quaternion.identity
        );

        Vector2 dir = (player.position - bottleSpawnPoint.position).normalized;
        bottle.GetComponent<Rigidbody2D>().linearVelocity = dir * 8f;

        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
        bossMovementAnimator.SetBool("molotovAttack", false);
    }

    //REGRESO A POSICIÓN INICIAL
    IEnumerator ReturnToStart()
    {
        rb.linearVelocity = Vector2.zero;

        while (Vector2.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                startPosition,
                returnSpeed * Time.deltaTime
            );
            yield return null;
        }
        bossMovementAnimator.SetBool("returnPosition", true);
        transform.position = startPosition;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isAttacking) return;

        if (other.collider.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
}
