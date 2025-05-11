using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseRestorationDamage : DamageOverTimeHandler
{
    public override void ApplyTicks(int tick, float timeDelay, float damage)
    {
        Debug.Log("Appying DOT");
        base.ApplyTicks(tick, timeDelay, damage);

        status.ChangeStateActivity("HIT_BY_INVERSE_RESTORATION", true);
    }


    protected override IEnumerator DamageOverTime(float timeDelay, float damage)
    {
        yield return StartCoroutine(base.DamageOverTime(timeDelay, damage));

        status.ChangeStateActivity("HIT_BY_INVERSE_RESTORATION", false);
    }

}
