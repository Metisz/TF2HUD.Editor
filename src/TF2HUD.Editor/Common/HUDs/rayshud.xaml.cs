﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TF2HUD.Editor.Common;

namespace TF2HUD.Editor.HUDs
{
    /// <summary>
    ///     Interaction logic for rayshud.xaml
    /// </summary>
    public partial class rayshud
    {
        public rayshud()
        {
            InitializeComponent();
            ReloadHudSettings();
        }

        /// <summary>
        ///     Disable crosshair options if the crosshair is toggled off.
        /// </summary>
        private void SetCrosshairControls()
        {
            CbXHairHitmarker.IsEnabled = CbXHairEnable.IsChecked ?? false;
            CpXHairColor.IsEnabled = CbXHairEnable.IsChecked ?? false;
            CpXHairPulse.IsEnabled = CbXHairEnable.IsChecked ?? false;
            IntXHairSize.IsEnabled = CbXHairEnable.IsChecked ?? false;
            CbXHairStyle.IsEnabled = CbXHairEnable.IsChecked ?? false;
            CbXHairEffect.IsEnabled = CbXHairEnable.IsChecked ?? false;
        }

        #region CLICK_EVENTS

        /// <summary>
        ///     Toggle ÜberCharge options depending on the style selected.
        /// </summary>
        private void CbUberStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CpUberFullColor.IsEnabled = false;
            CpUberFlash1.IsEnabled = false;
            CpUberFlash2.IsEnabled = false;

            CpUberFullColor.IsEnabled = CbUberStyle.SelectedIndex == 0;
            CpUberFlash1.IsEnabled = CbUberStyle.SelectedIndex == 1;
            CpUberFlash2.IsEnabled = CbUberStyle.SelectedIndex == 1;
        }

        /// <summary>
        ///     Toggle crosshair options if the crosshair is disabled.
        /// </summary>
        private void CbXHairEnable_OnClick(object sender, RoutedEventArgs e)
        {
            SetCrosshairControls();
        }

        private void btnHudsTF_Click(object sender, RoutedEventArgs e)
        {
            Utilities.OpenWebpage(Properties.Resources.url_rayshud_hudstf);
        }

        private void btnSteam_Click(object sender, RoutedEventArgs e)
        {
            Utilities.OpenWebpage(Properties.Resources.url_rayshud_steam);
        }

        #endregion CLICK_EVENTS

        #region SAVE_LOAD

