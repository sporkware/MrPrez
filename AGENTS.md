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

## Asset Management
- **3D Models**: Import from Unity Asset Store (e.g., "Low Poly White House", "US Capitol Building") or create in Blender. Assign to prefabs in WorldGenerator.
- **Textures**: Use free textures from OpenGameArt or create in GIMP. Apply to materials (e.g., roadMaterial, skinMaterials).
- **Animations**: Import from Mixamo or create in Blender. Assign to Animator controllers for Player/NPCs.
- **Audio Clips**: Download free SFX from Freesound.org or Zapsplat. Assign to AudioManager (musicTracks, gunshotClip, etc.). For voices, record or use placeholders.
- **ScriptableObjects**: Create via right-click in Unity: Dialogue, Quest, Achievement. Fill with data for content.
- **Import Process**: Drag assets into Assets/ folder, set import settings (e.g., texture compression), assign in inspectors.