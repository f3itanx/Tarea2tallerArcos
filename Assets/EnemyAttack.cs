using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    public float distance = 2.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Distancia al jugador: " + distance);
        if (distance < distance)
        {
            anim.SetTrigger("attack");
        }
    }

}
