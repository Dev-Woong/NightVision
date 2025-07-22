using UnityEngine;

public class CopController : EnemyController
{
    public bool doShoot = true;
    public float shootCoolTime = 3;
    public float curShootTime = 0;
    public Transform BulletTransform;
    public GameObject Bullet;
    public AudioClip shotClip;
    protected override void Move()
    {
        CoolTimeProcess();
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
                direction = new Vector2(closest.position.x - transform.position.x, 0);
                if (closest.position.x >= transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (closest.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                if (doShoot == true&& distanceToTarget<=5)
                {
                    animator.SetBool("isWalk", false);
                    rb.linearVelocity = Vector3.zero;
                    animator.SetTrigger("Attack");
                }
                else
                {
                    
                    if (distanceToTarget >= stopDistance)
                    {
                        rb.linearVelocity = (direction.normalized * speed);
                        animator.SetBool("isWalk", true);
                    }
                    else
                    {
                        animator.SetBool("isWalk", false);
                    }
                }
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    protected void CoolTimeProcess()
    {
        curShootTime -= Time.deltaTime;
        if (curShootTime <= 0)
        {
            doShoot = true;
        }
    }
    public void Shoot() // AnimationEvent
    {
        SFXManager.Instance.PlaySFX(shotClip);
        curShootTime = shootCoolTime;
        doShoot = false;
        moveAble = false;
        GameObject bullet = Instantiate(Bullet, BulletTransform.position, Quaternion.identity);
        LayerMask playerLayer = 6;
        if (gameObject.transform.localScale.x == 1)
        {
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, transform.right, 15, ps.atk);
        }
        else if (gameObject.transform.localScale.x == -1)
        {
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, -transform.right, 15, ps.atk);
        }
    }
}
