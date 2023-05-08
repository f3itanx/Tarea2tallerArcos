using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float vida;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void tomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            muerte();
        }
    }

    private void muerte()
    {
        animator.SetTrigger("Muerte");
        Destroy(gameObject, 2);
    }
}
