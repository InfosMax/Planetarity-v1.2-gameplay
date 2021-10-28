using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Planetarity.PlayerFunctionality;
using System.Collections.Generic;

namespace Planetarity.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject notificationPanel;
        private Text notificationText;
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Text HPText;
        [SerializeField]
        private Canvas playerCanvas;
        [SerializeField]
        private Transform rocketSelectionPanel;

        private Image sliderImage;
        private RealPlayer player;
        public Color camBackgroundColor1;
        public Color camBackgroundColor2;
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private Canvas screenSpaceCanvas;

        public bool IsMainMenuActive { get; set; }

        //private UIBuilder builder;

        private void Awake()
        {
            notificationText = notificationPanel.transform.GetChild(0).GetComponent<Text>();
            sliderImage = slider.transform.GetChild(0).GetComponent<Image>();
        }

        public void Init(RealPlayer player, List<GameObject> allPlayerObjects)
        {
            this.player = player;
            InitPlayerUI();
            AddPlayerWorldCanvases(allPlayerObjects);
        }

        private void InitPlayerUI()
        {
            player.HPchanged += OnPlayerHPChanged;
            player.CooldownChanged += OnPlayerCooldownChanged;

            InitRocketButtonsGraphics();
        }

        private void InitRocketButtonsGraphics()
        {
            var availableRocketTypes = player.GetAvailableRocketTypes();
            Button[] buttons = rocketSelectionPanel.GetComponentsInChildren<Button>();
            for (int i = 0; i < availableRocketTypes.Length; i++)
            {
                //RocketType tempRocketType = availableRocketTypes[i]; //Needs to save value for each closure
                var typeParameters = GameManagement.GameManager.Instance.RocketTypes.GetRocketParameters(availableRocketTypes[i]);
                if(buttons.Length > i)
                {
                    buttons[i].GetComponent<Image>().sprite = typeParameters.picture;
                    buttons[i].onClick.AddListener(() => player.SetRocketType(typeParameters.rocketType));
                }
            }
        }

        private void AddPlayerWorldCanvases(List<GameObject> PlayerObjects)
        {
            foreach(GameObject obj in PlayerObjects)
            {
                Instantiate(playerCanvas, obj.transform, false);
            }
        }

        void OnPlayerHPChanged(float newHP)
        {
            HPText.text = "    Current HP - " + newHP;
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

        public Canvas getMainCanvas()
        {
            return canvas;
        }

        void CameraBackgroundColorLerp()
        {
            screenSpaceCanvas.GetComponent<Image>().color = Color.Lerp(camBackgroundColor1, camBackgroundColor2, Mathf.PingPong(Time.unscaledTime * 0.1f, 1));
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
        private void OnDestroy()
        {
            player.HPchanged -= OnPlayerHPChanged;
            player.CooldownChanged -= OnPlayerCooldownChanged;
        }

        public void ToggleMainMenu()
        {
            IsMainMenuActive = !IsMainMenuActive;
            canvas.transform.Find("Main Menu").gameObject.SetActive(IsMainMenuActive);
        }

    }
}


