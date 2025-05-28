using JetBrains.Annotations;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
public class ScopeController: MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    public GameObject BrokenShotEffect;
    [SerializeField]
    PlayerController pc;
    public float scopeSpeed = 5;
    public float curTime;
    public float coolTime = 0.3f;
    public GameObject SnipeBG;
    public GameObject[] FireEffectPref;
    private Coroutine FireCoroutine;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }
    void Update()
    {
        ScopeMove();
        Fire();
        curTime -= Time.deltaTime;
    }
    public void ScopeMove()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            float h = Input.GetAxisRaw(Define.Horizontal);
            float v = Input.GetAxisRaw("Vertical");
            if (pc.gameObject.transform.localScale.x == -1)
            {
                h = -h;
            }
            Vector3 scopeMoveDir = new Vector3(h, v, 0);
            transform.Translate(scopeSpeed * Time.deltaTime * scopeMoveDir);
        }
    }
    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && pc.magazineDrum > 0&&curTime <=0)
        {
            if (FireCoroutine != null)
            {
                StopCoroutine(FireCoroutine);
            }
            float a = Random.Range(-0.3f, 0.3f);
            Vector3 recoilDir = new Vector3(a, 1f, 0f).normalized;
            impulseSource.GenerateImpulse(recoilDir);
            FireCoroutine = StartCoroutine(FireEffect());
            pc.magazineDrum--;
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

        
        if (pc.magazineDrum <= 0)
        {
            pc.EnterSnipeMode();
        }
    }
}


