using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

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