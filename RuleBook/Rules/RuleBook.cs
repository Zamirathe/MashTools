using System;
using System.Collections.Generic;

using Rocket.Unturned.Plugins;
using Rocket.Unturned.Player;
using UnityEngine;

using static Rocket.Unturned.RocketChat;
using static Rocket.Unturned.Events.RocketServerEvents;
using static Rocket.Unturned.Logging.Logger;

namespace Rocket.Mash.RuleBook {
    public class RuleBook : RocketPlugin<RuleBookConf> {
        public static Version Version = new Version(0, 0, 1, 0);
        public static RuleBook Instance;

        private List<AnnouncementQueue> pending;

        protected override void Load() {
            pending = new List<AnnouncementQueue>();
            Instance = this;
            hookEvents();
            Log(Configuration.LoadedText);
            }

        protected override void Unload() {
            unregisterEvents();
            base.Unload();
            }

        public void FixedUpdate() {
            if (this.Loaded && this.Configuration.Enabled) {
                if (pending.Count > 0) {
                    List<AnnouncementQueue> toFire = new List<AnnouncementQueue>();
                    foreach (AnnouncementQueue que in pending)
                        if (que.TimeToAnnounce > DateTime.Now) {
                            toFire.Add(que);
                            }
                    foreach (AnnouncementQueue que in toFire) {
                        pending.Remove(que);
                        Announce(que.Player);
                        }
                    }
                }
            }

        private void hookEvents() {
            OnPlayerConnected += rocketServerEvents_OnPlayerConnected;
            OnServerShutdown += rocketServerEvents_OnServerShutdown;
            }

        private void unregisterEvents() {
            OnPlayerConnected -= rocketServerEvents_OnPlayerConnected;
            OnServerShutdown -= rocketServerEvents_OnServerShutdown;
            }
        
        private void rocketServerEvents_OnServerShutdown() {
            unregisterEvents();
            }

        private void rocketServerEvents_OnPlayerConnected(RocketPlayer player) {
            pending.Add(
                new AnnouncementQueue() {
                    TimeToAnnounce = DateTime.Now.AddSeconds(Configuration.ConnectDelay),
                    Player = player
                    }
                );
            }

        public void Announce(RocketPlayer player, bool isCommand = false, int pageNum = 0) {
            if (pageNum > 0)
                pageNum--;
            int ruleStart = (pageNum * 4);
            if (!isCommand) {
                foreach (string rulesLine in Configuration.AnnounceText) {
                    Say(player, rulesLine, Color.cyan);
                    }
                } else {
                int lineCount = ruleStart;
                for (int i = ruleStart; i <= ruleStart + 3; i++) {
                    lineCount++;
                    if (i >= Configuration.RulesText.Length)
                        continue;
                    Say(player, $"{lineCount}. {Configuration.RulesText[i]}", Color.magenta);
                    }
                }

            }

        }

    }
