using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class DisplayTime : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        worldTime.WorldTimeChanged += OnWorldTimeChanged;
    }
    private void OnDestroy()
    {
        worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }
    private void OnWorldTimeChanged(object sender, TimeSpan newTime)
    {
        text.SetText(newTime.ToString(@"hh\:mm"));
    }
}

