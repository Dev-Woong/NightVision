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
        if (transform.position.x >= 20)
        {
            dir = transform.position;
            dir.x = -23f;
            transform.position = dir;
        }
    }

    void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        animator.SetTrigger("isLand");
    }
}
