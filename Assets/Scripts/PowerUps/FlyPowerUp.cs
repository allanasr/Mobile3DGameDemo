using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPowerUp : PowerUpBase
{
    public float heightAmount;
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(heightAmount, duration);
    }
}
