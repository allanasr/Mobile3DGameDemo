using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;
public class CollectableManager : Singleton<CollectableManager>
{
    public SOInt coins;
    public SOInt purpleCoins;

    private void Start()
    {
        coins.value = 0;
        purpleCoins.value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
    }  
    public void AddPurpleCoins(int amount = 1)
    {
        purpleCoins.value += amount;
    }

}
