using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMinigame : MonoBehaviour
{

    public MinigameTemplate myGame;
    FPSPlayerController myPlayer;
    public bool IsTriggered;
    public Animator myAnim;
    public GameObject[] finishTriggerList;

    void Start()
    {
        myPlayer = FindObjectOfType<FPSPlayerController>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsTriggered && !myAnim.GetBool("IsFinished"))
        {
            myAnim.SetBool("IsFinished", true);
            foreach(GameObject obj in finishTriggerList)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
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
