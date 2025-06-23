using Unity.VisualScripting;
using UnityEngine;

public class City : MonoBehaviour
{
    
    public Transform bd;
    public Transform bd1;
    public Transform bd2;

    float h;
    public float speed;

    void Start()
    { 
        speed = 0.05f;      
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
               
                bd.transform.Translate(Time.deltaTime * (speed * 1.2f) * moveDir);
                bd1.transform.Translate(Time.deltaTime * (speed * 1.4f) * moveDir);
                bd2.transform.Translate(Time.deltaTime * (speed * 1.6f) * moveDir);
            }
            else
            {
                bd.transform.Translate(Time.deltaTime * speed  * moveDir);
                bd1.transform.Translate(Time.deltaTime * (speed / 1.5f) * moveDir);
                bd2.transform.Translate(Time.deltaTime * (speed / 2f) * moveDir);
            }
        }
    }
}
