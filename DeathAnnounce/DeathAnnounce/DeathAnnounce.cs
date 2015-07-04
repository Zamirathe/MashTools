using Rocket.Unturned.Player;
using Rocket.Unturned.Plugins;

using static Rocket.Unturned.RocketChat;
using static Rocket.Unturned.Events.RocketPlayerEvents;
using static Rocket.Unturned.Logging.Logger;
using System;
using System.IO;
using SDG.Unturned;
using System.Collections.Generic;

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
    public class DeathAnnounce : RocketPlugin<DeathAnnounceConf> {
        public static System.Version Version = new Version(0, 0, 3, 2);
        public static DeathAnnounce Instance;

        private Dictionary<EDeathCause, DAUserMsg> CauseLookup;

        protected override void Load() {
            Instance = this;
            hookEvents();
            CauseLookup = new Dictionary<EDeathCause, DAUserMsg>();

            foreach (DAUserMsg daum in Configuration.UserMessages)
                CauseLookup.Add((EDeathCause)Enum.Parse(typeof(EDeathCause), daum.Cause), daum);

            Log(Instance.Configuration.LoadedText);
            }

        protected override void Unload() {
            unregisterEvents();
            base.Unload();
            }

        private void hookEvents() {
            OnPlayerDeath += DA_OnPlayerDeath;
            }

        private void unregisterEvents() {
            OnPlayerDeath -= DA_OnPlayerDeath;
            }

        private void DA_OnPlayerDeath(RocketPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer) {

            if (!this.Loaded || !this.Configuration.Enabled)
                return;

            if (murderer.ToString() == "90071992547409920") { murderer = (Steamworks.CSteamID)0; }
            if (murderer == null) { murderer = (Steamworks.CSteamID)0; }
            
            string Killer = string.Empty;
            if (murderer.ToString().Length > 1)
                Killer = RocketPlayer.FromCSteamID(murderer)?.CharacterName;

            string Message="";

            // if Killer is nothing AND we have notempty altMsg
            if (String.IsNullOrEmpty(Killer) && !String.IsNullOrEmpty(CauseLookup[cause].AltMessage)) {
                Message = CauseLookup[cause].AltMessage.Replace(@"%P", player.CharacterName);
                } else {
                Message = CauseLookup[cause].Message.Replace(@"%K", Killer);
                Message = Message.Replace(@"%P", player.CharacterName);
                }

            Say(Message, CauseLookup[cause].Color);

            }
        }
    }
