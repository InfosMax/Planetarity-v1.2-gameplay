using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.AstronomicalBodies
{
    public class Sun : SpaceBody
    {
        public new static int Size { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Speed = 0f;
            InitAppearance();
        }

        protected override void InitAppearance()
        {
            Size = Random.Range(10, 15);
            transform.localScale *= Size;
        }
    }
}
