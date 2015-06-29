using Rocket.Unturned.Player;
using Rocket.Unturned.Plugins;
using UnityEngine;

using static Rocket.Unturned.RocketChat;
using static Rocket.Unturned.Events.RocketPlayerEvents;
using static Rocket.Unturned.Logging.Logger;
using System.Collections.Generic;

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


namespace Rocket.Mash.DeathAnnounce {
    public class DeathAnnounce : RocketPlugin<DeathAnnounceConf> {
        public static System.Version Version = new System.Version(0, 0, 1, 0);
        public static DeathAnnounce Instance;

        protected override void Load() {
            Instance = this;
            hookEvents();
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

            DAUserMsg DAMsg = null;
            string Msg = string.Empty;
            string Name = RocketPlayer.FromCSteamID(murderer).CharacterName;
            
            foreach (DAUserMsg daum in Configuration.UserMessages)
                if (daum.Cause.ToUpper() == cause.ToString().ToUpper())
                    DAMsg = daum;                    

            if (DAMsg == null) {
                Log($"DeathAnnounce: UserMessage was empty for cause '{cause.ToString().ToUpper()}'");
                return;
                }

            Msg = DAMsg.CMessage.Replace("{0}", Name);

            Say($"{player.CharacterName} {Msg}", DAMsg.CColor);
            
            }

        }
    }
