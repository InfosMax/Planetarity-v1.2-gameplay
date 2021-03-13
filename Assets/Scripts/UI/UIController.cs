using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Planetarity.UI
{
    public class UIController : MonoBehaviour
    {
        public Color camBackgroundColor1;
        public Color camBackgroundColor2;
        private Canvas canvas;
        //private UIBuilder builder;
        
        void Start()
        {
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
                Debug.Log("Canvas is missing!");
        }
        private void Update()
        {
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
    }
}


