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
    public Transform emptycartridgePoint;
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
    }
    public void RandomFireEffect()
    {
        var fireEffect = Instantiate(ShotEffect);
        fireEffect.transform.position = transform.position;
        fireEffect.transform.eulerAngles = new Vector3(0, 0, 0);

    }
    public void RandomCartidgeEffect()
    {
        int a = Random.Range(0, 360);
        var brokeEffect = Instantiate(ShotEffect);
        brokeEffect.transform.position = transform.position;
        brokeEffect.transform.eulerAngles = new Vector3(0, 0, a);
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
