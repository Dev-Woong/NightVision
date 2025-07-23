using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Player_HandEffect : MonoBehaviour
{
    /// <summary>
    /// HandAttackEffect Animation Event
    /// </summary>
    public GameObject JabEffect;
    public GameObject HookEffect;
    public GameObject UppercutEffect;
    public GameObject[] KickEffect;
    public GameObject Combo3FinishAtkEffect;
    public GameObject Combo3FinishAtkEffect2;

    public Transform jabPoint;
    public Transform hookPoint;
    public Transform uppercutPoint;
    public Transform kick3Point;
    public Transform Combo3FinishAtkTr;
    public CinemachineImpulseSource impulseSource;
    public void Jab()
    {
        Instantiate(JabEffect,jabPoint);
    }
    public void Hook()
    {
        Instantiate(HookEffect, hookPoint);
    }
    public void Uppercut()
    {
        Instantiate (UppercutEffect, uppercutPoint);
    }
    public void CircleKick()
    {
        Instantiate(KickEffect[0], kick3Point);
    }
    public void Combo3FinishAttack()
    {
        var FinishAtk = Instantiate(Combo3FinishAtkEffect2);
        FinishAtk.transform.position = Combo3FinishAtkTr.position;
        for (int i = 0; i < 2; i++)
        {
            float a = Random.Range(-2, 2);
            Vector3 recoilDir = new Vector3(a, 2f, 0f).normalized;
            impulseSource.GenerateImpulse(recoilDir);
        }
        StartCoroutine(DelayFinishAtkEffect());
        
    }
    IEnumerator DelayFinishAtkEffect()
    {
        yield return new WaitForSeconds(0.05f);
        var FinishAtk2 = Instantiate(Combo3FinishAtkEffect);
        FinishAtk2.transform.position = Combo3FinishAtkTr.position;
    }
}
