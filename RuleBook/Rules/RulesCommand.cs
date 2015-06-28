using System;
using System.Collections.Generic;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;

using static Rocket.Unturned.Logging.Logger;

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
