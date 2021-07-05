using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonus : MonoBehaviour
{
    internal event Action<string> OnBonusTrigger;
    internal void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var bonusType = other.tag;
            OnBonusTrigger?.Invoke(bonusType);
            Destroy(gameObject);
        }
    }
}
