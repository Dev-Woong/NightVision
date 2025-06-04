using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class DamageHandler : MonoBehaviour 
{
    public AudioSource audioSource;
    public ScopeController sc;
    public float interval = 0.06f;
    private readonly HashSet<IDamageable> damagedTargets = new();
    WaitForSeconds Interval = new(0);
    private void Start()
    {
        Interval = new(interval);
    }
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
            float finalDmg = data.damageValue * 1; /*플레이어 공격 스탯*/
            float randDmg = Mathf.RoundToInt(Random.Range(finalDmg - 2, finalDmg + 2));
            dmg.TakeDamage((randDmg));
            Debug.Log($"{randDmg}");
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
