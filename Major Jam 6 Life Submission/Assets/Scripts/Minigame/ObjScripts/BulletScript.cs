using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifespan;
    public AnimatorOverrideController myAnim;
    private void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = myAnim;
        GetComponent<Animator>().SetTrigger("StartAnim");
        StartCoroutine(destroyAfterTime(lifespan));
    }

    IEnumerator destroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
