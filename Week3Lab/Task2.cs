using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Task2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //create input variables
        float bookCost = 100f;
        float copiesSold = 11;
        //create required variables
        float bookPurchaseCost = BookPurchaseCost(bookCost);
        Debug.Log("Books Cost: $" + bookPurchaseCost);
        float shipping = CalculateShipping(copiesSold);
        Debug.Log("Shipping Cost: $" + shipping);
        float profit = CalculateProfit(bookCost,shipping,bookPurchaseCost);
        Debug.Log("Your profit is $" + profit);

    }

    float CalculateProfit(float bookCost, float shipping, float bookPurchaseCost)
    {
        //calculate profit for the bookstore
        float profit = bookCost - (shipping + bookPurchaseCost);
        return profit;
    }

    float CalculateShipping(float copiesSold)
    {
        //calculate shipping costs
        float shipping = 3 + ((copiesSold - 1) * .75f);
        return shipping;
    }

    float BookPurchaseCost(float bookCost)
    {
        //calculate the cost of books for the store
        float bookPurchaseCost = bookCost * .6f;
        return bookPurchaseCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
