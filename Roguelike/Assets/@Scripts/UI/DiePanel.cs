using UnityEditor.Build.Content;
using UnityEngine;

public class DiePanel : MonoBehaviour
{
    public GameObject DieImage;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void DieAnim()
    {
        anim.SetTrigger("Die");
    }
    public void DieAnimEvent()
    {
        LoadingSceneManager.LoadScene("Home", null, true);
        DieImage.SetActive(false);
    }
}
