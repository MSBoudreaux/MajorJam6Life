using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMinigame : MonoBehaviour
{

    public MinigameTemplate myGame;
    FPSPlayerController myPlayer;
    public bool IsTriggered;

    void Start()
    {
        myPlayer = FindObjectOfType<FPSPlayerController>();
    }

    void Update()
    {
        
    }

    public void MinigameTrigger(MinigameTemplate inGame)
    {
        if(IsTriggered == false)
        {
            myPlayer.SetState(FPSPlayerController.State.Minigame);
            FindObjectOfType<MinigameManager>().StartMinigame(myGame, this.GetComponent<TriggerMinigame>());
        }
        else
        {
            Debug.Log("already done this one");
        }

    }
}
