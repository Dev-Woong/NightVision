using UnityEngine;

public class BuildingMove1 : MonoBehaviour
{
    public GameObject buildings;
    public GameObject buildings1;
    public GameObject buildings2;

    public GameObject shadow;

    public GameObject signs;
    public GameObject floor;
    public GameObject floor1;

    Vector2 buildPos;
    Vector2 shadowPos;
    Vector2 signsPos;
    Vector2 floorPos;
    Vector2 floor1Pos;


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
            floorPos.x = 113.5f;
            floor.transform.position = floorPos;
        }

        if (floor1.transform.position.x <= -101.1f)
        {
            floor1Pos = floor1.transform.position;
            floor1Pos.x = 107f;
            floor1.transform.position = floorPos;
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
        floor1.transform.Translate(Vector2.left * speed * Time.deltaTime);

    }
}
