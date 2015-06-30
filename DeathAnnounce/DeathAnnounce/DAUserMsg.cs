using System;
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
