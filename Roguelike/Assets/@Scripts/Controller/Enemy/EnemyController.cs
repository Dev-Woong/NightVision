using System.Collections;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : DamageAbleBase, IDamageable
{ 
    protected Rigidbody2D rb;
    protected Animator animator;
    protected PublicStatus ps;
    public WeaponType wType;
    public Transform damagePos;
    public GameObject damageText;

    public float speed;
    public float stopDistance;
    public float detectionRadius;

    public LayerMask PlayerLayer;
    
    public float curHp;
    public bool OnHit = false;
    public bool canAttack = true;

    public bool moveAble = true;
    public bool isGround = false;
    //public bool damageAble = true;
    protected Coroutine coAttack;
    public Transform closest;
    public Vector2 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ps = GetComponent<PublicStatus>();
        curHp = gameObject.GetComponent<PublicStatus>().maxHp;
        speed = GetComponent<PublicStatus>().speed;
    }
    void Update()
    {
        Move();
    }
    protected virtual void Move()
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
            if (closest != null && moveAble == true && isGround==true)
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
                    
                    animator.SetBool("isWalk", true);
                    rb.linearVelocity = (direction.normalized * speed);
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
            rb.linearVelocity = Vector3.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    public IEnumerator EnAttack()
    {
        canAttack = false;
        moveAble = false;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);
        canAttack = true;
        coAttack = null;
        yield return null; 
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 offsetPosition = transform.position;
        Gizmos.DrawWireSphere(offsetPosition, detectionRadius);
    }
    
    
    void Die()
    {
        gameObject.SetActive(false);
        //ReleaseObject();
    }

    
    public void OnMoveAble() // Animation Event
    {
        moveAble = true;
    }
    
    public override void OnDamage(float causerAtk)
    {
        if (damageAble == true)
        {
            curHp -= causerAtk;
            GameObject hudText = Instantiate(damageText);
            hudText.transform.position = damagePos.position;
            hudText.GetComponent<DamageText>().damage = causerAtk;
            moveAble = false;
            PlayHitAnimation(wType, causerAtk);
            if (curHp <= 0)
            {
                damageAble = false;
                animator.SetTrigger("Die");
            }
        }
    }
    protected virtual void PlayHitAnimation(WeaponType wType, float causerAtk)
    {
        switch (wType)
        {
            case WeaponType.Gun:
                animator.SetTrigger("Hit");
                curHp -= causerAtk / 4;
                break;
            case WeaponType.Hand:
                animator.SetTrigger("Hit");
                curHp -= causerAtk;
                break;
            case WeaponType.Sword:
                animator.SetTrigger("Hit");
                curHp -= causerAtk;
                break;
        }
    }
}
