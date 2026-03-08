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

    private float arcanePower = 0;
    private float magicOrbs = 0;
    private float timer = 0f;
    private float arcanePowerMod = .1f;
    private float magicOrbsMod = .01f;
    private float arcanePowerOverflow = 0f;
    private float magicOrbsOverflow = 0f;
    private float buttonModifier = 1f;

    List<Upgrade> upgrades = new List<Upgrade>();
    ResourceManager resources = new ResourceManager();


    // Start is called before the first frame update
    void Start()
    {
        Upgrade wizardScholar = new Upgrade("Wizard Scholar", 1f, 150);
        upgrades.Add(wizardScholar);
        Upgrade wizard = new Upgrade("Wizard", 3f, 400);
        upgrades.Add(wizard);
        Upgrade archWizard = new Upgrade("Arch Wizard", 10f, 1000);
        upgrades.Add(archWizard);

        resources.Resources.Add("Arcane Power", arcanePower);
        resources.Resources.Add("Magic Orbs", magicOrbs);
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
        arcanePower += (float)(timer * arcanePowerMod);
        arcanePowerText.text = "Arcane Power: " + Convert.ToString((int)arcanePower);
        resources.Resources["Arcane Power"] = arcanePower;

        if (MaxCheck(magicOrbs))
        {
            magicOrbsOverflow += 1;
            magicOrbs = 0;
        }
        magicOrbs += (float)(timer * magicOrbsMod);
        magicOrbsText.text = "Magic Orbs: " + Convert.ToString((int)magicOrbs);
        resources.Resources["Magic Orbs"] = magicOrbs;
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
        resources.Resources["Arcane Power"] = arcanePower;
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

    public void PurchaseUpgrade()
    {

    }
}
