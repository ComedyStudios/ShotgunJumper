using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    
    public float scoreGrowth;
    public TextMeshProUGUI text;
    [HideInInspector]
    public float score;

    public static Score Instance;
    
    private float _lastTick;

    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        text.text = $"Score:{(int)score}";
        if (Time.time - _lastTick >= Constants.Tick)
        {
            _lastTick = Time.time;
            score += scoreGrowth;
        }
    }
}
