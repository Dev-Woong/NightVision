using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class CameraChanger : MonoBehaviour
{
    public GameObject ScopeCam;
    public GameObject RifleCam;
    public GameObject[] mapCam;
    public PolygonCollider2D[] mappol;

    public int a;

    int c = 0;

    public bool portalUseAble = false;
    public bool moveLeft = true;
    public bool moveRight = true;



    private void Start()
    {
        Initialize();
    }



    public void Initialize()
    {
        mapCam = GameObject.FindGameObjectsWithTag("Cam");
        mappol = FindObjectsByType<PolygonCollider2D>(FindObjectsSortMode.InstanceID);
        moveLeft = true;
        moveRight = true;
        mapCam = mapCam.OrderBy(p => p.name).ToArray();
        mappol = mappol.OrderBy(p => p.name).ToArray();
        c = mappol.Length;
        a = 0;
        ScopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[0];
        RifleCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[0];
        StartCoroutine(DelayUpdate());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region 
        //if (collision.CompareTag("Player"))
        //{
        //    if (a > c - 1) { return; }

        //    if (a == 0)
        //    {
        //        ScopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[a+1];
        //        mapCam[a].Priority = 10;
        //        mapCam[a+1].Priority = 30;
        //        a++;
        //        StartCoroutine(DelayUpdate());
        //    }
        //    else if (a == 1)
        //    {
        //        ScopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[a-1];
        //        mapCam[a-1].Priority = 30;
        //        mapCam[a].Priority = 10;
        //        a--;
        //        StartCoroutine(DelayUpdate());
        //    }
        //    else if (a == 2)
        //    {

        //    }

        //}

        //if (a >= 0 && a < c - 1)
        //{
        //    // 다음 맵으로 이동
        //    a++;
        //    UpdateCamera(a - 1, a);

        //}

        //else if (a > 0 && a == c - 1)
        //{
        //    // 이전 맵으로 이동
        //    a--;
        //    UpdateCamera(a + 1, a);
        //}
        #endregion

        if (collision.CompareTag("Portal")&& a <= mapCam.Length )
        {
            if (portalUseAble == true && moveRight == true)
            {
                a++;
                UpdateCamera(a-1, a);
                portalUseAble = false;
                
            }
            else if (portalUseAble == true && moveLeft == true)
            {
                a--;
                UpdateCamera(a+1, a );
                portalUseAble = false;
               
            }
            else return;
        }
        if (collision.CompareTag("RightTrigger") && moveRight == true)
        {
            moveLeft = true;
            moveRight = false;
            portalUseAble = true;
        }
        if (collision.CompareTag("LeftTrigger") && moveLeft == true)
        {
            moveRight = true;
            moveLeft = false;
            portalUseAble = true;
        }
    }

    private void UpdateCamera(int curIndex, int newIndex) // prevIndex : 현재 맵 , newIndex : 다음 맵
    {
        // Confiner 변경
        

        // 카메라 우선순위 조절
        mapCam[curIndex].GetComponent<CinemachineCamera>().Priority = 10;
        mapCam[newIndex].GetComponent<CinemachineCamera>().Priority = 30;
        ScopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[newIndex];
        RifleCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[newIndex];

        // Confiner 캐시 초기화
        StartCoroutine(DelayUpdate());
    }

    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(0.1f);

        ScopeCam.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
        RifleCam.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
}