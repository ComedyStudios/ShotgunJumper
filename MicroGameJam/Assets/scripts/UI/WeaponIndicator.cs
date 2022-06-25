using System;
using GameMechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponIndicator: MonoBehaviour
    {
        private Slider _cooldownSlider;
        private Image _image;
        private TextMeshProUGUI _text;
        private void Start()
        {
            _cooldownSlider = GetComponent<Slider>();
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            _cooldownSlider.value = Math.Max(AttackScript.Instance.currentWeapon.hitCooldown - (Time.time - AttackScript.Instance.lastHit),0) /
                                   AttackScript.Instance.currentWeapon.hitCooldown;
            _image.sprite = AttackScript.Instance.currentWeapon.icon;
            _text.text = AttackScript.Instance.currentWeapon.itemName;
        }
    }
}