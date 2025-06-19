using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : DamageAbleBase, IDamageable
{ 
    Rigidbody2D rb;
    Animator animator;
    public EnemyData eData;

    public Transform damagePos;
    public GameObject damageText;

    public float speed = 50f;
    public float stopDistance;
    public float detectionRadius;

    public LayerMask PlayerLayer;
    
    public float CurrentHp;
    public float MaxHp = 100;
    public bool OnHit = false;

    public bool MoveAble = true;
    private Coroutine coAttack;
    private enum EnemyState 
    {
        Patrol,
        Trace,
        Attack,
        Hit,
        Die
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        damageText = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/@Prefabs/damageText.prefab", typeof(GameObject));
        damagePos = transform.Find("hud").transform;
        MaxHp = eData.enemyHp;
        InstanceHp();
    }
    void Update()
    {
        if (OnHit == false)
        {
            Move();
        }
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
                    //if (coAttack != null)
                    //{
                    //    StopCoroutine(coAttack);
                    //}
                    coAttack = StartCoroutine(EnAttack());
                }
                else
                {
                    if (MoveAble == true)
                    {
                        Vector2 direction = ((Vector2)closest.position - rb.position).normalized;
                        rb.linearVelocity = (direction * speed);
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
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Attack");
        //MoveAble = false;
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

    public void OnHitDisable()
    {
        OnHit = false;  
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
