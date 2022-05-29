using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayerState : MonoBehaviour
{
   public float maxHelth;
   public float health;
   public float healthReduction;
   public TextMeshProUGUI text;
   public GameObject healthBar; 
   private float time;
   private Slider _slider;
   
   
   private void Start()
   {
      health = maxHelth;
      time = Time.time;
      _slider = healthBar.GetComponent<UnityEngine.UI.Slider>();

   }

   void Update()
   {
      text.text = health.ToString();
      _slider.value = health / maxHelth;
      if (Time.time - time >= Constants.Tick)
      {
         time = Time.time;
         health -= healthReduction;
      }
      if (health <= 0)
      {
         //TODO: game over
      }
   }
}
