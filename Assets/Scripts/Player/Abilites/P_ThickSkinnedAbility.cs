using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Barrier Spell for Player when inverse is not toggled
// Expolsion Spell that targets Enemy when Inverse is not toggled 

public class P_ThickSkinnedAbility : Def_Ability
{

    [SerializeField] private Camera gameCam;

    [SerializeField] private GameObject rocks;
    private RaycastHit hit;

    [SerializeField] private GameObject barrierBG;
    [SerializeField] private Image barrierBar;
    [SerializeField] private TextMeshProUGUI barrierText;

    [SerializeField] private GameObject healthText;

    [SerializeField] private float maxBarrier;
    [SerializeField] private float barrier;

    private int duration;

    private bool thickSkinnedKey;

    public bool isActive;

    void Start()
    {
        castTime = 4.0f;

        coolDown = 8.0f;

        manaCost = 20.0f;

        readyToCast = true;

        inverseCastTime = 6.0f;

        inverseCoolDown = 15.0f;

        inverseManaCost = 25.0f;

        duration = 15;

        playerMana = gameObject.GetComponent<P_ManaController>();

        isActive = false;

        maxBarrier = barrier;

        barrierText.text = barrier + "/" + maxBarrier;
    }

    protected override void Update()
    {
        if(barrierBar.IsActive())
        {
            UpdateBarrierUI_Info();
        }

        CheckBarrierCondtion();

        CastInput();

        Cast();
    }


    protected override void CastInput()
    {
        thickSkinnedKey = Input.GetKey(KeyCode.H);
    }




    protected override void Cast()
    {
        if(!abilityCurrentlyCasting && readyToCast && thickSkinnedKey && ((playerMana.Mana - manaCost) >= 0.0f) && !isActive)
        {
            Debug.Log("Casting");
            readyToCast = false;
            abilityCurrentlyCasting = true;
            playerMana.Mana -= manaCost;

            SetBarrier();

            StartCoroutine(CastDelay());
        }
    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(castTime);

        abilityCurrentlyCasting = false;

        ActivateBarrier(true);

        StartCoroutine(DurationHandler());

        StartCoroutine(CoolDownHandler());
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("Ready to cast agian");
    }

    IEnumerator DurationHandler()
    {
        yield return new WaitForSeconds(duration);

        ActivateBarrier(false);

    }


    // Inverse of Ability

    protected override void InverseCast()
    {
        if (!abilityCurrentlyCasting && readyToCast && thickSkinnedKey && ((playerMana.Mana - inverseManaCost) >= 0.0f) && AimingAtGround())
        {
            readyToCast = false;

            abilityCurrentlyCasting = true;

            playerMana.Mana -= inverseManaCost;

            StartCoroutine(InverseCastDelay());
        }

        
    }

    protected override IEnumerator InverseCastDelay()
    {
        yield return new WaitForSeconds(inverseCastTime);

        Expolsion();

        abilityCurrentlyCasting = false;



    }


    protected override IEnumerator InverseCoolDownHandler()
    {
        yield return new WaitForSeconds(inverseCoolDown);

        readyToCast = true;
    }






    void UpdateBarrierUI_Info()
    {
        barrier = Mathf.Clamp(barrier, 0.0f, maxBarrier);
        barrierBar.fillAmount = Mathf.Clamp(barrier / maxBarrier, 0.0f, 1.0f);
        barrierText.text = barrier + "/" + maxBarrier;
    }

    void ActivateBarrier(bool status)
    {
        barrierBG.SetActive(status);
        isActive = status;
        healthText.SetActive(!status);
    }


    public void TakeDamage(float damage)
    {
        barrier -= damage;
    }

    private void SetBarrier()
    {
        barrier = maxBarrier;
    }

    private void CheckBarrierCondtion()
    {
        if(barrier == 0.0f)
        {
            ActivateBarrier(false);
            StopCoroutine(DurationHandler());
        }
    }


    private bool AimingAtGround()
    {
        Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        return Physics.Raycast(ray, out hit, 75, LayerMask.GetMask("Ground"));

    }

    private void Expolsion()
    {
       Instantiate(rocks, hit.transform.position, rocks.transform.rotation);
    }


}
