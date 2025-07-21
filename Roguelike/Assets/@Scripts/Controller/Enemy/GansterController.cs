using UnityEngine;

public class GansterController : EnemyController
{

    protected override void Move()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, PlayerLayer);

        if (hits.Length > 0 || onAttacked == true)
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
                }
                else
                {
                    Debug.Log($"ÀÌµ¿Áß {rb.linearVelocity}");
                    animator.SetBool("isWalk", true);
                    rb.linearVelocity = (direction.normalized * speed);
                    if (closest.position.x >= transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);

                    }
                    if (closest.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);

                    }

                }
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            rb.linearVelocity = Vector3.zero;
        }
    }
}
