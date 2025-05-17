using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSection : MonoBehaviour
{
    public Collider[] col;

    [SerializeField] private GameObject[] battleRestrictionWalls;


    private bool bossEncounterActive;

    // Start is called before the first frame update
    void Start()
    {
       bossEncounterActive = false;
       
        ToggleWallMessageState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!BossAlive() && bossEncounterActive)
        {
            battleRestrictionWalls[3].SetActive(false);
            ToggleWallMessageState(false);
        }
    }


    private bool BossAlive()
    {
        col = Physics.OverlapSphere(transform.position, 100, LayerMask.GetMask("Boss"));

        if(col.Length > 0 )
        {
            return true;
        }
        return false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            battleRestrictionWalls[0].SetActive(true);
            bossEncounterActive = true;
            ToggleWallMessageState(true);
        }
        
    }


    private void ToggleWallMessageState(bool active)
    {

        foreach (GameObject item in battleRestrictionWalls)
        {
            item.GetComponent<RestrictionWalls>().enabled = active;
        }
    }

}
