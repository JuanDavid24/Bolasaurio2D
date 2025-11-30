using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrc : MonoBehaviour
{
    private Rigidbody2D rb;
    private int movX = 1;
    private int movXPrev;
    
    [SerializeField] float patrolSpeed;
    [SerializeField] float maxWalkTime;
    Timer patrolTimer;
    float isWalking;

    Animator animator;
    public bool isAttacking = false;
    public int damage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        patrolTimer = GetComponent<Timer>();
        patrolTimer.timeLeft = maxWalkTime;
    }

    private void StandStill(float time)
    {       
            movXPrev = movX;
            movX = 0;
            patrolTimer.Restart(time);
    }

    public void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;
        animator.SetTrigger("attack");
        rb.velocity = Vector2.zero;
        return;
    }

    private void FlipSprite()
    {
        if (movX != 0)
        {
            transform.localScale = new Vector3(movX, 1, 1);
        }
    }

    private void DetectWalking()
    {
        isWalking = rb.velocity.x != 0 ? 1 : 0;
        animator.SetFloat("xVelocity", isWalking);
        movX = (int) Mathf.Sign(rb.velocity.x);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print("isAttacking" + isAttacking);
        DetectWalking();
        FlipSprite();

    }
}