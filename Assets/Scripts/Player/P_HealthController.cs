using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_HealthController : HealthController
{
    public LivingStatus GetLivingStatus()
    {
        return currentLivingStatus;
    }
}
