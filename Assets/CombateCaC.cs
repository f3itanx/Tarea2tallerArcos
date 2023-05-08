using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float TiempoEntreAtaques;
    [SerializeField] private float TiempoSiguienteAtaque;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       if (TiempoSiguienteAtaque > 0)
        {
            TiempoSiguienteAtaque -= Time.deltaTime;
        }
        
        
        if (Input.GetButtonDown("Fire1")) // Quitamos el punto y coma sobrante
        {
            Golpe();
            TiempoSiguienteAtaque = TiempoEntreAtaques;
        }
    }

    private void Golpe()
    {
        animator.SetTrigger("Golpe");

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<Enemy>().tomarDaño(dañoGolpe); // Cambiamos el nombre del método
            }
        }
    }

    private void OnDrawGizmosSelected() // Cambiamos el nombre del método y corregimos el uso de Gizmos
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
