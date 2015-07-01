using System;
using System.Collections.Generic;

using UnityEngine;
using Rocket.Unturned.Plugins;
using Rocket.Unturned.Player;

using static Rocket.Unturned.Logging.Logger;
using static Rocket.Unturned.RocketChat;
using Rocket.Unturned;
using SDG.Unturned;

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
    public class GearUp : RocketPlugin<GearUpConf> {
        public static Version Version = new Version(0, 0, 4, 6);
        public static GearUp Instance;

        public static GearUpCommand GearUpCmd;

        protected override void Load() {
            Instance = this;
            HookEvents();
            Log(this.Configuration.LoadedText);
            }

        protected override void Unload() {
            UnregisterEvents();
            base.Unload();
            }
        
        public void FixedUpdate() {
            // don't need this anymore :o
            }

        private void HookEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive += EventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnServerShutdown += EventServerShutdown;
            }

        private void UnregisterEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive -= EventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnServerShutdown -= EventServerShutdown;
            }

        private void EventServerShutdown() {
            UnregisterEvents();
            }

        private void EventPlayerSpawn(RocketPlayer player, Vector3 position, byte angle) {
            player.GetComponent<GearUpComp>().Timer.Start();
            }

        private void CriticalError() {
            Say("GearUp has stopped due to an error.", Color.red);
            this.Configuration.Enabled = false;
            }

        public override Dictionary<string, string> DefaultTranslations {
            get {
                return new Dictionary<string, string>() {
                        { "gear_given", "You've got stuff!" },
                        { "gear_gift", "It seems %P gave you some stuff." },
                        { "gear_gift_success", "GearUp given to %P." },
                        { "error_message", "An error occurred." },
                        { "access_denied", "GearUp.Self no permission." },
                        { "access_denied_gift", "GearUp.Gift no permission." },
                        { "command_disabled", "LoadOuts are spawn only!" },
                        { "command_announce", "Type /loadout to get some stuff!" },
                        { "not_ready", "GearUp will be available in %S seconds." },
                    };
                }
            }
        
        }

    }
