using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI popupText;
    public GameObject background;
    public GameObject myTrigger;
    public float waitTime = 4f;

    Coroutine c;

    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue myDialogue, GameObject inObj)
    {
        background.SetActive(true);
        Debug.Log("Trigger dialogue: " + myDialogue.Name);

        nameText.text = myDialogue.Name;

        sentences.Clear();
        foreach(string sentence in myDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        myTrigger = inObj;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        TriggerMinigame inGame = myTrigger.GetComponent<TriggerMinigame>();
        Debug.Log("done talking");
        if (myTrigger.GetComponent<TriggerDialogue>().IsIntroTrigger){
            FindObjectOfType<FPSPlayerController>().SetState(FPSPlayerController.State.FreeMovement);
        }
        if (inGame != null && inGame.IsTriggered == false)
        {
            inGame.MinigameTrigger(inGame.myGame);
            myTrigger = null;
        }
        else
        {
            FindObjectOfType<FPSPlayerController>().SetState(FPSPlayerController.State.FreeMovement);
            myTrigger = null;

        }

        nameText.text = null;
        dialogueText.text = null;
        background.SetActive(false);

    }

    public void DisplayTimedSentence(Dialogue myDialogue)
    {
        c = null;
        sentences.Clear();
        sentences.Enqueue(myDialogue.sentences[0]);
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        popupText.text = sentence;
        c = StartCoroutine(clearSentence(waitTime));
    }

    IEnumerator clearSentence(float time)
    {
        yield return new WaitForSeconds(time);
        popupText.text = null;
    }
}
