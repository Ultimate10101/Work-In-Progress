using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E_HealthController : HealthController
{
    private bool isDoubleDamageActive;
    private float timer;

    [SerializeField] private GameObject DeathSomke;

    public bool IsDoubleDamageActive
    {
        get { return isDoubleDamageActive; } 
    }


    protected override void Update()
    {
        base.Update();

        if(currentLivingStatus == LivingStatus.DEAD)
        {
            Instantiate(DeathSomke, transform.position, DeathSomke.transform.rotation);
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

            if (P_DTLMenu.DTLMenuRef.IncreasePotencyAcitve)
            {
                health -= (damage * 4);
            }
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
            gameObject.GetComponent<StatusEffectHandler>().ChangeStateActivity("DAMAGE_TAKEN_INCREASED", false);
        }
    }

}
