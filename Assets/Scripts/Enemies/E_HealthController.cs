using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E_HealthController : HealthController
{
    private bool isDoubleDamageActive;
    private float timer;

    public bool IsDoubleDamageActive
    {
        get { return isDoubleDamageActive; } 
    }


    protected override void Update()
    {
        base.Update();

        if(currentLivingStatus == LivingStatus.DEAD)
        {
            Destroy(gameObject);
        }

        if(isDoubleDamageActive)
        {
            TimeUntilEffectOff();
        }
    }


    public override void TakeDamage(float damage)
    {
        if (isDoubleDamageActive)
        {
            health -= (damage * 2);
        }
        else
        {
            base.TakeDamage(damage);
        }
    }


    public void ActivateDoubleDamage(int duration)
    {
        isDoubleDamageActive = true;

        timer = duration;
    }

    private void TimeUntilEffectOff()
    {
        timer = Mathf.MoveTowards(timer, 0.0f, 1.0f * Time.deltaTime);

        if(timer<= 0.0f)
        {
            isDoubleDamageActive=false;
        }
    }

}
