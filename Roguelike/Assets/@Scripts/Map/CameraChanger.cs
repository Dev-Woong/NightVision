using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class CameraChanger : MonoBehaviour
{
    public GameObject ScopeCam;
    public GameObject[] mapCam;
    public PolygonCollider2D[] mappol;

    public int a ;

    int c = 0;

    public bool portalUseAble = false;
    public bool moveLeft = true;
    public bool moveRight = true;

    void Awake()
    {
        mapCam = GameObject.FindGameObjectsWithTag("Cam");//(FindObjectsSortMode.InstanceID);
        mappol = FindObjectsByType<PolygonCollider2D>(FindObjectsSortMode.InstanceID);
    }

    void Start()
    {
        Initialize();
         moveLeft = true;
        moveRight = true;
    }

    public void Initialize()
    {
        // Scopecam 오브젝트 자동으로 찾기
        ScopeCam = GameObject.Find("Player").transform.Find("Scope_Cam").gameObject;

        // 맵 카메라 배열 초기화
        GameObject[] camObjs = GameObject.FindGameObjectsWithTag("Cam");


        mapCam = mapCam.OrderBy(p => p.name).ToArray();

        //mapCam = new CinemachineCamera[camObjs.Length];

        //for (int i = 0; i < camObjs.Length; i++)
        //{
        //    mapCam[i] = camObjs[i].GetComponent<CinemachineCamera>();
        //}

        // 폴리곤 배열 초기화

        mappol = mappol.OrderBy(p => p.name).ToArray();
        c = mappol.Length;

        a = 0;
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

        if (collision.CompareTag("Portal"))
        {
            if (portalUseAble == true && moveRight == true)
            {
                UpdateCamera(a, a + 1);
                portalUseAble = false;
                a++;
            }
            else if (portalUseAble == true && moveLeft == true)
            {
                UpdateCamera(a, a - 1);
                portalUseAble = false;
                a--;
            }
            else return;
        }
        if (collision.CompareTag("RightTrigger")&&moveRight ==true)
        {
            moveLeft = true;
            moveRight = false;
            portalUseAble = true;
        }
        if (collision.CompareTag("LeftTrigger")&&moveLeft==true)
        {
            moveRight = true;
            moveLeft = false;
            portalUseAble = true;
        }
    }

    private void UpdateCamera(int curIndex, int newIndex) // prevIndex : 현재 맵 , newIndex : 다음 맵
    {
        // Confiner 변경
        ScopeCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mappol[newIndex];

        // 카메라 우선순위 조절
        mapCam[curIndex].GetComponent<CinemachineCamera>().Priority = 10;
        mapCam[newIndex].GetComponent<CinemachineCamera>().Priority = 30;

        // Confiner 캐시 초기화
        StartCoroutine(DelayUpdate());
    }

    IEnumerator DelayUpdate() 
    {
        yield return new WaitForSeconds(0.1f);

        ScopeCam.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
}