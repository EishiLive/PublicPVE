using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Visual;
using EscapeFromTarkovCheat.Feature;
using UnityEngine;

namespace EscapeFromTarkovCheat.Utils
{
    class Settings
    {
        internal static KeyCode Skills = KeyCode.Keypad0;
        internal static bool DrawLootItems = true;
        internal static bool DrawLootableContainers = true;
        internal static bool DrawExfiltrationPoints = true;

        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = true;
        internal static bool DrawPlayerHealth = false;
        internal static bool DrawPlayerBox = false;
        internal static bool DrawPlayerLine = false;
        internal static bool DrawPlayerSkeleton = true;
        internal static bool NoVisor = true;
        public static Color SkeletonColor = Color.red;
        public static Color VisibleSkeletonColor = Color.green;
        public static Color CommonItemColor = Color.white;
        public static Color RareItemColor = new Color(0.38f, 0.43f, 1f);
        public static Color SuperRareItemColor = new Color(1f, 0.29f, 0.36f);
        public static Color QuestItemColor = Color.yellow;

        internal static float DrawLootItemsDistance = 300f;
        internal static float DrawLootableContainersDistance = 10f;
        internal static float DrawPlayersDistance = 200f;
        internal static float DrawPlayerSkeletonDistance = DrawPlayersDistance;
        internal static bool NoRecoil = true;

        // Paramètres Aimbot
        internal static bool Aimbot = true;
        internal static KeyCode AimbotKey = KeyCode.LeftControl;
        internal static float AimbotFOV = 10f;
        internal static float AimbotSmooth = 50f;
        internal static bool AimbotDrawFOV = true;
        internal static float AimbotMaxDistance = 200f; // Maximum distance for aimbot targeting
        internal static float AimbotSilentAimNextShotDelay = 0.25f; // Delay between silent aim shots
        internal static float AimbotSilentAimSpeedFactor = 100f; // Speed factor for silent aim

        internal static KeyCode InstaHeal = KeyCode.J;
        internal static KeyCode UnlockDoors = KeyCode.K;
        internal static KeyCode KillAll = KeyCode.L;
      //internal static KeyCode Skills = KeyCode.PageUp; // (MaxSkills) Works but disabled by default



    }
}