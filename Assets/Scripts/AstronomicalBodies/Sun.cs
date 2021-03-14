using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.AstronomicalBodies
{
    public class Sun : SpaceBody
    {
        public static int ComparingSize { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Speed = 0f;
            InitAppearance();
        }

        protected override void InitAppearance()
        {
            ComparingSize = Size = Random.Range(10, 15);
            transform.localScale *= Size;
        }
    }
}
