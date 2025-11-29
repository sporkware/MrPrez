# American President - BETA RELEASE

An open-world satirical simulator inspired by Grand Theft Auto, where you play as 'Mr. President' navigating political intrigue, corruption, and power in a fictionalized Washington D.C.

## 🚀 BETA RELEASE STATUS
**Version**: 0.1.0-BETA  
**Release Date**: November 28, 2025  
**Status**: Public BETA - Ready for testing

## Game Overview

- **Genre**: Open-world action-adventure with RPG elements
- **Platform**: PC (Windows/Mac/Linux), consoles, mobile
- **Engine**: Unity 2022.3 LTS
- **Target Audience**: Adults 18+ interested in political satire
- **Content Warning**: Mature themes, political satire, adult content

## Features

- **Open-World Exploration**: Free-roam in a detailed D.C. map with driving, walking, and flying
- **Political Simulation**: Make decisions that affect approval rating, influence, and corruption
- **Missions**: Main storyline and side quests involving rallies, negotiations, and covert ops
- **Combat**: Shooting, melee, and non-lethal options
- **Dialogue System**: Branching conversations with consequences and voice acting
- **Dynamic World**: Weather, day/night cycle, NPC schedules, random events
- **Mini-Games**: Card games, debates, press conferences, hacking, puzzles
- **Romance & Scandals**: Adult-themed interactions with consequences
- **Crafting & Inventory**: Item collection, crafting, and usage
- **Multiplayer Basics**: Lobby and networking setup for co-op
- **Audio & Visual Polish**: Dynamic music, post-processing effects, corruption overlays
- **Save/Load**: Full game state persistence
- **Achievements**: Unlockables for various actions
- **Cheats**: Development testing codes

## Project Structure

```
Assets/
├── Scenes/
│   └── MainMenu.txt (scene description)
├── Scripts/
│   ├── PlayerController.cs
│   ├── DialogueSystem.cs
│   ├── QuestManager.cs
│   ├── UIManager.cs
│   ├── WorldGenerator.cs
│   └── NPCController.cs
├── Prefabs/
├── Materials/
├── Textures/
└── Audio/
```

## Player Guide

### Controls
- **Movement**: WASD keys
- **Interact**: F key near NPCs/objects
- **Drive**: Enter vehicle with F, use WASD to control
- **Combat**: Left mouse to shoot, right mouse to aim
- **Mini-Games**: Follow on-screen prompts
- **Cheats**: C (god mode), M (money), H (health), A (approval), I (influence), R (reset corruption), F (refuel)

### Stats
- **Approval Rating**: Public opinion, affects NPC behavior
- **Influence**: Political power, unlocks options
- **Wealth**: Money for items and bribes
- **Corruption Level**: Risk of scandals

### Tips
- Balance your stats to avoid impeachment
- Explore hidden areas for secrets
- Use adult content toggle in settings for mature themes
- Complete quests for progression

## 🎮 Getting Started (Players)

### System Requirements
- **Minimum**: Intel i5, 8GB RAM, GTX 1050
- **Recommended**: Intel i7, 16GB RAM, RTX 3060
- **OS**: Windows 10+, macOS 10.15+, Ubuntu 18.04+

### Installation
1. Download the BETA build from [Release Page]
2. Extract ZIP file to desired location
3. Run `AmericanPresident.exe` (Windows) or `AmericanPresident.app` (Mac)
4. Configure settings in main menu

### Quick Start Guide
- **Movement**: WASD keys
- **Interact**: F key near NPCs/objects  
- **Drive**: Enter vehicle with F, use WASD to control
- **Combat**: Left mouse to shoot, right mouse to aim
- **Mini-Games**: Follow on-screen prompts
- **Cheats**: C (god mode), M (money), H (health), A (approval), I (influence), R (reset corruption), F (refuel)

## 🛠️ Getting Started (Developers)

1. Open this project in Unity 2022.3 LTS or later
2. Import required packages (URP, AI Navigation, TextMeshPro, Mirror for multiplayer)
3. Set up scenes: MainMenu and GameWorld as per AGENTS.md
4. Assign scripts to GameObjects and configure inspectors
5. Add assets (models, textures, audio) via Asset Store or free sources (see ASSETDL.md)
6. Run TestRunner for unit tests, then playtest

## Build Instructions

1. Open Build Settings (File > Build Settings)
2. Select target platform
3. Add scenes to build
4. Click Build

## Development Notes

- Use the provided scripts as foundation
- Implement vehicle physics for driving mechanics
- Create quest objectives and triggers
- Balance stats and consequences
- Add voice acting and sound effects
- Ensure ethical handling of political satire

## 🐛 BETA Testing

### How to Report Issues
- Use GitHub Issues: [Report Bug]
- Include: System specs, steps to reproduce, expected vs actual behavior
- For crashes: Include Unity log files

### Known Issues (BETA)
- Performance drops in crowded areas
- Occasional audio glitches
- Save file corruption on rare occasions
- Multiplayer features are experimental

### Feedback
- Join our Discord: [Link to Discord]
- Complete the BETA survey: [Survey Link]
- Read full BETA release notes: [BETA_RELEASE_NOTES.md](BETA_RELEASE_NOTES.md)

## 🤝 Contributing

This project is set up for autonomous AI development. Follow the Game Design Document (GDD) for feature implementation.

### Development Guidelines
- See AGENTS.md for coding standards and build instructions
- Follow Unity best practices for performance
- Test on target hardware before submitting PRs

## 📄 License

This project is licensed under the MIT License - see LICENSE file for details.

---

**Thank you for testing American President BETA!** 🇺🇸