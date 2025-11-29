# TROUBLESHOOTING.md

## 🛠️ American President BETA - Troubleshooting Guide

---

## 🚀 Installation Issues

### Unity Editor Won't Open Project
**Problem**: Project fails to open in Unity Editor  
**Solution**:
1. Ensure you have Unity 2022.3 LTS installed
2. Check that the project folder contains `Assets/` and `ProjectSettings/`
3. Try opening via Unity Hub > Add Project > Select folder
4. If still failing, create new Unity project and copy Assets folder

### Missing Packages/Dependencies
**Problem**: Scripts show errors about missing components  
**Solution**:
1. Open Unity Package Manager (Window > Package Manager)
2. Install required packages:
   - Universal Render Pipeline (URP)
   - AI Navigation
   - TextMeshPro
   - Mirror (for multiplayer)
3. Restart Unity Editor

---

## 🎮 Gameplay Issues

### Game Won't Start
**Problem**: Clicking Play does nothing  
**Solution**:
1. Check Console for errors (Window > General > Console)
2. Ensure MainMenu scene exists and is added to Build Settings
3. Verify GameManager script is attached to a GameObject
4. Check that all required prefabs are assigned

### Character Won't Move
**Problem**: Player character is frozen  
**Solution**:
1. Check PlayerController script is attached to player GameObject
2. Verify input settings (Edit > Project Settings > Input Manager)
3. Ensure character has Rigidbody component
4. Check for collision issues with terrain

### Vehicles Not Working
**Problem**: Can't enter or drive vehicles  
**Solution**:
1. Verify Vehicle script is attached to vehicle prefabs
2. Check that vehicles have WheelCollider components
3. Ensure player is near vehicle and pressing F key
4. Check Vehicle.cs for isDriving logic

---

## 🐛 Common Script Errors

### NullReferenceException
**Problem**: Scripts throwing null reference errors  
**Solution**:
1. Check that all public variables are assigned in Inspector
2. Use FindObjectOfType<> in Start() methods
3. Add null checks before accessing components
4. Verify GameObject names match script expectations

### Missing Component Errors
**Problem**: Scripts can't find required components  
**Solution**:
1. Add required components to GameObjects
2. Use [RequireComponent] attribute in scripts
3. Check GetComponent<> calls in Awake/Start methods
4. Verify component names are spelled correctly

---

## 🎨 Visual & Audio Issues

### Graphics Problems
**Problem**: Visual glitches, missing textures, poor performance  
**Solution**:
1. Check URP asset is assigned in Graphics settings
2. Verify materials are assigned to 3D models
3. Adjust quality settings (Edit > Project Settings > Quality)
4. Check post-processing stack configuration

### Audio Not Playing
**Problem**: No sound effects or music  
**Solution**:
1. Verify AudioListener component in scene
2. Check AudioSource components and clip assignments
3. Ensure audio files are imported correctly
4. Check AudioManager script initialization

---

## 💾 Save/Load Issues

### Save Files Not Working
**Problem**: Game won't save or load progress  
**Solution**:
1. Check SaveLoadManager script is present
2. Verify write permissions in game directory
3. Check JSON serialization for data classes
4. Ensure [System.Serializable] on save data classes

### Corrupted Save Files
**Problem**: Save files become unusable  
**Solution**:
1. Implement save file validation
2. Create backup save system
3. Add error handling for corrupted data
4. Use checksums for save file integrity

---

## 🌐 Multiplayer Issues

### Connection Problems
**Problem**: Can't connect to multiplayer lobby  
**Solution**:
1. Verify Mirror networking package is installed
2. Check NetworkManager component configuration
3. Ensure firewall allows Unity networking
4. Test on same network first

### Sync Issues
**Problem**: Players not synchronized properly  
**Solution**:
1. Check NetworkTransform components
2. Verify NetworkIdentity on synced objects
3. Ensure RPC calls are properly configured
4. Test with low latency connection

---

## 🔧 Performance Issues

### Low FPS
**Problem**: Game runs slowly or stutters  
**Solution**:
1. Check Unity Profiler (Window > Analysis > Profiler)
2. Reduce polygon count on 3D models
3. Optimize script execution in Update()
4. Use object pooling for frequently spawned objects
5. Adjust LOD settings for distant objects

### Memory Leaks
**Problem**: Game memory usage increases over time  
**Solution**:
1. Check for undisposed resources
2. Use Destroy() properly for GameObjects
3. Avoid circular references in scripts
4. Monitor memory usage in Profiler

---

## 📱 Platform-Specific Issues

### Windows
- **Run as Administrator** if save/write permissions issues
- **Update graphics drivers** for rendering problems
- **Disable antivirus** temporarily if installation fails

### macOS
- **Right-click > Open** if app is blocked by Gatekeeper
- **Allow in Security & Privacy** settings
- **Check macOS version compatibility** (10.15+)

### Linux
- **Install missing libraries** (libmono, etc.)
- **Set executable permissions** (`chmod +x`)
- **Check graphics drivers** (Mesa/NVIDIA)

---

## 🐞 Debugging Tools

### Unity Console
- **Window > General > Console** for error messages
- **Clear on Play** disabled to see all errors
- **Error Pause** to stop on exceptions

### Inspector Debugging
- **Debug mode** in Inspector to see private variables
- **Runtime debugging** while game is playing
- **Component references** to check connections

### Log Files
- **Player.log** in game data directory
- **Editor.log** in Unity installation folder
- **Crash logs** for application crashes

---

## 📞 Getting Help

### Self-Service
1. **Check Console** for specific error messages
2. **Review this guide** for common solutions
3. **Search Unity Forums** for similar issues
4. **Check script comments** for usage instructions

### Community Support
- **Discord**: [Link to Discord server]
- **Unity Forums**: [Link to forum thread]
- **Reddit**: [Link to subreddit]
- **GitHub Issues**: [Link to issues page]

### Reporting Bugs
When reporting bugs, include:
- **System specifications** (OS, CPU, GPU, RAM)
- **Unity version** (2022.3 LTS)
- **Error messages** (full console output)
- **Steps to reproduce** (detailed instructions)
- **Screenshots/videos** if applicable

---

## 🔄 Quick Fixes Checklist

### Before Reporting Issues
- [ ] Restart Unity Editor
- [ ] Clear Unity cache (Library folder)
- [ ] Reimport assets
- [ ] Check for script compilation errors
- [ ] Verify all required packages are installed
- [ ] Test in a new Unity project with minimal setup

### Common Quick Fixes
1. **Missing Reference**: Reassign in Inspector
2. **Script Error**: Check syntax and imports
3. **Performance**: Reduce quality settings
4. **Input**: Check Input Manager settings
5. **Audio**: Verify AudioSource components

---

*This guide will be updated as new issues are discovered during BETA testing.*