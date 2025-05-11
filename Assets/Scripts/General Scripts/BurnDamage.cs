using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : DamageOverTimeHandler
{
    public override void ApplyTicks(int tick, float timeDelay, float damage)
    {
        Debug.Log("Appying burn");
        base.ApplyTicks(tick, timeDelay, damage);

        status.ChangeStateActivity("BURNING", true);
    }


    protected override IEnumerator DamageOverTime(float timeDelay, float damage)
    {
        yield return StartCoroutine(base.DamageOverTime(timeDelay, damage));

        status.ChangeStateActivity("BURNING", false);
    }
}
