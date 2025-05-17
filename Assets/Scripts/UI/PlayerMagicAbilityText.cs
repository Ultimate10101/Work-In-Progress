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

    P_ThickSkinnedAbility thickSkinnedInfo;
    P_ThickSkinnedAbility inverseThickSkinnedInfo;

    P_CureAbility restorationInfo;
    P_CureAbility inverseRestorationInfo;

    P_FireboltAbility fireboltInfo;
    P_FireboltAbility arcaneShotInfo;

    private void Start()
    {
        thickSkinnedInfo = P_PlayerController.playerControllerRef.GetComponent<P_ThickSkinnedAbility>();
        inverseThickSkinnedInfo = P_PlayerController.playerControllerRef.GetComponent<P_ThickSkinnedAbility>();

        restorationInfo = P_PlayerController.playerControllerRef.GetComponent<P_CureAbility>();
        inverseRestorationInfo = P_PlayerController.playerControllerRef.GetComponent<P_CureAbility>();

        fireboltInfo = P_PlayerController.playerControllerRef.GetComponent<P_FireboltAbility>();
        arcaneShotInfo = P_PlayerController.playerControllerRef.GetComponent<P_FireboltAbility>();
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
            fireboltMaigcPrompt.text = "Left click - Deal Damage\r\nMana Cost: " + fireboltInfo.ManaCost +"\r\nReady To Use: " + fireboltInfo.ReadyToCast;
            restorationMaigcPrompt.text = "F - Heal over time\r\nMana Cost: " + restorationInfo.ManaCost + "\r\nReady To Use: " + restorationInfo.ReadyToCast;
            thickSkinnedMagicPrompt.text = "Q - Shield Yourself\r\nMana Cost: " + thickSkinnedInfo.ManaCost +"\r\nReady To Use: " + thickSkinnedInfo.ReadyToCast;

        }
        else
        {
            fireboltMaigcPrompt.text = "Left click  - Restore Mana\r\nMana Cost: " + arcaneShotInfo.InverseManaCost + "\r\nReady To Use: " + arcaneShotInfo.InverseReadyToCast;
            restorationMaigcPrompt.text = "F - Inflict Damage Over Time\r\nMana Cost: " + inverseRestorationInfo.InverseManaCost + "\r\nReady To Use: " + inverseRestorationInfo.InverseReadyToCast;
            thickSkinnedMagicPrompt.text = "Q - Inflict Increse Damage Taken\r\nMana Cost: " + inverseThickSkinnedInfo.InverseManaCost + "\r\nReady To Use: " + inverseThickSkinnedInfo.InverseReadyToCast;
        }
    } 
}
