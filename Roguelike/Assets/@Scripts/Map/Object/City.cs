using UnityEngine;

public class City : MonoBehaviour
{
   
    public GameObject bd;

    float h;
    public float speed;
    void Start()
    {
        speed = 2;        
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetButton(Define.Horizontal) /* && (isAttacking == false || weaponType==WeaponType.Gun)*/)
        {
            h = Input.GetAxisRaw(Define.Horizontal);
            Vector2 moveDir = new Vector2(h, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {

                bd.transform.Translate(Time.deltaTime * speed * 2 * moveDir);
            }
            else
            {

                bd.transform.Translate(Time.deltaTime * speed * moveDir);
            }
        }
    }
}
