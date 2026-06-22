# Cube Blast 🧊💥

[![Unity Version](https://img.shields.io/badge/Unity-6.4-blue)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-PC%20%7C%20Android-blue)](https://github.com/demonstalker86/cube-blast)

## 🎮 Description
A Unity game where cubes split and explode with physics. Click on a cube to make it disappear and spawn new cubes with random colors and sizes.

**Supported platforms:** PC (Windows, Mac, Linux) and Android.

## ✨ Features
- 🎯 Click on cube → disappears and spawns new cubes
- 🎲 Random number of new cubes (2-6 pieces)
- 📏 Each new cube is 2x smaller than the previous one
- 🎨 Random color for each cube
- 💥 Explosion force from the center of the disappeared cube
- 📉 Split chance decreases by 2x each time (100% → 50% → 25%...)
- 🚧 Invisible walls to limit cube flight
- 📱 Supports PC and mobile devices

## 🎯 Gameplay
Click on a cube
The cube disappears
Split chance is checked:
Success: new cubes appear (2-6 pieces) with explosion
Failure: cube simply disappears
The process repeats with each new cube

## 🛠 Technologies
- **Unity 6.4**
- **C#**
- **SOLID principles**
- **Git LFS**

## 📱 Controls
| Platform | Action |
|----------|--------|
| PC | Mouse click on cube |
| Mobile (Android) | Tap on cube |

## 🌿 Branches
This repository uses separate branches for different platforms:

- **`main`** - PC version (default)
- **`android-build`** - Android version with mobile optimizations

## 🔧 Installation

Clone repository

git clone https://github.com/demonstalker86/cube-blast.git

PC Version (main branch)
Switch to `main` branch:

git checkout main

Open Unity Hub
Click **Open Project**
Select the project folder
Open scene `Assets/Scenes/MainScene.unity`

Android Version (android-build branch)
Switch to `android-build` branch:

git checkout android-build

Open Unity Hub
Click **Open Project**
Select the project folder
Open scene `Assets/Scenes/MainScene.unity`
Build for Android: **File** → **Build Profiles** → **Android** → **Build and Run**

## 📁 Project Structure

cube-blast/
├── Assets/
│ ├── Scripts/
│ │ ├── Config.cs # Centralized settings
│ │ ├── InputListener.cs # Input handling
│ │ ├── Raycaster.cs # Raycast to detect cubes
│ │ ├── Cube.cs # Cube behavior
│ │ ├── ColorGenerator.cs # Random color generation
│ │ ├── Spawner.cs # Object spawning
│ │ ├── Exploder.cs # Explosion force
│ │ └── GameFlow.cs # Main game logic
│ ├── Prefabs/
│ │ └── Cube.prefab # Cube prefab
│ └── Scenes/
│ └── MainScene.unity # Main scene
└── ProjectSettings/ # Unity settings

## 📄 License
MIT

## 👤 Author

**demonstalker86**

- GitHub: [@demonstalker86](https://github.com/demonstalker86)

---

⭐ If you like this project, give it a star on GitHub!