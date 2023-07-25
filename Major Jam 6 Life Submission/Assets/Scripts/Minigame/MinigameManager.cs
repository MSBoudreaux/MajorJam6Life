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
    public GameObject playerObj;
    public MiniController player2D;
    public RectTransform templateTarget;
    public GameObject templateInstance;

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
        playerObj.transform.localPosition = new Vector3(0, 0, 0);
        //instantiate level
        templateInstance = Instantiate(currentGame.minigamePrefab, templateTarget);

        FindObjectOfType<MiniController>().SetState2D(MiniController.miniState.Movement);



        if (currentGame.myGoal == MinigameTemplate.Goal.Timed)
        {
            c = StartCoroutine(LevelTimer(currentGame.duration));
        }


    }

    public void EndMinigame(bool inWin)
    {
        win = inWin;
        c = StartCoroutine(EndScreen());
    }

    IEnumerator LevelTimer(float time)
    {
        yield return new WaitForSeconds(time);
        win = true;
        EndMinigame(win);
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

        //delete level instance
        Destroy(templateInstance);


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
                EndMinigame(win);
            }
        }

    }


}
