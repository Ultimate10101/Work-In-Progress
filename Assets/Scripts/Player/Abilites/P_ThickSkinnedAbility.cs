using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Barrier Spell for Player

public class P_ThickSkinnedAbility : Def_Ability
{
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

        duration = 15;

        playerMana = gameObject.GetComponent<P_ManaController>();

        isActive = false;

        maxBarrier = barrier;

        barrierText.text = barrier + "/" + maxBarrier;
    }

    void Update()
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
        if(readyToCast && thickSkinnedKey && ((playerMana.Mana - manaCost) >= 0.0f) && !isActive)
        {
            Debug.Log("Casting");
            readyToCast = false;
            playerMana.Mana -= manaCost;

            SetBarrier();

            StartCoroutine(CastDelay());
        }
    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(castTime);

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

}
