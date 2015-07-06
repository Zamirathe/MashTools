using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Rocket.Mash.GearUp {
    public class Kit {
        [XmlAttribute("KitName")]
        public string Name;
        public List<Gear> Equipment;
        }
    }
