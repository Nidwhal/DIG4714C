using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Get the amount to be paid and convert it to an integer
        int amountPaid = 567;

        //create variables for each bill denomination
        int hundreds = 0;
        int fifties = 0;
        int twenties = 0;
        int tens = 0;
        int fives = 0;
        int ones = 0;

        //determine bill count in descending order for optimal payout
        while (amountPaid >= 100) 
        {
            amountPaid -= 100;
            hundreds += 1;
            /*
            Debug.Log(amountPaid);
            Debug.Log(hundreds);
            */
        }
        while (amountPaid >= 50)
        { 
            amountPaid -= 50;
               fifties += 1;
            /*
            Debug.Log(amountPaid);
            Debug.Log(fifties);
            */
        }
        while (amountPaid >= 20)
        {
            amountPaid -= 20;
            twenties += 1;
            /*
            Debug.Log(amountPaid);
            Debug.Log(twenties);
            */
        }
        while (amountPaid >= 10)
        {
            amountPaid -= 10;
            tens += 1;
            /*
            Debug.Log(amountPaid);
            Debug.Log(tens);
            */
        }
        while (amountPaid >= 5)
        {
            amountPaid -= 5;
            fives += 1;
            /*
            Debug.Log(amountPaid);
            Debug.Log(fives);
            */
        }
        while (amountPaid >= 1)
        {
            amountPaid -= 1;
            ones += 1;
            /*
            Debug.Log(amountPaid);
            Debug.Log(ones);
            */
        }
        if (amountPaid == 0)
        {
            Debug.Log("You will be paid " + hundreds + " hundreds, " + fifties + " fifties, " + twenties + " twenties, " + tens + " tens, and " + ones + " ones.");

        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
