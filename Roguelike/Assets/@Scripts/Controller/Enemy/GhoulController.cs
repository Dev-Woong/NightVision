using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GhoulController : EnemyController 
{ 
    public bool ghoulWalkable = false;
    
    protected override void Move()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, PlayerLayer);

        if (hits.Length > 0)
        {
            float minDistance = Mathf.Infinity;
            foreach (Collider2D hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    closest = hit.transform;
                }
            }
            if (closest != null && moveAble == true && isGround == true)
            {
                float distanceToTarget = Vector2.Distance(transform.position, closest.position);
                direction = new Vector2(closest.position.x - rb.position.x, 0);
                if (distanceToTarget < stopDistance)
                {
                    animator.SetBool("isWalk", false);
                    if (canAttack == true)
                        coAttack = StartCoroutine(EnAttack());
                    else return;
                }
                else
                {
                    animator.SetBool("isWalk", true);
                    animator.SetBool("isIdle", false);
                    if (ghoulWalkable == true)
                    {
                        rb.linearVelocity = (direction.normalized * speed);
                    }
                    if (closest.position.x >= transform.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    if (closest.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }

                }
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isIdle", true);
            ghoulWalkable = false;
        }
    }
    public void GhoulWalkable()
    {
        ghoulWalkable = true;
    }
    
}
