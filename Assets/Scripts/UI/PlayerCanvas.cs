using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Planetarity.PlayerFunctionality;


namespace Planetarity.UI
{
    public class PlayerCanvas : MonoBehaviour
    {
        private Player player;

        private Slider slider;
        private Text UItext;

        private void Start()
        {
            player = GetComponentInParent<Player>();
            slider = transform.Find("Panel/Slider").GetComponent<Slider>();
            UItext = transform.Find("Panel/Player HP").GetComponent<Text>();

            player.HPchanged += OnPlayerHPChanged;
            player.CooldownChanged += OnPlayerCooldownChanged;

        }

        void OnPlayerHPChanged(float newHP)
        {
            if(player is RealPlayer)
            {
                UItext.text = "Your HP - " + newHP;
            }
            else
            {
                UItext.text = "Enemy HP - " + newHP;
            }
        }

        void OnPlayerCooldownChanged(float newCD)
        {
            slider.maxValue = newCD;
            slider.value = newCD;
        }
        void Update()
        {
            alignUI();
            if (slider.value > 0)
            {
                slider.value -= Time.deltaTime;
            }
        }

        private void OnDestroy()
        {
            player.HPchanged -= OnPlayerHPChanged;
            player.CooldownChanged -= OnPlayerCooldownChanged;
        }

        private void alignUI()
        {
            Quaternion newQuaternion = Quaternion.Euler(0f, 0f, -transform.rotation.z);
            GetComponent<RectTransform>().rotation = newQuaternion;
        }
    }
}


