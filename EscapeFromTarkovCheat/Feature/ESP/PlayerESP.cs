using System;
using System.Collections.Generic;
using System.Diagnostics;
using Comfort.Common;
using EFT;
using EFT.HealthSystem;
using EFT.Interactive;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feature.ESP
{
    public class PlayerESP : MonoBehaviour
    {
        private static readonly Color _playerColor = Color.green;
        private static readonly Color _botColor = Color.yellow;
        private static readonly Color _healthColor = Color.green;
        private static readonly Color _bossColor = Color.red; // Color for bosses
        public Color skeletonColor = Color.red;
        public Color visibleSkeletonColor = Color.green;

        public void OnGUI()
        {
            if (!Settings.DrawPlayers)
                return;

            foreach (GamePlayer gamePlayer in Main.GamePlayers)
            {
                if (!gamePlayer.IsOnScreen || gamePlayer.Distance > Settings.DrawPlayersDistance || gamePlayer.Player == Main.LocalPlayer)
                    continue;

                Color playerColor = gamePlayer.IsBoss ? _bossColor : (gamePlayer.IsAI ? _botColor : _playerColor);

                float boxPositionY = (gamePlayer.HeadScreenPosition.y - 10f);
                float boxHeight = (Math.Abs(gamePlayer.HeadScreenPosition.y - gamePlayer.ScreenPosition.y) + 10f);
                float boxWidth = (boxHeight * 0.65f);

                if (Settings.DrawPlayerBox)
                {
                    Render.DrawBox((gamePlayer.ScreenPosition.x - (boxWidth / 2f)), boxPositionY, boxWidth, boxHeight, playerColor);
                }

                if (Settings.DrawPlayerHealth)
                {
                    if (gamePlayer.Player.HealthController.IsAlive)
                    {
                        float currentPlayerHealth = gamePlayer.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Current;
                        float maximumPlayerHealth = gamePlayer.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Maximum;

                        float healthBarHeight = GameUtils.Map(currentPlayerHealth, 0f, maximumPlayerHealth, 0f, boxHeight);
                        Render.DrawLine(new Vector2((gamePlayer.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight - healthBarHeight)), new Vector2((gamePlayer.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight)), 3f, _healthColor);
                    }
                }

                if (Settings.DrawPlayerName)
                {
                    string playerText;

                    if (GamePlayer.IsBossByName(gamePlayer.BossNames))
                    {
                        playerText = $"gamePlayer.BossNames [{gamePlayer.FormattedDistance}]";
                    }
                    else if (gamePlayer.IsAI)
                    {
                        playerText = $"Scav/Raider [{gamePlayer.FormattedDistance}]";
                    }
                    else
                    {
                        playerText = $"PMC {gamePlayer.Player.Profile.Info.Nickname} [{gamePlayer.FormattedDistance}]";
                    }

                    var playerTextVector = GUI.skin.GetStyle("label").CalcSize(new GUIContent(playerText));
                    Render.DrawString(
                        new Vector2(gamePlayer.ScreenPosition.x - (playerTextVector.x / 2f), gamePlayer.HeadScreenPosition.y - 20f),
                        playerText,
                        playerColor
                    );
                }

                if (Settings.DrawPlayerLine)
                {
                    Render.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(gamePlayer.ScreenPosition.x, gamePlayer.ScreenPosition.y), 1.5f, /*GameUtils.IsVisible(destination)*/false ? Color.green : Color.red);
                }
                if (Settings.DrawPlayerSkeleton)
                {
                    PlayerBones.DrawSkeleton(gamePlayer);
                }
            }
        }
    }
}
