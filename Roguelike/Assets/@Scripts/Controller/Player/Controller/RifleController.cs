using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    private Coroutine FireCoroutine;
    protected PlayerController player;
    protected DamageHandler dHandle;
    public CinemachineImpulseSource impulseSource;
    public CinemachineCamera rifleCam;
    public AttackData rifleData;

    public GameObject CatridgeParticle;
    public GameObject ShotParticle;
    public float cartridgeForce = 5;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        dHandle = GetComponentInParent<DamageHandler>();
        rifleCam.Priority = 1;
    }
    void Start()
    {
       
        this.gameObject.SetActive(false);
    }
    public void Fire()
    {
        if (FireCoroutine != null)
        {
            StopCoroutine(FireCoroutine);
        }
        FireCoroutine = StartCoroutine(FireEffect());
        dHandle.PlayerCreateAttackBox(rifleData);
        RandomFireEffect();
        EmptyCartridgeEffect();
    }
    public void RandomFireEffect()
    {
        if (player.transform.localScale.x == 1)
        {
            ShotParticle.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (player.transform.localScale.x == -1)
        {
            ShotParticle.transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }

    public void EmptyCartridgeEffect()
    {
        if (player.transform.localScale.x == 1)
        {
            CatridgeParticle.transform.localScale = new Vector3(4.32f, 0.4f, 2);
        }
        else if (player.transform.localScale.x == -1)
        {
            CatridgeParticle.transform.localScale = new Vector3(-4.32f, 0.4f, 2);
        }
    }

    IEnumerator FireEffect()
    {
        int a = 0;
        rifleCam.Priority = 50;
        yield return null;
        while (a < rifleData.hitCount)
        {
            float b = Random.Range(0f, 0.2f);
            Vector3 recoilDir = new (-0.1f, b, 0f);
            impulseSource.GenerateImpulse(recoilDir);
            a++;
            yield return new WaitForSeconds(0.04f);
        }
        player.ExitRifleMode();
        rifleCam.Priority = 1;
    }
    
}
