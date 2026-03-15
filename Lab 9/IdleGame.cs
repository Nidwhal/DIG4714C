using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class IdleGame : MonoBehaviour
{
    public TMP_Text arcanePowerText;
    public TMP_Text magicOrbsText;
    public TMP_Text upgrade1Text;

    private float arcanePower = 0;
    private float magicOrbs = 0;
    private float timer = 0f;
    private float arcanePowerMod = .1f;
    private float magicOrbsMod = .01f;
    private float arcanePowerOverflow = 0f;
    private float magicOrbsOverflow = 0f;
    private float buttonModifier = 1f;
    private float aPUpgradeBoosts = 0f;
    private float mOUpgradeBoosts = 0f;
    private float upgradeCostIncrement = 1.1f;
    public enum ResourceType
    {
        ArcanePower,
        MagicOrbs
    };

    List<Upgrade> upgrades = new List<Upgrade>();
    ResourceManager resources = new ResourceManager();


    // Start is called before the first frame update
    void Start()
    {
        Upgrade wizardScholar = new Upgrade("Wizard Scholar", 1f, Convert.ToString(ResourceType.ArcanePower), 10, 1, Upgrade.UpgradeState.Available);
        upgrades.Add(wizardScholar);
        Upgrade wizard = new Upgrade("Wizard", 3f, Convert.ToString(ResourceType.ArcanePower), 400, 2, Upgrade.UpgradeState.Locked);
        upgrades.Add(wizard);
        Upgrade archWizard = new Upgrade("Arch Wizard", 10f, Convert.ToString(ResourceType.ArcanePower), 1000, 3, Upgrade.UpgradeState.Locked);
        upgrades.Add(archWizard);

        resources.Resources.Add(Convert.ToString(ResourceType.ArcanePower), arcanePower);
        resources.Resources.Add(Convert.ToString(ResourceType.MagicOrbs), magicOrbs);

        //upgrade1Text.text = upgrades[0].UpgradeName + "(" + upgrades[0].Count.ToString() + ")" + "\n Cost: " + upgrades[0].Cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;

        if (MaxCheck(arcanePower))
        {
            arcanePowerOverflow += 1;
            arcanePower = 0;
        }
        arcanePower += (timer * arcanePowerMod) + (aPUpgradeBoosts * timer);
        arcanePowerText.text = "Arcane Power: " + Convert.ToString((int)arcanePower);
        resources.Resources[Convert.ToString(ResourceType.ArcanePower)] = arcanePower;

        if (MaxCheck(magicOrbs))
        {
            magicOrbsOverflow += 1;
            magicOrbs = 0;
        }
        magicOrbs += (timer * magicOrbsMod) + (mOUpgradeBoosts * timer);
        magicOrbsText.text = "Magic Orbs: " + Convert.ToString((int)magicOrbs);
        resources.Resources[Convert.ToString(ResourceType.MagicOrbs)] = magicOrbs;
    }

    public void GenerateButton()
    {
        if(MaxCheck(arcanePower))
        {
            arcanePowerOverflow += 1;
            arcanePower = 0;
        }
        arcanePower += 1 * buttonModifier;
        arcanePowerText.text = "Arcane Power: " + Convert.ToString((int)arcanePower);
        resources.Resources[Convert.ToString(ResourceType.ArcanePower)] = arcanePower;
    }

    private bool MaxCheck(float check)
    {
        if(check > float.MaxValue - 10000)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PurchaseUpgrade(int upgrade)
    {
        if (upgrades[upgrade].Effect.ResourceType == Convert.ToString(ResourceType.ArcanePower))
        {
            if (arcanePower >= upgrades[upgrade].Cost)
            {
                arcanePower -= upgrades[upgrade].Cost;
                upgrades[upgrade].Count++;
                upgrades[upgrade].Cost = (int)(upgrades[upgrade].Cost * upgradeCostIncrement);
                upgrades[upgrade].Status = Upgrade.UpgradeState.Purchased;
                aPUpgradeBoosts = 0;
                //upgrade1Text.text = upgrades[upgrade].UpgradeName + "(" + upgrades[upgrade].Count.ToString() + ")" + "\n Cost: " + upgrades[upgrade].Cost.ToString();

                for (int i = 0; i < upgrades.Count; i++)
                {
                    aPUpgradeBoosts += upgrades[i].Effect.Boost * upgrades[i].Count * upgrades[i].PersonalModifier;
                }
            }
        }
        else if(upgrades[upgrade].Effect.ResourceType == Convert.ToString(ResourceType.MagicOrbs))
        {
            if (magicOrbs >= upgrades[upgrade].Cost)
            {
                magicOrbs -= upgrades[upgrade].Cost;
                upgrades[upgrade].Count++;
                upgrades[upgrade].Cost = (int)(upgrades[upgrade].Cost * upgradeCostIncrement);
                upgrades[upgrade].Status = Upgrade.UpgradeState.Purchased;
                mOUpgradeBoosts = 0;

                for (int i = 0; i < upgrades.Count; i++)
                {
                    mOUpgradeBoosts += upgrades[i].Effect.Boost * upgrades[i].Count * upgrades[i].PersonalModifier;
                }
            }
        }

    }
}
