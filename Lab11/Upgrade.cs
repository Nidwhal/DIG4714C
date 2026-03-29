using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public enum UpgradeState
    {
        Locked,
        Available,
        Purchased
    };
    public struct UpgradeEffect
    {
        public float Boost;
        public string ResourceType;
    }

    public readonly string UpgradeName;
    public float PersonalModifier;
    public int Cost;
    public int Count;
    public int Tier;
    public UpgradeState Status;
    public UpgradeEffect Effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Upgrade(string upgradeName, float boost, string resourceType, int cost, int tier, UpgradeState upgradeState)
    {
        UpgradeName = upgradeName;
        Effect.Boost = boost;
        Effect.ResourceType = resourceType;
        Cost = cost;
        Tier = tier;
        Status = upgradeState;
        PersonalModifier = 1f;
        Count = 0;
    }
}
