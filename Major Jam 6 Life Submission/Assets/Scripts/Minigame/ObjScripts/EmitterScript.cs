using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmitterScript : MonoBehaviour
{
    public GameObject bullet;
    GameObject bulletToShoot;
    public Vector3 angleToShoot;
    public float bulletSpeed;
    public float shootDelay;
    public float bulletLife;
    public Transform emitter;
    public AnimatorOverrideController animOverride;
    public float timeOffset; // best set to half of shootDelay


    public bool shootTrigger;
    public void Shoot()
    {
        shootTrigger = false;
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetTrigger("ShootEvent");
        }
        if (!shootTrigger && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
            shootTrigger = true;
        }

        angleToShoot = Vector3.Normalize(emitter.position - transform.position);
        GameObject bulletToShoot = Instantiate(bullet, emitter.position, Quaternion.Euler(angleToShoot), transform);
        bulletToShoot.GetComponent<RectTransform>().SetPositionAndRotation(bulletToShoot.GetComponent<RectTransform>().position,Quaternion.Euler(new Vector3(angleToShoot.x, angleToShoot.y, angleToShoot.z * -90)));
        bulletToShoot.GetComponent<BulletScript>().lifespan = bulletLife;
        bulletToShoot.transform.GetComponent<Rigidbody2D>().AddForce(angleToShoot * bulletSpeed, ForceMode2D.Impulse);
        
    }

    public void Start()
    {
        if (!GetComponent<Animator>())
        {
            StartCoroutine(Offset(timeOffset));
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = animOverride;
            StartCoroutine(Offset(timeOffset));
        }

    }

    IEnumerator Offset(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(ShootWait(shootDelay));
    }

    IEnumerator ShootWait(float time)
    {

        yield return new WaitForSeconds(time);
        Shoot();
        StartCoroutine(ShootWait(shootDelay));
    }


}
