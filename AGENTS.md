# AGENTS.md - Development Guidelines

## Build/Test Commands
- Unity 2022.3 LTS required - open project in Unity Editor
- Build: File > Build Settings > Select Platform > Build
- Test: Use Unity Test Runner (Window > General > Test Runner)
- Play Mode: Unity Editor Play button for quick testing
- No CLI build system - Unity-centric workflow

## Code Style Guidelines
- **Language**: C# for Unity scripts
- **Naming**: PascalCase for classes/methods, camelCase for variables
- **Imports**: Group UnityEngine imports first, then System imports
- **Formatting**: 4 spaces indentation, braces on new line
- **Types**: Use explicit types, avoid var where possible
- **Error Handling**: Use null checks and try-catch for external dependencies
- **Comments**: Minimal, only for complex logic
- **Structure**: Follow existing singleton pattern (GameManager.Instance)
- **Serialization**: Use [System.Serializable] for data classes
- **Performance**: Cache GetComponent calls in Start/Awake

## Project Structure
- Scripts in Assets/Scripts/ organized by system
- Use ScriptableObjects for data (Dialogue, Quests)
- Follow Unity component-based architecture
- Maintain separation between UI, gameplay, and data layers