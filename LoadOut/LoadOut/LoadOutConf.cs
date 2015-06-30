using Rocket.API;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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
    [Serializable]
    public class LoadOutConf : IRocketPluginConfiguration {
        public bool Enabled;
        public ushort SpawnDelay;
        public bool AllowFromCommand;
        public string LoadOutCommand;
        public int CommandCooldown;
        public string CommandCooldownNotReadyMessage;

        [XmlElement("ErrorColor")]
        public string ErrColor;

        public string LoadOutGivenMessage;
        public string AccessDeniedMessage;
        public string CommandDisabledMessage;
        public string AllowFromCommandAnnounceMessage;
        public bool OverrideKitCommand;
        public List<LoadOutEquip> LoadOutEquipment;

        public UnityEngine.Color ErrorColor {
            get {
                if (ErrColor.Split(':').Length != 3)
                    return UnityEngine.Color.gray;
                return StringToUEColor(ErrColor);
                }
            }

        [XmlIgnore]
        public string LoadedText { get { return $"{LoadOut.Version} by Mash"; } }

        public IRocketPluginConfiguration DefaultConfiguration {
            get {
                return new LoadOutConf() {
                    Enabled = true,
                    AllowFromCommand = true,
                    CommandCooldown = 900,
                    SpawnDelay = 2,
                    LoadOutGivenMessage = "You've got stuff!",
                    AccessDeniedMessage = "Insufficient permission.",
                    CommandDisabledMessage = "LoadOuts are join/spawn only.",
                    AllowFromCommandAnnounceMessage = "Type /loadout to get some starting gear!",
                    CommandCooldownNotReadyMessage = "LoadOut not ready for %S seconds.",
                    ErrColor = "1.0:0.0:1.0",
                    OverrideKitCommand = true,
                    LoadOutEquipment = new List<LoadOutEquip>() {
                        new LoadOutEquip(97, 1),
                        new LoadOutEquip(15, 1),
                        new LoadOutEquip(81, 1),
                        new LoadOutEquip(98, 2)
                        },
                    };
                }
            }

        public static UnityEngine.Color StringToUEColor(string input) {
            UnityEngine.Color retVal;

            string[] strings = input.Split(':');
            if (strings.Length != 3)
                throw new MissingFieldException("Rocket.Mash.DeathAnnounce::StringToUEColor", "Color param doesn't have 3 :-separated fields.");

            for (int i = 0; i < strings.Length; i++) {
                if (float.Parse(strings[i]) > 1.0f) { strings[i] = 1.0f.ToString(); }
                if (float.Parse(strings[i]) < 0f) { strings[i] = 0f.ToString(); }
                }

            retVal.r = float.Parse(strings[0]);
            retVal.g = float.Parse(strings[1]);
            retVal.b = float.Parse(strings[2]);
            retVal.a = 1.0f;

            return retVal;
            }
        }
    }