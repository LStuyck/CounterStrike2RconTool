# CS2 RCON Tool

## Context

Tool created for very basic Counter-Strike 2 server management for a  small LAN event.

## Tool based on Valve-RCON-protocol-in-VB.NET

https://github.com/fpaezf/Valve-RCON-protocol-in-VB.NET

## Setup CS2 Server for LAN

### Set RCON password

In file "C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\cfg\server.cfg"

Add following line

`rcon_password "yourpassword"`

### Start CS2 server

"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\bin\win64\cs2.exe" -dedicated -maxplayers 16 -maxplayers_override 16 -usercon +game_type 0 +game_mode 1 +map de_inferno +ip 0.0.0.0

## Tools

### Description

This repository contains 2 tools
- CLI
- UI

They work independently, use the one you want.

---
---

## UI Guide

### Download 

Download latest release of the program from the releases page of this repo [here](https://github.com/LStuyck/CounterStrike2RconTool/releases).

UI-win-x64.zip 

Unzip and start CounterStrike2RconTool.Wpf.exe

### Usage

#### Connect to server

Double click **RconWpf.exe** to start.

Set Ip, Port and Password of the server and click the **Connect** button.

#### Change game mode

To change game mode, select one from the Game Mode drop down and click the **Change game mode** button.

Always select a map after changing the game mode.

#### Change map

To change the map, select one from the Map drop down and click the **Change map** button.

#### Add and kick bots

⚠️ Unstable. ⚠️

Bots are added on the wrong team and in some cases server became unstable after running the command.

The **Kick all bots** button works.

#### Restart game

The **Restart game** button restarts current game with the current configuration  after a 5 seconds countdown.

#### Manual command

Type your command in the **Manual command** Textbox and click the **Send command** button to execute it.

You can execute several commands at once, they have to be "; " separated.

---
---

## CLI Guide

### Download 

Download latest release of the program from the releases page of this repo [here](https://github.com/LStuyck/CounterStrike2RconTool/releases).

CLI-win-x64.zip 

Unzip and .\CounterStrike2RconTool.Cli.exe

### Commands

#### updateinfo

Updates ip, port and password to use. Changes are saved in a configuration file.

`CounterStrike2RconTool.Cli.exe updateinfo --ip "172.30.240.1" --port 27015 --password "yourpassword"`

#### readfile

Reads a file with a set of commands to execute.

Sample files are included in the 'RconCommandFiles' directory. Create your own files or customize existing ones.

`CounterStrike2RconTool.Cli.exe readfile --file commands.txt`

#### exec

Executes a " ;" separated command.

`CounterStrike2RconTool.Cli.exe exec "your ; separated command"`