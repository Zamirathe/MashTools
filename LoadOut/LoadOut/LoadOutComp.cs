using Rocket.Unturned.Player;
using System;

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

namespace Rocket.Mash.LoadOut {

    public class LoadOutComp : RocketPlayerComponent {
        public DateTime Available;
        public System.Timers.Timer Timer;

        private LoadOutConf Config;

        private bool Active {
            get { return (LoadOut.Instance.Loaded && Config.Enabled && Player.HasPermission("LoadOut.Receive")); }
            }

        public void Start() {
            Config = LoadOut.Instance.Configuration;
            Available = DateTime.Now;
            Timer = new System.Timers.Timer(LoadOut.Instance.Configuration.SpawnDelay * 1000);
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
            }

        public void AskLoadOut(string from = null) {
            if (Available > DateTime.Now) {
                string notReadyMsg = Config.CommandCooldownNotReadyMessage.Replace("%S", ((int)(Available - DateTime.Now).TotalSeconds).ToString());
                Say(Player, Config.CommandCooldownNotReadyMessage, Config.ErrorColor);
                } else {
                Available = DateTime.Now.AddSeconds(Config.CommandCooldown);
                GiveLoadOut();
                }
            }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            Timer.Stop();
            if (Active)
                GiveLoadOut();
            }

        private void GiveLoadOut() {
            foreach (LoadOutEquip loe in LoadOut.Instance.Configuration.LoadOutEquipment) {
                if (Player.GiveItem(loe.EntityId, loe.EntityAmount) == false) {
                    LogError($"LoadOut> Failed to give {Player.CharacterName} item {loe.EntityId} x{loe.EntityAmount}.");
                    }
                }

            Say(Player, Config.LoadOutGivenMessage, Config.SuccessColor);

            }


        }
    }