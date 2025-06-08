using System.Collections;
using UnityEngine;

public class DelayEffectDestroy : MonoBehaviour
{
    private readonly WaitForSeconds wTime = new (0.5f);
    IEnumerator CoEffectDestroy()
    {
        yield return wTime;
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CoEffectDestroy());
    }
}
