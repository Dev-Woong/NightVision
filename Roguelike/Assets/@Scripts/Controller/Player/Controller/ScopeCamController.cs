using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScopeCamController : MonoBehaviour
{
    [Header("맵 순서대로 콜라이더 부착")]
    public PolygonCollider2D[] mapCollider;
    public PlayerController pc;
    private void Awake()
    {
        pc = GetComponentInParent<PlayerController>();
        gameObject.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mapCollider[0];
    }
    private void Start()
    {
        gameObject.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
    public void ChangeMapCollider(int SceneNum)
    {
        gameObject.GetComponent<CinemachineConfiner2D>().BoundingShape2D = mapCollider[SceneNum];
        StartCoroutine(DelayUpdate());
    }
    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
    }
    public void CamPositionSet()
    {
        transform.position = pc.transform.position;
    }
}
