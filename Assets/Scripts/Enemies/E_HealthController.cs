using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E_HealthController : HealthController
{
    public float Health
    {
        get { return health; }
    }

    protected override void Update()
    {
        base.Update();

        if(currentLivingStatus == LivingStatus.DEAD)
        {
            Destroy(gameObject);
        }
    }
}
