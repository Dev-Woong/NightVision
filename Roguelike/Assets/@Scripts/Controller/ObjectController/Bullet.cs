using Unity.Cinemachine;
using UnityEngine;

public class Bullet : PoolAble
{
    public float atk;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 bulletDir;
    public LayerMask targetMask;
    void Awake()
    {
        rb= GetComponent<Rigidbody2D>();    
    }
    public void SetBullet(LayerMask target, Vector3 direction,float setSpeed ,float targetAtk = 0)
    {
        targetMask = target;
        atk = targetAtk;
        bulletDir = direction;
        speed = setSpeed;
        if (setSpeed > 0)
        {
            ShootBullet(speed);
        }
        else 
        {
            return;
        }
    }
    public void ShootBullet(float speed)
    {
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = bulletDir*speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            if (targetMask == 7)
            {
                //other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }
}
