using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    int index = 0;
    public Dialogue[] myDialogue;
    public Dialogue[] mySecondDialogue;
    public TriggerMinigame myTrigger;
    public bool isAutoRead; //in case a trigger causes this dialogue to scroll automatically
    public bool hasResetIndex;

    public void Start()
    {
        myTrigger = GetComponent<TriggerMinigame>();
    }
    public void DialogueTrigger()
    {
        Dialogue[] dialogueToRead;
        if (myTrigger != null)
        {
            if (myTrigger.IsTriggered)
            {
                dialogueToRead = mySecondDialogue;
                if (!hasResetIndex)
                {
                    index = 0;
                    hasResetIndex = true;
                }
            }
            else dialogueToRead = myDialogue;
        }
        else dialogueToRead = myDialogue;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogueToRead[index], gameObject);
        if(index < dialogueToRead.Length - 1)
        {
            index++;
        }
    }

    public void TimedDialogueTrigger(int i)
    {
        if (isAutoRead)
        {
            FindObjectOfType<DialogueManager>().DisplayTimedSentence(myDialogue[i]);
        }
    }

}
