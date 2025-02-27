using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E_HealthController : HealthController
{
    protected override void Update()
    {
        base.Update();

        if(creatureLivingStatus == LivingStatus.DEAD)
        {
            Destroy(gameObject);
        }
    }
}
