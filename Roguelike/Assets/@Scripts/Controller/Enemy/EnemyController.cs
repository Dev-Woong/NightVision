using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Boss
}
public class EnemyController : DamageAbleBase, IDamageable
{
    public EnemyType eType;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected BoxCollider2D bc;
    protected PublicStatus ps;
    protected SpriteRenderer sr;
    public Transform damagePos;
    public GameObject damageText;

    public float speed;
    public float stopDistance;
    public float detectionRadius;

    public LayerMask PlayerLayer;

    public float curHp;
    public bool OnHit = false;
    public bool canAttack = true;
    public bool onAttacked = false;
    public bool moveAble = true;
    public bool isGround = false; 
    protected Coroutine coAttack;
    public Transform closest;
    public Vector2 direction;
    public AudioClip dieClip;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<PublicStatus>();
        curHp = gameObject.GetComponent<PublicStatus>().maxHp;
        speed = GetComponent<PublicStatus>().speed;
    }
    private void Update()
    {
        Move();
    }
    protected virtual void Move()
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
                if (closest.position.x >= transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (closest.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (distanceToTarget < stopDistance)
                {
                    animator.SetBool("isWalk", false);
                    if (canAttack == true)
                    {
                        
                        coAttack = StartCoroutine(EnAttack());
                    }
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

    public void DieSFX()
    {
        SFXManager.Instance.PlaySFX(dieClip);
    }
    void Die()
    {
        ps.checkDie = true;

        gameObject.SetActive(false);
    }


    public void OnMoveAble() // Animation Event
    {
        moveAble = true;
    }
    public void MoveAbleFalse()
    {
        moveAble = false;
    }

    public override void OnDamage(float causerAtk, WeaponType wType)
    {
        if (damageAble == true)
        {
            MonsterHitLogic(wType, causerAtk);
            if (curHp <= 0)
            {
                damageAble = false;
                gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero ;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                animator.SetTrigger("Die");
            }
        }
    }
    protected virtual void MonsterHitLogic(WeaponType wType, float causerAtk)
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
        onAttacked = true;
        detectionRadius = Mathf.Infinity;
        finalDmg = causerAtk-ps.def;
        curHp -= finalDmg;
        GameObject hudText = Instantiate(damageText);
        hudText.transform.position = damagePos.position;
        hudText.GetComponent<DamageText>().damage = Mathf.RoundToInt(finalDmg);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (collision.gameObject.GetComponent<Bullet>().targetMask == 7)
            {
                GameObject hudText = Instantiate(damageText);
                hudText.transform.position = damagePos.position;
                hudText.GetComponent<DamageText>().damage = collision.GetComponent<Bullet>().atk;
                if (curHp <= 0)
                {
                    damageAble = false;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    animator.SetTrigger("Die");
                }
            }
        }
    }
}

