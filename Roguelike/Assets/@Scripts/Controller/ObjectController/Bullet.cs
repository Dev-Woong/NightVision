using Unity.Cinemachine;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class Bullet : PoolAble
{
    public int atk;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 bulletDir;
    public LayerMask targetMask;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();    
    }
    public void SetBullet(LayerMask target, Vector3 direction,float setSpeed ,int targetAtk = 0)
    {
        targetMask = target;
        atk = targetAtk;
        bulletDir = direction;
        speed = setSpeed;
        ShootBullet(speed);
    }
    public void ShootBullet(float speed)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(bulletDir.normalized*speed,ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            
        }
        else if (other.gameObject.layer == 7)
        {

        }
    }
}
