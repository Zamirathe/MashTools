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

namespace Rocket.Mash.LoadOut {
    public class LoadOut : RocketPlugin<LoadOutConf> {
        public static Version Version = new Version(0, 0, 2, 0);
        public static LoadOut Instance;

        public static LoadOutCommand LoadOutCmd;

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
            //if (this.Loaded && this.Configuration.Enabled && PlayerQueue.Count > 0) {
            //    //ProcessLoadOuts();
            //    }
            }

        private void HookEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive += EventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnPlayerConnected += EventPlayerConnected;
            Unturned.Events.RocketServerEvents.OnServerShutdown += EventServerShutdown;
            }

        private void UnregisterEvents() {
            Unturned.Events.RocketPlayerEvents.OnPlayerRevive -= EventPlayerSpawn;
            Unturned.Events.RocketServerEvents.OnServerShutdown -= EventServerShutdown;
            }

        private void EventPlayerConnected(RocketPlayer player) {

            player.GetComponent<LoadOutComp>().Available = DateTime.Now;

            int items = 0;
            foreach (SDG.Unturned.Items i in player.Inventory.Items) {
                items += i.getItemCount();
                }

            if (items == 0) {
                Log($"{player.CharacterName}'s inventory empty, granting loadout.");
                GrantLoadOut(player);
                }

            }

        private void EventServerShutdown() {
            UnregisterEvents();
            }

        private void EventPlayerSpawn(RocketPlayer player, Vector3 position, byte angle) {
            //GrantLoadOut(player);
            player.GetComponent<LoadOutComp>().Timer.Start();
            }
                
        private void ProcessLoadOuts() {
            //foreach (LoadOutQueue que in PlayerQueue) {
            //    try {
            //        if (que.Player == null) {
            //            IndexesToRemove.Add(PlayerQueue.IndexOf(que));
            //            continue;
            //            }
            //        if (DateTime.Now < que.TimeToLoadOut) continue;
            //        IndexesToRemove.Add(PlayerQueue.IndexOf(que));
            //        foreach (LoadOutEquip loe in this.Configuration.LoadOutEquipment) {
            //            if (que.Player?.GiveItem(loe.EntityId, loe.EntityAmount) == false) {
            //                LogError($"LoadOut> Failed to give {que.Player.CharacterName} item {loe.EntityId} x {loe.EntityAmount}.");
            //                }
            //            }
            //        Say(que.Player, this.Configuration.LoadOutGivenMessage, Color.yellow);
            //        } catch {
            //        if (ErrOnLoop) CriticalError();
            //        ErrOnLoop = true;
            //        IndexesToRemove.Add(PlayerQueue.IndexOf(que));
            //        }
            //    }
            //foreach (int i in IndexesToRemove)
            //    PlayerQueue.RemoveAt(i);
            //IndexesToRemove.Clear();
            }

        private void CriticalError() {
            Say("LoadOut has stopped due to an error.", Color.red);
            this.Configuration.Enabled = false;
            }

        public void GrantLoadOut(RocketPlayer player, bool instant = false) {
            //DateTime ttl;

            //if (instant)
            //    ttl = DateTime.Now;
            //else
            //    ttl = DateTime.Now.AddSeconds(this.Configuration.SpawnDelay);

            //player.GetComponent<LoadOutCooldown>().

            //PlayerQueue.Add(
            //        new LoadOutQueue() {
            //            Player = player,
            //            TimeToLoadOut = ttl,
            //            });
            }



        }

    }
