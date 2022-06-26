using System;
using System.Collections;
using System.Collections.Generic;
using GameMechanics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayerState : MonoBehaviour
{
   public static PlayerState instance;
   public int maxHealth;
   public int healthReduction;
   public float defense;
   public TextMeshProUGUI text;
   public GameObject healthBar;
   public GameObject gameOverCanvas;
   public GameObject hud;
   
   private Slider _slider;

   
   public int health;
   
   private float _time;

   
   private void Awake()
   {
      instance = this;
      health = maxHealth;
      _time = Time.time;
      _slider = healthBar.GetComponent<UnityEngine.UI.Slider>();

   }

   void Update()
   {
      text.text = health.ToString();
      _slider.value = (float)health / (float)maxHealth;
      if (Time.time - _time >= Constants.Tick)
      {
         _time = Time.time;
         health -= healthReduction;
      }
      if (health <= 0)
      {
         hud.SetActive(false);
         gameOverCanvas.SetActive(true);
         GetComponent<PlayerMovement>().enabled = false;
      }
   }
}
