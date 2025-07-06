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

    public bool enterBerserkMode = false;
    public bool onParticles = true;
    public float elapsed = 0f;
    public Transform BulletTransform;
    public GameObject Bullet;
    public GameObject WarningIndicator;
    public GameObject Particles;
    public Vector2 distanceToTarget;
    Coroutine coShoot;
    Coroutine coSetColor;
    protected override void Move()
    {
        CoolTimeProcess();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, PlayerLayer);
        ParticleRot();
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
                if (onDropSequence == true && enterBerserkMode == true)
                {
                    rb.linearVelocity = Vector3.zero;
                    animator.SetBool("isWalk", false);
                    animator.SetBool("DropSequence",true);
                    //Particles.SetActive(false);
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
    public void EnterBerserkMode()
    {
        ps.atk *= 3f;
        ps.speed *= 1.8f;
        ps.def *= 0.3f;
        Particles.SetActive(true);
        animator.SetBool("SetBerserk", true);
        StartCoroutine(nameof(SetColor));
    }
    public void ParticleRot()
    {
        if (transform.localScale.x == 1)
        {
            Particles.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.localScale.x == -1)
        {
            Particles.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    IEnumerator SetColor()
    {
        int a = 0;
        float g = 1;
        float b = 1;
        while (a <40)
        {
            g -= 0.025f;
            b -= 0.025f;
            sr.color = new Color(1,g, b,1 );
            a++;
            yield return new WaitForSeconds(0.05f);
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
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isWalk", false);
        animator.SetBool("Spit", true);
        yield return null;
    }
    protected void DropAttackProcess()
    {
        damageAble = false;
        
        rb.linearVelocity = (direction.normalized * speed);
        doDropAttack = true;
        sr.color = new Color(0,0,0,0);
        WarningIndicator.SetActive(true);
    }
    protected void DoDrop() // Animation Event
    {
        WarningIndicator.SetActive(false);
        if (enterBerserkMode == false)
        {
            sr.color = new Color(1, 1, 1, 1);
        }
        else sr.color = new Color(1,0 , 0, 1);
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
    public void SetTParticles()
    {
        onParticles = !onParticles;
        Particles.SetActive(onParticles);
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
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, transform.right,20, ps.atk);
        }
        else if (gameObject.transform.localScale.x == -1)
        {
            bullet.transform.eulerAngles = new Vector3(0, 0, -90);
            bullet.GetComponent<Bullet>().SetBullet(playerLayer, -transform.right,20, ps.atk);
        }
       
    }
    protected override void MonsterHitLogic(WeaponType wType, float causerAtk)
    {
        float finalDmg;
        if (eType == EnemyType.Normal)
        {
            moveAble = false;
            switch (wType)
            {
                case WeaponType.Gun:
                    animator.SetTrigger("Hit");
                    break;
                case WeaponType.Hand:
                    animator.SetTrigger("Hit");
                    break;
                case WeaponType.Sword:
                    animator.SetTrigger("Hit");
                    break;
            }
        }
        finalDmg = causerAtk - ps.def;
        curHp -= finalDmg;
        GameObject hudText = Instantiate(damageText);
        hudText.transform.position = damagePos.position;
        hudText.GetComponent<DamageText>().damage = Mathf.RoundToInt(finalDmg);
        if (curHp <= ps.maxHp / 2 && enterBerserkMode == false)
        {
            enterBerserkMode = true;
            animator.SetBool("isWalk", false);
            animator.SetTrigger("EnterBerserk");
            moveAble = false;
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
