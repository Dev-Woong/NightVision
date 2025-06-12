using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject[] gameObjects;
    
    void Start()
    {   
        foreach (GameObject Gb in gameObjects)
        {
            Gb.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(false);
            }
        }
    }
}
