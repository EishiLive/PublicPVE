using System;
using System.Collections.Generic;
using System.Linq;
using EFT;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Data
{
    public class GamePlayer
    {
        public Player Player { get; }
        public Vector3 ScreenPosition => screenPosition;
        public Vector3 HeadScreenPosition => headScreenPosition;
        public string FormattedDistance => $"{(int)Math.Round(Distance)}m";
        public bool IsOnScreen { get; private set; }
        public bool IsVisible { get; private set; }
        public float Distance { get; private set; }
        public bool IsAI { get; private set; }
        public bool IsBoss { get; private set; }
        public string BossNames { get; private set; }

        public static bool IsBossByName(string name)
        {
            var bossNames = new HashSet<string>
            {
                "Килла", "Решала", "Глухарь", "Штурман", "Санитар", "Тагилла",
                "Зрячий", "Кабан", "Big Pipe", "Birdeye", "Knight", "Дед Мороз", "Коллонтай",
                "Killa", "Reshala", "Gluhar", "Shturman", "Sanitar", "Tagilla",
                "Zryachiy", "Kaban", "Ded Moroz", "Kollontay"
            };

            return bossNames.Contains(name);
        }

        private Vector3 screenPosition;
        private Vector3 headScreenPosition;

        public GamePlayer(Player player)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            screenPosition = default;
            headScreenPosition = default;
            IsOnScreen = false;
            Distance = 0f;
            IsAI = true;
            IsBoss = false;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsPlayerValid(Player))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(Player.Transform.position);

            headScreenPosition = Player.PlayerBones?.Head != null
                ? GameUtils.WorldPointToScreenPoint(Player.PlayerBones.Head.position)
                : default;

            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Main.MainCamera.transform.position, Player.Transform.position);

            IsAI = Player.Profile?.Info?.RegistrationDate <= 0;
            IsBoss = IsBossByName(Player.Profile?.Info?.Nickname);
        }

        public Vector3 GetBonePosition(BoneType boneType)
        {
            var bones = Player.PlayerBody?.SkeletonRootJoint?.Bones;
            Transform boneTransform = bones?.ElementAtOrDefault((int)boneType).Value;

            return boneTransform != null
                ? GameUtils.WorldPointToScreenPoint(boneTransform.position)
                : Vector3.zero;
        }
    }
}
