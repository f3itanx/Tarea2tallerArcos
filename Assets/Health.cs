using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private float vida;

    [SerializeField] private BarraDeVida barraDeVida;



    private void Start()
    {
        currentHealth = maxHealth;
        barraDeVida.InicializarBarraDeVida(vida);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        barraDeVida.CambiarVidaActual(vida);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        // Aquí puedes implementar lo que quieras que suceda cuando la salud llegue a 0
        Destroy(gameObject);
    }
}
