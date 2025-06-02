using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScopeCamController : MonoBehaviour
{
    [Header("�� ������� �ݶ��̴� ����")]
    public PolygonCollider2D[] mapCollider;
    public PlayerController pc;
    private void Start()
    {
        pc= GetComponentInParent<PlayerController>();   
    }
    public void ChangeMapCollider(int SceneNum)
    {
        gameObject.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mapCollider[SceneNum];
        StartCoroutine(DelayUpdate());
    }
    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
    public void CamPositionSet()
    {
        transform.position = pc.transform.position;
        //gameObject.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
    
}
