using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject[] targetList;
    public bool isPressed  = false;
    public Image mySprite;
    public Sprite upSprite;
    public Sprite downSprite;

    public AudioSource myAudio;
    public void TriggerButton(bool isFromTrigger)
    {
        if (isPressed)
        {
            return;
        }
        else
        {
            
            foreach(GameObject target in targetList)
            {
                if (target.CompareTag("Button") && target.activeSelf)
                {
                    target.GetComponent<ButtonScript>().isPressed = !target.GetComponent<ButtonScript>().isPressed;
                    target.GetComponent<ButtonScript>().TriggerButton(true);
                }
                else
                {
                    target.gameObject.SetActive(!target.gameObject.activeInHierarchy);
                }
            }
        }

        //DO NOT MAKE A BUTTON TRIGGER ITSELF PLEASE NEVER DO THIS I WILL COME TO YOUR HOUSE WHICH IS MY HOUSE AND HIT YOU WITH MY
        if (!isFromTrigger)
        {
            isPressed = false;
            myAudio.Play();
            isPressed = true;
        }

        
    }

    public void Update()
    {
        if (isPressed)
        {
            mySprite.sprite = downSprite;
        }
        else
        {
            mySprite.sprite = upSprite;
        }
    }
}
