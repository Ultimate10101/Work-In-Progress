using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ArmsColorDTL : MonoBehaviour
{

    [SerializeField] private GameObject meshArms;

    [SerializeField] private Material redArmsMat;
    [SerializeField] private Material greenArmsMat;
    [SerializeField] private Material blueArmsMat;
    [SerializeField] private Material purpleArmsMat;
    [SerializeField] private Material yellowArmsMat;

    private bool delayActive;

    // Start is called before the first frame update
    void Start()
    {
        meshArms.GetComponent<Renderer>().material = blueArmsMat;
        delayActive = false;
    }

    
    void Update()
    {
        UpdateColor();
    }


    private void UpdateColor()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            meshArms.GetComponent<Renderer>().material = greenArmsMat;
            delayActive = true;
            StartCoroutine(ColorDelayEffect());
        }

        if(!delayActive)
        {
            if (P_DTLMenu.DTLMenuRef.ReduceManaCostActive && P_DTLMenu.DTLMenuRef.IncreasePotencyAcitve)
            {
                meshArms.GetComponent<Renderer>().material = purpleArmsMat;
            }
            else if (P_DTLMenu.DTLMenuRef.ReduceManaCostActive)
            {
                meshArms.GetComponent<Renderer>().material = yellowArmsMat;
            }
            else if (P_DTLMenu.DTLMenuRef.IncreasePotencyAcitve)
            {
                meshArms.GetComponent<Renderer>().material = redArmsMat;
            }
            else
            {
                meshArms.GetComponent<Renderer>().material = blueArmsMat;
            }
        }      
    }



    private IEnumerator ColorDelayEffect()
    {
        yield return new WaitForSeconds(1);
        delayActive =  false;
    }
}
