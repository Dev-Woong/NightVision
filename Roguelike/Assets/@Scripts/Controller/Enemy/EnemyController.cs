using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : DamageAbleBase, IDamageable
{ 
    Rigidbody2D rb;
    Animator animator;
    public PublicStatus ps;

    public Transform damagePos;
    public GameObject damageText;

    public float speed = 50f;
    public float stopDistance;
    public float detectionRadius;

    public LayerMask PlayerLayer;
    
    public float curHp;
    public bool OnHit = false;

    public bool moveAble = true;
    public bool isGround = false;
    //public bool damageAble = true;
    private Coroutine coAttack;
    Transform closest;
    Vector2 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ps = GetComponent<PublicStatus>();
        damageText = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/@Prefabs/DamageText.prefab", typeof(GameObject));
        damagePos = transform.Find("hud").transform;
        curHp = gameObject.GetComponent<PublicStatus>().maxHp;
        
    }
    void Update()
    {
        Move();
        
    }
    void Move()
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

                if (distanceToTarget < stopDistance)
                {
                    //rb.linearVelocity = Vector2.zero;
                    animator.SetBool("isWalk", false);
                    if (coAttack == null)
                        coAttack = StartCoroutine(EnAttack());
                    else return;
                }
                else
                {
                    direction = new Vector2(closest.position.x - rb.position.x, 0);
                    rb.linearVelocity = (direction.normalized * speed);
                    if (closest.position.x >= transform.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    if (closest.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    animator.SetBool("isWalk", true);

                    animator.SetBool("isIdle", false);
                }
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isIdle", true);
            moveAble = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    IEnumerator EnAttack()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Attack");
        moveAble = false;
        yield return new WaitForSeconds(1.5f);
        moveAble = true;
        coAttack = null;
        animator.SetBool("isWalk", true);
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
            animator.SetTrigger("Hit");
            //if (curHp <= 0)
            //{
            //    damageAble = false;
            //    Debug.Log("Die");
            //    animator.SetTrigger("Die");
            //}
        }
        

    }
}
