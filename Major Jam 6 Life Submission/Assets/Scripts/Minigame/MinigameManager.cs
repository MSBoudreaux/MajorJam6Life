using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public float timer;
    public MinigameTemplate currentGame;
    public GameObject minigameScene;
    public GameObject overworldUI;
    public int currentHealth;
    public bool win;
    public MiniController player2D;

    Coroutine c;
    public void Start()
    {
    }
    public void StartMinigame(MinigameTemplate inGame)
    {
        
        currentGame = inGame;
        currentHealth = 5;
        overworldUI.SetActive(false);
        minigameScene.SetActive(true);
        FindObjectOfType<MiniController>().SetState2D(MiniController.miniState.Movement);

        c = StartCoroutine(LevelTimer(currentGame.duration));

    }

    public void EndMinigame()
    {
        c = StartCoroutine(EndScreen());
    }

    IEnumerator LevelTimer(float time)
    {
        yield return new WaitForSeconds(time);
        win = true;
        EndMinigame();
    }

    IEnumerator EndScreen()
    {
        if (win)
        {
            FindObjectOfType<MiniController>().SetState2D(MiniController.miniState.Win);
        }
        else
        {
            FindObjectOfType<MiniController>().SetState2D(MiniController.miniState.Lose);
        }

        yield return new WaitForSeconds(3f);

        c = null;
        currentGame = null;
        overworldUI.SetActive(true);
        minigameScene.SetActive(false);
        FindObjectOfType<FPSPlayerController>().SetState(FPSPlayerController.State.FreeMovement);

    }

    void Update()
    {
        if (c != null)
        {
            timer += Time.deltaTime;

            if (currentHealth == 0)
            {
                StopCoroutine(c);
                win = false;
                EndMinigame();
            }
        }

    }
}
