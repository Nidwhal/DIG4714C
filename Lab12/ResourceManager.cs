using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResourceManager
{
    public Dictionary<string, float> Resources;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ResourceManager()
    {
        Resources = new Dictionary<string, float>();
    }

    public void Generate(ref float resource, float addAmount)
    {
        resource += addAmount;
    }
}
