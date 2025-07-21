using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed;
    Vector3 dir;
    void Update()
    {
        Move();
        
    }

    void Move()
    {
        if (transform.position.x <= 6.8f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (transform.position.x >= 10)
            {
                dir = transform.position;
                dir.x = -10f;
                transform.position = dir;
            }
        }
    }
}
