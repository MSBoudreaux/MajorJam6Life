using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    public GameObject bullet;
    GameObject bulletToShoot;
    public float angleToShoot;
    public float bulletSpeed;
    public float shootDelay;
    public Transform emitter;
    public AnimatorOverrideController animOverride;
    public void Shoot()
    {
        GetComponent<Animator>().SetTrigger("ShootEvent");
        GameObject bulletToShoot = Instantiate(bullet, emitter.position, Quaternion.Euler(0, 0, angleToShoot), transform);
        bulletToShoot.transform.GetComponent<Rigidbody2D>().AddForce(-transform.up * bulletSpeed, ForceMode2D.Impulse);
        //bulletToShoot.transform.SetParent(null);
    }

    public void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = animOverride;
        StartCoroutine(ShootWait(shootDelay));
    }

    IEnumerator ShootWait(float time)
    {

        yield return new WaitForSeconds(time);
        Shoot();
        StartCoroutine(ShootWait(shootDelay));
    }


}
