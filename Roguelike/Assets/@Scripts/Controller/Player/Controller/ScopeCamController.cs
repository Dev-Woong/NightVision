using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScopeCamController : MonoBehaviour
{
    public PlayerController pc;
    private void Awake()
    {
        pc = GetComponentInParent<PlayerController>();
        gameObject.AddComponent<CinemachineConfiner2D>();
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
