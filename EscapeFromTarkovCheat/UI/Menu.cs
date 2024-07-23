using EscapeFromTarkovCheat;
using EscapeFromTarkovCheat.Feature;
using EscapeFromTarkovCheat.Utils;
using System;
using UnityEngine;

namespace Menu.UI
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private bool _isVisible = true;
        private bool _selectingAimbotKey = false;
        private int _currentTab = 0;

        // Références aux contrôles de couleur
        private Color _skeletonColor = Settings.SkeletonColor;
        private Color _visibleSkeletonColor = Settings.VisibleSkeletonColor;

        private Vector2 _aimbotScrollPosition;
        private Vector2 _itemScrollPosition;

        private ItemFeatures _itemFeatures;

        private void Start()
        {
            AllocConsoleHandler.Open();
            _mainWindow = new Rect(20f, 60f, 300f, 400f);
        }

        private void Awake()
        {
            _itemFeatures = new ItemFeatures();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _isVisible = !_isVisible;

            if (Input.GetKeyDown(KeyCode.Delete))
                Loader.Unload();
            if (_selectingAimbotKey)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        Settings.AimbotKey = keyCode;
                        _selectingAimbotKey = false;
                        break;
                    }
                }
            }
        }

        private void OnGUI()
        {
            if (!_isVisible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUI, "Eishi - Beta");
        }

        private void RenderUI(int id)
        {
            // Onglets
            string[] tabs = { "Main", "Player Visual", "Misc Visual", "Aimbot", "Item Spawner" };
            _currentTab = GUILayout.Toolbar(_currentTab, tabs, GUILayout.Height(30));

            switch (_currentTab)
            {
                case 0:
                    RenderMainTab();
                    break;
                case 1:
                    RenderPlayerVisualTab();
                    break;
                case 2:
                    RenderMiscVisualTab();
                    break;
                case 3:
                    RenderAimbotTab();
                    break;
                case 4:
                    RenderItemMenu(id);
                    break;
            }

            GUI.DragWindow();
        }

        private void RenderMainTab()
        {
            GUILayout.Label("Insert For Menu");
            GUILayout.Label("Press NumPad 0 to MaxSkills");
            GUILayout.Label("Press K to Open Doors");
            GUILayout.Label("Press L to Kill All");
        }

        private void RenderPlayerVisualTab()
        {
            Settings.DrawPlayers = GUILayout.Toggle(Settings.DrawPlayers, "Draw Players");
            Settings.DrawPlayerBox = GUILayout.Toggle(Settings.DrawPlayerBox, "Draw Player Box");
            Settings.DrawPlayerSkeleton = GUILayout.Toggle(Settings.DrawPlayerSkeleton, "Draw Skeleton");
            Settings.DrawPlayerName = GUILayout.Toggle(Settings.DrawPlayerName, "Draw Player Name");
            Settings.DrawPlayerLine = GUILayout.Toggle(Settings.DrawPlayerLine, "Draw Player Line");
            Settings.DrawPlayerHealth = GUILayout.Toggle(Settings.DrawPlayerHealth, "Draw Player Health");

            // Sélecteur de couleur pour les squelettes
            GUILayout.Label("Skeleton Color");
            _skeletonColor = RGBSlider(_skeletonColor);

            GUILayout.Label("Visible Skeleton Color");
            _visibleSkeletonColor = RGBSlider(_visibleSkeletonColor);

            Settings.SkeletonColor = _skeletonColor;
            Settings.VisibleSkeletonColor = _visibleSkeletonColor;

            GUILayout.Label($"Player Distance {(int)Settings.DrawPlayersDistance} m");
            Settings.DrawPlayersDistance = GUILayout.HorizontalSlider(Settings.DrawPlayersDistance, 0f, 2000f);
        }

        private Color RGBSlider(Color color)
        {
            GUILayout.BeginHorizontal();
            color.r = GUILayout.HorizontalSlider(color.r, 0f, 1f, GUILayout.Width(100));
            GUILayout.Label($"R: {color.r:F2}", GUILayout.Width(50));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            color.g = GUILayout.HorizontalSlider(color.g, 0f, 1f, GUILayout.Width(100));
            GUILayout.Label($"G: {color.g:F2}", GUILayout.Width(50));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            color.b = GUILayout.HorizontalSlider(color.b, 0f, 1f, GUILayout.Width(100));
            GUILayout.Label($"B: {color.b:F2}", GUILayout.Width(50));
            GUILayout.EndHorizontal();

            return color;
        }

        private void RenderMiscVisualTab()
        {

            Settings.DrawLootItems = GUILayout.Toggle(Settings.DrawLootItems, "Draw Loot Items");
            GUILayout.Label($"Loot Item Distance {(int)Settings.DrawLootItemsDistance} m");
            Settings.DrawLootItemsDistance = GUILayout.HorizontalSlider(Settings.DrawLootItemsDistance, 0f, 1000f);

            Settings.DrawLootableContainers = GUILayout.Toggle(Settings.DrawLootableContainers, "Draw Containers");
            GUILayout.Label($"Container Distance {(int)Settings.DrawLootableContainersDistance} m");
            Settings.DrawLootableContainersDistance = GUILayout.HorizontalSlider(Settings.DrawLootableContainersDistance, 0f, 1000f);

            Settings.DrawExfiltrationPoints = GUILayout.Toggle(Settings.DrawExfiltrationPoints, "Draw Exits");
            Settings.NoVisor = GUILayout.Toggle(Settings.NoVisor, "No Visor");
        }

        private void RenderAimbotTab()
        {
            _aimbotScrollPosition = GUILayout.BeginScrollView(_aimbotScrollPosition, false, true);

            Settings.Aimbot = GUILayout.Toggle(Settings.Aimbot, "Enable Aimbot");
            Settings.NoRecoil = GUILayout.Toggle(Settings.NoRecoil, "Enable No Recoil");

            if (Settings.Aimbot)
            {
                GUILayout.Label($"Aimbot Key: {Settings.AimbotKey}");

                if (_selectingAimbotKey)
                {
                    GUILayout.Label("Press any key...");
                }
                else
                {
                    if (GUILayout.Button("Change Aimbot Key", GUILayout.Width(150)))
                    {
                        _selectingAimbotKey = true;
                    }
                }

                GUILayout.Label($"FOV Radius: {Settings.AimbotFOV}");
                Settings.AimbotFOV = GUILayout.HorizontalSlider(Settings.AimbotFOV, 0f, 100f);

                GUILayout.Label($"Smoothness: {Settings.AimbotSmooth}");
                Settings.AimbotSmooth = GUILayout.HorizontalSlider(Settings.AimbotSmooth, 0f, 100f);
            }

            GUILayout.EndScrollView();
        }

        private void RenderItemMenu(int id)
        {
            _itemScrollPosition = GUILayout.BeginScrollView(_itemScrollPosition, false, true);

            GUILayout.Label("--- Item Spawner ---");
            GUILayout.Label("ID Search");
            _itemFeatures.SetSearchQuery(GUILayout.TextField(_itemFeatures.GetSearchQuery(), GUILayout.Width(200)));
            if (GUILayout.Button("Get Items from Backpack"))
            {
                _itemFeatures.GetItemsInInventory();
            }
            GUILayout.Label("Item Strings");
            GUILayout.TextArea(_itemFeatures.ItemStringText, GUILayout.Height(100));
            GUILayout.Label("_id");
            _itemFeatures.SetNewValues(
                GUILayout.TextField(_itemFeatures.NewValue1, GUILayout.Width(200)),
                GUILayout.TextField(_itemFeatures.NewValue2, GUILayout.Width(200)),
                GUILayout.TextField(_itemFeatures.NewValue3, GUILayout.Width(200))
            );
            if (GUILayout.Button("Spawn Item"))
            {
                _itemFeatures.SetItemsInInventory();
            }
            if (GUILayout.Button("Set Whole Inventory FiR"))
            {
                _itemFeatures.FoundInRaidInventory();
            }
            GUILayout.Label("--- Item Dupe ---");
            if (GUILayout.Button("Dupe Stack x2"))
            {
                _itemFeatures.DupeItemsInInventoryLow();
            }
            if (GUILayout.Button("Dupe Stack x60"))
            {
                _itemFeatures.DupeItemsInInventoryHigh();
            }
            if (GUILayout.Button("Reset Stack"))
            {
                _itemFeatures.ResetItemsInInventory();
            }
            if (GUILayout.Button("Dupe Rubel"))
            {
                _itemFeatures.DupeRubles();
            }
            if (GUILayout.Button("Dupe Dollars/Euros"))
            {
                _itemFeatures.DupeDollarsEuros();
            }

            GUILayout.EndScrollView();
            GUI.DragWindow();
        }
    }
}
