using System.Xml.Serialization;
using System;

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
