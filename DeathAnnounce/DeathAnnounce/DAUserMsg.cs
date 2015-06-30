using System;
using System.Xml.Serialization;

namespace Rocket.Mash.DeathAnnounce {
    [Serializable]
    public class DAUserMsg {
        [XmlAttribute("Cause")]
        public string Cause;

        //[XmlAttribute("Message")]
        public string Message;

        //[XmlAttribute("AltMessage")]
        public string AltMessage;

        [XmlAttribute("Color")]
        public string UEColor;

        public UnityEngine.Color Color {
            get {
                return StringToUEColor(UEColor);
                }
            }

        public DAUserMsg() { }
        public DAUserMsg(string cause, string cm, string c, string am = "") {
            this.Cause = cause;
            this.Message = cm;
            this.AltMessage = am;
            this.UEColor = c;
            }

        public static UnityEngine.Color StringToUEColor(string input) {
            UnityEngine.Color retVal;

            string[] strings = input.Split(':');
            if (strings.Length != 3)
                throw new MissingFieldException("Rocket.Mash.DeathAnnounce::StringToUEColor", "Color param doesn't have 3 :-separated fields.");

            for (int i = 0; i < strings.Length; i++) {
                if (float.Parse(strings[i]) > 1.0f) { strings[i] = 1.0f.ToString(); }
                }

            retVal.r = float.Parse(strings[0]);
            retVal.g = float.Parse(strings[1]);
            retVal.b = float.Parse(strings[2]);
            retVal.a = 1.0f;

            return retVal;
            }
        }
    }
