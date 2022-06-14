using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : PowerUpBase
{
    public float amount = 7;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(amount);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(1);
    }

}
