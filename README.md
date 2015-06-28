## MashTools

A collection of a few simple tools I had use for on my own server, so I thought I'd share them.

Also, all of my plugins support the '*' permission, so you don't have to add individual permissions
if you'd prefer just to assign global permission to access everything.

# DeathAnnounce

A simple plugin that announces when a player dies and by what means they met their end.
Death messages are customizable in the .translations file.

# LoadOut

Grants players a beginning set of gear when the join your server and when they are killed.
Items given are configurable in the .config.xml file.

# RuleBook

I couldn't find a rules plugin that suited my needs (I wanted individual numbered rules and pages) so I wrote RuleBook.
Allows an infinite number of rules that're automatically sorted into pages of 4 rules each. Players can use the /rules
command with or without a page identifier to see your configured rules.

## Commands
Command   | Action
----------|----------
rules     | Displays the first page of rules.
rules [n] | Displays page [n] of the rules.

## Permissions
Permission | Action
------- | -------
rules		| allow caller to read server rules (/rules, /rules 2, /rules 3, etc)


## Other Options (Applicable to all plugins)
Option | Action
------- | -------
Enabled								| Enables and disables the addon in it's entirety
