using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using System.Linq;

public class CameraChanger : MonoBehaviour
{
    public GameObject ScopeCam;
    public CinemachineCamera[] mapCam;
    public PolygonCollider2D[] mappol;
    
    public int a = 0;
    int c = 0;

    void Start()
    {
        ScopeCam = GameObject.Find("Player").transform.Find("Scope_Cam").gameObject;
        // Todo : 배열로된 오브젝트 찾는 방법 작성해야함
        c = mappol.Length;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (a > c - 1) { return; }

            if (a == 0)
            {
                ScopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[a+1];
                mapCam[a].Priority = 10;
                mapCam[a+1].Priority = 30;
                a++;
                StartCoroutine(DelayUpdate());
            }
            else if (a == 1)
            {
                ScopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[a-1];
                mapCam[a-1].Priority = 30;
                mapCam[a].Priority = 10;
                a--;
                StartCoroutine(DelayUpdate());
            }
        }
    }
    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        ScopeCam.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
}