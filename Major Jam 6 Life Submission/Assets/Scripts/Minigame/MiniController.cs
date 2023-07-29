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
    public bool isIFrames;
    public float iFrameTime;

    public AudioSource myAudioSource;
    public AudioClip[] mySounds;
    public bool soundPlayed = false;
    public bool hurtSoundPlayed = false;

    void FixedUpdate()
    {
        switch (state2D)
        {
            case miniState.Movement:
            {
                    if (FindObjectOfType<FPSPlayerController>().myState == FPSPlayerController.State.Minigame)
                    {
                        isActive = true;
                        soundPlayed = false;
                    }
                    else isActive = false;

                    if (isActive)
                    {
                        myAudioSource.clip = mySounds[0];
                        float x = Input.GetAxis("Horizontal");
                        float y = Input.GetAxis("Vertical");

                        Vector2 move = new Vector2(x * speed, y * speed);
                        rb.velocity = move;
                    }

                    myAnim.SetFloat("Speed", rb.velocity.magnitude);

                    if(isIFrames)
                    {
                        myAnim.SetBool("IFrames", true);
                        if (!hurtSoundPlayed)
                        {
                            myAudioSource.clip = mySounds[1];
                            myAudioSource.Play();
                        }
                        hurtSoundPlayed = true;
                        Debug.Log("Playing audio clip" + myAudioSource.clip.name);
                        myAnim.SetBool("IFrames", true);
                        StartCoroutine(iFrames(iFrameTime));

                    }

                    return;
                }

            case miniState.Win:
                {
                    if (myManager.currentGame.myGoal == MinigameTemplate.Goal.Timed)
                    {
                        if (!soundPlayed)
                        {
                            myAudioSource.clip = mySounds[3];
                            myAudioSource.Play();
                            Debug.Log("Playing audio clip" + myAudioSource.clip.name);

                        }
                        soundPlayed = true;
                        myAnim.SetBool("hasWonTimed", true);
                        myAnim.SetTrigger("Game End");
                        rb.velocity = new Vector2(0, 0);
                        return;
                    }
                    else
                    {
                        if (!soundPlayed)
                        {
                            myAudioSource.clip = mySounds[2];
                            myAudioSource.Play();
                            Debug.Log("Playing audio clip" + myAudioSource.clip.name);
                        }
                        soundPlayed = true;
                        myAnim.SetBool("hasWon", true);
                        myAnim.SetTrigger("Game End");
                        rb.velocity = new Vector2(0, 0);
                        return;
                    }

                }

            case miniState.Lose:
                {
                    if (!soundPlayed)
                    {
                        myAudioSource.clip = mySounds[4];
                        myAudioSource.Play();
                        Debug.Log("Playing audio clip" + myAudioSource.clip.name);

                    }
                    soundPlayed = true;

                    myAnim.SetBool("hasWon", false);
                    myAnim.SetTrigger("Game End");
                    rb.velocity = new Vector2(0, 0);
                    return;
                }


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox") && !isIFrames)
        {
            if(myManager.currentHealth > 0)
            {
                Debug.Log("got hit");
                myManager.currentHealth--;

                isIFrames = true;


            }
            if (collision.GetComponent<BulletScript>())
            {
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("Button"))
        {
            Debug.Log("pressed button");
            collision.transform.GetComponent<ButtonScript>().TriggerButton(false);
        }
    }

    IEnumerator iFrames(float time)
    {
        Debug.Log("Iframes Starting! IsIFrames = " + isIFrames.ToString() + ", hurtSoundPlayed = " + hurtSoundPlayed.ToString());
        yield return new WaitForSeconds(time);
        Debug.Log("Iframes Over! IsIFrames = " + isIFrames.ToString() + ", hurtSoundPlayed = " + hurtSoundPlayed.ToString());
        myAnim.SetBool("IFrames", false);
        hurtSoundPlayed = false;
        isIFrames = false;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Animator>())
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("TriggerAnim");
        }

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
