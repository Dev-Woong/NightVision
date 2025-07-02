using UnityEngine;

public class DiePanel : MonoBehaviour
{
    public GameObject DiePenal;


    void Start()
    {
        DiePenal.SetActive(false);
    }

    
    void EnDie()
    {
        DiePenal.SetActive(true);
    }
}
