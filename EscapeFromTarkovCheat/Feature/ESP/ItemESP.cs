using System;
using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using JsonType;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feature.ESP
{
    public class ItemESP : MonoBehaviour
    {
        private static readonly float CacheLootItemsInterval = 4f;
        private float _nextLootItemCacheTime;

        private readonly List<GameLootItem> _gameLootItems = new List<GameLootItem>();

        public void Update()
        {
            if (!Settings.DrawLootItems)
                return;

            if (Time.time >= _nextLootItemCacheTime)
            {
                if (Main.GameWorld?.LootItems != null)
                {
                    _gameLootItems.Clear();

                    for (int i = 0; i < Main.GameWorld.LootItems.Count; i++)
                    {
                        LootItem lootItem = Main.GameWorld.LootItems.GetByIndex(i);

                        if (!GameUtils.IsLootItemValid(lootItem) || (Vector3.Distance(Main.MainCamera.transform.position, lootItem.transform.position) > Settings.DrawLootItemsDistance))
                            continue;

                        _gameLootItems.Add(new GameLootItem(lootItem));
                    }

                    _nextLootItemCacheTime = Time.time + CacheLootItemsInterval;
                }
            }

            foreach (GameLootItem gameLootItem in _gameLootItems)
                gameLootItem.RecalculateDynamics();
        }

        private void OnGUI()
        {
            Debug.Log("OnGUI Called");
            if (Settings.DrawLootItems)
            {
                foreach (var gameLootItem in _gameLootItems)
                {
                    if (!GameUtils.IsLootItemValid(gameLootItem.LootItem) || !gameLootItem.IsOnScreen || gameLootItem.Distance > Settings.DrawLootItemsDistance)
                        continue;

                    string lootItemName = $"{gameLootItem.LootItem.Item.ShortName.Localized()} [{gameLootItem.FormattedDistance}]";
                    Color itemColor = GetItemColor(gameLootItem.LootItem.Item.Template.Rarity, gameLootItem.LootItem.Item.Template.QuestItem);

                    Render.DrawString(new Vector2(gameLootItem.ScreenPosition.x - 50f, gameLootItem.ScreenPosition.y), lootItemName, itemColor);
                }
            }
        }

        private Color GetItemColor(ELootRarity rarity, bool isQuestItem)
        {
            if (isQuestItem)
                return Settings.QuestItemColor;

            switch (rarity)
            {
                case ELootRarity.Common:
                    return Settings.CommonItemColor;
                case ELootRarity.Rare:
                    return Settings.RareItemColor;
                case ELootRarity.Superrare:
                    return Settings.SuperRareItemColor;
                default:
                    return Color.white; // Default color if rarity is unknown
            }
        }
    }
}
