using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSizePowerup : BasePowerup
{
    public override void Trigger() {
        paddleController.IncreaseWidth();
    }
}
