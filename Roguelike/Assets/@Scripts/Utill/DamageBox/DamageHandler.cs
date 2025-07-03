using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform target;
    public PublicStatus ps;
    
    Vector3 hitPos;
    private readonly HashSet<IDamageable> damagedTargets = new();
    private readonly WaitForSeconds Interval = new(0.04f);
    Coroutine coDamageProcess;
    public void Start()
    {
        ps = GetComponent<PublicStatus>();
    }
    public void PlayerCreateAttackBox(AttackData data)
    {
        if (data == null) return;
        damagedTargets.Clear();
        float x = transform.localScale.x;
        if (data.skillType != SkillType.AOE)
        {

            hitPos = transform.position + transform.right * data.rangeOffset * x;
        }
        else
        {

            if (target == null) { Debug.LogWarning("지정 타겟이 없음! "); return; }
            hitPos = target.position;
        }
        Collider2D[] hits = Physics2D.OverlapBoxAll(hitPos, data.hitBoxSize, 0, data.targetMask);
        Debug.DrawLine(transform.position, hitPos, Color.red, 1f);
        foreach (var hit in hits)
        {
            IDamageable dmg = hit.GetComponent<IDamageable>();
            if (dmg != null && !damagedTargets.Contains(dmg))
            {
                damagedTargets.Add(dmg);
                if (hit.GetComponent<DamageAbleBase>().damageAble == true)
                {
                    
                    coDamageProcess = StartCoroutine(HitDamage(dmg, data, x, hit.transform.position, hit.gameObject));
                }
            }
        }
    }
    IEnumerator HitDamage(IDamageable dmg, AttackData data, float x, Vector3 targetPos, GameObject target)
    {
        int currentHits = 0;
        var enemyCtrl = target.GetComponent<EnemyController>();
        if (data.knockBack == KnockBack.Done && enemyCtrl != null && enemyCtrl.eType != EnemyType.Boss)
        {
            
            if (target.layer == 6 && data.knockBackForceY > 0f)
            {
                target.GetComponent<PlayerController>().moveAble = false;
                target.GetComponent<PlayerController>().OnAirTool();
                target.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            else if (target.layer == 7 && data.knockBackForceY > 0f)
            {
                    target.GetComponent<EnemyController>().isGround = false;
            }  
            if (targetPos.x < transform.position.x)
            {
                target.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                target.GetComponent<Rigidbody2D>().AddForce(new Vector3(-data.knockBackForceX, data.knockBackForceY, 0), ForceMode2D.Impulse);
            }
            else
            {
                target.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                target.GetComponent<Rigidbody2D>().AddForce(new Vector3(data.knockBackForceX, data.knockBackForceY, 0), ForceMode2D.Impulse);
            }
        }
        while (currentHits < data.hitCount)
        {
            float finalDmg = data.damageValue * ps.atk;
            float randDmg = Mathf.RoundToInt(Random.Range(finalDmg * 0.9f, finalDmg * 1.1f));

            dmg.TakeDamage((randDmg), data.wType);

            //audioSource.PlayOneShot(data.SFX);
            if (data.HitEffect != null)
            {
                Vector3 effectPos = targetPos + new Vector3(data.effectPos.x * x, data.effectPos.y, data.effectPos.z);
                var effect = Instantiate(data.HitEffect, effectPos, Quaternion.identity);

                if (transform.localScale.x == -1)
                {
                    effect.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            currentHits++;
            yield return Interval;
        }
        yield return new WaitForSeconds(0.4f);
    }
}
