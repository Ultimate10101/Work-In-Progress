using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DTLMenu : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;

    [SerializeField] private GameObject DTL_Menu;

    [SerializeField] private GameObject normalMenu;
    [SerializeField] private GameObject inverseMenu;

    public bool DTL_MenuActive
    {
        get; private set;
    }

    public P_AssessFunction assessFunction;
    public P_CureAbility cureAbility;
    public P_FireboltAbility fireboltAbility;
    public P_FireboltLogic fireboltLogic;
    public P_ThickSkinnedAbility thickSkinnedAbility;


    private float restoration_StartingManaCost;
    private float thickskinned_StartingManaCost;
    private float firebolt_StartingManaCost;

    private float inverRestoration_StartingManaCost;
    private float inverThickskinned_StartingManaCost;

    private bool manaReduxKey;

    private bool inverseKey;

    private bool increasePotencyKey;


    public bool Inverse;

    private bool isIncreasePotencyAcitve;
    public bool IncreasePotencyAcitve
    {
        get { return !isIncreasePotencyAcitve; }
    }

    private bool isReduceManaCostActive;
    public bool ReduceManaCostActive
    {
        get { return !isReduceManaCostActive; }
    }

    private int firebolt_StartingDamage;
    private int firebolInverse_StartingDamage;
    private float dot_StartingDamage;
    [SerializeField] private AudioClip manaReduxSFX;
    [SerializeField] private AudioClip increasePotencySFX;
    [SerializeField] private AudioClip inverseSFX;

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
        DTL_Menu.SetActive(false);

        Inverse = false;

        StoreOGValues();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            DTL_Menu.SetActive(!DTL_Menu.activeSelf);
            DTL_MenuActive = DTL_Menu.activeSelf;

            if (DTL_MenuActive)
            {
                playerAnim.SetTrigger("Up");

                playerAnim.SetBool("DTL_Active", true);
            }

        }



        if (Inverse)
        {
            normalMenu.SetActive(false);
            inverseMenu.SetActive(true);
        }
        else
        {
            normalMenu.SetActive(true);
            inverseMenu.SetActive(false);
        }



        if (DTL_MenuActive)
        {
            MenuInputs();

            DTLMenuOptions();
        }
        else
        {
            playerAnim.SetBool("DTL_Active", false);
        }

       
    }




    private void MenuInputs()
    {
        manaReduxKey = Input.GetKeyDown(KeyCode.C);

        increasePotencyKey = Input.GetKeyDown(KeyCode.X);

        inverseKey = Input.GetKeyDown(KeyCode.R);
    }

    public void DTLMenuOptions()
    {
            // Reduce Player's Magic Spells Mana Cost by half
            if (manaReduxKey && !isReduceManaCostActive)
            {
                AudioManager.audioManagerRef.PlaySFX(manaReduxSFX);

                playerAnim.SetTrigger("IsUsingOtherDTLOptions");

                isReduceManaCostActive = true;

                StartCoroutine(ReduceManaCostDuration());
            }

            // Increase Effectiveness of Player's Magic Spells
            if (increasePotencyKey && !isIncreasePotencyAcitve)
            {
                AudioManager.audioManagerRef.PlaySFX(increasePotencySFX);

                playerAnim.SetTrigger("IsUsingOtherDTLOptions");

                isIncreasePotencyAcitve = true;

                StartCoroutine(PotencyIncreaseDuration());
            }

            // Inverse Player's Magic Abilites
            if (inverseKey)
            {
                AudioManager.audioManagerRef.PlaySFX(inverseSFX);
                
                playerAnim.SetTrigger("IsUsingOtherDTLOptions");

                Inverse = !Inverse;
            }

        // View Enemies' Weaknesses and abilites
        assessFunction.ActivateAssess();

        assessFunction.Assess(playerAnim);


    }

    // Add Healing into the Mix
    private IEnumerator PotencyIncreaseDuration()
    {
        fireboltLogic.DAMAGE *= 2;
        fireboltLogic.INVERSE_DAMAGE *= 2;

        cureAbility.InverseDamage *= 2;

        yield return new WaitForSeconds(15);

        isIncreasePotencyAcitve = false;

        fireboltLogic.DAMAGE = firebolt_StartingDamage;
        fireboltLogic.INVERSE_DAMAGE = firebolInverse_StartingDamage;

        cureAbility.InverseDamage = dot_StartingDamage;

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

        cureAbility.ManaCost = restoration_StartingManaCost;
        fireboltAbility.ManaCost = firebolt_StartingManaCost;
        thickSkinnedAbility.ManaCost = thickskinned_StartingManaCost;

        cureAbility.InverseManaCost = inverRestoration_StartingManaCost;
        thickSkinnedAbility.InverseManaCost = inverThickskinned_StartingManaCost;

    }



    public void StoreOGValues()
    {
        firebolt_StartingDamage = fireboltLogic.DAMAGE;
        firebolInverse_StartingDamage = fireboltLogic.INVERSE_DAMAGE;

        dot_StartingDamage = cureAbility.InverseDamage;

        restoration_StartingManaCost = cureAbility.ManaCost;
        thickskinned_StartingManaCost = thickSkinnedAbility.ManaCost;
        firebolt_StartingManaCost = fireboltAbility.ManaCost;

        inverRestoration_StartingManaCost = cureAbility.InverseManaCost;
        inverThickskinned_StartingManaCost = thickSkinnedAbility.InverseManaCost;
    }

}

