# Cube Blast 🧊💥

[![Unity Version](https://img.shields.io/badge/Unity-6.4-blue)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

## 🎮 Description
A Unity game where cubes split and explode with physics. Click on a cube to make it disappear and spawn new cubes with random colors and sizes.

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
1. Click on a cube
2. The cube disappears
3. Split chance is checked:
   - Success: new cubes appear (2-6 pieces) with explosion
   - Failure: cube simply disappears
4. The process repeats with each new cube

## 🛠 Technologies
- **Unity 6.4**
- **C#**
- **SOLID principles**
- **Git LFS**

## 📱 Controls
| Platform | Action |
|----------|--------|
| PC | Mouse click on cube |
| Mobile | Tap on cube |

## 🔧 Installation

### Clone repository
```bash
https://github.com/demonstalker86/cube-blast

Open in Unity

Open Unity Hub
Click Open Project
Select the project folder
Open scene Assets/Scenes/MainScene.unity

📁 Project Structure

cube-blast/
├── Assets/
│   ├── Scripts/
│   │   ├── InputHandler.cs      # Input handling
│   │   ├── CubeController.cs    # Cube behavior
│   │   ├── CubeManager.cs       # Cube management
│   │   └── GameManager.cs       # Game management
│   ├── Prefabs/
│   │   └── CubePrefab.prefab    # Cube prefab
│   └── Scenes/
│       └── MainScene.unity      # Main scene
└── ProjectSettings/             # Unity settings

📄 License
Distributed under the MIT License. See LICENSE for more information.

👤 Author

GitHub: @demonstalker86