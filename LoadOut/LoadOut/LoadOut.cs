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
    and the author tags are not to be removed from unmodified versions
    of this software.

    - Mash    
    
    Rocket   - copyright © 2015 ROCKET FOUNDATION
    Unturned - copyright © 2015 Smartly Dressed Games
    Unity    - Copyright © 2015 Unity Technologies
    */

namespace Rocket.Mash.LoadOut {
    public class LoadOut : RocketPlugin<LoadOutConf> {
        public static Version Version = new Version(0, 0, 2, 0);
        public static LoadOut Instance;

        private List<int> IndexesToRemove;

        private List<LoadOutQueue> _playerQueue;

        private bool ErrOnLoop = false;

        protected override void Load() {
            Instance = this;
            _playerQueue = new List<LoadOutQueue>();
            IndexesToRemove = new List<int>();
            hookEvents();
            Log(this.Configuration.LoadedText);
            }

        protected override void Unload() {
            unregisterEvents();
            base.Unload();
            }
        
        public void FixedUpdate() {

            if (this.Loaded && this.Configuration.Enabled && _playerQueue.Count > 0) {

                foreach (LoadOutQueue que in _playerQueue) {
                    try {
                        if (que.Player == null) {
                            IndexesToRemove.Add(_playerQueue.IndexOf(que));
                            continue;
                            }

                        if (DateTime.Now < que.TimeToLoadOut)
                            continue;

                        IndexesToRemove.Add(_playerQueue.IndexOf(que));

                        foreach (LoadOutEquip loe in this.Configuration.LoadOutEquipment) {
                            if (que.Player?.GiveItem(loe.EntityId, loe.EntityAmount) == false) {
                                LogError($"LoadOut> Failed to give {que.Player.CharacterName} item {loe.EntityId} x {loe.EntityAmount}.");
                                }
                            }
                        Say(que.Player, this.Configuration.LoadOutGivenMessage, Color.yellow);

                      } catch {

                        if (ErrOnLoop)
                            CriticalError();

                        ErrOnLoop = true;
                        IndexesToRemove.Add(_playerQueue.IndexOf(que));
                        }

                    } 

                foreach(int i in IndexesToRemove)
                    _playerQueue.RemoveAt(i);

                IndexesToRemove.Clear();

                }
            }

        private void CriticalError() {
            Say("LoadOut has stopped due to an error.", Color.red);
            this.Configuration.Enabled = false;
            _playerQueue.Clear();
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
