using System;
using EFT;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feature
{
    class NoRecoil : MonoBehaviour
    {
        public void Update()
        {
            if (Main.GameWorld != null && Settings.NoRecoil)
            {
                Norecoil();
            }
        }

        private void Norecoil()
        {
            var lp = Main.LocalPlayer;

            if (lp == null)
            {
                Debug.LogWarning("LocalPlayer is null");
                return;
            }

            var weapon = lp.ProceduralWeaponAnimation;

            if (Main.GameWorld == null)
            {
                Debug.LogWarning("GameWorld is null");
                return;
            }

            if (Main.MainCamera == null)
            {
                Debug.LogWarning("MainCamera is null");
                return;
            }

            if (Main.GamePlayers.IsNullOrEmpty())
            {
                Debug.LogWarning("GamePlayers is null or empty");
                return;
            }

            if (weapon == null)
            {
                Debug.LogWarning("Weapon is null");
                return;
            }

            if (weapon.Shootingg == null)
            {
                Debug.LogWarning("Weapon.Shootingg is null");
                return;
            }

            if (weapon.Shootingg.NewShotRecoil == null)
            {
                Debug.LogWarning("Weapon.Shootingg.NewShotRecoil is null");
                return;
            }

            weapon.Shootingg.NewShotRecoil.RecoilEffectOn = false;

            Debug.Log("No Recoil applied");
        }
    }
}

