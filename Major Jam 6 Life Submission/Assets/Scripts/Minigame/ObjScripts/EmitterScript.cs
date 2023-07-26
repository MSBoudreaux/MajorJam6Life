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
    public Transform emitter;
    public AnimatorOverrideController animOverride;
    public void Shoot()
    {
        GetComponent<Animator>().SetTrigger("ShootEvent");
        angleToShoot = Vector3.Normalize(emitter.position - transform.position);
        GameObject bulletToShoot = Instantiate(bullet, emitter.position, Quaternion.Euler(angleToShoot), transform);
        //bulletToShoot.transform.SetParent(null);
        bulletToShoot.transform.GetComponent<Rigidbody2D>().AddForce(angleToShoot * bulletSpeed, ForceMode2D.Impulse);
        
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
