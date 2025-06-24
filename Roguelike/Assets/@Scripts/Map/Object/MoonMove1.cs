using UnityEngine;

public class MoonMove1 : MonoBehaviour
{
    
    public float speed;
    Vector3 dir;

    void Update()
    {
        Move();
        if (transform.position.x >= 55)
        {
            dir = transform.position;
            dir.x = -12f;
            transform.position = dir;
        }
    }

    void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
