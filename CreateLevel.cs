using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{

    public int pyramidSize;
    GameObject celestialObject;

    // Start is called before the first frame update
    void Start()
    {
        CreateGround();
        CreateCelestialObject();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the celestial object
        celestialObject.transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);

    }

    void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.localScale = new Vector3(2, 1, 2);
        ground.name = "Ground";

        Renderer renderer = ground.GetComponent<Renderer>();
        renderer.material.SetColor("_Color",Color.red);
    }

    void CreateCelestialObject()
    {
        celestialObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        celestialObject.name = "Celestial Object";
        Renderer renderer = celestialObject.GetComponent<Renderer>();

        celestialObject.transform.position = new Vector3(1,1,0);
        renderer.material.SetColor("_Color",Color.yellow);

    }

    void CreatePyramid()
    {
        int stones = 0;

        if(pyramidSize > 3 || pyramidSize < 10)
        {
            //Create pyramid gameobject array and get the size requirement
            for(int counter = pyramidSize; counter > 0; counter--)
            {
                stones += counter * counter;
            }
            GameObject[] pyramid = new GameObject[stones];

            //Create Cubes
            for (int counter = pyramidSize; counter > 0; counter--)
            {
                
            }
        }

    }

    void CreateForest()
    {

    }
}
