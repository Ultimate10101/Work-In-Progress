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

    [SerializeField] private ParticleSystem rockEffect;

    private int duration;

    private bool thickSkinnedKey;

    public bool isActive;

    [SerializeField] private AnimationClip thickSkinned;
    [SerializeField] private AnimationClip inverseThickSkinned;

    void Start()
    {
        // Regular Magic variables

        coolDown = 8.0f;

        manaCost = 20.0f;

        duration = 15;


        // Inverse Magic variables

        inverseCoolDown = 6.0f;

        inverseManaCost = 10.0f;

        
        readyToCast = true;
        readyToInverseCast = true;

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
        thickSkinnedKey = Input.GetKey(KeyCode.Q);
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

            rockEffect.Play();

            AudioManager.audioManagerRef.PlaySFX(magicSFX);

            StartCoroutine(CastDelay());
        }
    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(thickSkinned.length);

        Debug.Log("ThickSkinned Casted");

        rockEffect.Stop();

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
        if (barrier == 0.0f)
        {
            Debug.Log("ThickSkinned barrier was broken");

            ActivateBarrier(false);
            StopCoroutine(DurationHandler());
        }
    }




    // Inverse of Ability

    protected override void InverseCast()
    {
        if (!abilityCurrentlyCasting && readyToInverseCast && thickSkinnedKey && ((playerMana.Mana - inverseManaCost) >= 0.0f))
        {
            AudioManager.audioManagerRef.PlaySFX(inverseMagicSFX);

            Debug.Log("Casting Inverse ThickSkinned");

            readyToInverseCast = false;

            abilityCurrentlyCasting = true;

            playerMana.Mana -= inverseManaCost;

            playerAnim.SetTrigger("IsDebuffing");

            rockEffect.Play();

            StartCoroutine(InverseCastDelay());
        }

        
    }

    protected override IEnumerator InverseCastDelay()
    {
        yield return new WaitForSeconds(inverseThickSkinned.length);

        rockEffect.Stop();

        Debug.Log("Inverse ThickSkinned Casted");

        abilityCurrentlyCasting = false;

        EnemyDoubleDamage();

        StartCoroutine(InverseCoolDownHandler());

    }


    protected override IEnumerator InverseCoolDownHandler()
    {
        yield return new WaitForSeconds(inverseCoolDown);

        readyToInverseCast = true;

        Debug.Log("Inverse ThickSkinned cooldown recharged");
    }


    private void EnemyDoubleDamage()
    {
        Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 30.0f);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            if (!hit.collider.gameObject.GetComponent<E_HealthController>().IsDoubleDamageActive)
            {
                hit.collider.gameObject.GetComponent<E_HealthController>().ActivateDoubleDamage(15);
                hit.collider.gameObject.GetComponent<StatusEffectHandler>().ChangeStateActivity("DAMAGE_TAKEN_INCREASED", true);

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
