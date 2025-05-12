using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSection : MonoBehaviour
{
    private Collider[] col;

    [SerializeField] GameObject battleRestrictionArea;

    public int enemyCount;



    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesInArea();

        if (enemyCount == 0)
        {
            battleRestrictionArea.SetActive(false);
        }
    }


    private void EnemiesInArea()
    {
        col = Physics.OverlapSphere(transform.position, 50, LayerMask.GetMask("Enemy"));

        enemyCount = col.Length;
    }

}
