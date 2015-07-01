using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
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

    public class GearUpComp : RocketPlayerComponent {
        public DateTime Available;
        public System.Timers.Timer Timer;

        private Dictionary<string, string> Translations;
        private GearUpConf Config;

        private bool Active {
            get { return (GearUp.Instance.Loaded && Config.Enabled && Player.HasPermission("GearUp.Receive")); }
            }

        public void Start() {
            Config = GearUp.Instance.Configuration;
            Translations = GearUp.Instance.Translations;
            Available = DateTime.Now;
            Timer = new System.Timers.Timer(GearUp.Instance.Configuration.SpawnDelay * 1000);
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
            }

        private void OnPlayerConnected(RocketPlayer player) {
            throw new NotImplementedException();
            }

        public void AskLoadOut(RocketPlayer from = null) {
            Say(Player, "we were asked loadout.");
            if ((Available > DateTime.Now) && from == null) {
                string notReadyMsg = Translations["not_ready"].Replace("%S", ((int)(Available - DateTime.Now).TotalSeconds).ToString());
                Say(Player, notReadyMsg, Config.ErrorColor);
                } else {
                Log("Avail<Now | from != null");
                Available = DateTime.Now.AddSeconds(Config.CommandCooldown);
                GiveLoadOut(from);
                }
            }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            Timer.Stop();
            Log("Timer popped.");
            if (Active)
                GiveLoadOut();
            }

        private void GiveLoadOut(RocketPlayer from = null) {
            foreach (GearUpEquip loe in GearUp.Instance.Configuration.LoadOutEquipment) {
                if (Player.GiveItem(loe.EntityId, loe.EntityAmount) == false) {
                    LogError($"LoadOut> Failed to give {Player.CharacterName} item {loe.EntityId} x{loe.EntityAmount}.");
                    Say(from, Translations["error_message"], Config.ErrorColor);
                    }
                }

            if (from == null) {
                Say(Player, Translations["loadout_given"], Config.SuccessColor);
                } else {
                Say(Player, Translations["loadout_gift"].Replace("%P", from.CharacterName), Config.SuccessColor);
                Say(from, Translations["loadout_gift_success"].Replace("%P", Player.CharacterName), Config.SuccessColor);
                }
            }
        }
    }