# American President

An open-world satirical simulator inspired by Grand Theft Auto, where you play as 'Mr. President' navigating political intrigue, corruption, and power in a fictionalized Washington D.C.

## Game Overview

- **Genre**: Open-world action-adventure with RPG elements
- **Platform**: PC, consoles, mobile
- **Engine**: Unity 2022.3 LTS
- **Target Audience**: Adults 18+ interested in political satire

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

## Getting Started

1. Open this project in Unity 6.0 LTS (recommended) or 2022.3 LTS
2. Import required packages (URP, AI Navigation, TextMeshPro, Mirror for multiplayer)
3. Set up scenes: MainMenu and GameWorld as per AGENTS.md
4. Assign scripts to GameObjects and configure inspectors
5. Add assets (models, textures, audio) via Asset Store or free sources
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

## Contributing

This project is set up for autonomous AI development. Follow the Game Design Document (GDD) for feature implementation.