using Rocket.API;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Rocket.Mash.LoadOut {
    [Serializable]
    public class LoadOutConf : IRocketPluginConfiguration {
        public bool Enabled;
        public ushort SpawnDelay;
        public bool AllowFromCommand;
        public string LoadOutCommand;
        public string LoadOutCommandHelp;
        public int CommandCooldown;
        public string LoadOutGivenMessage;
        public string AccessDeniedMessage;
        public string CommandDisabledMessage;
        public string AllowFromCommandAnnounceMessage;
        public bool OverrideKitCommand;
        public List<LoadOutEquip> LoadOutEquipment;

        [XmlIgnore]
        public string LoadedText { get { return $"{LoadOut.Version} by Mash"; } }

        public IRocketPluginConfiguration DefaultConfiguration {
            get {
                return new LoadOutConf() {
                    Enabled = true,
                    AllowFromCommand = true,
                    CommandCooldown = 300,
                    SpawnDelay = 5,
                    LoadOutGivenMessage = "Starting load-out given.",
                    AccessDeniedMessage = "LoadOuts are given on (re)spawn only.",
                    AllowFromCommandAnnounceMessage = "Type /loadout to get some starting gear!",
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

        }
    }