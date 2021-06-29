using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTrigger : MonoBehaviour
{
    internal event Func<int> OnTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger?.Invoke();
    }
}
