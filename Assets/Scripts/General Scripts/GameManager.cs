using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerRef;

    private void Awake()
    {
        if (gameManagerRef == null)
        {
            gameManagerRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }



    public enum PlayerInSection
    {
        PLAYER_IN_COMBAT,
        PLAYER_EXPLORING,

    }

    private PlayerInSection playerInSection;

    // Start is called before the first frame update
    void Start()
    {
        playerInSection = PlayerInSection.PLAYER_EXPLORING;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void PlayerInSectionTransition()
    {
        switch (playerInSection)
        {
            case PlayerInSection.PLAYER_IN_COMBAT:
                break;
            case PlayerInSection.PLAYER_EXPLORING:
                break;
        }


        // Changes Player States and Set variables
    }




}
