using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrc : MonoBehaviour
{
    private bool playerDetected = false;
    private Rigidbody2D rb;
    private int movX = 1;
    private int movXPrev;
    [SerializeField] float patrolSpeed;
    [SerializeField] float maxWalkTime;
    Timer patrolTimer;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        patrolTimer = GetComponent<Timer>();
        patrolTimer.timeLeft = maxWalkTime;
    }
    private void Patrol()
    {
        if (patrolTimer.timerOn) return;
        if (movX == 0)
        {
            movX = -movXPrev;
            patrolTimer.Restart(maxWalkTime);
        }
        else StandStill(1f);
    }

    private void StandStill(float time)
    {       
            movXPrev = movX;
            movX = 0;
            patrolTimer.Restart(time);
    }

    private void Attack()
    {
        DetectPlayer();
        //Debug.Log("player detected " + playerDetected);

        // Probando animacion
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetTrigger("attack");
        }

        //if (playerDetected)
        //{
        //    animator.SetTrigger("attack");
        //}
    }

    private void DetectPlayer()
    {
        // TODO
    }
    private void FlipSprite()
    {
        if (movX != 0)
        {
            transform.localScale = new Vector3(movX, 1, 1);
        }
    }
    private void DetectXMovement()
    {
        float isWalking = rb.velocity.x != 0 ? 1 : 0;
        animator.SetFloat("xVelocity", isWalking);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Patrol();
        FlipSprite();
        Attack();
        rb.velocity = new Vector2 (patrolSpeed * movX, 0);
        DetectXMovement();
    }
}