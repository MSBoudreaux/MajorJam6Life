using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniController : MonoBehaviour
{
    public enum miniState
    {
        Movement,
        Win,
        Lose
    }
    public float speed;
    public Rigidbody2D rb;
    public bool isActive;
    public Animator myAnim;
    public MinigameManager myManager;

    public miniState state2D;

    void Update()
    {
        switch (state2D)
        {
            case miniState.Movement:
            {
                    if (FindObjectOfType<FPSPlayerController>().myState == FPSPlayerController.State.Minigame)
                    {
                        isActive = true;
                    }
                    else isActive = false;

                    if (isActive)
                    {
                        float x = Input.GetAxis("Horizontal");
                        float y = Input.GetAxis("Vertical");

                        Vector2 move = new Vector2(x * speed, y * speed);
                        rb.velocity = move;
                    }

                    myAnim.SetFloat("Speed", rb.velocity.magnitude);
                    return;
                }

            case miniState.Win:
                {
                    myAnim.SetBool("hasWon", true);
                    myAnim.SetTrigger("Game End");
                    rb.velocity = new Vector2(0, 0);
                    return;
                }

            case miniState.Lose:
                {
                    myAnim.SetBool("hasWon", false);
                    myAnim.SetTrigger("Game End");
                    rb.velocity = new Vector2(0, 0);
                    return;
                }


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            if(myManager.currentHealth > 0)
            {
                Debug.Log("got hit");
                myManager.currentHealth--;
                if (collision.GetComponent<BulletScript>())
                {
                    Destroy(collision.gameObject);
                }
            }
        }
        else if (collision.CompareTag("Button"))
        {
            Debug.Log("pressed button");
            collision.transform.GetComponent<ButtonScript>().TriggerButton(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish") && (myManager.currentGame.myGoal == MinigameTemplate.Goal.ReachEnd || myManager.currentGame.myGoal == MinigameTemplate.Goal.Race))
        {
            myManager.EndMinigame(true);
        }
        else if (collision.transform.CompareTag("Hitbox"))
        {
            if (myManager.currentHealth > 0)
            {
                Debug.Log("got hit");
                myManager.currentHealth--;
                if (collision.transform.GetComponent<BulletScript>())
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    public void SetState2D(miniState inState)
    {
        state2D = inState;
    }

}
