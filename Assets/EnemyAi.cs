using Unity.VisualScripting;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Transform player;
    bool isPlayerDetected = false;
    bool isAttacking = false;
    float detectionRange = 1.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Character").transform;
        animator = GetComponent<Animator>();
    }

    void attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("attack");
        }
    }

    void detectPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (!isPlayerDetected)
        {
            if(distance <= detectionRange)
            {
                isPlayerDetected = true;
                attack();
            }
            else
            {
                isPlayerDetected = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        detectPlayer();
    }
}
