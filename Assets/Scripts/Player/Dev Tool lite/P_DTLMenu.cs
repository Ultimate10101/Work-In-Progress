using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Rendering;

public class P_DTLMenu : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;


    [SerializeField] private GameObject P_DTLCanvas;

    public bool DTL_MenuActive
    {
        get; private set;
    }

    //[SerializeField] private GameObject PlayerCrosshair;


    public P_AssessAbility assessAbility;
    public P_CureAbility cureAbility;
    public P_FireboltAbility fireboltAbility;
    public P_FireboltLogic fireboltLogic;
    public P_ThickSkinnedAbility thickSkinnedAbility;


    private float c_StartingManaCost;
    private float ts_StartingManaCost;
    private float fb_StartingManaCost;

    private float inverC_StartingManaCost;
    private float inverTS_StartingManaCost;

    private bool manaReduxKey;

    private bool inverseKey;

    private bool increasePotencyKey;


    public bool Inverse;
    private bool isIncreasePotencyAcitve;
    private bool isReduceManaCostActive;

    private int fb_startingDamage;
    private int fbInverse_startingDamage;

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

        DTL_MenuActive = false;

        StoreOGValues();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateMenu();

        MenuInputs();

        DTLMenuOptions();

    }


    private void ActivateMenu()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            P_DTLCanvas.SetActive(!P_DTLCanvas.activeSelf);

            if (P_DTLCanvas.activeSelf)
            {
                playerAnim.SetTrigger("Up");
                playerAnim.SetBool("DTL_Active", true);

                DTL_MenuActive = true;
            }
            else
            {
                playerAnim.SetBool("DTL_Active", false);

                DTL_MenuActive = false;
            }
        }


        //PlayerCrosshair.SetActive(!P_DTLCanvas.activeSelf);
    }


    private void MenuInputs()
    {
        manaReduxKey = Input.GetKeyDown(KeyCode.I);

        increasePotencyKey = Input.GetKeyDown(KeyCode.O);

        inverseKey = Input.GetKeyDown(KeyCode.P);
    }

    public void DTLMenuOptions()
    {
        if (DTL_MenuActive)
        {
            // Reduce Player's Magic Spells Mana Cost by half
            if (manaReduxKey && isReduceManaCostActive)
            {
                playerAnim.SetTrigger("IsUsingOtherDTLOptions");

                isReduceManaCostActive = true;

                StartCoroutine(ReduceManaCostDuration());
            }

            // Increase Effectiveness of Player's Magic Spells
            if (increasePotencyKey && !isIncreasePotencyAcitve)
            {
                playerAnim.SetTrigger("IsUsingOtherDTLOptions");

                isIncreasePotencyAcitve = true;

                StartCoroutine(PotencyIncreaseDuration());
            }

            // Inverse Player's Magic Abilites
            if (inverseKey)
            {
                playerAnim.SetTrigger("IsUsingOtherDTLOptions");

                Inverse = !Inverse;
            }

            // View Enemies' Weaknesses and abilites
            assessAbility.ActivateAssess();

            assessAbility.Assess(playerAnim);
        }


    }

    // Add Healing into the Mix
    private IEnumerator PotencyIncreaseDuration()
    {
        fireboltLogic.DAMAGE *= 2;
        fireboltLogic.INVERSE_DAMAGE *= 2;

        yield return new WaitForSeconds(15);

        isIncreasePotencyAcitve = false;

        fireboltLogic.DAMAGE = fb_startingDamage;
        fireboltLogic.INVERSE_DAMAGE = fbInverse_startingDamage;

    }


    private IEnumerator ReduceManaCostDuration()
    {
        cureAbility.ManaCost /= 2;
        fireboltAbility.ManaCost /= 2;
        thickSkinnedAbility.ManaCost /= 2;

        cureAbility.InverseManaCost /= 2;
        thickSkinnedAbility.InverseManaCost /= 2;

        yield return new WaitForSeconds(20);

        isReduceManaCostActive = false;

        cureAbility.ManaCost = c_StartingManaCost;
        fireboltAbility.ManaCost = fb_StartingManaCost;
        thickSkinnedAbility.ManaCost = ts_StartingManaCost;

        cureAbility.InverseManaCost = inverC_StartingManaCost;
        thickSkinnedAbility.InverseManaCost = inverTS_StartingManaCost;

    }



    public void StoreOGValues()
    {
        fb_startingDamage = fireboltLogic.DAMAGE;
        fbInverse_startingDamage = fireboltLogic.INVERSE_DAMAGE;

        c_StartingManaCost = cureAbility.ManaCost;
        ts_StartingManaCost = thickSkinnedAbility.ManaCost;
        fb_StartingManaCost = fireboltAbility.ManaCost;

        inverC_StartingManaCost = cureAbility.InverseManaCost;
        inverTS_StartingManaCost = thickSkinnedAbility.InverseManaCost;
    }

}

