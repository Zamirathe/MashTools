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
    and the author tags are not to be removed from unmodified versions
    of this software.

    - Mash    
    
    Rocket   - copyright © 2015 ROCKET FOUNDATION
    Unturned - copyright © 2015 Smartly Dressed Games
    Unity    - Copyright © 2015 Unity Technologies
    */

namespace Rocket.Mash.DeathAnnounce {
    public class DeathAnnounceConf : IRocketPluginConfiguration {

        public List<DAUserMsg> UserMessages;

        public bool Enabled;

        public string LoadedText { get { return $"{DeathAnnounce.Version} by Mash"; } }

        public IRocketPluginConfiguration DefaultConfiguration {
            get {
                return new DeathAnnounceConf() {
                    Enabled = true,
                    UserMessages = new List<DAUserMsg>() {
                        new DAUserMsg("BLEEDING", "bled out.", Color.gray),
                        new DAUserMsg("BONES", "fell to pieces.", Color.gray),
                        new DAUserMsg("FREEZING", "turned into a popsicle.", Color.gray),
                        new DAUserMsg("FOOD", "starved to death.", Color.gray),
                        new DAUserMsg("WATER", "died of thirst.", Color.gray),
                        new DAUserMsg("GUN", "was shot dead by {0}", Color.gray),
                        new DAUserMsg("MELEE", "was axed by {0}", Color.gray),
                        new DAUserMsg("ZOMBIE", "tried making out with a zombie.", Color.gray),
                        new DAUserMsg("SUICIDE", "just couldn't take it anymore.", Color.gray),
                        new DAUserMsg("KILL", "was killed by {0}", Color.gray),
                        new DAUserMsg("INFECTION", "died of zombgreen.", Color.gray),
                        new DAUserMsg("PUNCH", "was knocked out by {0}", Color.gray),
                        new DAUserMsg("BREATH", "forgot to surface for air.", Color.gray),
                        new DAUserMsg("ROADKILL", "was run down by {0}.", Color.gray),
                        new DAUserMsg("VEHICLE", "was run down by {0}.", Color.gray),
                        new DAUserMsg("GRENADE", "was run down by {0}.", Color.gray),
                        new DAUserMsg("SHRED", "was run down by {0}.", Color.gray)
                        }
                    };
                }
            }
        }
    }