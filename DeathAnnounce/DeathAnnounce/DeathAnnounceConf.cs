using Rocket.API;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

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

namespace Rocket.Mash.DeathAnnounce {
    public class DeathAnnounceConf : IRocketPluginConfiguration {
        public bool Enabled;
        public List<DAUserMsg> UserMessages;
        public string LoadedText { get { return $"{DeathAnnounce.Version} by Mash"; } }
        public string AccessDeniedMessage { get { return $"Access denied for user %U"; } }

        public IRocketPluginConfiguration DefaultConfiguration {
            get {
                return new DeathAnnounceConf() {
                    Enabled = true,
                    UserMessages = new List<DAUserMsg>() {
                        new DAUserMsg("BLEEDING", "%P bled out.", "0.5:0.5:0.5"),
                        new DAUserMsg("BONES", "%P fell to pieces.", "0.5:0.5:0.5"),
                        new DAUserMsg("FREEZING", "%P missed the defrost cycle.", "0.5:0.5:0.5"),
                        new DAUserMsg("FOOD", "I think %P was hungry.", "0.5:0.5:0.5"),
                        new DAUserMsg("WATER", "%P died of thirst.", "0.5:0.5:0.5"),
                        new DAUserMsg("GUN", "%P was shot dead by %K", "0.5:0.5:0.5", "%P was shot dead."),
                        new DAUserMsg("MELEE", "%P was axed by %K", "0.5:0.5:0.5", "%P got the axe."),
                        new DAUserMsg("ZOMBIE", "%P tried making out with a zombie.", "0.5:0.5:0.5"),
                        new DAUserMsg("SUICIDE", "%P just couldn't take it anymore.", "0.5:0.5:0.5"),
                        new DAUserMsg("KILL", "%K killed %P.", "0.5:0.5:0.5", "%P was killed."),
                        new DAUserMsg("INFECTION", "%P died of zombgreen.", "0.5:0.5:0.5"),
                        new DAUserMsg("PUNCH", "%K knocked %P the f**k out!", "0.5:0.5:0.5", "%P was knocked cold."),
                        new DAUserMsg("BREATH", "%P forgot to surface for air.", "0.5:0.5:0.5"),
                        new DAUserMsg("ROADKILL", "%K thought %P was a speedbump.", "0.5:0.5:0.5", "%P was run down."),
                        new DAUserMsg("VEHICLE", "%P was run down by %K.", "0.5:0.5:0.5", "%P died by vehicle."),
                        new DAUserMsg("GRENADE", "%P stepped on %K's grenade.", "0.5:0.5:0.5", "%P stepped on a grenade."),
                        new DAUserMsg("SHRED", "%P wasn't a ninja turtle.", "0.5:0.5:0.5")
                        }
                    };
                }
            }
        }
    }