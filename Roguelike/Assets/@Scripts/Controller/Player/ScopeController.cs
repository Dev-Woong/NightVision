using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
public class ScopeController: MonoBehaviour
{
    
    public GameObject BrokenShotEffect;
    [SerializeField]
    PlayerController pc;
    public float scopeSpeed = 5;
    public GameObject SnipeBG;
    private Coroutine FireCoroutine;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        BrokenShotEffect.SetActive(false);
    }
    void Update()
    {
        ScopeMove();
        Fire();
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && pc.magazineDrum > 0)
        {
            if (FireCoroutine != null)
            {
                StopCoroutine(FireCoroutine);
            }

            FireCoroutine = StartCoroutine(FireEffect());
            pc.magazineDrum--;
        }
    }
    IEnumerator FireEffect()
    {
        SnipeBG.GetComponent<SpriteRenderer>().color = new Color32(80, 78, 56, 255);
        BrokenShotEffect.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        SnipeBG.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 244);

        yield return new WaitForSeconds(0.1f);

        BrokenShotEffect.SetActive(false);
        if (pc.magazineDrum <= 0)
        {
            pc.EnterSnipeMode();
        }
    }
}


