#!/bin/bash

# Script to download free Unity assets for beta testing

# Create directories
mkdir -p Assets/Models Assets/Textures Assets/Audio Assets/UI Assets/Tools

# Download from Top Assets For Unity Free repo
echo "Downloading Folder Icons..."
curl -L -o Assets/Tools/Folder.Icons.unitypackage https://github.com/phancyn/Top-Assets-For-Unity-Free/releases/download/Downloaded/Folder.Icons.unitypackage

# echo "Skipping MegaFiers 2 (large file)..."

echo "Downloading Minimalist Main Menu Pack..."
curl -L -o Assets/UI/Minimalist.Main.Menu.Pack.v1.2.unitypackage https://github.com/phancyn/Top-Assets-For-Unity-Free/releases/download/Downloaded/Minimalist.Main.Menu.Pack.v1.2.unitypackage

echo "Downloading Rainbow Folders..."
curl -L -o Assets/Tools/Rainbow.Folders.unitypackage https://github.com/phancyn/Top-Assets-For-Unity-Free/releases/download/Downloaded/Rainbow.Folders.unitypackage

echo "Downloading vFavorites 2..."
curl -L -o Assets/Tools/vFavorites.2.v2.0.7.unitypackage https://github.com/phancyn/Top-Assets-For-Unity-Free/releases/download/Downloaded/vFavorites.2.v2.0.7.unitypackage

echo "Downloading vTabs 2..."
curl -L -o Assets/Tools/vTabs.2.v2.0.13.unitypackage https://github.com/phancyn/Top-Assets-For-Unity-Free/releases/download/Downloaded/vTabs.2.v2.0.13.unitypackage

# For other assets, use placeholders or manual download
echo "For 3D models, textures, audio, please download manually from ASSETDL.md or use placeholders (cubes, default materials) for beta testing."

echo "Download complete."