﻿using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feature
{
    class Aimbot : MonoBehaviour
    {
        public void Update()
        {
            if ((Main.GameWorld != null))
            {
                if (Settings.Aimbot)
                    Aim();
                    Nosway();
            }
        }

        private void Aim()
        {
            if (Input.GetKey(Settings.AimbotKey))
            {
                Vector3 target = Vector3.zero;
                float distanceOfTarget = 9999f;

                foreach (var player in Main.GamePlayers)
                {
                    if (player == null || player.Player.IsYourPlayer)
                        continue;

                    Vector3 destination = GameUtils.GetBonePosByID(player.Player, 132); //Neck

                    float distance = Vector3.Distance(Main.MainCamera.transform.position, player.Player.Transform.position);
                    if (distance > 200f)
                        continue;

                    if (destination != Vector3.zero && CaulculateInFov(destination) <= Settings.AimbotFOV)
                    {

                        if (distanceOfTarget > distance)
                        {

                            distanceOfTarget = distance;
                            float travelTime = distance / 350f;
                            destination.x += (player.Player.Velocity.x * travelTime);
                            destination.y += (player.Player.Velocity.y * travelTime);

                            target = destination;
                        }
                    }
                }

                if (target != Vector3.zero)
                {
                    AimAtPos(target);
                }
            }

        }

        private void Nosway()
        {
            var lp = Main.LocalPlayer;
            var weapon = lp.ProceduralWeaponAnimation;
            if (weapon == null)
                return;
            weapon.Breath.Intensity = 0;
            weapon.Walk.Intensity = 0;
            weapon.AimSwayMax = Vector3.zero;
            weapon.AimSwayMin = Vector3.zero;
            weapon.ForceReact.Intensity = 0;
            weapon.WalkEffectorEnabled = false;
        }

        public static float CaulculateInFov(Vector3 position1)
        {
            Vector3 position2 = Main.MainCamera.transform.position;
            Vector3 forward = Main.MainCamera.transform.forward;
            Vector3 normalized = (position1 - position2).normalized;
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, normalized), -1f, 1f)) * 57.29578f;
        }

        public static void AimAtPos(Vector3 position)
        {
            Vector3 b = Main.LocalPlayer.Fireport.position - Main.LocalPlayer.Fireport.up * 1f;
            Vector3 eulerAngles = Quaternion.LookRotation((position - b).normalized).eulerAngles;

            if (eulerAngles.x > 180f)
                eulerAngles.x -= 360f;

            Main.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }

    }
}