        /// <summary>
        ///     Save user settings to file.
        /// </summary>
        public void SaveHudSettings()
        {
            try
            {
                var settings = Properties.rayshud.Default;
                settings.color_health_normal = CpHealthNormal.SelectedColor?.ToString();
                settings.color_health_buff = CpHealthBuffed.SelectedColor?.ToString();
                settings.color_health_low = CpHealthLow.SelectedColor?.ToString();
                settings.color_ammo_clip = CpAmmoClip.SelectedColor?.ToString();
                settings.color_ammo_clip_low = CpAmmoClipLow.SelectedColor?.ToString();
                settings.color_ammo_reserve = CpAmmoReserve.SelectedColor?.ToString();
                settings.color_ammo_reserve_low = CpAmmoReserveLow.SelectedColor?.ToString();
                settings.color_uber_bar = CpUberBarColor.SelectedColor?.ToString();
                settings.color_uber_full = CpUberFullColor.SelectedColor?.ToString();
                settings.color_xhair_normal = CpXHairColor.SelectedColor?.ToString();
                settings.color_xhair_pulse = CpXHairPulse.SelectedColor?.ToString();
                settings.color_uber_flash1 = CpUberFlash1.SelectedColor?.ToString();
                settings.color_uber_flash2 = CpUberFlash2.SelectedColor?.ToString();
                settings.color_normal = CpItemNormal.SelectedColor?.ToString();
                settings.color_unique = CpItemUnique.SelectedColor?.ToString();
                settings.color_strange = CpItemStrange.SelectedColor?.ToString();
                settings.color_vintage = CpItemVintage.SelectedColor?.ToString();
                settings.color_haunted = CpItemHaunted.SelectedColor?.ToString();
                settings.color_genuine = CpItemGenuine.SelectedColor?.ToString();
                settings.color_collectors = CpItemCollectors.SelectedColor?.ToString();
                settings.color_unusual = CpItemUnusual.SelectedColor?.ToString();
                settings.color_community = CpItemCommunity.SelectedColor?.ToString();
                settings.color_valve = CpItemValve.SelectedColor?.ToString();
                settings.color_civilian = CpItemCivilian.SelectedColor?.ToString();
                settings.color_freelance = CpItemFreelance.SelectedColor?.ToString();
                settings.color_mercenary = CpItemMercenary.SelectedColor?.ToString();
                settings.color_commando = CpItemCommando.SelectedColor?.ToString();
                settings.color_assassin = CpItemAssassin.SelectedColor?.ToString();
                settings.color_elite = CpItemElite.SelectedColor?.ToString();
                settings.val_uber_animation = CbUberStyle.SelectedIndex;
                settings.val_health_style = CbHealthStyle.SelectedIndex;
                settings.val_xhair_size = IntXHairSize.Value ?? 18;
                settings.val_xhair_style = CbXHairStyle.SelectedIndex;
                settings.val_xhair_effect = CbXHairEffect.SelectedIndex;
                settings.toggle_xhair_enable = CbXHairEnable.IsChecked ?? false;
                settings.toggle_xhair_pulse = CbXHairHitmarker.IsChecked ?? false;
                settings.toggle_disguise_image = CbDisguiseImage.IsChecked ?? false;
                settings.toggle_menu_images = CbMenuImages.IsChecked ?? false;
                settings.toggle_transparent_viewmodels = CbTransparentViewmodel.IsChecked ?? false;
                settings.toggle_damage_pos = CbDamagePos.IsChecked ?? false;
                settings.toggle_chat_bottom = CbChatBottom.IsChecked ?? false;
                settings.toggle_center_select = CbTeamCenter.IsChecked ?? false;
                settings.toggle_classic_menu = CbClassicHud.IsChecked ?? false;
                settings.toggle_min_scoreboard = CbScoreboard.IsChecked ?? false;
                settings.toggle_alt_player_model = CbPlayerModel.IsChecked ?? false;
                settings.toggle_metal_pos = CbMetalPos.IsChecked ?? false;
                settings.val_main_menu_bg = CbMainMenuBackground.SelectedIndex;
                settings.Save();
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error,
                    string.Format(Properties.Resources.error_app_save, MainWindow.HudSelection), ex.Message);
            }
        }

        /// <summary>
        ///     Load user settings from file.
        /// </summary>
        public void ReloadHudSettings()
        {
            try
            {
                var settings = Properties.rayshud.Default;
                var cc = new ColorConverter();
                CpHealthNormal.SelectedColor = (Color) cc.ConvertFrom(settings.color_health_normal);
                CpHealthBuffed.SelectedColor = (Color) cc.ConvertFrom(settings.color_health_buff);
                CpHealthLow.SelectedColor = (Color) cc.ConvertFrom(settings.color_health_low);
                CpAmmoClip.SelectedColor = (Color) cc.ConvertFrom(settings.color_ammo_clip);
                CpAmmoClipLow.SelectedColor = (Color) cc.ConvertFrom(settings.color_ammo_clip_low);
                CpAmmoReserve.SelectedColor = (Color) cc.ConvertFrom(settings.color_ammo_reserve);
                CpAmmoReserveLow.SelectedColor = (Color) cc.ConvertFrom(settings.color_ammo_reserve_low);
                CpUberBarColor.SelectedColor = (Color) cc.ConvertFrom(settings.color_uber_bar);
                CpUberFullColor.SelectedColor = (Color) cc.ConvertFrom(settings.color_uber_full);
                CpXHairColor.SelectedColor = (Color) cc.ConvertFrom(settings.color_xhair_normal);
                CpXHairPulse.SelectedColor = (Color) cc.ConvertFrom(settings.color_xhair_pulse);
                CpUberFlash1.SelectedColor = (Color) cc.ConvertFrom(settings.color_uber_flash1);
                CpUberFlash2.SelectedColor = (Color) cc.ConvertFrom(settings.color_uber_flash2);
                CpItemNormal.SelectedColor = (Color)cc.ConvertFrom(settings.color_normal);
                CpItemUnique.SelectedColor = (Color)cc.ConvertFrom(settings.color_unique);
                CpItemStrange.SelectedColor = (Color)cc.ConvertFrom(settings.color_strange);
                CpItemVintage.SelectedColor = (Color)cc.ConvertFrom(settings.color_vintage);
                CpItemHaunted.SelectedColor = (Color)cc.ConvertFrom(settings.color_haunted);
                CpItemGenuine.SelectedColor = (Color)cc.ConvertFrom(settings.color_genuine);
                CpItemCollectors.SelectedColor = (Color)cc.ConvertFrom(settings.color_collectors);
                CpItemUnusual.SelectedColor = (Color)cc.ConvertFrom(settings.color_unusual);
                CpItemCommunity.SelectedColor = (Color)cc.ConvertFrom(settings.color_community);
                CpItemValve.SelectedColor = (Color)cc.ConvertFrom(settings.color_valve);
                CpItemCivilian.SelectedColor = (Color)cc.ConvertFrom(settings.color_civilian);
                CpItemFreelance.SelectedColor = (Color)cc.ConvertFrom(settings.color_freelance);
                CpItemMercenary.SelectedColor = (Color)cc.ConvertFrom(settings.color_mercenary);
                CpItemCommando.SelectedColor = (Color)cc.ConvertFrom(settings.color_commando);
                CpItemAssassin.SelectedColor = (Color)cc.ConvertFrom(settings.color_assassin);
                CpItemElite.SelectedColor = (Color)cc.ConvertFrom(settings.color_elite);
                CbUberStyle.SelectedIndex = settings.val_uber_animation;
                CbHealthStyle.SelectedIndex = settings.val_health_style;
                IntXHairSize.Value = settings.val_xhair_size;
                CbXHairStyle.SelectedIndex = settings.val_xhair_style;
                CbXHairEffect.SelectedIndex = settings.val_xhair_effect;
                CbXHairEnable.IsChecked = settings.toggle_xhair_enable;
                CbXHairHitmarker.IsChecked = settings.toggle_xhair_pulse;
                CbDisguiseImage.IsChecked = settings.toggle_disguise_image;
                CbMenuImages.IsChecked = settings.toggle_menu_images;
                CbTransparentViewmodel.IsChecked = settings.toggle_transparent_viewmodels;
                CbDamagePos.IsChecked = settings.toggle_damage_pos;
                CbChatBottom.IsChecked = settings.toggle_chat_bottom;
                CbTeamCenter.IsChecked = settings.toggle_center_select;
                CbClassicHud.IsChecked = settings.toggle_classic_menu;
                CbScoreboard.IsChecked = settings.toggle_min_scoreboard;
                CbPlayerModel.IsChecked = settings.toggle_alt_player_model;
                CbMainMenuBackground.SelectedIndex = settings.val_main_menu_bg;
                CbMetalPos.IsChecked = settings.toggle_metal_pos;
                SetCrosshairControls();
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error,
                    string.Format(Properties.Resources.error_app_load, MainWindow.HudSelection), ex.Message);
            }
        }

        /// <summary>
        ///     Reset user settings to their default values.
        /// </summary>
        /// <remarks>TODO: Default settings should be read from a JSON file.</remarks>
        public void ResetHudSettings()
        {
            try
            {
                var cc = new ColorConverter();
                CpHealthNormal.SelectedColor = (Color) cc.ConvertFrom("#EBE2CA");
                CpHealthBuffed.SelectedColor = (Color) cc.ConvertFrom("#30FF30");
                CpHealthLow.SelectedColor = (Color) cc.ConvertFrom("#FF9900");
                CpAmmoClip.SelectedColor = (Color) cc.ConvertFrom("#30FF30");
                CpAmmoClipLow.SelectedColor = (Color) cc.ConvertFrom("#FF2A82");
                CpAmmoReserve.SelectedColor = (Color) cc.ConvertFrom("#48FFFF");
                CpAmmoReserveLow.SelectedColor = (Color) cc.ConvertFrom("#FF801C");
                CpUberBarColor.SelectedColor = (Color) cc.ConvertFrom("#EBE2CA");
                CpUberFullColor.SelectedColor = (Color) cc.ConvertFrom("#FF3219");
                CpXHairColor.SelectedColor = (Color) cc.ConvertFrom("#F2F2F2");
                CpXHairPulse.SelectedColor = (Color) cc.ConvertFrom("#FF0000");
                CpUberFlash1.SelectedColor = (Color) cc.ConvertFrom("#FFA500");
                CpUberFlash2.SelectedColor = (Color) cc.ConvertFrom("#FF4500");
                CpItemNormal.SelectedColor = (Color)cc.ConvertFrom("#B2B2B2");
                CpItemUnique.SelectedColor = (Color)cc.ConvertFrom("#FFD700");
                CpItemStrange.SelectedColor = (Color)cc.ConvertFrom("#CF6A32");
                CpItemVintage.SelectedColor = (Color)cc.ConvertFrom("#476291");
                CpItemHaunted.SelectedColor = (Color)cc.ConvertFrom("#38F3AB");
                CpItemGenuine.SelectedColor = (Color)cc.ConvertFrom("#4D7455");
                CpItemCollectors.SelectedColor = (Color)cc.ConvertFrom("#AA0000");
                CpItemUnusual.SelectedColor = (Color)cc.ConvertFrom("#8650AC");
                CpItemCommunity.SelectedColor = (Color)cc.ConvertFrom("#70B04A");
                CpItemValve.SelectedColor = (Color)cc.ConvertFrom("#A50F79");
                CpItemCivilian.SelectedColor = (Color)cc.ConvertFrom("#B0C3D9");
                CpItemFreelance.SelectedColor = (Color)cc.ConvertFrom("#5E98D9");
                CpItemMercenary.SelectedColor = (Color)cc.ConvertFrom("#4B69FF");
                CpItemCommando.SelectedColor = (Color)cc.ConvertFrom("#8847FF");
                CpItemAssassin.SelectedColor = (Color)cc.ConvertFrom("#D32CE6");
                CpItemElite.SelectedColor = (Color)cc.ConvertFrom("#EB4B4B");
                CbUberStyle.SelectedIndex = 0;
                CbHealthStyle.SelectedIndex = 0;
                IntXHairSize.Value = 18;
                CbXHairStyle.SelectedIndex = 24;
                CbXHairEffect.SelectedIndex = 0;
                CbXHairEnable.IsChecked = false;
                CbXHairHitmarker.IsChecked = true;
                CbDisguiseImage.IsChecked = false;
                CbMenuImages.IsChecked = false;
                CbTransparentViewmodel.IsChecked = false;
                CbDamagePos.IsChecked = false;
                CbChatBottom.IsChecked = false;
                CbTeamCenter.IsChecked = false;
                CbClassicHud.IsChecked = false;
                CbScoreboard.IsChecked = false;
                CbPlayerModel.IsChecked = false;
                CbMetalPos.IsChecked = false;
                CbMainMenuBackground.SelectedIndex = 0;
                SetCrosshairControls();
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error,
                    string.Format(Properties.Resources.error_app_reset, MainWindow.HudSelection), ex.Message);
            }
        }

        /// <summary>
        ///     Apply user settings to the HUD files.
        /// </summary>
        public void ApplyHudSettings()
        {
            if (!MainMenuStyle()) return;
            if (!MainMenuClassImage()) return;
            if (!ScoreboardStyle()) return;
            if (!TeamSelect()) return;
            if (!HealthStyle()) return;
            if (!UberchargeStyle()) return;
            if (!ChatBoxPos()) return;
            if (!Colors()) return;
            if (!DamagePosition()) return;
            if (!MetalPosition()) return;
            if (!PlayerModelPos()) return;
            if (!MainMenuBackground()) return;
            if (!Common.DisguiseImage()) return;
            if (!Common.Crosshair(CbXHairStyle.SelectedValue.ToString(), IntXHairSize.Value,
                CbXHairEffect.SelectedValue.ToString())) return;
            if (!Common.CrosshairPulse()) return;
            if (!Common.ItemColors()) return;
            Common.TransparentViewmodels();
        }

        #endregion SAVE_LOAD

        #region CONTROLLER

        /// <summary>
        ///     Update the client scheme colors.
        /// </summary>
        private static bool Colors()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_clientscheme_colors, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                // Health
                lines[Utilities.FindIndex(lines, "\"Health Normal\"")] =
                    $"\t\t\"Health Normal\"\t\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_health_normal)}\"";
                lines[Utilities.FindIndex(lines, "\"Health Buff\"")] =
                    $"\t\t\"Health Buff\"\t\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_health_buff)}\"";
                lines[Utilities.FindIndex(lines, "\"Health Hurt\"")] =
                    $"\t\t\"Health Hurt\"\t\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_health_low)}\"";
                lines[Utilities.FindIndex(lines, "\"Heal Numbers\"")] =
                    $"\t\t\"Heal Numbers\"\t\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_health_healed)}\"";
                // Ammo
                lines[Utilities.FindIndex(lines, "\"Ammo In Clip\"")] =
                    $"\t\t\"Ammo In Clip\"\t\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_ammo_clip)}\"";
                lines[Utilities.FindIndex(lines, "\"Ammo In Reserve\"")] =
                    $"\t\t\"Ammo In Reserve\"\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_ammo_reserve)}\"";
                lines[Utilities.FindIndex(lines, "\"Ammo In Clip Low\"")] =
                    $"\t\t\"Ammo In Clip Low\"\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_ammo_clip_low)}\"";
                lines[Utilities.FindIndex(lines, "\"Ammo In Reserve Low\"")] =
                    $"\t\t\"Ammo In Reserve Low\"\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_ammo_reserve_low)}\"";
                // Crosshair
                lines[Utilities.FindIndex(lines, "\"Crosshair\"")] =
                    $"\t\t\"Crosshair\"\t\t\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_xhair_normal)}\"";
                lines[Utilities.FindIndex(lines, "CrosshairDamage")] =
                    $"\t\t\"CrosshairDamage\"\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_xhair_pulse)}\"";
                // ÜberCharge
                lines[Utilities.FindIndex(lines, "\"Uber Bar Color\"")] =
                    $"\t\t\"Uber Bar Color\"\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_uber_bar)}\"";
                lines[Utilities.FindIndex(lines, "\"Solid Color Uber\"")] =
                    $"\t\t\"Solid Color Uber\"\t\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_uber_full)}\"";
                lines[Utilities.FindIndex(lines, "\"Flashing Uber Color1\"")] =
                    $"\t\t\"Flashing Uber Color1\"\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_uber_flash1)}\"";
                lines[Utilities.FindIndex(lines, "\"Flashing Uber Color2\"")] =
                    $"\t\t\"Flashing Uber Color2\"\t\t\"{Utilities.RgbConverter(Properties.rayshud.Default.color_uber_flash2)}\"";
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_colors, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the position of the damage value.
        /// </summary>
        private static bool DamagePosition()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_huddamageaccount, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                var start = Utilities.FindIndex(lines, "DamageAccountValue");
                lines[Utilities.FindIndex(lines, "\"xpos\"", start)] =
                    $"\t\t\"xpos\"\t\t\t\t\t\"{(Properties.rayshud.Default.toggle_damage_pos ? "c-188" : "c108")}\"";
                lines[Utilities.FindIndex(lines, "\"xpos_minmode\"", start)] =
                    $"\t\t\"xpos_minmode\"\t\t\t\"{(Properties.rayshud.Default.toggle_damage_pos ? "c-138" : "c58")}\"";
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_damage_pos, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the position of the metal counter.
        /// </summary>
        private static bool MetalPosition()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_hudlayout, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                var start = Utilities.FindIndex(lines, "CHudAccountPanel");
                lines[Utilities.FindIndex(lines, "\"xpos\"", start)] =
                    $"\t\t\"xpos\"\t\t\t\t\t\"{(Properties.rayshud.Default.toggle_metal_pos ? "c-20" : "c200")}\"";
                lines[Utilities.FindIndex(lines, "\"xpos_minmode\"", start)] =
                    $"\t\t\"xpos_minmode\"\t\t\t\"{(Properties.rayshud.Default.toggle_metal_pos ? "c-30" : "c130")}\"";
                lines[Utilities.FindIndex(lines, "\"ypos\"", start)] =
                    $"\t\t\"ypos\"\t\t\t\t\t\"{(Properties.rayshud.Default.toggle_metal_pos ? "c110" : "c130")}\"";
                lines[Utilities.FindIndex(lines, "\"ypos_minmode\"", start)] =
                    $"\t\t\"ypos_minmode\"\t\t\t\"{(Properties.rayshud.Default.toggle_metal_pos ? "c73" : "c83")}\"";
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_damage_pos,
                    ex.Message); // TODO: Use a unique error message.
                return false;
            }
        }

        /// <summary>
        ///     Set the player health style.
        /// </summary>
        private static bool HealthStyle()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_hudplayerhealth, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                var index = Properties.rayshud.Default.val_health_style -
                            1; // TODO: These next three lines are a bit ugly.
                lines[0] = Utilities.CommentOutTextLine(lines[0]);
                lines[1] = Utilities.CommentOutTextLine(lines[1]);
                if (Properties.rayshud.Default.val_health_style > 0)
                    lines[index] = lines[index].Replace("//", string.Empty);
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_health_style, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Toggle custom main menu backgrounds.
        /// </summary>
        /// <remarks>TODO: Consider refactoring.</remarks>
        private static bool MainMenuBackground()
        {
            try
            {
                var line1 = "\t\"$baseTexture\" \"console/backgrounds/background_modern\"";
                var line2 = "\t\"$baseTexture\" \"console/backgrounds/background_modern_widescreen\"";
                var chapterbackgrounds = string.Format(Properties.Resources.file_chapterbackgrounds, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var chapterbackgroundsTemp = chapterbackgrounds.Replace(".txt", ".file");

                // Restore the backgrounds to the default
                if (File.Exists(chapterbackgroundsTemp))
                    File.Move(chapterbackgroundsTemp, chapterbackgrounds);

                switch (Properties.rayshud.Default.val_main_menu_bg)
                {
                    case 1: // Classic
                        line1 = "\t\"$baseTexture\" \"console/backgrounds/background_classic\"";
                        line2 = "\t\"$baseTexture\" \"console/backgrounds/background_classic_widescreen\"";
                        break;

                    case 2: // Default
                        if (File.Exists(chapterbackgrounds))
                            File.Move(chapterbackgrounds, chapterbackgroundsTemp);
                        return true;
                }

                var file = string.Format(Properties.Resources.file_background_upward, MainWindow.HudPath,
                    MainWindow.HudSelection, "upward.vmt");
                var lines = File.ReadAllLines(file);
                var start = Utilities.FindIndex(lines, "baseTexture");
                lines[start] = line1;
                File.WriteAllLines(file, lines);

                file = string.Format(Properties.Resources.file_background_upward, MainWindow.HudPath,
                    MainWindow.HudSelection, "upward_widescreen.vmt");
                lines = File.ReadAllLines(file);
                lines[start] = line2;
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_menu_background,
                    ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Toggle main menu class images.
        /// </summary>
        private static bool MainMenuClassImage()
        {
            try
            {
                var file = Properties.rayshud.Default.toggle_classic_menu
                    ? string.Format(Properties.Resources.file_custom_mainmenu_classic, MainWindow.HudPath,
                        MainWindow.HudSelection)
                    : string.Format(Properties.Resources.file_custom_mainmenu, MainWindow.HudPath,
                        MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                var start = Utilities.FindIndex(lines, "TFCharacterImage");
                lines[Utilities.FindIndex(lines, "ypos", start)] =
                    $"\t\t\"ypos\"\t\t\t\"{(Properties.rayshud.Default.toggle_menu_images ? "-80" : "9999")}\"";
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_menu_class_image,
                    ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the main menu style.
        /// </summary>
        /// <remarks>TODO: Consider refactoring.</remarks>
        private static bool MainMenuStyle()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_mainmenuoverride, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                var index = Properties.rayshud.Default.toggle_classic_menu ? 1 : 2;
                lines[1] = Utilities.CommentOutTextLine(lines[1]);
                lines[2] = Utilities.CommentOutTextLine(lines[2]);
                lines[index] = lines[index].Replace("//", string.Empty);
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_main_menu, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the scoreboard style.
        /// </summary>
        private static bool ScoreboardStyle()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_scoreboard, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                lines[0] = Properties.rayshud.Default.toggle_min_scoreboard
                    ? lines[0].Replace("//", string.Empty)
                    : Utilities.CommentOutTextLine(lines[0]);
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_scoreboard, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the team and class select style.
        /// </summary>
        private static bool TeamSelect()
        {
            try
            {
                // CLASS SELECT
                var file = string.Format(Properties.Resources.file_classselection, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                lines[0] = Properties.rayshud.Default.toggle_center_select
                    ? lines[0].Replace("//", string.Empty)
                    : Utilities.CommentOutTextLine(lines[0]);
                File.WriteAllLines(file, lines);

                // TEAM MENU
                file = string.Format(Properties.Resources.file_teammenu, MainWindow.HudPath, MainWindow.HudSelection);
                lines = File.ReadAllLines(file);
                lines[0] = Properties.rayshud.Default.toggle_center_select
                    ? lines[0].Replace("//", string.Empty)
                    : Utilities.CommentOutTextLine(lines[0]);
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_team_select, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the ÜberCharge style.
        /// </summary>
        private static bool UberchargeStyle()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_hudanimations, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                var start = Utilities.FindIndex(lines, "HudMedicCharged");
                var index1 = Utilities.FindIndex(lines, "HudMedicOrangePulseCharge", start);
                var index2 = Utilities.FindIndex(lines, "HudMedicSolidColorCharge", start);
                var index3 = Utilities.FindIndex(lines, "HudMedicRainbowCharged", start);
                lines[index1] = Utilities.CommentOutTextLine(lines[index1]);
                lines[index2] = Utilities.CommentOutTextLine(lines[index2]);
                lines[index3] = Utilities.CommentOutTextLine(lines[index3]);
                var index = Properties.rayshud.Default.val_uber_animation switch
                {
                    1 => index2,
                    2 => index3,
                    _ => index1
                };
                lines[index] = lines[index].Replace("//", string.Empty);
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_uber_animation, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the player model position and orientation.
        /// </summary>
        private static bool PlayerModelPos()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_hudplayerclass, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                lines[0] = Properties.rayshud.Default.toggle_alt_player_model
                    ? lines[0].Replace("//", string.Empty)
                    : Utilities.CommentOutTextLine(lines[0]);
                File.WriteAllLines(file, lines);

                file = string.Format(Properties.Resources.file_hudlayout, MainWindow.HudPath, MainWindow.HudSelection);
                lines = File.ReadAllLines(file);
                var start = Utilities.FindIndex(lines, "DisguiseStatus");
                lines[Utilities.FindIndex(lines, "xpos", start)] =
                    $"\t\t\"xpos\"\t\t\t\t\t\"{(Properties.rayshud.Default.toggle_alt_player_model ? 100 : 15)}\"";
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_player_model_pos,
                    ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Set the chatbox position.
        /// </summary>
        private static bool ChatBoxPos()
        {
            try
            {
                var file = string.Format(Properties.Resources.file_basechat, MainWindow.HudPath,
                    MainWindow.HudSelection);
                var lines = File.ReadAllLines(file);
                var start = Utilities.FindIndex(lines, "HudChat");
                lines[Utilities.FindIndex(lines, "ypos", start)] =
                    $"\t\t\"ypos\"\t\t\t\t\t\"{(Properties.rayshud.Default.toggle_chat_bottom ? 315 : 25)}\"";
                File.WriteAllLines(file, lines);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.ShowMessageBox(MessageBoxImage.Error, Properties.Resources.error_chat_pos, ex.Message);
                return false;
            }
        }

        #endregion CONTROLLER
    }
}