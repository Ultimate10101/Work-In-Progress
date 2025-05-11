using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{

    private Dictionary<string, bool> StatusEffects = new Dictionary<string, bool>();

    private List<string> ListOfStatusEffectsStates;

    private void Start()
    {
        ListOfStatusEffectsStates = new List<string>();

        AddStatusEffectsToList();

        AddToDic();
    }

    private void Update()
    {
        if (StatusEffects["STUNNED"])
        {
            StartCoroutine(UnStun());
        }
    }

    private void AddStatusEffectsToList()
    {
        ListOfStatusEffectsStates.Add("NEUTRAL"); // Always to be the first state

        ListOfStatusEffectsStates.Add("STUNNED");
        ListOfStatusEffectsStates.Add("HIT_BY_INVERSE_RESTORATION");
        ListOfStatusEffectsStates.Add("DAMAGE_TAKEN_INCREASED");
        ListOfStatusEffectsStates.Add("BURNING");
    }

    private void AddToDic()
    {
        for (int i = 0; i < ListOfStatusEffectsStates.Count; i++)
        {
            if (i == 0)
            {
                StatusEffects.Add(ListOfStatusEffectsStates[i], true);
            }
            else
            {
                StatusEffects.Add(ListOfStatusEffectsStates[i], false);
            }
        }
    }


    public void ChangeStateActivity(string state, bool active)
    {
        StatusEffects[state] = active;
    }

    public bool GetState(string state)
    {
        return StatusEffects[state];   
    }


    private IEnumerator UnStun()
    {
        yield return new WaitForSeconds(3.0f);

        ChangeStateActivity("STUNNED", false);
    }
}
