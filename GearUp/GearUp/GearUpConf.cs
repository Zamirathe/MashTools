﻿using Rocket.API;
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

namespace Rocket.Mash.GearUp {
    [Serializable]
    public class GearUpConf : IRocketPluginConfiguration {
        public bool Enabled;
        public string DatabaseAddress; 
        public string DatabaseName;
        public string DatabaseUsername; 
        public string DatabasePassword;
        public int DatabasePort;
        public string TableName;
        public ushort SpawnDelay;
        [XmlElement("JoinRequireEmptyInventory")]
        public bool RequireEmpty;
        public bool AllowCmd;
        //public string LoadOutCommand;
        public int Cooldown;
        public int VIPCooldown;
        [XmlElement("ErrorColor")]
        public string EColor;
        [XmlElement("GearGivenColor")]
        public string SColor;
        [XmlElement("InfoColor")]
        public string IColor;

        public Kit DefaultKit;

        [XmlArray("Kits")]
        public List<Kit> UserKits;

        [XmlIgnore]
        public string LoadedText { get { return $"{GearUp.Version} by Mash"; } }

        [XmlIgnore]
        public List<string> HelpText = new List<string>() {
            "/gu Give yourself the gear!",
            "/gu [player] Give another player gear.",
            "/gu -[on|off|-|info] enable/disable/status/info of GearUp",
            "/gu -reset [player] reset your cooldown or [player]s",
            };

        public UnityEngine.Color ErrorColor {
            get {
                if (EColor.Split(':').Length != 3)
                    return UnityEngine.Color.gray;
                return StringToUEColor(EColor);
                }
            }
        public UnityEngine.Color SuccessColor {
            get {
                if (SColor.Split(':').Length != 3)
                    return UnityEngine.Color.gray;
                return StringToUEColor(SColor);
                }
            }
        public UnityEngine.Color InfoColor {
            get {
                if (IColor.Split(':').Length != 3)
                    return UnityEngine.Color.gray;
                return StringToUEColor(IColor);
                }
            }

        public IRocketPluginConfiguration DefaultConfiguration {
            get {
                return new GearUpConf() {
                    Enabled = true,
                    DatabaseAddress = "localhost", 
                    DatabaseName = "unturned",
                    DatabaseUsername = "unturned", 
                    DatabasePassword = "changeme",
                    DatabasePort = 3306,
                    TableName = "gearup",
                    AllowCmd = true,
                    RequireEmpty = true,
                    Cooldown = 900,
                    VIPCooldown = 450,
                    SpawnDelay = 2,
                    EColor = "1.0:0.0:1.0",
                    SColor = "0.0:0.8:0.0",
                    IColor = "0.8:0.8:0",
                    DefaultKit = new Kit() {
                        Name = "OnJoin",
                        //PermissionGroup = "",
                        Items = new List<Item>() {
                            new Item(97, 1),
                            new Item(15, 1),
                            new Item(81, 1),
                            new Item(98, 2)
                            },
                        },
                    UserKits = new List<Kit>() {
                        new Kit() {
                            Name = "food",
                            PermissionGroup = "self",
                            Items = new List<Item>() {
                                new Item() {
                                    ID = 91,
                                    Amount = 1,
                                    },
                                new Item() {
                                    ID = 83,
                                    Amount = 1
                                    },
                                },
                            },
                        }
                    };
                }
            }

        public static UnityEngine.Color StringToUEColor(string input) {
            UnityEngine.Color retVal;

            string[] strings = input.Split(':');
            if (strings.Length != 3)
                throw new MissingFieldException("Rocket.Mash.GearUp::StringToUEColor", "Color param doesn't have 3 :-separated fields.");

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
