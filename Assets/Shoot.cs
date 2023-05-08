using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float delay = 0.5f; // El delay deseado en segundos

    private Animator animator;
    private bool canShoot = true; // Variable para controlar si se puede disparar

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                animator.SetTrigger("shoot");
            }
            else
            {
                animator.SetTrigger("shootRun");
            }
            canShoot = false;
            Invoke("ShootBullet", delay);
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Disparar en la dirección correcta
        if (transform.localScale.x > 0)
        {
            rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(transform.right * -1f * bulletSpeed, ForceMode2D.Impulse);
        }

        canShoot = true; // Se puede volver a disparar después del delay
    }
}
