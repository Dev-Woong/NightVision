using UnityEngine;

public class BusMove : MonoBehaviour
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
        if (transform.position.x <= -23)
        {
            dir = transform.position;
            dir.x = 20f;
            transform.position = dir;
        }
    }

    void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        animator.SetTrigger("isLand");
    }
}
