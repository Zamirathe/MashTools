using System;
using System.Collections.Generic;

using UnityEngine;
using Rocket.Unturned.Plugins;
using Rocket.Unturned.Player;

using SDG;

using static Rocket.Unturned.Logging.Logger;
using static Rocket.Unturned.RocketChat;

namespace Rocket.Mash.LoadOut {
    public class LoadOut : RocketPlugin<LoadOutConf> {
        public static Version Version = new Version(0, 0, 1, 0);
        public static LoadOut Instance;

        private List<LoadOutQueue> _playerQueue;

        protected override void Load() {
            Instance = this;
            _playerQueue = new List<LoadOutQueue>();
            hookEvents();
            Log(this.Configuration.LoadedText);
            }

        protected override void Unload() {
            unregisterEvents();
            base.Unload();
            }
        
        public void FixedUpdate() {

            if (this.Loaded && this.Configuration.Enabled && _playerQueue.Count > 0) {
                int Index = -1;

                foreach (LoadOutQueue que in _playerQueue) {
                    if (DateTime.Now < que.TimeToLoadOut)
                        continue;

                    Index = _playerQueue.IndexOf(que);
                    foreach (LoadOutEquip loe in this.Configuration.LoadOutEquipment) {
                        if (!que.Player.GiveItem(loe.EntityId, loe.EntityAmount))
                            LogError($"LoadOut> Failed to give {que.Player.CharacterName} item {loe.EntityId} x {loe.EntityAmount}.");
                        }
                    Say(que.Player, this.Configuration.LoadOutGivenMessage, Color.yellow);
                    }

                if (Index >= 0)
                    _playerQueue.RemoveAt(Index);
                }
            }

        private void hookEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive += eventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnPlayerConnected += eventPlayerConnected;
            Unturned.Events.RocketServerEvents.OnServerShutdown += eventServerShutdown;
            }

        private void eventPlayerConnected(RocketPlayer player) {

            int items = 0;
            foreach (SDG.Unturned.Items i in player.Inventory.Items) {
                items += i.getItemCount();
                }

            if (items == 0) {
                Log($"{player.CharacterName}'s inventory empty, granting loadout.");
                GrantLoadOut(player);
                }

            }

        private void unregisterEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive -= eventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnServerShutdown -= eventServerShutdown;
            }

        private void eventServerShutdown() {
            unregisterEvents();
            }

        private void eventPlayerSpawn(RocketPlayer player, Vector3 position, byte angle) {
            GrantLoadOut(player);
            }

        public void GrantLoadOut(RocketPlayer player, bool instant = false) {
            DateTime ttl;

            if (instant)
                ttl = DateTime.Now;
            else
                ttl = DateTime.Now.AddSeconds(this.Configuration.SpawnDelay);

            _playerQueue.Add(
                    new LoadOutQueue() {
                        Player = player,
                        TimeToLoadOut = ttl,
                        });
            }

        }

    }
