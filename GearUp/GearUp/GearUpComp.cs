using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using static Rocket.Unturned.Logging.Logger;
using static Rocket.Unturned.RocketChat;
using static Rocket.Mash.GearUp.GearUp;

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

        private bool Active {
            get { return (GearUp.Instance.Loaded && Config.Enabled); }
            }

        public void Start() {
            Available = DateTime.Now;
            Timer = new System.Timers.Timer(Config.SpawnDelay * 1000);
            Timer.Elapsed += Timer_Elapsed;

            int items = 0;
            foreach (SDG.Unturned.Items i in Player.Inventory.Items) {
                items += i.getItemCount();
                }

            if (items == 0) {
                if (Active)
                    Timer.Start();
                } else {
                Say(Player, TDict["not_new_player"], Config.InfoColor);
                }
            }

        public void AskGearUp(RocketPlayer from = null) {
            if ((Available > DateTime.Now) && from == null) {
                string notReadyMsg = TDict["not_ready"].Replace("%S", ((int)(Available - DateTime.Now).TotalSeconds).ToString());
                Say(Player, notReadyMsg, Config.ErrorColor);
                } else {
                Available = DateTime.Now.AddSeconds(Config.Cooldown);
                GiveGear(from);
                }
            }

        public void ResetCooldown(RocketPlayer player = null) {
            Available = DateTime.Now;
            if (player != null)
                Say(Player, $"GU: Cooldown was reset by {player.CharacterName}.", Config.SuccessColor);
            else
                Say(Player, "GU: Cooldown reset.", Config.SuccessColor);
            }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            Timer.Stop();
            if (Active)
                GiveGear();
            }

        private void GiveGear(RocketPlayer from = null) {
            foreach (Gear g in Config.GearList) {
                if (Player.GiveItem(g.ID, g.Amount) == false) {
                    LogError($"GearUp> Failed to give {Player.CharacterName} item {g.ID} x{g.Amount}.");
                    Say(from, TDict["error_message"], Config.ErrorColor);
                    }
                }

            if (from == null) {
                Say(Player, TDict["gear_given"], Config.SuccessColor);
                } else {
                Say(Player, TDict["gear_gift"].Replace("%P", from.CharacterName), Config.SuccessColor);
                Say(from, TDict["gear_gift_success"].Replace("%P", Player.CharacterName), Config.SuccessColor);
                }
            }
        }
    }