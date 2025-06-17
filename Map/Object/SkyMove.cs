using UnityEditor;
using UnityEngine;

public class SkyMove : MonoBehaviour
{
    MeshRenderer render;

    public float speed;
    public float offset;

    void Start()
    {
       render = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        offset = offset + Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
