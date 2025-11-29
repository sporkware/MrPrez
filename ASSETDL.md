# Asset Download Links - BETA Release Ready

This file contains links to free or low-cost assets for the American President game. Download and import into Unity's Assets folder.

## 🚀 BETA Release Asset Status
All required asset categories have placeholder links. For BETA release, use Unity primitives and default materials as placeholders while downloading specific assets.

## 3D Models

- **Mr. President (Fat Bureaucrat)**: [Fat Man Basemesh on Sketchfab](https://sketchfab.com/3d-models/fat-man-basemesh-3d-model-3cd28e0e724548f39dff837dec3f4c29) - Free FBX base mesh for customization.
- **White House**: [Low Poly White House on Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/low-poly-white-house-230387) - Free.
- **US Capitol**: [Capitol Building on CGTrader](https://www.cgtrader.com/free-3d-models/architectural/exterior/us-capitol-building) - Free OBJ.
- **Vehicles**: [Low Poly Car Pack on Unity Asset Store](https://assetstore.unity.com/packages/3d/vehicles/land/low-poly-car-pack-23341) - Free.
- **NPCs**: [Cartoon People Pack on Unity Asset Store](https://assetstore.unity.com/packages/3d/characters/cartoon-people-pack-21741) - Free.
- **Weapons**: [Low Poly Guns on Unity Asset Store](https://assetstore.unity.com/packages/3d/props/guns/low-poly-guns-163051) - Free.
- **Props**: [Office Furniture on CGTrader](https://www.cgtrader.com/free-3d-models/furniture) - Free models.
- **Helicopter**: [Low Poly Helicopter on Unity Asset Store](https://assetstore.unity.com/packages/3d/vehicles/air/low-poly-helicopter-13552) - Free.
- **Motorcycle**: [Low Poly Motorcycle on Unity Asset Store](https://assetstore.unity.com/packages/3d/vehicles/land/low-poly-motorcycle-23342) - Free.
- **Urban Buildings**: [Low Poly Buildings on Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/urban/low-poly-buildings-pack-100058) - Free.
- **Crowd NPCs**: [Crowd Pack on Unity Asset Store](https://assetstore.unity.com/packages/3d/characters/crowd-pack-1-161147) - Free.

## Textures

- **Road/Textures**: [Free PBR Materials on OpenGameArt](https://opengameart.org/content/free-pbr-materials) - Asphalt, concrete, etc.
- **Skin/Clothing**: [Free Texture Packs on Texture.com](https://www.texture.com/free-textures) - Fabric, skin tones.
- **Buildings**: [Urban Textures on Freepik](https://www.freepik.com/search?query=urban%20textures) - Free downloads.

## Audio

- **Music**: [Free Patriotic Music on Freesound](https://freesound.org/search/?q=patriotic) - Search for "anthem" or "march".
- **SFX**: [Gunshot Sounds on Freesound](https://freesound.org/search/?q=gunshot) - Free WAV files.
- **Voice Lines**: Record your own or use [ElevenLabs](https://elevenlabs.io/) for AI-generated voices (free tier available).
- **Ambient Sounds**: [City Ambience on Freesound](https://freesound.org/search/?q=city%20ambience) - Free WAV.
- **Explosion Sounds**: [Explosion SFX on Freesound](https://freesound.org/search/?q=explosion) - Free WAV.
- **Car Engine Sounds**: [Car Engine on Freesound](https://freesound.org/search/?q=car%20engine) - Free WAV.
- **Footstep Sounds**: [Footsteps on Freesound](https://freesound.org/search/?q=footsteps) - Free WAV.
- **Radio Broadcasts**: [News Radio on Freesound](https://freesound.org/search/?q=news%20radio) - Free WAV for placeholders.

## Animations

- **Walk/Run Cycles**: [Mixamo Free Animations](https://www.mixamo.com/) - Download FBX for characters.
- **Idle/Combat**: [Unity Asset Store Free Packs](https://assetstore.unity.com/packages/3d/animations/free-cartoon-animations-pack-225944) - Free.

## UI Elements

- **Icons**: [Free UI Icons on Freepik](https://www.freepik.com/search?query=ui%20icons) - PNG/SVG.
- **Fonts**: [Google Fonts](https://fonts.google.com/) - Free for UI text.

## Skyboxes & Environments

- **Skybox**: [Free Skyboxes on OpenGameArt](https://opengameart.org/content/free-skyboxes) - Cubemap textures.
- **Terrain Textures**: [Grass/Road Textures on OpenGameArt](https://opengameart.org/content/free-grass-and-dirt-textures) - Free PNG.

## Particles

- **Rain Particles**: [Particle Effects on Unity Asset Store](https://assetstore.unity.com/packages/vfx/particles/free-particle-effects-144361) - Free pack.
- **Explosion Particles**: [Explosion VFX on Unity Asset Store](https://assetstore.unity.com/packages/vfx/particles/explosion-vfx-pack-24420) - Free.

## Shaders & Effects

- **Corruption Shaders**: [Free Shader Graph Examples on Unity Learn](https://learn.unity.com/tutorial/shader-graph-introduction) - Tutorials for custom shaders.
- **Post-Processing Profiles**: [Free Post-Processing Stack on Unity Asset Store](https://assetstore.unity.com/packages/essentials/post-processing-stack-83912) - Free.

## Instructions

1. Download assets in compatible formats (FBX, OBJ, PNG, WAV, etc.).
2. Import into Unity via Assets > Import New Asset.
3. Assign to prefabs/scripts in inspectors (e.g., PlayerController model, AudioManager clips).
4. For paid assets, check Unity Asset Store for free alternatives.
5. Ensure licenses allow commercial use if planning release.

## Unity Setup Instructions

1. **Install Unity 2022.3 LTS or later**:
   - Open Unity Hub.
   - Go to Installs > Install Editor > Select Unity 2022.3.x LTS > Install.

2. **Open Project**:
   - In Unity Hub, go to Projects > Add > Select the /home/a/mrprez folder.
   - Open the project.

3. **Install Packages**:
   - Window > Package Manager > Install: Universal RP, AI Navigation, TextMeshPro.
   - If needed, install Mirror for multiplayer.

4. **Configure Project**:
   - Edit > Project Settings > Graphics > Set URP Asset.
   - Quality Settings: Set to Low for testing.

5. **Set Up Scenes**:
   - Create MainMenu.unity: Add Canvas, UI elements (title, buttons), assign MenuController script.
   - Create GameWorld.unity: Add Terrain, Directional Light, Skybox. Place prefabs for White House, Capitol. Add GameManager, WorldGenerator, etc.

6. **Assign Scripts**:
   - Create empty GameObjects and attach scripts (e.g., GameManager to root).
   - Assign public fields in inspectors (e.g., PlayerController to player object).

7. **Import Assets**:
   - Download from links above.
   - Drag into Assets folder.
   - Assign to scripts (e.g., AudioManager clips, AssetLoader prefabs).

8. **Test Build**:
   - File > Build Settings > Add scenes > Build.
   - Run TestRunner for unit tests.

## Direct Download Repos

- **Top Assets For Unity Free**: [GitHub Repo](https://github.com/phancyn/Top-Assets-For-Unity-Free) - Free Unity assets with direct download links in releases.

## Adult Content Assets

- **Voice Lines for Flirtation/Scandals**: [ElevenLabs AI Voices](https://elevenlabs.io/) - Generate custom voice lines for adult-themed dialogues (free tier available).
- **Sensual Music Tracks**: [Freesound Sensual Audio](https://freesound.org/search/?q=sensual) - Free WAV files for romantic/tense scenes.
- **Character Outfits (Revealing)**: [Sketchfab Adult Models](https://sketchfab.com/tags/adult) - Free 3D models (ensure compliance with terms).
- **UI Icons for Romance**: [Freepik Romance Icons](https://www.freepik.com/search?query=romance%20icons) - Free PNG for heart, kiss, etc.

## Notes

- Use placeholders (cubes, default materials) for testing.
- Customize models in Blender if needed.
- Update this file as more assets are found.