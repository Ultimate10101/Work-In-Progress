using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DTLMenu : MonoBehaviour
{
    [SerializeField] private GameObject P_DTLCanvas;

    [SerializeField] private GameObject PlayerCrosshair;

    public P_AssessAbility assessAbility;

    public P_CureAbility cureAbility;

    public P_FireboltAbility fireboltAbility;

    public P_FireboltLogic fireboltLogic;

    public P_ThickSkinnedAbility thickSkinnedAbility;

    private bool manaReduxKey;

    public bool inverseKey;

    private bool increasePotencyKey;
    // Start is called before the first frame update

    public static P_DTLMenu DTLMenuRef;


    void Awake()
    {
        if (DTLMenuRef == null)
        {
            DTLMenuRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        P_DTLCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            P_DTLCanvas.SetActive(!P_DTLCanvas.activeSelf);
        }

        if(P_DTLCanvas.activeSelf)
        {
            PlayerCrosshair.SetActive(!PlayerCrosshair);
        }

        if(!P_DTLCanvas.activeSelf)
        {
            PlayerCrosshair.SetActive(PlayerCrosshair);
        }

        CastInputs();

        DTLMenuOptions();

        assessAbility.ActivateAssess();

        assessAbility.Assess();
    }


    private void CastInputs()
    {
        manaReduxKey = Input.GetKeyDown(KeyCode.I);

        increasePotencyKey = Input.GetKeyDown(KeyCode.O);

        inverseKey = Input.GetKeyDown(KeyCode.P);
    }

    public void DTLMenuOptions()
    {
        if(P_DTLCanvas.activeSelf)
        {
            if(manaReduxKey)
            {
                cureAbility.ManaCost /= 2;

                fireboltAbility.ManaCost /= 2;

                thickSkinnedAbility.ManaCost /= 2;
            }

            if(increasePotencyKey)
            {
                fireboltLogic.DAMAGE *= 2;
            }

            if(inverseKey)
            {
                
            }
        }
    }
}
