using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float lightAttackDamage = 10f;  // Da�o del ataque ligero
    [SerializeField] private float heavyAttackDamage = 20f;  // Da�o del ataque fuerte
    [SerializeField] private float attackRange = 1f;         // Rango de ataque
    [SerializeField] private LayerMask attackLayer;          // Capa para detectar objetivos de ataque
    [SerializeField] private float attackCooldown = 0.5f;    // Tiempo m�nimo entre ataques

    [Header("Animation Settings")]
    [SerializeField] private Animator animator;              // Animator del personaje
    [SerializeField] private string lightAttackTrigger;      // Nombre del trigger para la animaci�n del ataque ligero
    [SerializeField] private string heavyAttackTrigger;      // Nombre del trigger para la animaci�n del ataque fuerte

    private bool isAttacking = false; // Bandera para evitar atacar continuamente
    private float lastAttackTime = 0f; // �ltimo momento en el que se realiz� un ataque

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        // Si no estamos atacando y ha pasado el tiempo m�nimo desde el �ltimo ataque
        if (!isAttacking && Time.time - lastAttackTime > attackCooldown)
        {
            // Atacar al presionar clic izquierdo o derecho
            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                animator.SetBool("isLightAttacking", true);
                Attack(lightAttackDamage);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                isAttacking = true;
                animator.SetBool("isHeavyAttacking", true);
                Attack(heavyAttackDamage);
            }
        }
        else
        {
            // Reiniciar la bandera de ataque despu�s de la animaci�n
            animator.SetBool("isLightAttacking", false);
            animator.SetBool("isHeavyAttacking", false);
            isAttacking = false;
        }
    }

    private void Attack(float attackDamage)
    {
        lastAttackTime = Time.time;

        // Detectar objetos en el rango de ataque
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, attackLayer);

        // Aplicar da�o a los objetos detectados
        foreach (Collider2D hit in hits)
        {
            Health health = hit.GetComponent<Health>();
            if (health != null)
            {
                int damage = Mathf.RoundToInt(attackDamage);
                health.TakeDamage(damage);
            }
        }
    }

    // Dibujar gizmos para visualizar el rango



    // Dibujar gizmos para visualizar el rango de ataque
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
