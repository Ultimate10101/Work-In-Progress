using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ManageAbility : MonoBehaviour
{

    Def_Ability[] playerAbilites;

    


    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponents<Def_Ability>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
