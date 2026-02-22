using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateLevel : MonoBehaviour
{
    public int pyramidSize;
    public int trees;
    public float treeProximity;
    GameObject celestialObject;

    // Start is called before the first frame update
    void Start()
    {
        CreateGround();
        CreateCelestialObject();
        CreatePyramid();
        CreateForest();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the celestial object
        celestialObject.transform.RotateAround(Vector3.zero, new Vector3(-1,0,5), 20 * Time.deltaTime);

    }

    void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.localScale = new Vector3(4, .1f, 4);
        ground.transform.position = new Vector3(0, -.5f, 0);
        ground.name = "Ground";

        Renderer renderer = ground.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.red);
    }

    void CreateCelestialObject()
    {
        celestialObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        celestialObject.name = "Celestial Object";
        Renderer renderer = celestialObject.GetComponent<Renderer>();

        celestialObject.transform.position = new Vector3(0, 13, 0);
        renderer.material.SetColor("_Color", Color.yellow);

        //add light
        Light dayNight = celestialObject.AddComponent<Light>();
        dayNight.color = Color.yellow;
        dayNight.areaSize = new Vector2(100,100);
        dayNight.range = 50;
    }

    void CreatePyramid()
    {
        //create pyramid
        if (pyramidSize > 3 && pyramidSize < 10)
        {
            GameObject pyramid = new GameObject();
            pyramid.name = "Pyramid";
            GameObject pyramidCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pyramidCube.transform.parent = pyramid.transform;

            float sideSpacing = 1.1f;
            float sideOffset = .5f;

            pyramidCube.GetComponent<Renderer>().material.color = new Color(Random.Range(.8f, 1f), Random.Range(.4f, .6f), 0);
            pyramidCube.name = "Stone";


            for(int yMove = 0; yMove <= pyramidSize; yMove++)
            {
                for (int zMove = 0; zMove < pyramidSize - yMove; zMove++)
                {
                    for (int xMove = 0; xMove < pyramidSize - yMove; xMove++)
                    {
                        GameObject newCube = Instantiate(pyramidCube, new Vector3((yMove * sideOffset) + (sideSpacing * xMove), yMove, (yMove * sideOffset) + (sideSpacing * zMove)), Quaternion.identity);
                        newCube.GetComponent<Renderer>().material.color = new Color(Random.Range(.8f, 1f), Random.Range(.4f, .6f), 0);
                        newCube.transform.parent = pyramid.transform;
                    }
                }
            }
        }

    }

    void CreateForest()
    {
        //create the tree and set properties
        GameObject forest = new GameObject();
        forest.name = "Forest";
        GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        tree.transform.parent = forest.transform;
        tree.GetComponent<Renderer>().material.color = new Color(0, Random.Range(.1f, .9f), 0);
        tree.name = "Tree";
        tree.transform.localScale = new Vector3(.2f, 1.5f, .2f);
        tree.transform.position = new Vector3(-10, 0, 3);


        for (int counter=0;counter < trees; counter++)
        {
            GameObject newTree = Instantiate(tree);
            newTree.transform.localPosition = new Vector3(-10 + Random.Range(treeProximity * -1, treeProximity), 0, 3 + Random.Range(treeProximity * -1, treeProximity));
            newTree.GetComponent<Renderer>().material.color = new Color(0, Random.Range(.1f, .9f), 0);
            newTree.transform.localScale = new Vector3(Random.Range(.5f, 1.5f), Random.Range(.5f, 2.5f), Random.Range(.5f, 1.5f));
            newTree.transform.parent = forest.transform;
        }

    }
}
