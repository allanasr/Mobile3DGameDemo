using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : CollectableBase
{

    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddCoins();

    }
}
