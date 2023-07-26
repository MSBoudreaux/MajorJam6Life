using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinigameTemplate
{
    public string title;
    public float duration;
    public GameObject minigamePrefab;
    public Goal myGoal;
    public int startHealth;

    public enum Goal
    {
        Timed,
        ReachEnd,
        Race
    }

}
