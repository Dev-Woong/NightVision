using UnityEngine;

public class CarMove : MonoBehaviour
{
    Animator animator;
    public float speed;
    Vector3 dir;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        Move();
        if (transform.position.x <= -12)
        {
            dir = transform.position;
            dir.x = 13f;
            transform.position = dir;
        }
    }

    void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        animator.SetTrigger("isLand");
    }
}
