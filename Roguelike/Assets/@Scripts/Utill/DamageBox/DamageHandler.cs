using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour 
{
    public AttackData attackData;
    public AudioSource audioSource;
    public float interval = 0.1f;
    private readonly HashSet<IDamageable> damagedTargets = new();

    public void CreateAttackBox(AttackData data)
    {
        if (data == null) return;
        damagedTargets.Clear();
        attackData = data; 
        Vector3 hitPos = transform.position + transform.right * data.rangeOffset;
        Collider2D[] hits = Physics2D.OverlapBoxAll(hitPos, data.hitBoxSize, 0, data.targetMask);

        foreach (var hit in hits)
        {
            IDamageable dmg = hit.GetComponent<IDamageable>();
            if (dmg != null)
            {
                damagedTargets.Add(dmg);
                StartCoroutine(HitDamage(dmg, data));
            }
        }
    }
    IEnumerator HitDamage(IDamageable dmg, AttackData data)
    {
        int currentHits = 0;
        while (currentHits < data.hitCount)
        {
            dmg.TakeDamage(data.damageValue);
            audioSource.PlayOneShot(data.SFX);
            Instantiate(data.HitEffect);
            currentHits++;
            yield return new WaitForSeconds(interval);
        }
    }
    
    // 히트박스 확인용 
    private void OnDrawGizmosSelected()
    {
        if (attackData == null) return;

        Gizmos.color = Color.red;
        Vector3 spawnPos = transform.position + transform.right * attackData.rangeOffset;
        Gizmos.DrawWireCube(spawnPos, attackData.hitBoxSize);
    }
}
