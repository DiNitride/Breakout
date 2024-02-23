using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : BasePowerup
{
    public override void Trigger() {
        puckMovementController.IncreaseSpeed();
    }
}
