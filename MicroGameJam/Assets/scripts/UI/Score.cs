using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    
    public float scoreGrowth;
    public TextMeshProUGUI text;
    [HideInInspector]
    public float score;
    
    private float _lastTick;

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
