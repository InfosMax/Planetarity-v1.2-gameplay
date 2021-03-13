using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.UI
{
    public class UIBuilder : MonoBehaviour
    {
        private Canvas canvas;
        void Start()
        {
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
                Debug.Log("Canvas is missing!");
        }

        public Canvas CreateGUI()
        {
            return canvas;
        }

    }
}

