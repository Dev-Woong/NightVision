using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CyberSamuraiController : EnemyController
{
    public bool canRush = true;
    public bool canShoot = true;
    public float rushCoolTime = 5;
    public float shootCoolTime = 3;
    public float canRushTime = 0;
    public float canShootTime = 0;
    public Transform BulletTransform;
    public GameObject Bullet;

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
                if (canRush == true)
                {
                    animator.SetBool("isWalk", false);
                    RushAttack(closest);
                }
                if (canShoot == true)
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
            transform.position = closest.position + new Vector3(-stopDistance*1.5f, 0.7f, 0);
        }
        else if (closest.localScale.x == -1)
        {
            transform.position = closest.position + new Vector3(stopDistance*1.5f, 0.7f, 0);
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
        canShootTime = shootCoolTime;
        canShoot = false;
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
        canRushTime = rushCoolTime;
        canRush = false;
    }
    protected void CoolTimeProcess()
    {
        canRushTime -= Time.deltaTime;  
        canShootTime -= Time.deltaTime;
        if (canRushTime <= 0)
        {
            canRush = true;
        }
        if (canShootTime <= 0)
        {
            canShoot = true;
        }
    }
   
}
