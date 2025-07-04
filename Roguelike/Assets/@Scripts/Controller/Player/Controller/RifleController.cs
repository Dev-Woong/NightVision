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
    public GameObject ShotEffect;
    public GameObject ParticleCatridge;
    public float cartridgeForce = 5;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        dHandle = GetComponentInParent<DamageHandler>();

        ParticleCatridge.GetComponent<ParticleSystem>().Stop();
        ShotEffect.GetComponent<ParticleSystem>().Stop();   
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
    }
    
    public void RandomFireEffect()
    {
        ShotEffect.SetActive(true);
        if (player.transform.localScale.x == 1)
        {
            ShotEffect.transform.eulerAngles = new Vector3(0, 0, 0);
            ShotEffect.GetComponent<ParticleSystem>().Play();
        }
        else if (player.transform.localScale.x == -1)
        {
            ShotEffect.transform.eulerAngles= new Vector3(0, 0, 180);
            ShotEffect.GetComponent<ParticleSystem>().Play();
        }
    }
    
    public void EmptyCartridgeEffect()
    {
        ParticleCatridge.SetActive(true);
        if (player.transform.localScale.x == 1)
        {
            ParticleCatridge.transform.localScale = new Vector3(4.32f, 0.4f, 2);
            ParticleCatridge.GetComponent<ParticleSystem>().Play();
        }
        else if (player.transform.localScale.x == -1)
        {
            ParticleCatridge.transform.localScale = new Vector3(-4.32f, 0.4f, 2);
            ParticleCatridge.GetComponent<ParticleSystem>().Play();
        }
    }
    IEnumerator FireEffect()
    {
        int a = 0;
        rifleCam.Priority = 50;
        RandomFireEffect();
        EmptyCartridgeEffect();
        while (a < rifleData.hitCount)
        {
            float b = Random.Range(0f, 0.2f);
            Vector3 recoilDir = new (-0.1f, b, 0f);
            impulseSource.GenerateImpulse(recoilDir);
            a++;
            yield return new WaitForSeconds(0.04f);
        }
        ParticleCatridge.GetComponent<ParticleSystem>().Stop();
        ShotEffect.GetComponent<ParticleSystem>().Stop();
        player.ExitRifleMode();
        rifleCam.Priority = 1;
        
    }
    
}
