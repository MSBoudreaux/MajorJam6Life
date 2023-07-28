using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTurn : MonoBehaviour
{
    FPSPlayerController myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<FPSPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angleToTurn = new Vector3();
        angleToTurn = Vector3.Normalize(transform.position - myPlayer.transform.position);
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(angleToTurn));
    }
}
