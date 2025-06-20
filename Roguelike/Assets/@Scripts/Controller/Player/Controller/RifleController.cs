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
    public Transform firePoint;
    public Transform emptyCartridgePoint;
    public GameObject emptycartridge;
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
    }
    
    public void RandomFireEffect()
    {
        var fireEffect = Instantiate(ShotEffect);
        fireEffect.transform.position = firePoint.position;
        if (player.transform.localScale.x == 1) { fireEffect.transform.eulerAngles = new Vector3(0, 0, 0); }
        else if (player.transform.localScale.x == -1) { fireEffect.transform.eulerAngles = new Vector3(0, 0, 180); }
        
    }
    public void RandomCartridgeEffect()
    {
        var brokeEffect = Instantiate(emptycartridge,emptyCartridgePoint.position,Quaternion.identity);
        float x = Random.Range(-0.2f, 0.0f);
        float y = Random.Range(0.25f, 0f);
        if (player.transform.localScale.x == 1)
        {
            brokeEffect.GetComponent<Rigidbody2D>().AddForce(new Vector3(x, y, 0) * cartridgeForce, ForceMode2D.Impulse);
        }
        else if (player.transform.localScale.x == -1)
        {

            brokeEffect.GetComponent<Rigidbody2D>().AddForce(new Vector3(-x, y, 0) * cartridgeForce, ForceMode2D.Impulse);
        }
    }
    IEnumerator FireEffect()
    {
        int a = 0;
        rifleCam.Priority = 50;
        while (a < rifleData.hitCount)
        {
            RandomFireEffect();
            float b = Random.Range(0f, 0.2f);
            Vector3 recoilDir = new (-0.1f, b, 0f);
            impulseSource.GenerateImpulse(recoilDir);
            RandomCartridgeEffect();
            a++;
            yield return new WaitForSeconds(0.04f);
        }
        player.ExitRifleMode();
        rifleCam.Priority = 1;
    }
    
}
