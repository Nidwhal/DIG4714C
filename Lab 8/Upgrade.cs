using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public readonly string UpgradeName;
    public float ArcanePowerModifier;
    public float PersonalModifier;
    public int Cost;
    public int Count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Upgrade(string upgradeName, float modifier, int cost)
    {
        UpgradeName = upgradeName;
        ArcanePowerModifier = modifier;
        Cost = cost;
        PersonalModifier = 1f;
        Count = 0;
    }
}
