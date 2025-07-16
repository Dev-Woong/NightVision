using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScopeCamController : MonoBehaviour
{
    [Header("�� ������� �ݶ��̴� ����")]
    //public PolygonCollider2D[] mapCollider;
    public PlayerController pc;
    private void Awake()
    {
        pc = GetComponentInParent<PlayerController>();
        gameObject.AddComponent<CinemachineConfiner2D>();
        //gameObject.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mapCollider[0];
    }
    private void Start()
    {
        gameObject.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
    public void CamPositionSet()
    {
        transform.position = pc.transform.position;
    }
}
