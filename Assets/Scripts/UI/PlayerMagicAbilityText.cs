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
            playerMagicAbilityTextButtons.text = "Q - Use Thickskinned" + "\n" + "      - Shield Yourself" + 
                                                "\n" + "E - Use Firebolt" + "\n" + "        - Deal Damage" + 
                                                "\n"+ "F - Use Restoration" + "\n" + "      - Heal health over time";
        }
        else
        {
            playerMagicAbilityTextButtons.text = "Q - Inflict Increse Damage Taken" + "\n" +"       - Enemy takes more Damage" +
                                                "\n" + "E - Use Arcane Shot" + "\n" + "     - Restore Mana" +
                                                "\n" + "F - Inflict Damage Over Time" + "\n" + "        - Enemy Takes Damage Over Time";
        }
    }
}
