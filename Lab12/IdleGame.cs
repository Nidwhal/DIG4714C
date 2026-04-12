using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IdleGame : MonoBehaviour
{
    public TMP_Text arcanePowerText;
    public TMP_Text magicOrbsText;
    public TMP_Text upgrade1Text;
    public TMP_Text errorMessageText;

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
    private string _dataPath;
    private string _xmlFile;
    private string _jsonUpgrades;
    private string _textFile;
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
        _dataPath = Application.persistentDataPath + "/Player_Data";
        _xmlFile = _dataPath + "/Resources.xml";
        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }
        if (!File.Exists(_xmlFile))
        {
            FileStream xmlStream = File.Create(_xmlFile);
            XmlWriter xmlWriter = XmlWriter.Create(xmlStream);
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("Resources");

            xmlWriter.WriteStartElement("Resource");
            xmlWriter.WriteElementString("Name", "ArcanePower");
            xmlWriter.WriteElementString("Amount", "100");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Resource");
            xmlWriter.WriteElementString("Name", "MagicOrbs");
            xmlWriter.WriteElementString("Amount", "10");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            xmlStream.Close();
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(_xmlFile);

        XmlNodeList nodes = xmlDoc.SelectNodes("Resources/Resource");
        foreach (XmlNode node in nodes)
        {
            resources.Resources.Add(node["Name"].InnerText, float.Parse(node["Amount"].InnerText));
        }
        arcanePower = resources.Resources[Convert.ToString(ResourceType.ArcanePower)];
        arcanePowerText.text = "Arcane Power: " + Convert.ToString((int)arcanePower);
        magicOrbs = resources.Resources[Convert.ToString(ResourceType.MagicOrbs)];
        magicOrbsText.text = "Magic Orbs: " + Convert.ToString((int)magicOrbs);

        //resources.Resources.Add(Convert.ToString(ResourceType.ArcanePower), arcanePower);
        //resources.Resources.Add(Convert.ToString(ResourceType.MagicOrbs), magicOrbs);

        _jsonUpgrades = _dataPath + "/_jsonUpgrades.json";
        if (File.Exists(_jsonUpgrades))
        {
            using (StreamReader stream = new StreamReader(_jsonUpgrades))
            {
                var jsonString = stream.ReadToEnd();
                var upgradesData = JsonUtility.FromJson<UpgradesSave>(jsonString);
                foreach (var upgradeSaved in upgradesData.upgradesSave)
                {
                    upgrades.Add(new Upgrade(upgradeSaved.UpgradeName, 10f, "ArcanePower", upgradeSaved.Cost, upgradeSaved.Tier, Upgrade.UpgradeState.Purchased, upgradeSaved.Count));
                }
            }
            for (int i = 0; i < upgrades.Count; i++)
            {
                aPUpgradeBoosts += upgrades[i].Effect.Boost * upgrades[i].Count * upgrades[i].PersonalModifier;
            }
        }
        else
        {
            Upgrade wizardScholar = new Upgrade("Wizard Scholar", 10f, Convert.ToString(ResourceType.ArcanePower), 100, 1, Upgrade.UpgradeState.Available);
            upgrades.Add(wizardScholar);
            //Upgrade wizard = new Upgrade("Wizard", 3f, Convert.ToString(ResourceType.ArcanePower), 400, 2, Upgrade.UpgradeState.Locked);
            //upgrades.Add(wizard);
            //Upgrade archWizard = new Upgrade("Mana Well", 10f, Convert.ToString(ResourceType.ArcanePower), 1000, 3, Upgrade.UpgradeState.Locked);
            //upgrades.Add(archWizard);
        }

        upgrade1Text.text = upgrades[0].UpgradeName + "(" + upgrades[0].Count.ToString() + ")" + "\n Cost: " + upgrades[0].Cost.ToString();


        _textFile = _dataPath + "/Save_Data.txt";
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists...");
            return;
        }

        File.WriteAllText(_textFile, $"GameStarted: {DateTime.Now}\n");
        Debug.Log(File.ReadAllText(_textFile));
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
        resources.Generate(ref arcanePower, (timer * arcanePowerMod) + (aPUpgradeBoosts * timer));
        arcanePowerText.text = "Arcane Power: " + Convert.ToString((int)arcanePower);
        resources.Resources[Convert.ToString(ResourceType.ArcanePower)] = arcanePower;

        if (MaxCheck(magicOrbs))
        {
            magicOrbsOverflow += 1;
            magicOrbs = 0;
        }
        resources.Generate(ref magicOrbs, (timer * magicOrbsMod) + (mOUpgradeBoosts * timer));
        magicOrbsText.text = "Magic Orbs: " + Convert.ToString((int)magicOrbs);
        resources.Resources[Convert.ToString(ResourceType.MagicOrbs)] = magicOrbs;
    }

    private void OnApplicationQuit()
    {
        UpgradesSave us = new UpgradesSave();
        us.upgradesSave = upgrades;
        string jsonString = JsonUtility.ToJson(us, true);
        using (StreamWriter stream = File.CreateText(_jsonUpgrades))
        {
            stream.WriteLine(jsonString);
        }

        File.AppendAllText(_textFile, $"GameEnded: {DateTime.Now}\n");
        Debug.Log(File.ReadAllText(_textFile));
    }

    [Serializable]
    public class UpgradesSave
    {
        public List<Upgrade> upgradesSave;
    }

    public void GenerateButton()
    {
        if(MaxCheck(arcanePower))
        {
            arcanePowerOverflow += 1;
            arcanePower = 0;
        }
        resources.Generate(ref arcanePower, 1 * buttonModifier);
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
        string errorMessage = "Successfully purchased!";
        if (upgrades[upgrade].Effect.ResourceType == Convert.ToString(ResourceType.ArcanePower))
        {
            if (arcanePower >= upgrades[upgrade].Cost)
            {
                arcanePower -= upgrades[upgrade].Cost;
                upgrades[upgrade].Count++;
                upgrades[upgrade].Cost = (int)(upgrades[upgrade].Cost * upgradeCostIncrement);
                upgrades[upgrade].Status = Upgrade.UpgradeState.Purchased;
                aPUpgradeBoosts = 0;
                upgrade1Text.text = upgrades[upgrade].UpgradeName + "(" + upgrades[upgrade].Count.ToString() + ")" + "\n Cost: " + upgrades[upgrade].Cost.ToString();

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
        else
        {
            errorMessage = "Not enough resources to purchase.";
        }
        errorMessageText.text = errorMessage;
    }
}
