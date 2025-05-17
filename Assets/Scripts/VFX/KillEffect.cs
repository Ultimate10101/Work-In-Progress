using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEffect : MonoBehaviour
{
    [SerializeField] private int killEffectTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, killEffectTime);
    }

 
}
