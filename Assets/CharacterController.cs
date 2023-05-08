using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public LayerMask capaPiso;
    public int saltosMaximos;

    private Rigidbody2D rigibody;
    private bool mirandoDerecha = true;
    private BoxCollider2D boxCollider;
    private int saltoRestantes;
    private bool yaSalto = false;
    private Animator animator;

    private void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltoRestantes = saltosMaximos;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaPiso);
        return hit.collider != null;
    }

    void ProcesarSalto()
    {
        if (EstaEnSuelo())
        {
            saltoRestantes = saltosMaximos;
            yaSalto = false; // Si está en el suelo, puede saltar de nuevo
        }

        if (Input.GetKeyDown(KeyCode.Space) && !yaSalto) // Solo salta si no ha saltado antes
        {
            yaSalto = true; // Ya saltó, no puede saltar de nuevo
            rigibody.velocity = new Vector2(rigibody.velocity.x, 0f);
            rigibody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");

        rigibody.velocity = new Vector2(inputMovimiento * velocidad, rigibody.velocity.y);

        GestionarOrientacion(inputMovimiento);

        animator.SetBool("isRunning", inputMovimiento != 0f);
        animator.SetBool("Jump", !EstaEnSuelo());
    }

    void GestionarOrientacion(float inputMovimineto)
    {
        if ((mirandoDerecha == true && inputMovimineto < 0) || (mirandoDerecha == false && inputMovimineto > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
