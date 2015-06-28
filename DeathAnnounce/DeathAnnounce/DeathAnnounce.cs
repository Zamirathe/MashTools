using Rocket.Unturned.Player;
using Rocket.Unturned.Plugins;
using UnityEngine;

using static Rocket.Unturned.RocketChat;
using static Rocket.Unturned.Events.RocketPlayerEvents;
using static Rocket.Unturned.Logging.Logger;
using System.Collections.Generic;

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

            Say($"{player.CharacterName} {Instance.Translate(cause.ToString().ToUpper())}", Color.grey);
            
            }

        public override Dictionary<string, string> DefaultTranslations {
            get {
                return new Dictionary<string, string>() {
                        { "BLEEDING", "bled out." },
                        { "BONES", "fell to pieces." },
                        { "FREEZING", "turned into a popsicle." },
                        { "FOOD", "starved to death." },
                        { "WATER", "died of thirst." },
                        { "GUN", "was shot dead." },
                        { "MELEE", "got the axe." },
                        { "ZOMBIE", "tried making out with a zombie." },
                        { "SUICIDE", "just couldn't take it anymore." },
                        { "KILL", "was killed." },
                        { "INFECTION", "died of zombgreen." },
                        { "PUNCH", "got knocked out." }
                    };
                }
            }
        }
    }
