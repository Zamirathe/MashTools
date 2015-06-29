using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Rocket.Mash.DeathAnnounce {
    [Serializable]
    public class DAUserMsg {
        [XmlAttribute("Cause")]
        public string Cause;
        [XmlAttribute("Message")]
        public string CMessage;
        [XmlAttribute("Color")]
        public Color CColor;

        public DAUserMsg() { }
        public DAUserMsg(string cause, string cm, Color c) {
            this.Cause = cause;
            this.CMessage = cm;
            this.CColor = c;
            }

        }
    }
