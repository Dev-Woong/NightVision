
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
public class ScopeController: MonoBehaviour
{
    protected PlayerController player;
    protected PortalController portal;
    private Coroutine FireCoroutine;
    public CinemachineImpulseSource impulseSource;
    public CinemachineCamera scopeCam;
    public PolygonCollider2D[] mapCollider;
    public GameObject BrokenShotEffect;
    public GameObject[] FireEffectPref;
    public GameObject SnipeBG;
    public float scopeMoveSpeed = 5;
    public float coolTime = 0.3f;
    private float curTime;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        portal = GetComponentInParent<PortalController>();
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
    public void ChangeMapCollider(int SceneNum)
    {
        scopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mapCollider[SceneNum];
        scopeCam.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
    public void ExitScopeCam()
    {
        if (player.snipeMode == false)
        {
            scopeCam.Priority = 1;
            player.magazineDrum = 5;
        }
    }
    public void EnterScopeCam()
    {
        if (player.snipeMode == true)
        {
            scopeCam.Priority = 30;
        }
    }
    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && player.magazineDrum > 0&&curTime <=0)
        {
            if (FireCoroutine != null)
            {
                StopCoroutine(FireCoroutine);
            }
            float a = Random.Range(-0.3f, 0.3f);
            Vector3 recoilDir = new Vector3(a, 1f, 0f).normalized;
            impulseSource.GenerateImpulse(recoilDir);
            FireCoroutine = StartCoroutine(FireEffect());
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
        SnipeBG.GetComponent<SpriteRenderer>().color = new Color32(80, 78, 56, 255);
        RandomFireEffect();
        RandomBrokeEffect();
        yield return new WaitForSeconds(0.05f);
        SnipeBG.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 244);
        yield return new WaitForSeconds(0.03f);
        if (player.magazineDrum <= 0)
        {
            player.EnterSnipeMode();
        }
    }
}


