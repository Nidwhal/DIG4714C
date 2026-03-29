using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardScholar : Generator
{
    float generationAmount = 1f; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override float Produce()
    {
        return generationAmount;
    }
}
