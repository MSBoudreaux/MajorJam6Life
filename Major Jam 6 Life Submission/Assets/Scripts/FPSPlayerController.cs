using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{

    public enum State
    {
        FreeMovement,
        Talking,
        Menu,
        Cutscene,
        Minigame
    }

    public State myState;
    public GameObject self;

    //movement data
    public CharacterController controller;

    public float speed = 12f;
    public Camera myCam;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float height = 1f;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    public bool isGrounded = true;

    public GameObject interactable;

    public GameObject pauseMenu;

    void Start()
    {
        
    }

    void Update()
    {
        switch (myState)
        {
            case State.FreeMovement:
            
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("Menu Opened");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    myState = State.Menu;
                    pauseMenu.SetActive(true);

                    break;
                }

                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if(isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }

                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 move = new Vector3();
                move = transform.right * x + transform.forward * z;
                controller.Move(move * speed * Time.deltaTime);

                if(Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2);
                }

                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);

                LookForward();

                if (Input.GetKeyDown("e") && interactable != null)
                {
                    SetState(State.Talking);
                    TriggerDialogue dialogue = interactable.gameObject.GetComponentInParent<TriggerDialogue>();
                    dialogue.DialogueTrigger();
                }

                break;
                

            case State.Talking:

                if (Input.GetKeyDown("e"))
                {
                    Debug.Log("Display next sentence.");
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
                break;

            case State.Menu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    myState = State.FreeMovement;
                    pauseMenu.SetActive(false);
                }
                break;
        }

    }

    private GameObject LookForward()
    {
        Vector3 start = myCam.transform.position;
        Vector3 forward = myCam.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(start, forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                interactable = hit.collider.gameObject;
            }
            else interactable = null;
        }

        return interactable;

    }

    public void SetState(State inState)
    {
        FindObjectOfType<FPSPlayerController>().myState = inState;
    }

    public static State getState()
    {
        return FindObjectOfType<FPSPlayerController>().myState;
    }


}
