#### MashTools

A collection of a few simple tools I had use for on my own server, so I thought I'd share them.


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
AltMessage|String|Message displayed if message SHOULD have a "murderer" but doesn't.<br/>ie;PlayerX stepped on 's grenade. Since %K doesn't contain a name, this AltMessage will be used.<br/>Leave blank to disable

Variable|Value
--------|-----
%P      |The player that died
%K      |The 'killer' if applicable

# GearUp
#### Formerly LoadOut

Grants players a beginning set of gear when they join your server and when they respawn.
(Items are not given if the player is holding any item(s))
Items and message colors are configurable in the .config.xml file, messages in translations.

Permissions are in order of inheritance. Each permission inherits any above it
##### Permissions
Permission   | Action
------------ | -------
gearup.info  | Allows caller to view information on GearUp
gearup.self  | Allows caller to issue themselves the configured items
gearup.other | Allows caller to issue themselves and others items
gearup.admin | Allows caller to enable/disable the plugin, reset cooldowns and issue items to anyone


All "/gearup" commands are aliased as "/gu" and "/gear"
##### Commands
Command             | Action
--------------------|---------
gear -?             | View command help
gear -info          | View plugin info (author, version, status)
gear                | Give yourself items
gear [player]       | Give [player] items
gear -[on/off/-]    | Enable/Disable the plugin (-- to view status)
gear -reset         | Resets your cooldown timer
gear -reset [player]| Resets [player]s cooldown timer


##### Config
Option      | Effect
------------|-------
Enabled     | Whether the plugin should be enabled on startup
AllowCmd    | Disables ALL commands (effectively making plugin function on-join/spawn only)
Cooldown    | Command cooldown (how much time to wait before player can use GearUp again)
SpawnDelay  | How long to wait after player joins to give items
ErrorColor  | Text color used for error messages<br />Colors are 3 FLOATs.. 0.0 - 1.0, colon separated. (1:1:1 or 0.5:0.5:0.5)
SuccessColor| Text color used for successfull commands
InfoColor   | Text color used for informational messages
GearList    | The equipment/items that will be issued by this plugin

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
