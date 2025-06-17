using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : DamageAbleBase, IDamageable
{ 
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    public GameObject nearObject;
    public GameObject damageText;
    
    public Transform damagePos;

    public float speed = 50f;
    public float stopDistance;
    public float detectionRadius;

    public LayerMask PlayerLayer;
    
    public float CurrentHp;
    public float MaxHp = 100;

   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        damageText = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/@Prefabs/damageText.prefab", typeof(GameObject));
        damagePos = transform.Find("hud").transform;

        InstanceHp();
    }
    void Update()
    {
        //Move();  
    }
    void InstanceHp()
    {
        CurrentHp = MaxHp;
    }
    void Move()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, PlayerLayer);

        if (hits.Length > 0)
        {
            Transform closest = null;
            float minDistance = 8;

            foreach (Collider2D hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                animator.SetBool("isWalk", true);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = hit.transform;
                }
            }

            if (closest != null)
            {
                float distanceToTarget = Vector2.Distance(transform.position, closest.position);

                if (distanceToTarget < stopDistance)
                {
                    rb.linearVelocity = Vector2.zero;
                    animator.SetBool("isWalk", false);
                    StartCoroutine(EnAttack());
                }

                else
                {
                    Vector2 direction = ((Vector2)closest.position - rb.position).normalized;
                    transform.Translate(direction * speed );
                    if(closest.position.x > transform.position.x)
                    {
                        transform.localScale = new Vector3(-1,1,1);
                    }
                    if(closest.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    animator.SetBool("isWalk", true);
                    animator.SetBool("isAttack", false);
                }
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    IEnumerator EnAttack()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 offsetPosition = transform.position;
        Gizmos.DrawWireSphere(offsetPosition, detectionRadius);
    }
    
    
    void Die()
    {
        ReleaseObject();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerLayer != 0)
        {
            

            
        }
    }

    public override void OnDamage(float causerAtk)
    { 
        CurrentHp -= causerAtk;
        GameObject hudText = Instantiate(damageText);
        hudText.transform.position = damagePos.position;
        hudText.GetComponent<DamageText>().damage = causerAtk;
        animator.SetTrigger("doHit");
        //if (CurrentHp <= 0)
        //{
        //    Die();
        //}
    }
}
