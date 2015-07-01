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

        private bool Initialized = false;
        private GearUpConf Config;

        #region CmdConf
        public bool RunFromConsole { get { return false; } }

        public string Name { get { return "gearup"; } }

        public string Help { get { return "Grants a starting set of equipment."; } }

        public string Syntax {
            get {
                return "None.";
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

        public void Execute(RocketPlayer player, string[] cmd) {
            if (!Initialized)
                Initialize();

            if (player == null)
                return;

            if (!Config.AllowFromCommand) {
                Say(player, GearUp.Instance.Translations["command_disabled"], Config.ErrorColor);
                Log("Command disabled.");
                return;
                }

            if (!player.HasPermission("GearUp") && !player.HasPermission("*")) {
                Say(player, GearUp.Instance.Translations["access_denied"], Color.red);
                Log($"GearUp> {player.CharacterName} doesn't have permission.");
                return;
                }

            if (cmd.Length == 0) {
                Log($"GearUp> Called by {player.CharacterName}");
                player.GetComponent<GearUpComp>().AskLoadOut();
                } else {
                Log("cmd.Length > 0");
                if (player.HasPermission("GearUp.Other") || player.HasPermission("*")) {
                    Log("Has permission to give other.");
                    if (RocketPlayer.FromName(cmd[1]) != null) {
                        Log("RP.FromName != null");
                        RocketPlayer p = RocketPlayer.FromName(cmd[1]);
                        if (p != null) {
                            p.GetComponent<GearUpComp>().AskLoadOut(p);
                            }
                        }
                    } else {
                    Say(player, GearUp.Instance.Translations["access_denied_gift"], Config.ErrorColor);
                    }
                }
            }
        }
    }
