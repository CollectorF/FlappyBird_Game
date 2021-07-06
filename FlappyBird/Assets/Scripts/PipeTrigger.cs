using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTrigger : MonoBehaviour
{
    internal event Func<int, int> OnTrigger;
    private int score;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnTrigger?.Invoke(score);
        }
    }
}
