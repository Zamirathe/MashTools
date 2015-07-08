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

        [XmlAttribute("GearupPermissionRequired")]
        public string PermissionGroup;

        public List<Item> Items;

        public Kit() { }

        public Kit(string name, string perms, List<Item> items) {
            this.Name = name;
            this.PermissionGroup = perms;
            this.Items = items;
            }
        }
    }
