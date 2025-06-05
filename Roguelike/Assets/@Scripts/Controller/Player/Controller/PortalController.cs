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
    public ScopeCamController scController;
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Map"))
        {
            nextSceneNum = other.GetComponent<Portal>().pNum.portalNum;
            //scope.transform.position = StartPoint[nextSceneNum].position;
            scController.ChangeMapCollider(nextSceneNum);
            scController.transform.position = StartPoint[nextSceneNum].position;
            StartCoroutine(DelayCamSwitch());
        }
    }
    
    IEnumerator DelayCamSwitch()
    {
        yield return new WaitForSeconds(0.1f);
        scController.ChangeMapCollider(nextSceneNum);
        yield return new WaitForSeconds(0.9f);
        mapCamera[tempSceneNum].Priority = 0;
        mapCamera[nextSceneNum].Priority = 20;
        yield return new WaitForSeconds(0.01f);
        tempSceneNum = nextSceneNum;

    }

    void Update()
    {
       
    }
}
