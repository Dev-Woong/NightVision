using JetBrains.Annotations;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [Header("맵마다 시작포인트 지정")]
    public Transform[] StartPoint;
    [Header("맵마다 시네머신 부착")]
    public CinemachineCamera[] mapCamera;
    public ScopeController scope;

    public BoxCollider2D[] Walls;
    public ScopeCamController scController;
    public bool canPortalInteract = false;
    public int curSceneNum = 0;
    public int tempSceneNum = 0;
    public int nextSceneNum = 0;
    void Start()
    {
        StartCamSetting();
        transform.position = StartPoint[0].position;
    }
    public void StartCamSetting()
    {
        for (int i = 1; i < mapCamera.Length; i++)
        {
            mapCamera[i].Priority = 1;
        }
    }
    public void UsePotal()
    {
        if (Input.GetKeyDown(KeyCode.F) && canPortalInteract == true)
        {
            for (int i = 0; i < Walls.Length - 1; i++)
            {
                Walls[i].enabled = false;
            }
            transform.position = StartPoint[nextSceneNum].position;
            scope.transform.position = StartPoint[nextSceneNum].position;
            scController.ChangeMapCollider(nextSceneNum);
            scController.transform.position = StartPoint[nextSceneNum].position;
            StartCoroutine(DelayCamSwitch());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            nextSceneNum = other.GetComponent<Portal>().pNum.portalNum;
            tempSceneNum = nextSceneNum;
            canPortalInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            nextSceneNum = curSceneNum;
            canPortalInteract = false;
        }
    }
    IEnumerator DelayCamSwitch()
    {
        yield return new WaitForSeconds(0.1f);
        nextSceneNum = tempSceneNum;
        
        scController.ChangeMapCollider(nextSceneNum);
        yield return new WaitForSeconds(0.9f);
        mapCamera[curSceneNum].Priority = 0;
        mapCamera[nextSceneNum].Priority = 20; 
        for (int i = 0; i < Walls.Length - 1; i++)
        {
            Walls[i].enabled = true;
        }
        yield return new WaitForSeconds(0.01f);
        curSceneNum = nextSceneNum;
        
    }

    void Update()
    {
        UsePotal();
    }
}
