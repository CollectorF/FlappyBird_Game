using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonus : MonoBehaviour
{
    [SerializeField]
    private int scoreForBotusCatch = 2;

    private bool bonusIsActivated = false;
    internal event Action<string, int> OnBonusTrigger;

    internal void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !bonusIsActivated)
        {
            bonusIsActivated = true;
            var bonusType = tag;
            OnBonusTrigger?.Invoke(bonusType, scoreForBotusCatch);
            Destroy(gameObject);
        }
    }
}
