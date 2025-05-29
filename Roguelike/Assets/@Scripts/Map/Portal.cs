using UnityEngine;
using System.Collections;

public class Portal: MonoBehaviour
{
    Animator animator;
    public PortalNumber pNum;
    public bool canInteract = false;
    public GameObject[] Banner;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        //Banner = GameObject.Find("Map2Banners").transform.Find("Map2Panel").gameObject;
        //Banner.SetActive(false);

        for (int i = 1;  i < Banner.Length; i++)
        {
            Banner[i].SetActive(false);
        }
    }
    private void Update()
    {
        UsePotal();
    }

    public void UsePotal()
    {
        if (canInteract == true && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("isOpen");
            StartCoroutine(MovePosition());
            StartCoroutine(DisEnAni());
        }
    }
    IEnumerator MovePosition()
    {
        yield return new WaitForSeconds(1.5f);
        Banner[pNum.portalNum].SetActive(true);
    }
    IEnumerator DisEnAni()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("isOpened");
        yield return new WaitForSeconds(1f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canInteract = true;
            animator.SetBool("isPlayerOpen", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canInteract = false;
            animator.SetBool("isPlayerOpen", false);
        }
    }
}
