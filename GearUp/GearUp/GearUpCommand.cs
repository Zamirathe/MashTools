using System;
using System.Collections.Generic;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using UnityEngine;

using static Rocket.Unturned.RocketChat;
using static Rocket.Unturned.Logging.Logger;
using Rocket.API;
using SDG.Unturned;
using Rocket.Unturned.Plugins;

/*  All code is copyright © 2015 Auria.pw
    Code, and their compiled assemblies, are released (forcefully)
    under the GNU GPL. By using any of this code and/or software
    you agree to hold indemnify the author and any associated
    peoples from which this software was obtained.
    
    This disclaimer/notice is to be maintained in all source files
    and the author tags are not to be removed any versions of this
    software.

    - Mash    
    
    Rocket   - copyright © 2015 ROCKET FOUNDATION
    Unturned - copyright © 2015 Smartly Dressed Games
    Unity    - Copyright © 2015 Unity Technologies
    */

namespace Rocket.Mash.GearUp {
    public class GearUpCommand : IRocketCommand {

        private enum PERM : int {
            None = 0,
            Info = 1,
            Self = 2,
            Other = 3,
            Admin = 4,
            };

        private bool Initialized = false;
        private GearUpConf Config;

        #region CmdConf
        public bool RunFromConsole { get { return false; } }

        public string Name { get { return "gearup"; } }

        public string Help { get { return "See /gearup -?"; } }

        public string Syntax {
            get {
                return "/GearUp [params]";
                }
            }

        public List<string> Aliases {
            get {
                return new List<String>() { "gear", "gu", "kit" };
                }
            }

        #endregion CmdConf

        private void Initialize() {
            Initialized = true;
            Config = GearUp.Instance.Configuration;
            }

        private void ShowHelp(RocketPlayer p) {
            foreach (string s in Config.HelpText) {
                Say(p, $"{s}", Config.InfoColor);
                }
            }

        private void ReportPermittedKits(RocketPlayer p) {
            string kitsList = ""; //Config.DefaultKit.Name;
            foreach (Kit k in Config.UserKits) {
                if (p.HasPermission("gearup.kits." + k.PermissionGroup))
                    kitsList += k.Name + ", ";
                }
            //if (kitsList.Length < 3)
            //    return;

            //kitsList.Remove(kitsList.Length - 3, 2);
            kitsList = kitsList.TrimEnd(", ".ToCharArray());
            Say(p, $"Kits: {kitsList}", Config.InfoColor);
            return;
            }

        private PERM GetPermLevel(RocketPlayer p) {
            PERM perm = PERM.None;

            if (p.HasPermission(@"gearup.info"))
                perm = PERM.Info;

            if (p.HasPermission(@"gearup.self"))
                perm = PERM.Self;

            if (p.HasPermission(@"gearup.other"))
                perm = PERM.Other;

            if (p.HasPermission(@"gearup.admin"))
                perm = PERM.Admin;

            return perm;
            }

        private Kit GetKitByName(string name) {
            Kit retval = null;
            foreach (Kit k in Config.UserKits) {
                if (k.Name.ToLower() == name.ToLower()) {
                    retval = k;
                    break;
                    }
                }

            return retval;
            }

        public void Execute(RocketPlayer player, string[] cmd) {
            if (!Initialized)
                Initialize();

            if (player == null)
                return;

            if (!Config.AllowCmd) {
                Say(player, GearUp.TDict["command_disabled"]?.ToString(), Config.ErrorColor);
                Log("GU: Commands are disabled.");
                return;
                }

            PERM perms = GetPermLevel(player);

            try {

                if (perms <= PERM.None && string.Join("", cmd) != "-info") {
                    Say(player, GearUp.TDict["access_denied"]?.ToString(), Color.red);
                    return;
                    }

                if (cmd.Length == 0 && perms >= PERM.Self) {
                    player.GetComponent<GearUpComp>()?.AskGearUp();
                    return;
                    }

                if (cmd.Length >= 1) {

                    if (!cmd[0].StartsWith("-")) {

                        RocketPlayer pFromCmd = RocketPlayer.FromName(cmd[0]);
                        Kit kFromCmd = GetKitByName(cmd[0]);

                        // kit
                        if (kFromCmd != null && perms >= PERM.Self) {
                            player.GetComponent<GearUpComp>()?.AskGearUp(null, kFromCmd);
                            return;
                            }

                        // player
                        if (pFromCmd != null && perms >= PERM.Other) {
                            if (cmd.Length >= 2) {
                                kFromCmd = GetKitByName(cmd[1]);
                                if (kFromCmd != null) {
                                    pFromCmd.GetComponent<GearUpComp>()?.AskGearUp(player, kFromCmd);
                                    //Say(player, $"{GearUp.TDict["gear_gift_success"]?.Replace("%P", pFromCmd.CharacterName)}", Config.SuccessColor);
                                    return;
                                    } else {
                                    Say(player, $"{GearUp.TDict["access_denied_gift"]?.ToString()}", Config.ErrorColor);
                                    return;
                                    }
                                } else {
                                Say(player, $"{GearUp.TDict["error_user_nokit"]?.ToString()}", Config.ErrorColor);
                                return;
                                }
                            }

                        // neither; bad user, no biscuit! D:<
                        Say(player, $"No matching kits or players, kits: ", Config.ErrorColor);
                        ReportPermittedKits(player);
                        return;

                        } else if (cmd[0].StartsWith("-")) {

                        switch (cmd[0]) {
                            case "-on":
                                if ((int)perms < (int)PERM.Admin)
                                    return;
                                GearUp.Instance.enabled = true;
                                Say(player, "GU: Enabled", Config.SuccessColor);
                                break;

                            case "-off":
                                if ((int)perms < (int)PERM.Admin)
                                    return;
                                GearUp.Instance.enabled = false;
                                Say(player, "GU: Disabled", Config.SuccessColor);
                                break;

                            case "-kits":
                            case "-list":
                            case "-l":
                                ReportPermittedKits(player);
                                break;

                            case "-?":
                                if ((int)perms < (int)PERM.Info)
                                    return;
                                ShowHelp(player);
                                break;

                            case "--":
                                if ((int)perms < (int)PERM.Info)
                                    return;
                                Say(player, $"GU: Plugin {(GearUp.Instance.enabled == true ? "enabled" : "disabled")}.", Color.gray);
                                break;

                            case "-reset":
                                if ((int)perms < (int)PERM.Admin)
                                    return;
                                if (cmd.Length >= 2) {
                                    if (!string.IsNullOrEmpty(cmd[1])) {
                                        RocketPlayer p = RocketPlayer.FromName(cmd[1]);
                                        if (p == null) {
                                            Say(player, $"GU: Failed to find player name matching '{cmd[1]}'!", Config.ErrorColor);
                                            } else {
                                            p.GetComponent<GearUpComp>()?.ResetCooldown(player);
                                            }
                                        }
                                    } else {
                                    player.GetComponent<GearUpComp>()?.ResetCooldown();
                                    }
                                break;

                            case "-info":
                                Say(player, $"GearUp {GearUp.Version} by Mash - Auria.pw [{(GearUp.Instance.enabled == true ? "enabled" : "disabled")}]", Config.InfoColor);
                                break;

                            default:
                                Say(player, "GU: Unknown operand", Config.ErrorColor);
                                break;

                            }
                        return;
                        }
                    }
                } catch (Exception ex) {

                GearUp.STOP(ex.Message);

                }
            }
        }
    }
