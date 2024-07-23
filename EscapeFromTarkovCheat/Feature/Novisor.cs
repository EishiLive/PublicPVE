using System;
using UnityEngine;
using EscapeFromTarkovCheat.Utils;

namespace EscapeFromTarkovCheat.Feature
{
    class NoVisor : MonoBehaviour
    {
        public void Update()
        {
            if (Main.GameWorld != null && Settings.NoVisor)
            {
                Novisor();
            }
        }

        void Novisor()
        {
            var mainCamera = Main.MainCamera;
            var visorComponent = mainCamera.GetComponent<VisorEffect>();

            if (visorComponent == null || Mathf.Abs(visorComponent.Intensity - Convert.ToInt32(!true)) < Mathf.Epsilon)
            {
                return;
            }

            visorComponent.Intensity = Convert.ToInt32(!true);
        }
    }
}