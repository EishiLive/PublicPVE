using System;
using EFT.Interactive;
using EscapeFromTarkovCheat.Utils;
using JsonType;
using UnityEngine;

namespace EscapeFromTarkovCheat.Data
{
    public class GameLootItem
    {
        public LootItem LootItem { get; }
        public Vector3 ScreenPosition { get; private set; }
        public bool IsOnScreen { get; private set; }
        public float Distance { get; private set; }
        public string FormattedDistance => $"{Math.Round(Distance)}m";

        // Constructor that assumes the lootItem is always non-null
        public GameLootItem(LootItem lootItem)
        {
            LootItem = lootItem ?? throw new ArgumentNullException(nameof(lootItem));
            ScreenPosition = default;
            Distance = 0f;
        }

        // Method to recalculate dynamics and update item color
        public void RecalculateDynamics()
        {
            // Your existing method implementation here
        }
    }
}
