using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            StartCoroutine(CoBulletDestroy());
        }
    }
    IEnumerator CoBulletDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
