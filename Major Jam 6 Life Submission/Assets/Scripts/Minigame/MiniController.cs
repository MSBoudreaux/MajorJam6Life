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
            myManager.currentHealth--;
            if (myManager.currentHealth == 0)
            {
                myManager.EndMinigame();
            }
        }
    }

    public void SetState2D(miniState inState)
    {
        state2D = inState;
    }

}
