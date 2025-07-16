using UnityEngine;

public class CyberSamuraiController : EnemyController
{
    public bool doRush = true;
    public bool doShoot = true;
    public float rushCoolTime = 5;
    public float shootCoolTime = 3;
    public float curRushTime = 0;
    public float curShootTime = 0;
    public Transform BulletTransform;
    public GameObject Bullet;
    public AudioClip shotClip;
    protected override void Move()
    {
        CoolTimeProcess();
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
                if (closest.position.x >= transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (closest.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (doRush == true)
                {
                    animator.SetBool("isWalk", false);
                    RushAttack(closest);
                }
                else if (doShoot == true)
                {
                    animator.SetBool("isWalk", false);
                    animator.SetTrigger("Shoot");
                }
                else
                {
                    if (distanceToTarget < stopDistance)
                    {
                        animator.SetBool("isWalk", false);
                        if (canAttack == true)
                            coAttack = StartCoroutine(EnAttack());
                    }
                    else
                    {
                        animator.SetBool("isWalk", true);
                        rb.linearVelocity = (direction.normalized * speed);
                        
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
    public void RushTelePortAnimEvent()
    {
        if (closest.localScale.x == 1)
        {
            transform.position = closest.position + new Vector3(-stopDistance*1.1f, 0.7f, 0);
        }
        else if (closest.localScale.x == -1)
        {
            transform.position = closest.position + new Vector3(stopDistance*1.1f, 0.7f, 0);
        }
        if (closest.position.x >= transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (closest.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    } 
    
    public void Shoot() // AnimationEvent
    {
        curShootTime = shootCoolTime;
        SFXManager.Instance.PlaySFX(shotClip);
        doShoot = false;
        moveAble = false;
        GameObject bullet = Instantiate(Bullet, BulletTransform.position, Quaternion.identity);
        LayerMask playerLayer = 6;
        if (gameObject.transform.localScale.x == 1)
        {
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, transform.right, 15, ps.atk);
        }
        else if (gameObject.transform.localScale.x ==-1)
        {
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, -transform.right, 15, ps.atk);
        }
    }
    
    public void RushAttack(Transform targetPos)
    {
        
        animator.SetTrigger("Rush");
        curRushTime = rushCoolTime;
        doRush = false;
    }
    protected void CoolTimeProcess()
    {
        curRushTime -= Time.deltaTime;  
        curShootTime -= Time.deltaTime;
        if (curRushTime <= 0)
        {
            doRush = true;
        }
        if (curShootTime <= 0)
        {
            doShoot = true;
        }
    }
   
}
