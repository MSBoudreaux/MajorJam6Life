using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MinigameManager : MonoBehaviour
{
    public float timer;
    public MinigameTemplate currentGame;
    public GameObject minigameScene;
    public GameObject overworldUI;
    public int currentHealth;
    public bool win;
    public GameObject playerObj;
    public MiniController player2D;
    public RectTransform templateTarget;
    public GameObject templateInstance;
    public TriggerMinigame currentTrigger;
    public TextMeshProUGUI levelTitle;
    public TextMeshProUGUI displayHealth;
    public TextMeshProUGUI displayTime;

    Coroutine c;
    bool crRunning;
    bool isEnding;
    public void Start()
    {

    }

    public void StartMinigame(MinigameTemplate inGame, TriggerMinigame inGameTrigger)
    {
        currentTrigger = inGameTrigger;
        currentGame = inGame;
        currentHealth = currentGame.startHealth;
        overworldUI.SetActive(false);
        minigameScene.SetActive(true);
        //instantiate level
        templateInstance = Instantiate(currentGame.minigamePrefab, templateTarget);
        playerObj.transform.localPosition = GameObject.FindGameObjectWithTag("2DSpawn").transform.localPosition;

        levelTitle.text = currentGame.title;
        timer = 0;


        FindObjectOfType<MiniController>().SetState2D(MiniController.miniState.Movement);

        if (currentGame.myGoal == MinigameTemplate.Goal.Timed || currentGame.myGoal == MinigameTemplate.Goal.Race)
        {
            c = StartCoroutine(LevelTimer(currentGame.duration));
        }


    }

    public void EndMinigame(bool inWin)
    {
        win = inWin;
        Debug.Log("win status:" + win);
        c = StartCoroutine(EndScreen());
    }

    IEnumerator LevelTimer(float time)
    {
        crRunning = true;
        yield return new WaitForSeconds(time);
        crRunning = false;
        if(currentGame.myGoal == MinigameTemplate.Goal.Timed)
        {
            win = true;
            Debug.Log("won timed");
        }
        else if(currentGame.myGoal == MinigameTemplate.Goal.Race)
        {
            win = false;
            Debug.Log("lost race");
        }
        EndMinigame(win);
    }

    IEnumerator EndScreen()
    {
        isEnding = true;
        if (win)
        {
            FindObjectOfType<MiniController>().SetState2D(MiniController.miniState.Win);
            currentTrigger.IsTriggered = true;
        }
        else
        {
            FindObjectOfType<MiniController>().SetState2D(MiniController.miniState.Lose);
            currentTrigger.IsTriggered = false;
        }
        yield return new WaitForSeconds(3f);

        c = null;
        currentGame = null;
        currentTrigger = null;
        levelTitle.text = null;
        //delete level instance
        Destroy(templateInstance);


        overworldUI.SetActive(true);
        minigameScene.SetActive(false);
        FindObjectOfType<FPSPlayerController>().SetState(FPSPlayerController.State.FreeMovement);
        isEnding = false;

    }

    void Update()
    {
        if (minigameScene.activeSelf)
        {
            if (currentHealth == 0 && !isEnding)
            {
                Debug.Log("died");
                StopAllCoroutines();
                c = null;
                win = false;
                EndMinigame(win);
            }

            displayHealth.text = currentHealth.ToString();
            if (currentGame.duration - timer > 0)
            {
                float currentTime = currentGame.duration - timer;
                displayTime.text = Math.Round(currentTime, 1).ToString();
            }
            else displayTime.text = "0";

            if (crRunning && currentGame.myGoal != MinigameTemplate.Goal.ReachEnd)
            {
                if (timer <= currentGame.duration)
                    timer += Time.deltaTime;
            }


        }


 

    }


}
