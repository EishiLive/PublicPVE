﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EFT;
using EFT.InventoryLogic;
using EscapeFromTarkovCheat.Data;

#nullable enable

namespace EscapeFromTarkovCheat.Data
{
    internal sealed class NotNullWhenAttribute : Attribute
    {
        public bool ReturnValue { get; }

        public NotNullWhenAttribute(bool returnValue)
        {
            ReturnValue = returnValue;
        }
    }
    public static class PlayerExtensions
    {
        public static bool IsValid([NotNullWhen(true)] this Player? player)
        {
            return player != null
                   && player.Transform != null
                   && player.Transform.Original != null
                   && player.PlayerBones != null
                   && player.PlayerBones.transform != null
                   && player.PlayerBody != null
                   && player.PlayerBody.BodySkins != null;
        }

        public static bool IsAlive([NotNullWhen(true)] this Player? player)
        {
            if (!IsValid(player))
                return false;

            return Main.LocalPlayer.HealthController is { IsAlive: true };
        }

        public static bool HasItemComponentInSlot<T>(this Player? player, EquipmentSlot slot) where T : class, IItemComponent
        {
            if (!IsValid(player))
                return false;

            var playerSlotItem = player.Profile?.Inventory?.Equipment?.GetSlot(slot)?.ContainedItem;
            if (playerSlotItem == null)
                return false;

            return playerSlotItem
                .GetAllItems()
                .GetComponents<T>()
                .Any();
        }

        public static HostileType GetHostileType(this Player player)
        {
            var info = player.Profile?.Info;
            if (info == null)
                return HostileType.Scav;

            var settings = info.Settings;
            if (settings != null)
            {
                switch (settings.Role)
                {
                    case WildSpawnType.pmcBot:
                        return HostileType.ScavRaider;
                    case WildSpawnType.sectantWarrior:
                        return HostileType.Cultist;
                    case WildSpawnType.assault:
                        return HostileType.Scav;
                    case WildSpawnType.assaultGroup:
                        return HostileType.ScavAssault;
                    case WildSpawnType.marksman:
                        return HostileType.Marksman;
                    case WildSpawnType.exUsec:
                        return HostileType.RogueUsec;
                }

                if (settings.IsBoss())
                    return HostileType.Boss;
            }

            return info.Side switch
            {
                EPlayerSide.Bear => HostileType.Bear,
                EPlayerSide.Usec => HostileType.Usec,
                _ => HostileType.Scav
            };
        }

    }
}