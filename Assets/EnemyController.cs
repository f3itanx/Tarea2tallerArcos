using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float attackRange = 1f; // Rango de ataque del enemigo
    public float attackDelay = 1f; // Tiempo de espera entre ataques
    public int attackDamage = 1; // Daño del ataque
    public Transform attackPoint; // Punto de origen del ataque
    public LayerMask playerLayer; // Capa que identifica al jugador
    public Animator animator; // Animator del enemigo

    private float attackTimer = 0f; // Temporizador para el tiempo de espera entre ataques
    private bool isAttacking = false; // Flag para controlar si el enemigo está atacando
    private bool isFacingRight = true; // Flag para controlar la dirección del enemigo

    void Update()
    {
        // Si el enemigo no está atacando, verifica si el jugador está dentro del rango de ataque
        if (!isAttacking)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            if (playerCollider != null)
            {
                // Si el jugador está dentro del rango de ataque, comienza la animación de ataque
                isAttacking = true;
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            // Si el enemigo está atacando, comienza el temporizador para el tiempo de espera entre ataques
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                // Si ha pasado suficiente tiempo desde el último ataque, realiza un nuevo ataque
                Attack();
                attackTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        // Voltea la dirección del enemigo si es necesario
        if (isAttacking)
        {
            FlipToPlayer();
        }
    }

    void Attack()
    {
        // Busca el jugador dentro del rango de ataque
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (playerCollider != null)
        {
            // Si encuentra al jugador, realiza el ataque
            playerCollider.GetComponent<Health>().TakeDamage(attackDamage);
        }
        // Finaliza la animación de ataque
        isAttacking = false;
        animator.SetTrigger("Idle");
    }

    void FlipToPlayer()
    {
        // Voltea la dirección del enemigo para que siempre mire hacia el jugador
        Vector3 playerDirection = (attackPoint.position - GameObject.FindGameObjectWithTag("Player").transform.position).normalized;
        if ((playerDirection.x < 0f && isFacingRight) || (playerDirection.x > 0f && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el rango de ataque del enemigo en el editor de Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
