using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeHandler : MonoBehaviour
{

    protected StatusEffectHandler status;

    private void Start()
    {
        status = GetComponent<StatusEffectHandler>();
    }


    private List<int> DamageTickTimer = new List<int>();


    public virtual void ApplyTicks(int tick, float timeDelay, float damage)
    {
        if (DamageTickTimer.Count <= 0)
        {
            DamageTickTimer.Add(tick);
            StartCoroutine(DamageOverTime(timeDelay, damage));
            status.ChangeStateActivity("NEUTRAL", false);
        }
        else
        {
            DamageTickTimer.Add(tick);
        }

    }


    protected virtual IEnumerator DamageOverTime(float timeDelay, float damage)
    {
        while (DamageTickTimer.Count > 0)
        {
            for (int i = 0; i < DamageTickTimer.Count; ++i)
            {
                DamageTickTimer[i]--;   // decrement item in list
            }
            gameObject.GetComponent<E_HealthController>().TakeDamage(damage);
            DamageTickTimer.RemoveAll(item => item == 0); // Removes every element in the list that has reached zero
            yield return new WaitForSeconds(timeDelay);
        }
        status.ChangeStateActivity("NEUTRAL", true);
    }
}
