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
                return new List<String>() { "gear", "gu" };
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

        public void Execute(RocketPlayer player, string[] cmd) {
            if (!Initialized)
                Initialize();

            if (player == null)
                return;

            PERM perms = PERM.None;

            if (player.HasPermission(@"gearup.info"))
                perms = PERM.Info;

             if (player.HasPermission(@"gearup.self"))
                perms = PERM.Self;

             if (player.HasPermission(@"gearup.other"))
                perms = PERM.Other;

             if (player.HasPermission(@"gearup.admin"))
                perms = PERM.Admin;

            if (!Config.AllowCmd) {
                Say(player, GearUp.Instance.Translations["command_disabled"], Config.ErrorColor);
                Log("GU: Commands are disabled.");
                return;
                }

            if ((int)perms <= (int)PERM.None && string.Join("", cmd) != "-info") {                
                Say(player, GearUp.Instance.Translations["access_denied"], Color.red);
                Log($"GU: {player.CharacterName} doesn't have PERM => {perms.ToString()}");
                return;
                }
            if (cmd?.Length == 0) {
                if ((int)perms < (int)PERM.Self)
                    return;
                Log($"GU> Called by {player.CharacterName}");
                player.GetComponent<GearUpComp>().AskGearUp();
                } else if (!cmd[0].StartsWith("-")) {
                if ((int)perms >= (int)PERM.Other && cmd.Length >= 1) {
                        if (RocketPlayer.FromName(cmd[1]) != null) {
                            RocketPlayer p = RocketPlayer.FromName(cmd[1]);
                            if (p != null) {
                                p.GetComponent<GearUpComp>().AskGearUp(player);
                                }
                            }
                        } else {
                        Say(player, GearUp.Instance.Translations["access_denied_gift"], Config.ErrorColor);
                        return;
                        }
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
                                    p.GetComponent<GearUpComp>().ResetCooldown(player);
                                    }
                                }
                            } else {
                            player.GetComponent<GearUpComp>().ResetCooldown();
                            }
                        break;

                    case "-info":
                        Say(player, $"GearUp {GearUp.Version} by Mash - Auria.pw [{(GearUp.Instance.enabled == true ? "enabled" : "disabled")}]", Config.InfoColor);
                        break;

                    default:
                        Say(player, "GU: Unknown operand", Config.ErrorColor);
                        break;

                    }
                }
            }
        }
    }
