using System;
using System.Collections.Generic;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;

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


namespace Rocket.Mash.RuleBook {
    public class RulesCommand : IRocketCommand {
        public bool RunFromConsole { get { return false; } }
        public string Name { get { return "rules"; } }
        public string Help { get { return "Displays the server rules."; } }

        public string Syntax {
            get {
                return "/rules [page number]. ie; /rules 3";
                }
            }

        public List<string> Aliases {
            get {
                return new List<string>();
                }
            }

        public void Execute(RocketPlayer player, string[] args) {
            string cmd = args.Length > 0 ? args[0].ToString() : "0";
            Log($"Rules[{cmd}] called by {player.CharacterName}");
            int pageNum = 0;
            if (cmd?.Length >= 1) {
                if (int.TryParse(cmd, out pageNum))
                    RuleBook.Instance.Announce(player, true, pageNum);
                } else {
                    RuleBook.Instance.Announce(player, true, 0);
                }
            }
        }
    }
