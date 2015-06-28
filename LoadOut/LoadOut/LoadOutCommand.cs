using System;
using System.Collections.Generic;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using UnityEngine;

// static imports directly into our namespace.. So I can just Log(blah) as if it's a local func
using static Rocket.Unturned.RocketChat;
using static Rocket.Unturned.Logging.Logger;

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
                //if (LoadOut.Instance.Configuration.OverrideKitCommand)
                    return new List<String>() { "lo", "kit" };
                //else
                //    return new List<String>() { "lo", };
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
