using UnityEngine;

public class RetryPenal : MonoBehaviour
{

    public GameObject RetryCanvas;

    void Start()
    {
        RetryCanvas.SetActive(false);
    }

    void OnRetry()
    {
        RetryCanvas.SetActive(true);  
    }
    
}
