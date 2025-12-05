using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAndChaseAI : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform player;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 4f;
    public float distance = 7f;
    public float attackDistance = 1f;
    private float playerDistance;
    private Transform currentTarget;
    private Rigidbody2D rb;
    EnemyController enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyController>();
        currentTarget = pointA;
    }

    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        if (playerDistance < distance)
        {
            if(playerDistance <= attackDistance)
            {
                enemy.Attack();
            }
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!enemy.isAttacking)
        {
            float distanceToTarget = Vector3.Distance(currentTarget.position, transform.position);
            if (distanceToTarget < 0.5f)
            {
                currentTarget = currentTarget == pointA ? pointB: pointA;
                print("current target " + currentTarget);
            }
            MoveTowardsX(currentTarget.position);
        }
    }

    private void MoveTowardsX(Vector3 targetPosition)
    {
        float direction = Mathf.Sign(targetPosition.x - transform.position.x);
        rb.velocity = new Vector2(direction * patrolSpeed, rb.velocity.y);
    }

    private void Chase()
    {
        if (!enemy.isAttacking)
            MoveTowardsX(player.position);
    }
}
