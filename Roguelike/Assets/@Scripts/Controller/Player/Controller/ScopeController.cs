
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
public class ScopeController: MonoBehaviour
{
    protected PlayerController player;
    protected DamageHandler dHandle;
    private Coroutine FireCoroutine;
    

    public CinemachineImpulseSource impulseSource;
    public CinemachineCamera scopeCam;

    public AttackData snipeData;
    public GameObject BrokenShotEffect;
    public GameObject[] FireEffectPref;
    public GameObject SnipeBG;
    public float scopeMoveSpeed = 5;
    public float coolTime = 0.3f;
    private float curTime;
    WaitForSeconds wTime = new(0.1f);
    Color BaseScopeBGColor = new Color32(0, 0, 0, 244);
    Color FireScopeBGColor = new Color32(80, 78, 56, 255);
    
    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        dHandle = GetComponentInParent<DamageHandler>();    
        scopeCam.Priority = 1;
    }
    void Update()
    {
        ScopeMove();
        ExitScopeCam();
        EnterScopeCam();
        Fire();
        curTime -= Time.deltaTime;
    }
    public void ScopeMove()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            float h = Input.GetAxisRaw(Define.Horizontal);
            float v = Input.GetAxisRaw("Vertical");
            if (player.gameObject.transform.localScale.x == -1)
            {
                h = -h;
            }
            Vector3 scopeMoveDir = new Vector3(h, v, 0);
            transform.Translate(scopeMoveSpeed * Time.deltaTime * scopeMoveDir);
        }
    }
    

    public void ExitScopeCam()
    {
        if (player.snipeMode == false)
        {
            scopeCam.Priority = 1;
            player.magazineDrum = 5;
            SnipeBG.GetComponent<SpriteRenderer>().color = BaseScopeBGColor;
        }
    }
    public void EnterScopeCam()
    {
        if (player.snipeMode == true)
        {
            scopeCam.GetComponent<ScopeCamController>().CamPositionSet();
            scopeCam.Priority = 30;
        }
    }
    
    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.S) && player.magazineDrum > 0&&curTime <=0)
        {
            if (FireCoroutine != null)
            {
                StopCoroutine(FireCoroutine);
            }
            float a = Random.Range(-0.3f, 0.3f);
            Vector3 recoilDir = new Vector3(a, 1f, 0f).normalized;
            impulseSource.GenerateImpulse(recoilDir);
            FireCoroutine = StartCoroutine(FireEffect());
            dHandle.PlayerCreateAttackBox(snipeData);
            player.magazineDrum--;
            curTime = coolTime;
        }
    }
    public void RandomFireEffect()
    {
        int a;
        a = Random.Range(0, 4);
        int b = Random.Range(0, 360);
        var fireEffect = Instantiate(FireEffectPref[a]);
        fireEffect.transform.position = transform.position;
        fireEffect.transform.eulerAngles = new Vector3(0, 0, b);
        
    }
    public void RandomBrokeEffect()
    {
        int a = Random.Range(0, 360);
        var brokeEffect = Instantiate(BrokenShotEffect);
        brokeEffect.transform.position = transform.position;
        brokeEffect.transform.eulerAngles = new Vector3(0, 0, a);
    }
    IEnumerator FireEffect()
    {
        SnipeBG.GetComponent<SpriteRenderer>().color = FireScopeBGColor;
        RandomFireEffect();
        RandomBrokeEffect();
        yield return new WaitForSeconds(0.1f);
        SnipeBG.GetComponent<SpriteRenderer>().color = BaseScopeBGColor;
        yield return new WaitForSeconds(0.03f);
        if (player.magazineDrum <= 0)
        {
            player.ExitSnipeMode();
            ExitScopeCam();
        }
    }
}


