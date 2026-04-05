using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    static string[] firstNames = new string[]
    {
        "Colin", "Justin", "Joe", "Mike",  "Oliver", "Ron", "Ashley", "Jessica", "Cody", "Allen",
        "Diego", "Adam", "Emma", "Alex", "Steve", "Eve", "Randy", "Eric", "Luke", "Lauren",
        "John", "Emily", "Mary", "Roger", "Jake", "Jenna", "Gerald", "Nick", "Fred", "Greg",
    };

    static string[] randomNames = new string[15];
    HashSet<string> seen = new HashSet<string>();
    HashSet<string> duplicates = new HashSet<string>();

    // Start is called before the first frame update
    void Start()
    {
        //populate randomNames with 15 random names from firstNames array
        for (int i = 0; i < randomNames.Count(); i++)
        {
            randomNames[i] = firstNames[Random.Range(0, 29)];
            if (!seen.Add(randomNames[i]))
            {
                duplicates.Add(randomNames[i]);
            }
        }
        Debug.Log("Created the name array: " + string.Join(", ", randomNames));

        if (duplicates.Count() > 0)
        {
            Debug.Log("The array has duplicate names: " + string.Join(", ",duplicates));
        }
        else
        {
            Debug.Log("The array has no duplicate names.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
