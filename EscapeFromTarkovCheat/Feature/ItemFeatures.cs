using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EFT.InventoryLogic;
using EscapeFromTarkovCheat.Utils;
using EscapeFromTarkovCheat;

namespace EscapeFromTarkovCheat.Feature
{
    public class ItemFeatures
    {
        public List<Item> collectedItems = new List<Item>();
        public string _searchQuery = "";
        public string _itemStringText = "";
        public string _newValueToWrite1 = "";
        public string _newValueToWrite2 = "";
        public string _newValueToWrite3 = "";

        public string ItemStringText => _itemStringText;
        public string GetSearchQuery() => _searchQuery;
        public string NewValue1 => _newValueToWrite1;
        public string NewValue2 => _newValueToWrite2;
        public string NewValue3 => _newValueToWrite3;

        public void GetItemsInInventory()
        {
            collectedItems.Clear();
            var itemNames = new List<string>();

            var slots = new[] { EquipmentSlot.Backpack };
            var items = Main.LocalPlayer.Profile.Inventory.GetItemsInSlots(slots);

            if (items != null)
            {
                foreach (var playerItem in items)
                {
                    if (GameUtils.IsInventoryItemValid(playerItem) && playerItem.Name.ToLower().Contains(_searchQuery.ToLower()))
                    {
                        itemNames.Add(playerItem.Name);
                        collectedItems.Add(playerItem);
                    }
                }
                _itemStringText = string.Join("\n", itemNames);
            }
        }

        public void SetItemsInInventory()
        {
            if (collectedItems.Count > 0)
            {
                foreach (var playerItem in collectedItems)
                {
                    if (playerItem.Name.ToLower().Contains(_searchQuery.ToLower()))
                    {
                        playerItem.Template._id = _newValueToWrite1;
                        if (int.TryParse(_newValueToWrite2, out int newWidth))
                        {
                            playerItem.Template.Width = newWidth;
                        }

                        if (int.TryParse(_newValueToWrite3, out int newHeight))
                        {
                            playerItem.Template.Height = newHeight;
                        }
                    }
                }
            }
        }

        public void ResetItemsInInventory()
        {
            if (collectedItems.Count > 0)
            {
                foreach (var playerItem in collectedItems)
                {
                    playerItem.StackObjectsCount = 1;
                }
            }
        }

        public void DupeItemsInInventoryLow()
        {
            if (collectedItems.Count > 0)
            {
                foreach (var playerItem in collectedItems)
                {
                    playerItem.StackObjectsCount = 2;
                }
            }
        }

        public void DupeDollarsEuros()
        {
            if (collectedItems.Count > 0)
            {
                foreach (var playerItem in collectedItems)
                {
                    playerItem.StackObjectsCount = 50000;
                }
            }
        }

        public void DupeRubles()
        {
            if (collectedItems.Count > 0)
            {
                foreach (var playerItem in collectedItems)
                {
                    playerItem.StackObjectsCount = 500000;
                }
            }
        }

        public void DupeItemsInInventoryHigh()
        {
            if (collectedItems.Count > 0)
            {
                foreach (var playerItem in collectedItems)
                {
                    playerItem.StackObjectsCount = 60;
                }
            }
        }

        public void FoundInRaidInventory()
        {
            var items = Main.LocalPlayer.Profile.Inventory.GetPlayerItems(EPlayerItems.All);
            if (items != null)
            {
                foreach (var playerItem in items)
                {
                    if (GameUtils.IsInventoryItemValid(playerItem))
                    {
                        playerItem.SpawnedInSession = true;
                    }
                }
            }
        }

        public void SetSearchQuery(string searchQuery)
        {
            _searchQuery = searchQuery;
        }

        public void SetNewValues(string newValue1, string newValue2, string newValue3)
        {
            _newValueToWrite1 = newValue1;
            _newValueToWrite2 = newValue2;
            _newValueToWrite3 = newValue3;
        }
    }
}