using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_HealthController : HealthController
{
    protected override void Update()
    {
        base.Update();

        if(currentLivingStatus == LivingStatus.DEAD)
        {
            GameManager.gameManagerRef.GameOver = true;
        }
    }
}
