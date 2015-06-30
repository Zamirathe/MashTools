#### MashTools

A collection of a few simple tools I had use for on my own server, so I thought I'd share them.

Also, all of my plugins support the '*' permission, so you don't have to add individual permissions
if you'd prefer just to assign global permission to access everything.

# DeathAnnounce

A simple plugin that announces when a player dies and by what means they met their end.

##### Permissions
Permission       | Action
---------------- | -------
DeathAnnounce    | Allows caller to enable/disable plugin

##### Commands
Command     | Action
------------|---------
DA          | Shows current plugin status (ie; on/off)
DA [on/off] | Toggle plugin on/off

##### Configuration
Field | Type | About
------|------|------
CAUSE |STATIC|Type of death - DO NOT CHANGE
Color |Float Array|Color in RGB, each value is a float from 0 to 1.0
Message|String|Message displayed for this type of death
AltMessage|String|Message displayed if message SHOULD have a "murderer" but doesn't.
 | |Leave blank to disable.

Variable|Value
--------|-----
%P      |The player that died
%K      |The 'killer' if applicable.

# LoadOut

Grants players a beginning set of gear when they join your server and when they are killed.
(Items are not given if the player is holding any item(s))
Items given are configurable in the .config.xml file.

##### Permissions
Permission | Action
---------- | -------
loadout    | Allows caller to use the loadout command to grant themselves the equipment configured

##### Commands
Command | Action
--------|---------
loadout | Grants you the starting loadout set of equipment
lo      | Alias
kit     | Alias

# RuleBook

I couldn't find a rules plugin that suited my needs (I wanted individual numbered rules and pages) so I wrote RuleBook.
Allows an infinite number of rules that're automatically sorted into pages of 4 rules each. Players can use the /rules
command with or without a page identifier to see your configured rules.

##### Commands
Command   | Action
----------|----------
rules     | Displays the first page of rules
rules [n] | Displays page [n] of the rules

##### Permissions
Permission | Action
---------- | -------
rules	     | Allow caller to read server rules (/rules, /rules 2, /rules 3, etc)


#### Other Options (Applicable to all plugins)
Option | Action
------- | -------
Enabled								| Enables and disables the addon in it's entirety
