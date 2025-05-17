using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDTLFunctionText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dtlFunctionPrompt1;
    [SerializeField] private TextMeshProUGUI dtlFunctionPrompt3;
    [SerializeField] private TextMeshProUGUI dtlFunctionPrompt4;


    private P_DTLMenu functionInfo;

    private P_AssessFunction assessInfo;

    // Start is called before the first frame update
    void Start()
    {
        assessInfo = P_PlayerController.playerControllerRef.GetComponent<P_AssessFunction>();

        functionInfo = P_PlayerController.playerControllerRef.GetComponent<P_DTLMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        dtlFunctionPrompt1.text = "T - Assess Enemies\r\nReady To Use: " + assessInfo.ReadyToActivate;
        dtlFunctionPrompt3.text = "X - Increase Potency\r\nReady To Use: " + functionInfo.IncreasePotencyAcitve;
        dtlFunctionPrompt4.text = "C - Reduce Mana Cost\r\nReady To Use: " + functionInfo.ReduceManaCostActive;

    }
}
