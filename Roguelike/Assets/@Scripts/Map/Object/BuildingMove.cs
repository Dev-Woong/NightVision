using UnityEngine;

public class BuildingMove : MonoBehaviour
{
    public GameObject buildings;
    public GameObject buildings1;
    public GameObject buildings2;

    public GameObject shadow;

    public GameObject signs;
    public GameObject floor;
    

    Vector2 buildPos;
    Vector2 shadowPos;
    Vector2 signsPos;
    Vector2 floorPos;

    public float speed;

    void Update()
    {
        if (buildings.transform.position.x <= -112)
        {
            buildPos = buildings.transform.position;
            buildPos.x = 112f;
            transform.position = buildPos;
        }
        
        if (buildings1.transform.position.x <= -125)
        {
            buildPos = buildings1.transform.position;
            buildPos.x = 125;
            buildings1.transform.position = buildPos;
        }

        if (buildings2.transform.position.x <= -131)
        {
            buildPos = buildings2.transform.position;
            buildPos.x = 131;
            buildings2.transform.position = buildPos;
        }

        if(shadow.transform.position.x <= -50)
        {
            shadowPos = shadow.transform.position;
            shadowPos.x = 50;
            shadow.transform.position = shadowPos;
        }

        if(signs.transform.position.x <= -110)
        {
            signsPos = signs.transform.position;
            signsPos.x = 110;
            signs.transform.position = signsPos;
        }

        if(floor.transform.position.x <= -101.1f)
        {
            floorPos = floor.transform.position;
            floorPos.x = 114.3f;
            floor.transform.position = floorPos;
        }
        

        MoveBuildings();  
    }

    void MoveBuildings()
    {
        buildings.transform.Translate(Vector2.left * speed * Time.deltaTime);
        buildings1.transform.Translate(Vector2.left * (speed/4) * Time.deltaTime);
        buildings2.transform.Translate(Vector2.left * (speed/6) * Time.deltaTime);
        shadow.transform.Translate(Vector2.left * (speed * 2f) * Time.deltaTime);
        signs.transform.Translate(Vector2.left * (speed * 1.5f) * Time.deltaTime);
        floor.transform.Translate(Vector2.left * speed * Time.deltaTime);
        

    }
}
