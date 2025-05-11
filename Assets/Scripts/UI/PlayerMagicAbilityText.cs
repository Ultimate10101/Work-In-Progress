using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMagicAbilityText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerMagicAbilityTextButtons;

    // Update is called once per frame
    void Update()
    {
        if (!P_DTLMenu.DTLMenuRef.Inverse)
        {
            playerMagicAbilityTextButtons.text = "Q - Use Thickskinned" + "\n" + "E - Use Firebolt" + "\n" + "F - Use Restoration";
        }
        else
        {
            playerMagicAbilityTextButtons.text = "Q - Inflict Increse Damage Taken" + "\n" + "E - Use Arcane Shot" + "\n" + "F - Inflict Damage Over Time";
        }
    }
}
