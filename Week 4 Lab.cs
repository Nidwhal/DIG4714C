using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewBehaviourScript : MonoBehaviour
{
    public string characterName;
    public int characterLevel;
    public string characterClass;
    public int con;
    public string characterRace;
    public bool tough;
    public bool stout;


    Dictionary<string, int> hitDice = new Dictionary<string, int>()
        {
            {"Artificer", 8},
            {"Barbarian", 12},
            {"Bard", 8},
            {"Cleric", 8},
            {"Druid", 8},
            {"Figther", 10},
            {"Monk", 8},
            {"Ranger", 10},
            {"Rogue", 8},
            {"Paladin", 10},
            {"Sorcerer", 6},
            {"Wizard", 6},
            {"Warlock", 8}
        };

    Dictionary<int, int> conModifiers = new Dictionary<int, int>()
    {
        {1, -5},
        {2, -4},
        {3, -4},
        {4, -3},
        {5, -3},
        {6, -2},
        {7, -2},
        {8, -1},
        {9, -1},
        {10, 0},
        {11, 0},
        {12, 1},
        {13, 1},
        {14, 2},
        {15, 2},
        {16, 3},
        {17, 3},
        {18, 4},
        {19, 4},
        {20, 5},
        {21, 5},
        {22, 6},
        {23, 6},
        {24, 7},
        {25, 7},
        {26, 8},
        {27, 8},
        {28, 9},
        {29, 9},
        {30, 10}
    };


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(characterName + " the level " + characterLevel + " " + characterRace + " " + characterClass + " has "  + Convert.ToString(AveragedHP()) + " averaged HP!");
        Debug.Log(characterName + " the level " + characterLevel + " " + characterRace + " " + characterClass + " has " + Convert.ToString(RandomHP()) + " rolled HP!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int AveragedHP()
    {
         float averagedHPCalc = 0;

        //add hp for each level equal to the average dice roll
        for(int counter = 0; counter < characterLevel; counter++)
        {
            averagedHPCalc += hitDice[characterClass]/2 + conModifiers[con];
        }

        //check if character is a dwarf, goliath, or orc
        if(characterRace == "Dwarf")
        {
            averagedHPCalc += characterLevel * 2;
        }
        else if (characterRace == "Orc" || characterRace == "Goliath")
        {
            averagedHPCalc += characterLevel;
        }

        //check if character has tough or stout perks
        if (tough)
        {
            averagedHPCalc += characterLevel * 2;
        }

        if (stout)
        {
            averagedHPCalc += characterLevel;
        }

        int averagedHP = Convert.ToInt32(averagedHPCalc);

        if(averagedHP <= 0)
        {
            averagedHP = 1;
        }

        return averagedHP;
    }

    int RandomHP()
    {
        int randomHP = 0;
        int random;

        //add hp for each level at random based on class range
        for(int counter = 0; counter < characterLevel; counter++)
        {
            random = Random.Range(1, hitDice[characterClass]);
            randomHP += random + conModifiers[con];
        }

        //check if character is a dwarf, goliath, or orc
        if (characterRace == "Dwarf")
        {
            randomHP += characterLevel * 2;
        }
        else if (characterRace == "Orc" || characterRace == "Goliath")
        {
            randomHP += characterLevel;
        }

        //check if the character has tough or stout perks
        if (tough)
        {
            randomHP += characterLevel * 2;
        }

        if (stout)
        {
            randomHP += characterLevel;
        }

        if (randomHP <= 0)
        {
            randomHP = 1;
        }

        return randomHP;
    }

}
