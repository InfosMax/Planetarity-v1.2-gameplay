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


        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Text HPValueText;
        [SerializeField]
        private Text HPTitleText;

        private Quaternion zeroRotation = Quaternion.Euler(Vector3.zero);
        private Transform cachedTransform;

        private void Start()
        {
            cachedTransform = transform;
            player = GetComponentInParent<Player>();

            player.HPchanged += OnPlayerHPChanged;
            player.CooldownChanged += OnPlayerCooldownChanged;

            //set equal size of all PlanetCanvases relative to parent
            transform.localScale *= Mathf.Max(1f, 8f/player.transform.localScale.x);
        }

        void OnPlayerHPChanged(float newHP)
        {
            HPValueText.text = newHP.ToString();
        }

        void OnPlayerCooldownChanged(float newCD)
        {
            slider.maxValue = newCD;
            slider.value = newCD;
        }
        void Update()
        {
            cachedTransform.rotation = zeroRotation;
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

        public void InitMainPlayerUI()
        {
            HPTitleText.text = "Your HP -";

            HPValueText.color = Color.green;
            HPTitleText.color = Color.green;
        }
    }
}


