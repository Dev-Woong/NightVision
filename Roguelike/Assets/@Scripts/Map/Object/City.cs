using Unity.VisualScripting;
using UnityEngine;

public class City : MonoBehaviour
{
    
    public Transform[] bd;
    



    float h;
    public float speed;



    void Start()
    { 
        speed = 0.03f;      
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetButton(Define.Horizontal))
        {
            h = Input.GetAxisRaw(Define.Horizontal);
            Vector2 moveDir = new Vector2(-h, 0);
            
            if (Input.GetKey(KeyCode.LeftShift))
            {

                for (int i = 0; i < bd.Length ; i++) 
                {
                    float a = 0.8f * (i + 1);
                    bd[i].transform.Translate(Time.deltaTime * speed * a * moveDir);
                }
            }
            else
            {
                for (int i = 0; i < bd.Length; i++)
                {
                    float a =  0.4f * (i + 1);
                    bd[i].transform.Translate(Time.deltaTime * speed * a * moveDir);
                }
            }
        }
    }
}
