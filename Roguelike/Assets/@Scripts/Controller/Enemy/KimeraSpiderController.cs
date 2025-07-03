using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;

public class KimeraSpiderController : EnemyController
{
    public bool doShoot = true;
    public float shootCoolTime = 6;
    public float curShootTime = 0;

    public bool doDropAttack = false;
    public bool onDropSequence = false;
    public float dropAttackCoolTime = 10;
    public float curDropAttackTime = 0;

  
    public float elapsed = 0f;
    public Transform BulletTransform;
    public GameObject Bullet;
    public GameObject WarningIndicator;
    public Vector2 distanceToTarget;
    Coroutine coShoot;
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
                    direction = new Vector2(closest.position.x - rb.position.x, 0);
                }
            }
            if (closest != null && moveAble == true && isGround == true)
            {
                float distanceToTarget = Vector2.Distance(transform.position, closest.position);
                
                if (closest.position.x >= transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (closest.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (doShoot == true)
                {
                    animator.SetBool("isWalk", false);
                    
                    coShoot ??= StartCoroutine(nameof(CoShoot)); 
                    rb.linearVelocity = Vector3.zero;
                }
                if (onDropSequence == true && curHp <= ps.maxHp/2)
                {
                    rb.linearVelocity = Vector3.zero;
                    animator.SetBool("isWalk", false);
                    animator.SetBool("DropSequence",true);
                    onDropSequence = false;
                    rb.linearVelocity = Vector3.zero;
                    moveAble = false;
                }
                if (distanceToTarget < stopDistance)
                {
                    animator.SetBool("isWalk", false);
                    if (canAttack == true)
                    {
                        rb.linearVelocity=Vector3.zero;
                        coAttack = StartCoroutine(EnAttack());
                    }
                }
                else
                {
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
        }
    }
    public void SetShootFalseState()
    {
        animator.SetBool("Spit", false);
        coShoot = null;
        moveAble = true;
    }
    IEnumerator CoShoot()
    {
        moveAble = false;
        
        yield return new WaitForSeconds(0.2f);
        Debug.Log("1");
        animator.SetBool("isWalk", false);
        animator.SetBool("Spit", true);
        yield return null;
    }
    protected void DropAttackProcess()
    {
        damageAble = false; 
        rb.linearVelocity = (direction.normalized * speed);
        doDropAttack = true;
        Debug.Log(elapsed);
        WarningIndicator.SetActive(true);
    }
    protected void DoDrop() // Animation Event
    {
        WarningIndicator.SetActive(false);
        doDropAttack = false;
        animator.SetTrigger("DropAttack");
        damageAble = true;
        rb.linearVelocity = Vector3.zero;
    }
    public void EndDropAttack() // Animation Event
    {
        animator.SetBool("DropSequence", false);
        curDropAttackTime = dropAttackCoolTime;
        
    }
    protected void Shoot() // AnimationEvent
    {
        curShootTime = shootCoolTime;
        doShoot = false;
        moveAble = false;
        //distanceToTarget = (new Vector3(closest.position.x,closest.position.y+0.2f,0) - BulletTransform.position).normalized;
        GameObject bullet = Instantiate(Bullet, BulletTransform.position, Quaternion.identity);
        LayerMask playerLayer = 6;
        if (gameObject.transform.localScale.x == 1)
        {   
            bullet.transform.eulerAngles = new Vector3(0, 0, 90);
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, transform.right,10, ps.atk);
        }
        else if (gameObject.transform.localScale.x == -1)
        {
            bullet.transform.eulerAngles = new Vector3(0, 0, -90);
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, -transform.right,10, ps.atk);
        }
       
    }
    protected void CoolTimeProcess()
    {
        curDropAttackTime -= Time.deltaTime;
        curShootTime -= Time.deltaTime;
        if (curDropAttackTime <= 0)
        {
            onDropSequence = true;
        }
        else { onDropSequence = false; }
        if (curShootTime <= 0)
        {
            doShoot = true;
        }
        if (doDropAttack == true)
        {
            
            Vector3 dir = new Vector3(direction.x, transform.position.y, 0);
            rb.linearVelocity = (speed*4f *dir.normalized);
        }
        
    }
}
