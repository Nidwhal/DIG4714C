using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.Collections.Unicode;

public class HonorCardGame : MonoBehaviour
{
    static string[] suits = 
    {
        "\u2660", "\u2663", "\u2665", "\u2666" 
    };

    static char[] cards =
    {
        'J', 'Q', 'K', 'A'
    };

    static int handSize = 4;

    static List<string> unshuffledDeck = new List<string>();
    static List<string> hand = new List<string>();
    static Stack<string> deck = new Stack<string>();
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateDeck();
        ShuffleDeck();
        DrawHand();
        while (!gameOver)
        {
            TakeTurn();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateDeck()
    {
        for (int i = 0; i < cards.Count(); i++)
        {
            for(int suitTrack = 0; suitTrack < suits.Count(); suitTrack++)
            {
                unshuffledDeck.Add(cards[i] + suits[suitTrack]);
            }
        }
        //Debug.Log("The unshuffled deck has:\n" + string.Join(", " , unshuffledDeck));
    }

    void ShuffleDeck()
    {
        System.Random random = new System.Random();
        List<string> tempDeck = new List<string>();

        tempDeck = unshuffledDeck.OrderBy(i => random.Next()).ToList();

        for(int i = 0; i < tempDeck.Count(); i++)
        {
            deck.Push(tempDeck[i]);
        }

        //Debug.Log("The shuffled deck has:\n" + string.Join(", ", deck));
    }

    void DrawHand()
    {
        for (int i = 0; i < handSize; i++)
        {
            hand.Add(deck.Pop());
        }
        Debug.Log("I made the initial deck and draw. My hand is: " + string.Join(", ", hand));
        CheckWin();

        if (gameOver)
        {
            Debug.Log("My starting hand is a win! The Game is WON!");
        };
    }

    void CheckWin()
    {
        var winCheck = hand.Select(x => x.Substring(1)).ToList();

        //Debug.Log("wincheck: " + string.Join(", ", winCheck));
        //Debug.Log(winCheck.GroupBy(x => x).Any(y => y.Count() >= 3));

        if (winCheck.GroupBy(x => x).Any(y => y.Count() >= 3))
        {
            gameOver = true;
        }
        else
        {
            gameOver = false;
        };
    }
    void TakeTurn()
    {
        if (deck.Count() > 0)
        {
            System.Random random = new System.Random();
            int rnd = random.Next(0, 4);

            string oldCard = hand[rnd];

            hand.RemoveAt(rnd);
            hand.Add(deck.Pop());
            CheckWin();

            if (gameOver)
            {
                Debug.Log("I discarded " + oldCard + " and drew " + hand.Last() + ". My hand is: " + string.Join(", ", hand) + ". The game is WON.");
            }
            else
            {
                Debug.Log("I discarded " + oldCard + " and drew " + hand.Last() + ". My hand is: " + string.Join(", ", hand) + ". This is not a winning hand. I will attempt to play another round.");
            }

        }
        else
        {
            Debug.Log("The deck is empty.The game is LOST");
            gameOver = true;
        }
    }
}
