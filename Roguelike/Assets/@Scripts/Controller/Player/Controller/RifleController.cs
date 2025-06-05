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
    public GameObject RifleBG;
    public Transform firePoint;
    public Transform emptyCartridgePoint;
    public GameObject emptycartridge;
    Color BaseScopeBGColor = new Color32(0, 0, 0, 244);
    Color FireScopeBGColor = new Color32(80, 78, 56, 255);
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        dHandle = GetComponentInParent<DamageHandler>();
        rifleCam.Priority = 1;
    }
    public void Fire()
    {
        if (FireCoroutine != null)
        {
            StopCoroutine(FireCoroutine);
        }
        float a = Random.Range(-0.3f, 0.3f);
        Vector3 recoilDir = new Vector3(0.8f, a, 0f).normalized;
        impulseSource.GenerateImpulse(recoilDir);
    }
    public void RandomFireEffect()
    {
        var fireEffect = Instantiate(ShotEffect);
        fireEffect.transform.position = firePoint.position;
    }
    public void RandomCartidgeEffect()
    {
        float x = Random.Range(0, 360);
        var brokeEffect = Instantiate(ShotEffect);
        brokeEffect.transform.position = emptyCartridgePoint.position;
    }
    IEnumerator FireEffect()
    {
        //SnipeBG.GetComponent<SpriteRenderer>().color = FireScopeBGColor;
        //RandomFireEffect();
        //RandomBrokeEffect();
        yield return new WaitForSeconds(0.05f);
        //SnipeBG.GetComponent<SpriteRenderer>().color = BaseScopeBGColor;
        yield return new WaitForSeconds(0.03f);

    }
    void Update()
    {
        
    }
}
