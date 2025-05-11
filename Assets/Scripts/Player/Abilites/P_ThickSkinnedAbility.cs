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
    [SerializeField] private GameObject barrierBG;
    [SerializeField] private Image barrierBar;
    [SerializeField] private TextMeshProUGUI barrierText;

    [SerializeField] private GameObject healthText;

    [SerializeField] private float maxBarrier;
    [SerializeField] private float barrier;

    private int duration;

    private bool thickSkinnedKey;

    public bool isActive;

    private 

    void Start()
    {
        // Regular Magic variables
        castTimeLengthOffset = 1.0f;

        coolDown = 8.0f;

        manaCost = 20.0f;

        duration = 15;


        // Inverse Magic variables
        inverseCastTimeLengthOffset = 1.0f;

        inverseCoolDown = 1.0f;

        inverseManaCost = 0.0f;

        
        readyToCast = true;

        playerMana = gameObject.GetComponent<P_ManaController>();

        isActive = false;

        maxBarrier = barrier;

        barrierText.text = barrier + "/" + maxBarrier;
    }

    protected override void Update()
    {
        base.Update();

        if (barrierBar.IsActive())
        {
            UpdateBarrierUI_Info();
            CheckBarrierCondtion();
        }

        
    }


    protected override void CastInput()
    {
        thickSkinnedKey = Input.GetKey(KeyCode.H);
    }




    protected override void Cast()
    {
        if(!abilityCurrentlyCasting && readyToCast && thickSkinnedKey && ((playerMana.Mana - manaCost) >= 0.0f) && !isActive)
        {
            Debug.Log("Casting ThickSkinned");
            readyToCast = false;
            abilityCurrentlyCasting = true;
            playerMana.Mana -= manaCost;

            SetBarrier();

            playerAnim.SetTrigger("IsShielding");

            StartCoroutine(CastDelay());
        }
    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length - castTimeLengthOffset);

        Debug.Log("ThickSkinned Casted");

        abilityCurrentlyCasting = false;

        ActivateBarrier(true);

        StartCoroutine(DurationHandler());

        StartCoroutine(CoolDownHandler());
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("ThickSkinned cooldown recharged");
    }

    IEnumerator DurationHandler()
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("ThickSkinned Duration over");

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
            Debug.Log("ThickSkinned barrier was broken");

            ActivateBarrier(false);
            StopCoroutine(DurationHandler());
        }
    }


<<<<<<< Updated upstream
    private bool AimingAtGround()
=======


    // Inverse of Ability

    protected override void InverseCast()
    {
        if (!abilityCurrentlyCasting && readyToCast && thickSkinnedKey && ((playerMana.Mana - inverseManaCost) >= 0.0f))
        {
            Debug.Log("Casting Inverse ThickSkinned");

            readyToCast = false;

            abilityCurrentlyCasting = true;

            playerMana.Mana -= inverseManaCost;

            playerAnim.SetTrigger("IsDebuffing");

            StartCoroutine(InverseCastDelay());
        }

        
    }

    protected override IEnumerator InverseCastDelay()
    {
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length - inverseCastTimeLengthOffset);

        Debug.Log("Inverse ThickSkinned Casted");

        abilityCurrentlyCasting = false;

        EnemyDoubleDamage();

        StartCoroutine(InverseCoolDownHandler());

    }


    protected override IEnumerator InverseCoolDownHandler()
    {
        yield return new WaitForSeconds(inverseCoolDown);

        readyToCast = true;

        Debug.Log("Inverse ThickSkinned cooldown recharged");
    }


    private void EnemyDoubleDamage()
>>>>>>> Stashed changes
    {
        Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 30.0f);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            if (!hit.collider.gameObject.GetComponent<E_HealthController>().IsDoubleDamageActive)
            {
                hit.collider.gameObject.GetComponent<E_HealthController>().ActivateDoubleDamage(15);

                Debug.Log("Enemy hit and effect active");
            }
            else
            {
                Debug.Log("Effect Already Active on this target");
            }
        }
        else
        {
            Debug.Log("No valid Target was hit - double damage effect");
        }


    }


}
