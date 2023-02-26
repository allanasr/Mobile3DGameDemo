using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : CollectableBase
{
    public float duration;
    protected override void Collect()
    {
        base.Collect();
        StartPowerUp();
        PlayerController.Instance.Bounce();
    }

    protected virtual void StartPowerUp()
    {
        Invoke(nameof(EndPowerUp), duration);
    }
    protected virtual void EndPowerUp() 
    {

    }
}
