using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public struct MedalProps
{
    public string Type;
    public int Score;
    public Sprite Medal;
}

public class ScoreboardMedals : MonoBehaviour
{
    [SerializeField]
    private List<MedalProps> medals;
    [SerializeField]
    private Image medal;

    internal Image SetMedal(int score)
    {
        foreach (var item in medals)
        {
            if (score >= item.Score)
            {
                medal.enabled = true;
                medal.sprite = item.Medal;
            }
        }
        return medal;
    }
}
