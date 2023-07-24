using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    int index = 0;
    public Dialogue[] myDialogue;
    public bool isAutoRead; //in case a trigger causes this dialogue to scroll automatically

    public void DialogueTrigger()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(myDialogue[index], this.gameObject);
        if(index < myDialogue.Length - 1)
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
