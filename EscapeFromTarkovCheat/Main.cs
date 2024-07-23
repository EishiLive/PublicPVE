using System;
using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.InputSystem;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Feature;
using EscapeFromTarkovCheat.Feature.ESP;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;
using System.Linq;
namespace EscapeFromTarkovCheat
{
    public class Main : MonoBehaviour
    {
        public static List<GamePlayer> GamePlayers = new List<GamePlayer>();
        public static Player LocalPlayer;
        public static GameWorld GameWorld;
        public static Camera MainCamera;

        private float _nextPlayerCacheTime;
        private static readonly float _cachePlayersInterval = 4f;

        public void Awake()
        {
            GameObject hookObject = new GameObject();

            hookObject.AddComponent<Menu.UI.Menu>();
            hookObject.AddComponent<PlayerESP>();
            hookObject.AddComponent<ItemESP>();
            hookObject.AddComponent<PlayerBones>();
            hookObject.AddComponent<LootableContainerESP>();
            hookObject.AddComponent<ExfiltrationPointsESP>();
            hookObject.AddComponent<NoRecoil>();
            hookObject.AddComponent<Aimbot>();
            DontDestroyOnLoad(hookObject);
        }

        public void Update()
        {
            if (Settings.DrawPlayers)
            {
                if (Time.time >= _nextPlayerCacheTime)
                {
                    GameWorld = Singleton<GameWorld>.Instance;
                    MainCamera = Camera.main;

                    if ((GameWorld != null) && (GameWorld.RegisteredPlayers != null))
                    {
                        GamePlayers.Clear();

                        foreach (Player player in GameWorld.RegisteredPlayers)
                        {
                            if (player.IsYourPlayer)
                            {
                                LocalPlayer = player;
                                continue;
                            }

                            if (!GameUtils.IsPlayerAlive(player) || (Vector3.Distance(MainCamera.transform.position, player.Transform.position) > Settings.DrawPlayersDistance))
                                continue;

                            GamePlayers.Add(new GamePlayer(player));
                        }

                        _nextPlayerCacheTime = (Time.time + _cachePlayersInterval);
                    }
                }

                foreach (GamePlayer gamePlayer in GamePlayers)
                    gamePlayer.RecalculateDynamics();
            }


            if (Input.GetKeyDown(Settings.UnlockDoors))
            {
                foreach (var door in FindObjectsOfType<Door>())
                {
                    if (door.DoorState == EDoorState.Open || Vector3.Distance(door.transform.position, LocalPlayer.Position) > 20f)
                        continue;

                    door.DoorState = EDoorState.Shut;
                }
            }

            if (Input.GetKeyDown(Settings.KillAll))
            {
                var gameWorld = Singleton<GameWorld>.Instance;
                if (gameWorld != null)
                {
                    IEnumerable<Player> players = gameWorld.AllAlivePlayersList.Where(x => !x.IsYourPlayer);
                    foreach (Player player in players)
                    {

                        if (!player.IsYourPlayer)
                        {

                            player.ActiveHealthController.Kill(EDamageType.Landmine);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(Settings.InstaHeal))
            {
                LocalPlayer.Heal(EBodyPart.Head, 9999);
                LocalPlayer.Heal(EBodyPart.Chest, 9999);
                LocalPlayer.Heal(EBodyPart.LeftLeg, 9999);
                LocalPlayer.Heal(EBodyPart.RightLeg, 9999);
                LocalPlayer.Heal(EBodyPart.LeftArm, 9999);
                LocalPlayer.Heal(EBodyPart.RightArm, 9999);

            }
            if (Input.GetKeyDown(Settings.Skills))
            {
                var lp = Main.LocalPlayer;

                if (lp != null)
                {
                    lp.Skills.Strength.SetLevel(51);
                    lp.Skills.StressResistance.SetLevel(51);
                    lp.Skills.MagDrills.SetLevel(51);
                    lp.Skills.Melee.SetLevel(51);
                    lp.Skills.HideoutManagement.SetLevel(51);
                    lp.Skills.Crafting.SetLevel(51);
                    lp.Skills.HeavyVests.SetLevel(51);
                    lp.Skills.LightVests.SetLevel(51);
                    lp.Skills.LMG.SetLevel(51);
                    lp.Skills.Assault.SetLevel(51);
                    lp.Skills.Pistol.SetLevel(51);
                    lp.Skills.Perception.SetLevel(51);
                    lp.Skills.Sniper.SetLevel(51);
                    lp.Skills.Sniping.SetLevel(51);
                    lp.Skills.Endurance.SetLevel(51);
                    lp.Skills.Throwing.SetLevel(51);
                    lp.Skills.Charisma.SetLevel(51);
                    lp.Skills.Health.SetLevel(51);
                    lp.Skills.Vitality.SetLevel(51);
                    lp.Skills.Metabolism.SetLevel(51);
                    lp.Skills.Immunity.SetLevel(51);
                    lp.Skills.Intellect.SetLevel(51);
                    lp.Skills.Attention.SetLevel(51);
                    lp.Skills.Revolver.SetLevel(51);
                    lp.Skills.Shotgun.SetLevel(51);
                    lp.Skills.HMG.SetLevel(51);
                    lp.Skills.DMR.SetLevel(51);
                    lp.Skills.AimDrills.SetLevel(51);
                    lp.Skills.Surgery.SetLevel(51);
                    lp.Skills.CovertMovement.SetLevel(51);
                    lp.Skills.Search.SetLevel(51);
                    lp.Skills.WeaponTreatment.SetLevel(51);
                    lp.Skills.SMG.SetLevel(51);
                }

            }
        }
    }
}