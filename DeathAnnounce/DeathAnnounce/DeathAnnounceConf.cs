using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

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
        public bool Enabled;

        public string LoadedText { get { return $"{DeathAnnounce.Version} by Mash"; } }

        public IRocketPluginConfiguration DefaultConfiguration {
            get {
                return new DeathAnnounceConf() {
                    Enabled = true,
                    };
                }
            }
        }
    }