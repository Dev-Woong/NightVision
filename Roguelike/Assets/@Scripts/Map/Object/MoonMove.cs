using UnityEngine;

public class MoonMove : MonoBehaviour
{
    
    public float speed;
    Vector3 dir;

    void Update()
    {
        Move();
        if (transform.position.x >= 24)
        {
            dir = transform.position;
            dir.x = -23f;
            transform.position = dir;
        }
    }

    void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
