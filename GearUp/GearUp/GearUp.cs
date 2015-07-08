using System;
using System.Collections.Generic;

using UnityEngine;
using Rocket.Unturned.Plugins;
using Rocket.Unturned.Player;

using static Rocket.Unturned.Logging.Logger;
using static Rocket.Unturned.RocketChat;

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
        public static Version Version = new Version(0, 0, 7, 0);
        public static GearUp Instance;
        public static DatabaseMgr Database;

        public static GearUpCommand GearUpCmd;
        public static Dictionary<string, string> TDict;
        public static GearUpConf Config;

        protected override void Load() {
            Instance = this;
            Database = new DatabaseMgr();
            TDict = DefaultTranslations;
            Config = Configuration;
            HookEvents();
            Log(this.Configuration.LoadedText);
            }

        protected override void Unload() {
            UnregisterEvents();
            base.Unload();
            }
        
        public void FixedUpdate() {
            }

        private void HookEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive += EventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnServerShutdown += EventServerShutdown;
            Unturned.Events.RocketServerEvents.OnPlayerDisconnected += EventPlayerDisconnected;
            Unturned.Events.RocketServerEvents.OnPlayerConnected += EventPlayerConnected;
            }

        private void UnregisterEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive -= EventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnServerShutdown -= EventServerShutdown;
            Unturned.Events.RocketServerEvents.OnPlayerDisconnected -= EventPlayerDisconnected;
            Unturned.Events.RocketServerEvents.OnPlayerConnected -= EventPlayerConnected;
            }

        private void EventPlayerDisconnected(RocketPlayer player) {
            this.Database.AddCooldown("Global", player.CSteamID.ToString(), player.GetComponent<GearUpComp>().Available);
        }
        
        private void EventPlayerConnected(RocketPlayer player) {
            DateTime? cooldown = this.Database.GetCooldown("Global", player.CSteamID.ToString());
            if (cooldown != null)
                player.GetComponent<GearUpComp>().Available = cooldown.Value;
        }
        private void EventServerShutdown() {
            UnregisterEvents();
            }

        private void EventPlayerSpawn(RocketPlayer player, Vector3 position, byte angle) {
            player.GetComponent<GearUpComp>().Timer.Start();
            }

        public static void STOP(string err = null) { Instance.CriticalError(err); }
        private void CriticalError(string err = null) {
            this.Configuration.Enabled = false;
            Say($"GearUp {Version} has been disabled due to an error.", Color.red);
            LogError(err);
            }

        public override Dictionary<string, string> DefaultTranslations {
            get {
                return new Dictionary<string, string>() {
                        { "gear_given", "GU: You've got stuff!" },
                        { "gear_gift", "GU: It seems %P gave you some stuff." },
                        { "gear_gift_success", "GU: Gear sent to %P." },
                        { "error_message", "GU: An error occurred." },
                        { "error_user_nokit", "GU: Must specify a kit." },
                        { "access_denied", "GU: No permission." },
                        { "access_denied_gift", "GU: No 'other' permission." },
                        { "command_disabled", "GU: GearUps are spawn only!" },
                        { "command_announce", "GU: Type /gear to get some stuff!" },
                        { "not_ready", "GU: Available in %S seconds." },
                        { "not_new_player", "GU: Not eligible of gear." },
                    };
                }
            }
        
        }

    }
