using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject StartGameUI;
    [SerializeField]
    private GameObject GameplayUI;
    [SerializeField]
    private GameObject GameresultUI;

    internal event Action OnTap;

    private void Start()
    {
        StartGameUI.SetActive(true);
    }


    private void Update()
    {
        
    }

    public void OnTapToPlay()
    {
        OnTap?.Invoke();
        StartGameUI.SetActive(false);
        GameplayUI.SetActive(true);
        Debug.Log("OnTapToPlay invoked");
    }
}
