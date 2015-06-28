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
    and the author tags are not to be removed from unmodified versions
    of this software.

    - Mash    
    
    Rocket   - copyright © 2015 ROCKET FOUNDATION
    Unturned - copyright © 2015 Smartly Dressed Games
    Unity    - Copyright © 2015 Unity Technologies
    */


namespace Rocket.Mash.LoadOut {
    public class LoadOutCommand : IRocketCommand {

        public bool RunFromConsole { get { return false; } }

        public string Name { get { return "LoadOut"; } }

        public string Help { get { return "Grants a starting set of equipment."; } }

        public string Syntax {
            get {
                return "None.";
                }
            }

        public List<string> Aliases {
            get {
                    return new List<String>() { "lo", "kit" };
                }
            }

        public void Execute(RocketPlayer player, string[] cmd) {

            if (!player.HasPermission("LoadOut") && !player.HasPermission("*")) {
                Say(player, LoadOut.Instance.Configuration.AccessDeniedMessage, Color.red);
                Log($"LoadOut> {player.CharacterName} doesn't have permission.");
                return;
                }
            
            if (!LoadOut.Instance.Configuration.AllowFromCommand) {
                Say(player, LoadOut.Instance.Configuration.CommandDisabledMessage, Color.yellow);
                return;
                }

            Log($"LoadOut> Called by {player.CharacterName}");

            LoadOut.Instance.GrantLoadOut(player, true);

            }

        }
    }
