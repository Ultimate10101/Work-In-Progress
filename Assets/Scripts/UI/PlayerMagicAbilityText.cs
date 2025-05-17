using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMagicAbilityText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fireboltMaigcPrompt;
    [SerializeField] private TextMeshProUGUI restorationMaigcPrompt;
    [SerializeField] private TextMeshProUGUI thickSkinnedMagicPrompt;


    private P_PlayerController player;

    P_ThickSkinnedAbility thickSkinnedMana;
    P_ThickSkinnedAbility inverseThickSkinnedMana;

    P_CureAbility restorationMana;
    P_CureAbility inverseRestorationMana;

    P_FireboltAbility fireboltMana;
    P_FireboltAbility arcaneShotMana;

    private void Start()
    {
        thickSkinnedMana  = P_PlayerController.playerControllerRef.GetComponent<P_ThickSkinnedAbility>();
        inverseThickSkinnedMana = P_PlayerController.playerControllerRef.GetComponent<P_ThickSkinnedAbility>();

        restorationMana = P_PlayerController.playerControllerRef.GetComponent<P_CureAbility>();
        inverseRestorationMana = P_PlayerController.playerControllerRef.GetComponent<P_CureAbility>();

        fireboltMana = P_PlayerController.playerControllerRef.GetComponent<P_FireboltAbility>();
        arcaneShotMana = P_PlayerController.playerControllerRef.GetComponent<P_FireboltAbility>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }


    private void UpdateText()
    {
        if (!P_DTLMenu.DTLMenuRef.Inverse)
        {
            fireboltMaigcPrompt.text = "Left click - Deal Damage\r\nMana Cost: " + fireboltMana.ManaCost +"\r\nReady To Use: " + fireboltMana.ReadyToCast;
            restorationMaigcPrompt.text = "F - Heal over time\r\nMana Cost: " + restorationMana.ManaCost + "\r\nReady To Use: " + restorationMana.ReadyToCast;
            thickSkinnedMagicPrompt.text = "Q - Shield Yourself\r\nMana Cost: " + thickSkinnedMana.ManaCost +"\r\nReady To Use: " + thickSkinnedMana.ReadyToCast;

        }
        else
        {
            fireboltMaigcPrompt.text = "Left click  - Restore Mana\r\nMana Cost: " + fireboltMana.InverseManaCost + "\r\nReady To Use: " + fireboltMana.InverseReadyToCast;
            restorationMaigcPrompt.text = "F - Inflict Damage Over Time\r\nMana Cost: " + restorationMana.InverseManaCost + "\r\nReady To Use: " + restorationMana.InverseReadyToCast;
            thickSkinnedMagicPrompt.text = "Q - Inflict Increse Damage Taken\r\nMana Cost: " + thickSkinnedMana.InverseManaCost + "\r\nReady To Use: " + thickSkinnedMana.InverseReadyToCast;
        }
    } 
}
