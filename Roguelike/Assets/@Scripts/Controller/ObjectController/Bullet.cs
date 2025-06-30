using Unity.Cinemachine;
using Unity.Properties;
using UnityEditor.U2D.Sprites;
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
            Debug.Log($"발사 성공 ! {targetMask},{bulletDir},{setSpeed},{speed}");
            ShootBullet(speed);
        }
        else 
        {
            Debug.Log($"속도 조절 실패 !{setSpeed},{speed} ");
        }
    }
    public void ShootBullet(float speed)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(bulletDir*speed,ForceMode2D.Impulse);
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
