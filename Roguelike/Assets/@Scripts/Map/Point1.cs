using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class Point1 : MonoBehaviour
{
    public GameObject player;
    public GameObject nearObject;
    public GameObject map2CCam;

    public GameObject Banner2;

    void Start()
    {
        nearObject = null;

        player = GameObject.Find("Player");

        map2CCam = GameObject.Find("Camera2").transform.Find("Map2Ccam").gameObject;
        map2CCam.SetActive(false);

        Banner2 = GameObject.Find("Map2Banners").transform.Find("Map2Panel").gameObject;
        Banner2.SetActive(false);
     
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            nearObject = collision.gameObject;

            player.transform.position = new Vector3(25.5f, -4.47f, 0);

            map2CCam.SetActive(true);
            map2CCam.GetComponent<CinemachineCamera>().Target.TrackingTarget = player.transform;
            map2CCam.GetComponent<CinemachineCamera>().Priority = 1;

            Banner2.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            nearObject = null;

        }
    }
}
