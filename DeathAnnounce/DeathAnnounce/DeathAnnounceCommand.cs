using System;
using System.Collections.Generic;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using UnityEngine;

using static Rocket.Unturned.RocketChat;
using static Rocket.Unturned.Logging.Logger;

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

namespace Rocket.Mash.DeathAnnounce {
    public class DeathAnnounceCommand : IRocketCommand {

        public bool RunFromConsole { get { return false; } }

        public string Name { get { return "DA"; } }

        public string Help { get { return "Toggles death annoucements on/off."; } }

        public string Syntax {
            get {
                return "/DA [on|off] - If [on|off] not specified, shows current state.";
                }
            }

        public List<string> Aliases {
            get {
                    return new List<String>() { };
                }
            }

        public void Execute(RocketPlayer player, string[] cmd) {

            if (player == null)
                return;

            if (!player.HasPermission("DeathAnnounce") && !player.HasPermission("*")) {
                Say(player, DeathAnnounce.Instance.Configuration.AccessDeniedMessage.Replace("%U", player.CharacterName), Color.red);
                Log($"DeathAnnounce> {player.CharacterName} doesn't have permission.");
                return;
                }

            if (cmd[0] != "on" || cmd[0] != "off")
                Say(player, $"DeathAnnounce is currently {(DeathAnnounce.Instance.Configuration.Enabled == true ? "on" : "off")}");
              else if (cmd[0].ToLower() == "on")
                DeathAnnounce.Instance.Configuration.Enabled = true;
              else if (cmd[0].ToLower() == "off")
                DeathAnnounce.Instance.Configuration.Enabled = false;
                
            
            Log($"DeathAnnounce[{cmd[0]}]> Called by {player.CharacterName}");

            }

        }
    }
