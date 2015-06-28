using System.Xml.Serialization;
using System;

namespace Rocket.Mash.LoadOut {
    [Serializable]
    public class LoadOutEquip {
        [XmlAttribute("Id")]
        public ushort EntityId;
        [XmlAttribute("Amount")]
        public byte EntityAmount;

        public LoadOutEquip() { }

        public LoadOutEquip(ushort id, byte amount) {
            this.EntityId = id;
            this.EntityAmount = amount;
            }
        }
    }
