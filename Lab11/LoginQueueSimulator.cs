using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LoginQueueSimulator : MonoBehaviour
{
    //create firstnames arry and lastinitials array
    static string[] firstNames = new string[]
    {
        "Colin", "Justin", "Joe", "Mike",  "Oliver", "Ron", "Ashley", "Jessica", "Cody", "Allen",
         "Diego", "Adam", "Emma", "Alex", "Steve", "Eve", "Randy", "Eric", "Luke", "Lauren",
          "John", "Emily", "Mary", "Roger", "Jake", "Jenna", "Gerald", "Nick", "Fred", "Greg",
    };
    static char[] lastInitials = new char[]
    {
        'A','B','C','D','E','F','G','H','I','J',
        'K','L','M','N','O','P','Q','R','S','T',
        'U','V','W','X','Y','Z'
    };

    //login queue
    Queue<string> loginQueue = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        int playerCount = Random.Range(4, 7);

        for (int i=0; i < playerCount; i++)
        {
            loginQueue.Enqueue(GetRandomPlayerName());
        }
        List<string> playerList = loginQueue.ToList();
        Debug.Log($"Initial login queue created.There are 5 players in the queue: " + string.Join(", ", playerList));

        //add invokerepeating for add player and login player
        InvokeRepeating(nameof(AddPlayer),1f,5f);
        InvokeRepeating(nameof(LoginPlayer),1f,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetRandomPlayerName()
    {
        return firstNames[Random.Range(0,29)] + " " + lastInitials[Random.Range(0,25)] + ".";
    }

    void AddPlayer()
    {
        loginQueue.Enqueue(GetRandomPlayerName());
        Debug.Log(loginQueue.Last().ToString() + " is trying to login and added to the login queue.");
    }
    void LoginPlayer()
    {
        if (loginQueue.Count() > 0)
        {
            Debug.Log(loginQueue.Dequeue() + " is now inside the game.");
        }
        else
        {
            Debug.Log("Login server is idle. No players are waiting.");
        }
    }
}
