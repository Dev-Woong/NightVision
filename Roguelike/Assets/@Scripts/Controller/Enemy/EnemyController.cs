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
    
    public float CurrentHp;
    public bool OnHit = false;

    public bool MoveAble = true;
    private Coroutine coAttack;
    Vector2 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ps = GetComponent<PublicStatus>();
        damageText = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/@Prefabs/DamageText.prefab", typeof(GameObject));
        damagePos = transform.Find("hud").transform;
        CurrentHp = gameObject.GetComponent<PublicStatus>().maxHp;
        
    }
    void Update()
    {
        
    }
    void Move()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, PlayerLayer);

        if (hits.Length > 0)
        {
            Transform closest = null;
            float minDistance = Mathf.Infinity;
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
                    if (coAttack == null)
                        coAttack = StartCoroutine(EnAttack());
                    else return;
                }
                else
                {
                    if (ps.canMove== true)
                    {
                        direction = new Vector2(closest.position.x - rb.position.x, 0);
                        rb.linearVelocity = (direction.normalized * speed);
                        if (closest.position.x > transform.position.x)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        if (closest.position.x < transform.position.x)
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        animator.SetBool("isWalk", true);
                    }
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
        
        animator.SetTrigger("Attack");
        MoveAble = false;
        yield return new WaitForSeconds(1.5f);
        MoveAble = true;
        coAttack = null;

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

    
    public void OnMoveAble() // Animation Event
    {
        MoveAble = true;
    }
    
    public override void OnDamage(float causerAtk)
    {
        CurrentHp -= causerAtk;
        GameObject hudText = Instantiate(damageText);
        hudText.transform.position = damagePos.position;
        hudText.GetComponent<DamageText>().damage = causerAtk;
        animator.SetBool("isWalk", false);
        animator.SetTrigger("Hit");
        if (CurrentHp <= 0)
        {
          // Die();
        }
    }
}
