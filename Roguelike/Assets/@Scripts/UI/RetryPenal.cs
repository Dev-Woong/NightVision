using UnityEngine;

public class RetryPenal : MonoBehaviour
{

    public GameObject Rp;

    void Start()
    {
        Rp.SetActive(false);
    }

    void OnRetry()
    {
        Rp.SetActive(true);  
    }
    
}
