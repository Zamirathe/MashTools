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

        private enum PERM : byte {
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
            if (player.HasPermission("GearUp.Info"))
                perms = PERM.Info;

            if (player.HasPermission("GearUp.Self"))
                perms = PERM.Self;

            if (player.HasPermission("GearUp.Other"))
                perms = PERM.Other;

            if (player.HasPermission("*") || player.HasPermission("GearUp.Admin") || player.HasPermission("GearUp.*"))
                perms = PERM.Admin;

            if (!Config.AllowFromCommand) {
                Say(player, GearUp.Instance.Translations["command_disabled"], Config.ErrorColor);
                Log("Command disabled.");
                return;
                }

            if (perms == PERM.None && cmd[0] != "-info") {
                Say(player, GearUp.Instance.Translations["access_denied"], Color.red);
                Log($"GearUp> {player.CharacterName} doesn't have permission.");
                return;
                }

            if (cmd.Length == 0) {
                if (perms < PERM.Self)
                    return;

                Log($"GearUp> Called by {player.CharacterName}");
                player.GetComponent<GearUpComp>().AskGearUp();

                } else if (!cmd[0].StartsWith("-")) {

                Log("cmd.Length > 0");

                if (perms >= PERM.Other) {
                    Log("Has permission to give other.");
                    if (RocketPlayer.FromName(cmd[1]) != null) {
                        Log("RP.FromName != null");
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
                        if (perms < PERM.Admin)
                            return;
                        GearUp.Instance.enabled = true;
                        Say(player, "GearUp enabled.", Config.SuccessColor);
                        break;

                    case "-off":
                        if (perms < PERM.Admin)
                            return;
                        GearUp.Instance.enabled = false;
                        Say(player, "GearUp disabled.", Config.SuccessColor);
                        break;

                    case "-?":
                        if (perms < PERM.Info)
                            return;
                        ShowHelp(player);
                        break;

                    case "--":
                        if (perms < PERM.Info)
                            return;
                        Say(player, $"GearUp plugin {(GearUp.Instance.enabled == true ? "enabled" : "disabled")}.", Color.gray);
                        break;

                    case "-reset":
                        if (perms < PERM.Admin)
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
                        Say(player, $"GearUp {GearUp.Version} by Mash - Auria.pw", Config.InfoColor);
                        break;

                    default:
                        Say(player, "Unknown operand", Config.ErrorColor);
                        break;

                    }
                }
            }
        }
    }
