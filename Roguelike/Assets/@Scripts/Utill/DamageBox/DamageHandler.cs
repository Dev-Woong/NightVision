using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class DamageHandler : MonoBehaviour 
{
    public AudioSource audioSource;
    public ScopeController sc;
    public PlayerStatus ps;
    
    private readonly HashSet<IDamageable> damagedTargets = new();
    private readonly WaitForSeconds Interval = new(0.04f);
    
    public void CreateAttackBox(AttackData data)
    {
        if (data == null) return;
        damagedTargets.Clear();
        float x = transform.localScale.x;
        if (data.weaponType != WeaponType.Gun)
        {
            Vector3 hitPos = transform.position + transform.right * data.rangeOffset * x;
            Collider2D[] hits = Physics2D.OverlapBoxAll(hitPos, data.hitBoxSize, 0, data.targetMask);
            foreach (var hit in hits)
            {
                IDamageable dmg = hit.GetComponent<IDamageable>();
                if (dmg != null && !damagedTargets.Contains(dmg))
                {
                    damagedTargets.Add(dmg);
                    StartCoroutine(HitDamage(dmg, data, x, hit.transform.position));
                }
            }
        }
        else 
        {
            Vector3 hitPos = sc.transform.position;
            Collider2D[] hits = Physics2D.OverlapBoxAll(hitPos, data.hitBoxSize, 0, data.targetMask);
            foreach (var hit in hits)
            {
                IDamageable dmg = hit.GetComponent<IDamageable>();
                if (dmg != null && !damagedTargets.Contains(dmg))
                {
                    damagedTargets.Add(dmg);
                    StartCoroutine(HitDamage(dmg, data, x, hit.transform.position));
                }
            }
        }
    }
    IEnumerator HitDamage(IDamageable dmg, AttackData data,float x,Vector3 enemyPos)
    {
        int currentHits = 0;
        while (currentHits < data.hitCount)
        {
            float finalDmg = data.damageValue * ps.atk;
            float randDmg = Mathf.RoundToInt(Random.Range(finalDmg * 0.9f, finalDmg *1.1f));
            dmg.TakeDamage((randDmg));
            //audioSource.PlayOneShot(data.SFX);
            if (data.HitEffect != null) 
            {
                Vector3 effectPos = enemyPos + new Vector3(data.effectPos.x * x, data.effectPos.y, data.effectPos.z);
                var effect = Instantiate(data.HitEffect, effectPos, Quaternion.identity);

                if (transform.localScale.x == -1)
                {
                    effect.GetComponent<SpriteRenderer>().flipX = true;
                }
            } 
            currentHits++;
            yield return Interval;
        }
        
    }
}
