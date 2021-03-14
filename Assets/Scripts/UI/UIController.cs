using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Planetarity.PlayerFunctionality;

namespace Planetarity.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject notificationPanel;
        private bool isRoutineShowing;
        private Text notificationText;
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Text HPtext;
        [SerializeField]
        private Canvas playerCanvas;

        private Image sliderImage;
        private Player player;
        private GameManagement.GameManager GM;
        public Color camBackgroundColor1;
        public Color camBackgroundColor2;
        [SerializeField]
        private Canvas canvas;
        //private UIBuilder builder;

        private void Start()
        {
            notificationText = notificationPanel.transform.GetChild(0).GetComponent<Text>();
            GM = GameManagement.GameManager.Instance;
            player = GM.Player;
            player.HPchanged += OnPlayerHPChanged;
            player.CooldownChanged += OnPlayerCooldownChanged;

            sliderImage = slider.transform.GetChild(0).GetComponent<Image>();

            addPlayerWorldCanvases();
        }

        private void addPlayerWorldCanvases()
        {
            foreach(GameObject planet in GM.GetPlanets())
            {
                Instantiate(playerCanvas, planet.transform, false);
            }
        }

        void OnPlayerHPChanged(float newHP)
        {
            HPtext.text = "    Current HP - " + newHP;
        }

        void OnPlayerCooldownChanged(float newCD)
        {
            slider.maxValue = newCD;
            slider.value = newCD;
            sliderImage.color = new Color(1f, 0.5f, 0f);
        }

        private void Update()
        {
            if (slider.value > 0)
            {

                slider.value -= Time.deltaTime;
                if (slider.value <= 0)
                    sliderImage.color = Color.green;
            }

            CameraBackgroundColorLerp();
        }

        public Canvas getCanvas()
        {
            return canvas;
        }

        void CameraBackgroundColorLerp()
        {
            canvas.GetComponent<Image>().color = Color.Lerp(camBackgroundColor1, camBackgroundColor2, Mathf.PingPong(Time.unscaledTime * 0.1f, 1));
        }

        public void ShowNotification(string text)
        {
            notificationText.text = text;
            
            StartCoroutine("ShowNotificationEnum", text);
        }

        private IEnumerator ShowNotificationEnum(string text)
        {
            notificationPanel.SetActive(true);
            yield return new WaitForSeconds(text.Length / 3f + 1f);
            notificationPanel.SetActive(false);
        }
    }
}


