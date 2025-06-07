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
    Color BaseBGColor = new Color32(0, 0, 0, 244);
    Color FireBGColor = new Color32(80, 78, 56, 255);
    public float cartridgeForce = 10;
    int magazineDrum = 0;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        dHandle = GetComponentInParent<DamageHandler>();
        rifleCam.Priority = 1;
    }
    public void Fire()
    {
        magazineDrum = rifleData.hitCount;
        if (FireCoroutine != null)
        {
            StopCoroutine(FireCoroutine);
        }
        float a = Random.Range(-0.3f, 0.3f);
        Vector3 recoilDir = new Vector3(0.8f, a, 0f).normalized;
        impulseSource.GenerateImpulse(recoilDir);
        FireCoroutine = StartCoroutine(FireEffect());
        dHandle.CreateAttackBox(rifleData);
       
        if (magazineDrum < 0)
        {
            //라이플캠 종료 및 라이플상태 종료
            player.ExitRifleMode();
            ExitRifleCam();
        }

    }
    public void ExitRifleCam()
    {
        if (player.snipeMode == false)
        {
            rifleCam.Priority = 1;
            RifleBG.GetComponent<SpriteRenderer>().color = BaseBGColor;
        }
    }
    public void RandomFireEffect()
    {
        var fireEffect = Instantiate(ShotEffect);
        fireEffect.transform.position = firePoint.position;
    }
    public void RandomCartridgeEffect()
    {
        var brokeEffect = Instantiate(ShotEffect);
        brokeEffect.transform.position = emptyCartridgePoint.position;
        float x = Random.Range(-0.8f, -0.3f);
        float y = Random.Range(0.2f, -1f);
        brokeEffect.GetComponent<Rigidbody2D>().AddForce(new Vector3(x, y, 0)*cartridgeForce,ForceMode2D.Impulse);
    }
    IEnumerator FireEffect()
    {
        int a = 0;
        while (a < rifleData.hitCount)
        {
            RifleBG.GetComponent<SpriteRenderer>().color = FireBGColor;
            RandomFireEffect();
            RandomCartridgeEffect();
            yield return new WaitForSeconds(0.01f);
            RifleBG.GetComponent<SpriteRenderer>().color = BaseBGColor;
            yield return new WaitForSeconds(0.02f);
            a++;
            magazineDrum--;
        }

    }
    
}
