using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class P_ManaController : MonoBehaviour
{
    [SerializeField] private Image manaGuage;
    [SerializeField] private TextMeshProUGUI manaText;

    private float maxMana = 50.0f;
    [SerializeField] private float mana;
    [SerializeField] private float manaIncVal;

    public float Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateManaUI_Info();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateManaUI_Info();
    }

    void UpdateManaUI_Info()
    {
        mana = Mathf.Clamp(mana, 0.0f, 50.0f);
        manaGuage.fillAmount = Mathf.Clamp(mana / maxMana, 0.0f, 1.0f);
        manaText.text = mana + "/" + maxMana;
    }

    public void ManaIncrease()
    {
        mana += manaIncVal;
      
    }
}
