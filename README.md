# 🍄 Mario Multiverse

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2022.3.62f3-000000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Firebase](https://img.shields.io/badge/Firebase-FFCA28?style=for-the-badge&logo=firebase&logoColor=black)
![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20PC-blue?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

**A collection of classic arcade games reimagined with Mario — all in one app!**

*Super Mario Bros • Pac-Man • Flappy Bird • Chrome Dino • Donkey Kong • Multiverse Runner*

---

</div>

## 📖 Table of Contents

- [About The Project](#-about-the-project)
- [Game Modes](#-game-modes)
- [Features](#-features)
- [Screenshots](#-screenshots)
- [Tech Stack & Architecture](#-tech-stack--architecture)
- [Project Structure](#-project-structure)
- [Scripts Breakdown](#-scripts-breakdown)
- [Skills Learned](#-skills-learned)
- [Skills Currently Learning](#-skills-currently-learning)
- [Getting Started](#-getting-started)
- [Build & Run](#-build--run)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)

---

## 🎮 About The Project

**Mario Multiverse** is a 2D mobile/PC game built with **Unity (C#)** that combines six beloved classic arcade games into a single application, all starring Mario as the protagonist. The game features a full **Firebase authentication system** (login, register, password reset), a **main menu with a coin shop** to buy power-ups, and a **game selection hub** to choose between different game modes.

Players earn **coins** across all game modes and can spend them in the shop to purchase power-ups like **Star Man** (invincibility) and **Big Mushroom** (grow). Progress is persisted locally via **PlayerPrefs** and user accounts are managed through **Firebase Auth + Realtime Database**.

---

## 🕹️ Game Modes

### 1. 🏃 Super Mario Bros (Classic Platformer)
The flagship game mode — a full recreation of the original Super Mario Bros.

- **Side-scrolling platformer** with left/right movement, jumping, and running
- **Enemies**: Goomba (stomping AI), Koopa Troopa (shell mechanics)
- **Power-ups**: Magic Mushroom (grow big), Star Man (invincibility with color-shifting animation), 1-Up Mushroom (extra life), Coins
- **Level elements**: Mystery blocks (item spawning), Brick blocks (breakable), Pipes (entry/exit/warp system), Flag pole (level completion)
- **Physics**: Ground detection via OverlapCircle raycasting, 2D physics materials (bounciness, no friction)
- **Audio**: Jump SFX (small/big), coin collection, power-up, power-down, death, flagpole, invincibility theme, ground theme
- **Touch controls**: On-screen buttons for mobile with `TouchControlsKit` integration
- **Lives system** with game over and retry mechanics

### 2. 👻 Pac-Man
A full Pac-Man clone with Mario as the playable character.

- **Tilemap-based maze** with 38 unique wall tiles
- **4 Ghost AIs** — Blinky, Pinky, Inky, and Clyde — each with unique chase behavior
- **Ghost State Machine**: Scatter → Chase → Frightened → Eyes (return to home)
- **Pellets & Power Pellets**: Eat all pellets to win the round; power pellets make ghosts vulnerable
- **Ghost multiplier scoring**: Eating consecutive ghosts increases points (200 → 400 → 800 → 1600)
- **Swipe controls** for mobile using `SwipeController` library
- **Node-based pathfinding** for ghost movement with available direction detection
- **Passage system** for teleporting between maze sides
- **Lives system** (3 lives), score display, game over screen
- **Audio**: Chomp, ghost eat, death SFX

### 3. 🐦 Flappy Bird
Mario takes flight in this Flappy Bird recreation.

- **Tap-to-flap** mechanics with physics-based gravity
- **Procedurally generated pipes** that spawn and move across the screen
- **Score tracking** with high score persistence via PlayerPrefs
- **Pipe gap randomization** for varied difficulty
- **Game over on collision** or going off-screen
- **Fly button** for touch controls on mobile

### 4. 🦖 Chrome Dino Runner
The classic Chrome dinosaur game, Mario-style.

- **Endless runner** with auto-scrolling ground
- **Obstacle spawning**: Cacti (6 variants — small/large, single/double/triple) and Pterodactyl birds
- **Progressive difficulty**: Game speed increases over time (`gameSpeedIncrease * Time.deltaTime`)
- **Score milestones**: Audio cue every 100 points
- **High score persistence** via PlayerPrefs
- **Jump mechanics** with ground check physics
- **Retry system** with game over UI

### 5. 🦍 Donkey Kong
A recreation of the arcade classic Donkey Kong.

- **Platform climbing** with ladder mechanics
- **Donkey Kong AI** that throws barrels using **raycasting** to detect the player's direction
- **Barrel physics**: Barrels roll with force-based movement (`ForceMode2D.Impulse`)
- **Climbing system**: Detect ladder colliders and switch to climb sprite/movement
- **Objective-based levels**: Reach the Princess to complete the level
- **Lives system** (3 lives) with level restart on death
- **Touch controls**: Left, Right, Jump, and Climb buttons via `TouchControlsKit`
- **Sprite-based animation** cycling through run frames at 12 FPS

### 6. 🌌 Multiverse Runner (Endless Runner)
An original game mode — a Mario-themed endless runner with enemies from all other games.

- **Auto-scrolling side-scroller** where Mario runs forward automatically
- **Multi-game enemies**: Goombas, Koopas, Pac-Man ghosts, Dino birds, Bomb characters, and Donkey Kong barrel spawners all appear as obstacles
- **Power-up system**: Use Star Man and Big Mushroom from inventory (purchased in shop)
- **Falling platforms** that crumble when stepped on
- **Score tracking** based on distance traveled
- **Progressive difficulty**: Speed increases over time
- **Touch controls**: Jump, Star Power button, Big Mushroom button

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔐 **Firebase Authentication** | Full login/register system with email/password, email validation (Gmail, Hotmail, Yahoo), duplicate email detection, password reset via email |
| 🗄️ **Firebase Realtime Database** | Stores usernames linked to user IDs for profile management |
| 🪙 **Coin Economy System** | Earn coins in-game → spend in shop → buy power-ups (Star Man: 50 coins, Big Mushroom: 30 coins) |
| 💾 **Persistent Data** | PlayerPrefs-based saving for coins, power-ups, and high scores across sessions |
| 🎵 **Sound System** | Comprehensive SFX and background music for all game modes (jump, coin, power-up, death, invincibility theme, milestone sounds) |
| 📱 **Mobile-First Design** | Touch controls with on-screen buttons (`TouchControlsKit`), swipe controls (`SwipeController`), landscape orientation lock |
| 🎬 **Animation System** | Unity Animator controllers for Mario (small/big), Goombas, Koopas, Pac-Man, Dino, Bomb characters, and Birds |
| ⏸️ **Pause System** | In-game pause menu with resume, retry, and quit functionality |
| 🔄 **Singleton Game Managers** | DontDestroyOnLoad pattern for persistent managers across scene transitions |
| 🎨 **Custom UI** | Multiple UI asset packs (2D Casual UI, Fancy UI, Simple Buttons, FreeButtonSet) for polished menus |
| 📐 **Physics Materials** | Custom 2D physics materials for bounciness (Star Man), zero friction (pipes), and no friction (walls) |
| 🗺️ **Tilemap System** | Pac-Man maze built with Unity Tilemaps and 38+ unique wall tiles |

---

## 🛠️ Tech Stack & Architecture

### Engine & Language
- **Unity 2022.3.62f3** (LTS) — 2D game development
- **C#** — All game logic and scripting

### Backend & Services
- **Firebase Authentication** — Email/password auth, password reset
- **Firebase Realtime Database** — User profile storage

### Key Unity Systems Used
| System | Usage |
|---|---|
| **Rigidbody2D / Collider2D** | All game physics — movement, jumping, collisions |
| **Physics2D (OverlapCircle / OverlapBox / Raycast)** | Ground detection, ladder detection, barrel throw direction |
| **Animator Controllers** | State machines for Mario, enemies, power-ups |
| **Tilemaps** | Pac-Man maze construction |
| **SpriteRenderer** | Multi-sprite switching (small/big Mario), color manipulation (Star Power) |
| **AudioSource / AudioClip** | SFX and music playback |
| **SceneManager** | Scene transitions (12 scenes total) |
| **PlayerPrefs** | Local data persistence |
| **Coroutines** | Async operations (Firebase calls, animations, timed events) |
| **Singleton Pattern** | `DontDestroyOnLoad` for persistent managers |
| **TextMeshPro** | UI text rendering |
| **Canvas / UI System** | Menus, HUD, buttons |
| **Physics2D Materials** | Bounciness, friction control |
| **Layer-based collision** | 12 custom layers (Ground, Pacman, Ghost, Pellet, Obstacle, Node, Player, Ladder, PowerUps, Enemies) |
| **Sorting Layers** | 7 sorting layers (Background, PowerUps, Enemies, Mario, Pipes, Blocks, Default) |

### Third-Party Packages & Assets
| Package | Purpose |
|---|---|
| **Firebase SDK** | Authentication & database |
| **TouchControlsKit** | Mobile on-screen touch buttons/joystick |
| **SwipeController** | Swipe gesture detection (Pac-Man) |
| **Cinemachine** | Camera system |
| **TextMesh Pro** | Advanced text rendering |
| **NavMeshPlus** | 2D navigation mesh support |
| **A* Pathfinding Project** | AI pathfinding |
| **Odin Inspector** | Editor tooling |
| **ParrelSync** | Multiplayer testing |
| **2D Casual UI / Fancy UI** | UI art assets |

---

## 📁 Project Structure

```
Mario-Multiverse/
├── Assets/
│   ├── Scripts/                    # All C# game scripts (66 scripts)
│   ├── Scenes/                     # 12 Unity scenes
│   │   ├── LoginSystem.unity       # Firebase auth (login/register)
│   │   ├── MainMenu.unity          # Main menu with shop
│   │   ├── GameSelectionScene.unity # Game mode selector hub
│   │   ├── Preload.unity           # Preload scene for Donkey Kong
│   │   ├── MarioPreload.unity      # Preload scene for Mario
│   │   ├── MultiversePreload.unity # Preload scene for Multiverse
│   │   ├── Mario.unity             # Super Mario Bros level
│   │   ├── Level 1.unity           # Donkey Kong level
│   │   ├── Pacman.unity            # Pac-Man maze
│   │   ├── DinoGame.unity          # Chrome Dino runner
│   │   ├── FlappyBirdGame.unity    # Flappy Bird
│   │   └── MultiVerse.unity        # Multiverse endless runner
│   ├── Prefabs/                    # Reusable game objects
│   │   ├── Mario.prefab            # Mario character
│   │   ├── Goomba.prefab           # Goomba enemy
│   │   ├── Koopa.prefab            # Koopa Troopa enemy
│   │   ├── Ghost_Blinky.prefab     # Pac-Man ghost (red)
│   │   ├── Ghost_Pinky.prefab      # Pac-Man ghost (pink)
│   │   ├── Ghost_Inky.prefab       # Pac-Man ghost (cyan)
│   │   ├── Ghost_Clyde.prefab      # Pac-Man ghost (orange)
│   │   ├── Barrel.prefab           # Donkey Kong barrel
│   │   ├── DonkeyKong.prefab       # Donkey Kong character
│   │   ├── PowerUps/               # Power-up prefabs
│   │   │   ├── Coin.prefab
│   │   │   ├── MagicMushroom.prefab
│   │   │   ├── Starman.prefab
│   │   │   └── 1UpMushroom.prefab
│   │   └── ...
│   ├── MultiVersePrefabs/          # Multiverse-specific prefabs
│   │   ├── Enemies/                # Multi-game enemy prefabs
│   │   │   ├── M_Goomba.prefab
│   │   │   ├── Koopa.prefab
│   │   │   ├── M_Pacman.prefab
│   │   │   ├── Bird.prefab
│   │   │   ├── EnemyDino.prefab
│   │   │   ├── Bomb_1.prefab
│   │   │   └── M_DonkeyKong.prefab
│   │   ├── Brick.prefab
│   │   ├── MysteryBlock.prefab
│   │   ├── FallingPlatform.prefab
│   │   └── ...
│   ├── DinoPrefabs/                # Dino game obstacles
│   │   ├── Cactus_Large_*.prefab   # Cactus variants
│   │   ├── Cactus_Small_*.prefab   # Small cactus variants
│   │   └── Bird.prefab             # Pterodactyl
│   ├── FlappyPrefabs/              # Flappy Bird pipe prefabs
│   ├── Animations/                 # Animation controllers & clips
│   │   ├── MarioAnimator.controller
│   │   ├── MarioBig.controller
│   │   ├── Goomba.controller
│   │   ├── Koopa.controller
│   │   ├── M_Pacman.controller
│   │   ├── EnemyDino.controller
│   │   ├── Bomb.controller
│   │   └── ... (28 animation files)
│   ├── Sprites/                    # Game sprites and textures
│   │   ├── Pacman_*.png            # Pac-Man sprites (eat/death)
│   │   ├── Ghost_*.png             # Ghost sprites (body/eyes/vulnerable)
│   │   ├── Wall_*.png              # 38 Pac-Man maze wall tiles
│   │   ├── Mario_*.png             # Mario run/climb sprites
│   │   ├── DonkeyKong.png          # DK sprite
│   │   ├── Dino_*.png              # Dino sprites
│   │   ├── Cactus_*.png            # Obstacle sprites
│   │   └── ... (107 sprite files)
│   ├── Tiles/                      # Unity Tilemap tiles
│   │   ├── Wall_00.asset - Wall_37.asset
│   │   ├── Pellet_Tile.asset
│   │   ├── PowerPellet_Tile.asset
│   │   └── Node_Tile.asset
│   ├── SoundEffects/               # Audio files
│   │   ├── 01. Ground Theme.mp3
│   │   ├── 05. Invincibility Theme.mp3
│   │   ├── smb_*.wav               # Mario SFX (coin, jump, death, etc.)
│   │   └── pacman-*/               # Pac-Man SFX folders
│   ├── Physics/                    # 2D physics materials
│   │   ├── Bounciness.physicsMaterial2D
│   │   ├── NoFriction.physicsMaterial2D
│   │   └── ZeroFriction.physicsMaterial2D
│   ├── Firebase/                   # Firebase SDK
│   ├── Fonts/                      # Custom fonts
│   ├── Mario Assets/               # Super Mario tutorial assets
│   ├── 2D Casual UI/               # UI asset pack
│   ├── Fancy UI/                   # UI asset pack
│   ├── FreeButtonSet/              # Button sprites
│   ├── Simple Buttons/             # Button sprites
│   ├── GameUI assets/              # Game HUD assets
│   ├── Project UI/                 # Project UI elements
│   ├── VictorsAssets/              # Touch control assets
│   ├── SwipeController-master/     # Swipe detection library
│   ├── TextMesh Pro/               # TMP resources
│   ├── materials/                  # Shared materials
│   ├── Resources/                  # Runtime-loaded assets
│   ├── Plugins/                    # Native plugins
│   ├── StreamingAssets/            # Streaming data
│   └── ExternalDependencyManager/  # EDM4U (Firebase deps)
├── Packages/
│   └── manifest.json               # Unity package dependencies
├── ProjectSettings/                # Unity project configuration
└── README.md                       # This file
```

---

## 📜 Scripts Breakdown

### 🔐 Authentication & Data Management
| Script | Description |
|---|---|
| `AuthManager.cs` | Firebase Authentication — login, register (with email domain validation for Gmail/Hotmail/Yahoo), logout, forgot password, user profile creation, stores username in Realtime Database |
| `MainManager.cs` | **Singleton** — Global persistent manager for coins, star power, and big mushroom counts. Saves/loads from PlayerPrefs. Survives scene transitions with `DontDestroyOnLoad` |
| `DataSaver.cs` | Utility for data persistence operations |

### 🏠 Menu & Navigation
| Script | Description |
|---|---|
| `MainMenuManager.cs` | Main menu — play button, quit, high score display, **coin shop** (Buy Star Man for 50 coins, Buy Big Mushroom for 30 coins), coin balance display |
| `GameSelection.cs` | Game selection hub — routes to all 6 game modes, destroys existing game managers before loading new ones to prevent conflicts |
| `UIManager.cs` | Login/Register UI screen management (toggle between login and register panels) |
| `PauseManager.cs` | In-game pause — time scale manipulation (`Time.timeScale = 0/1`), resume, retry, quit to menu |
| `RetryManager.cs` | Game over retry functionality |

### 🏃 Super Mario Bros Scripts
| Script | Description |
|---|---|
| `Movement.cs` | **Full Mario controller** (classic mode) — horizontal movement via `TouchControlsKit`, jumping with variable height (release jump = reduce velocity), ground detection (OverlapCircle), camera boundary clamping, enemy collision (death if small, shrink if big, bounce if jumping), mushroom collection (grow), star man (invincibility with rainbow color animation for 10 seconds), 1-Up collection, collision check triggers |
| `MarioMovement.cs` | **Mario controller** (Multiverse mode) — vertical-only auto-runner, tap to jump, star power and big mushroom buttons from inventory, same grow/shrink/starpower mechanics |
| `MarioGameManager.cs` | **Singleton** — Lives system, game failed/retry, scene reloading for Mario levels |
| `GoombaMovement.cs` | Goomba enemy AI — horizontal patrol movement, direction flipping on wall collision, stomped death (flatten animation + destroy), star power death |
| `KoopaMovement.cs` | Koopa Troopa AI — patrol movement, shell mechanics (enter shell on stomp, shell slide on second stomp), wall bounce in shell mode |
| `BlockItem.cs` | Mystery block — spawns items (coins, mushrooms, stars) when hit from below, becomes empty after hit |
| `Hit.cs` | Block hit detection — handles head-bump physics and item spawning logic |
| `Coin.cs` | Coin collection — adds coins to MainManager, plays SFX, animated coin sprite |
| `CoinManager.cs` | Coin counter UI management |
| `FlagPole.cs` | Level completion — flag slide animation, score bonus, scene transition |
| `Pipe.cs` | Pipe entry/exit system — trigger-based warp zones with enter/exit animations |
| `MushroomMove.cs` | Mushroom power-up movement — emerges from block, moves horizontally, flips on wall collision |
| `StarMovement.cs` | Star power-up movement — bouncing physics with `Bounciness` material, horizontal movement |
| `CheckMarioVisible.cs` | Camera visibility check for Mario (death if off-screen) |

### 👻 Pac-Man Scripts
| Script | Description |
|---|---|
| `Pacman.cs` | Pac-Man player controller — swipe input (mobile) + keyboard input (PC), rotation to face movement direction, death sequence animation, state reset |
| `P_Movement.cs` | Pac-Man movement system — tile-based grid movement, direction queuing, wall collision detection via raycasting, speed control |
| `P_GameManager.cs` | **Singleton** — Full Pac-Man game loop: lives (3), scoring with ghost multiplier, pellet tracking, round completion, game over, state reset for all ghosts + pacman |
| `Ghost.cs` | Ghost base class — manages ghost state machine (scatter/chase/frightened/eyes), initial behavior, home behavior, point values |
| `GhostBehavior.cs` | Base class for ghost AI behaviors — enable/disable/duration management |
| `GhostChase.cs` | Chase behavior — each ghost targets differently (Blinky: direct, Pinky: ahead, Inky: complex, Clyde: distance-based) |
| `GhostScatter.cs` | Scatter behavior — ghosts retreat to assigned corners |
| `GhostFrightened.cs` | Frightened behavior — ghosts turn blue, random movement, can be eaten, flashing white warning before timer ends |
| `GhostEyes.cs` | Eyes behavior — disembodied eyes return to ghost home after being eaten |
| `GhostHome.cs` | Home behavior — ghosts bounce inside the home pen, exit sequence |
| `Node.cs` | Pathfinding node — detects available directions at intersections using Physics2D raycasting, used by ghost AI for navigation |
| `Pellet.cs` | Pellet collection — reports to P_GameManager when eaten |
| `PowerPellet.cs` | Power pellet — extends Pellet with a configurable duration for ghost frightened state |
| `Passage.cs` | Teleportation passage — wraps Pac-Man/ghosts to opposite side of maze |
| `AnimatedSprite.cs` | Sprite frame animation — used for Pac-Man chomp animation and death sequence |

### 🦖 Chrome Dino Scripts
| Script | Description |
|---|---|
| `DinoGameManager.cs` | **Singleton** — Endless runner loop: progressive speed increase, score tracking (5-digit display), high score persistence, milestone sound every 100 points, game over/retry |
| `Player.cs` | Dino player — jump on touch/space, ground detection, animator control |
| `Obstacale.cs` | Obstacle movement — moves left at `gameSpeed`, destroyed when off-screen |
| `ObstacleSpawner.cs` | Spawns obstacles — random selection from cactus/bird prefab array, random spawn intervals |
| `Ground.cs` | Scrolling ground — seamless looping ground texture at game speed |
| `Spawner.cs` | Generic spawner utility |
| `AnimatedScript.cs` | Sprite animation helper for dino game |

### 🦍 Donkey Kong Scripts
| Script | Description |
|---|---|
| `DMarioMove.cs` | DK Mario controller — horizontal movement, jumping, **ladder climbing** (detect ladder layer, switch to climb sprite), collision with objectives (level complete) and obstacles (level failed), sprite-based frame animation at 12 FPS |
| `DonkeyBarrelThrow.cs` | Donkey Kong AI — uses **raycasting** (left + right rays) to detect player, throws barrel prefabs with `ForceMode2D.Impulse` force in the detected direction, 2-second cooldown between throws |
| `DonkeyGameManager.cs` | **Singleton** — Lives system (3 lives), level failed/complete handling, scene reloading |
| `Barrel.cs` | Barrel behavior — rolling physics, collision detection |

### 🌌 Multiverse Runner Scripts
| Script | Description |
|---|---|
| `GameManager.cs` | **Singleton** — Multiverse game loop: progressive speed, distance-based scoring, game failed with 2-second reload delay |
| `M_UiManager.cs` | Multiverse HUD — score display, game speed tracking |
| `MUIManager.cs` | Additional Multiverse UI management |
| `M_Obstacle.cs` | Multiverse obstacle behavior — enemies from all games move left at game speed |
| `M_ObstacleSpawner.cs` | Spawns multi-game enemies at random intervals |
| `M_Barrel.cs` | Multiverse barrel variant |
| `M_DonkeySpawner.cs` | Donkey Kong barrel spawner for Multiverse mode |
| `M_Ground.cs` | Scrolling ground for Multiverse |
| `MarioUIManager.cs` | Mario-specific UI elements in Multiverse |

### 🐦 Flappy Bird Scripts
| Script | Description |
|---|---|
| `BirdScript.cs` | Flappy Mario — tap/space to flap (upward velocity), death on collision or going off-screen, fly button toggle |
| `LogicScript.cs` | Score manager — increment on pipe pass, high score tracking, game over state |
| `PipeSpawnScript.cs` | Pipe spawner — instantiates pipes at random heights with timed intervals |
| `PipeMoveScript.cs` | Pipe movement — scrolls pipes leftward, destroys when off-screen |
| `PipeMiddleScript.cs` | Score trigger — detects when bird passes through pipe gap |
| `F_GameManager.cs` | Flappy Bird game manager |
| `Exit.cs` | Exit/quit functionality |

---

## 🧠 Skills Learned

### Game Development (Unity & C#)
- ✅ **2D Game Physics** — Rigidbody2D, Collider2D, Physics2D materials (bounciness, friction), force-based movement, gravity manipulation
- ✅ **Player Controllers** — Touch input, keyboard input, swipe gestures, platformer movement, grid-based movement
- ✅ **State Machines** — Ghost AI with multiple behavioral states (scatter, chase, frightened, eyes, home)
- ✅ **Enemy AI** — Patrol movement, player detection via raycasting, directional barrel throwing, pathfinding via node systems
- ✅ **Tilemap System** — Building complex mazes with custom rule tiles
- ✅ **Animation System** — Animator controllers, animation clips, sprite-based frame animation, transition conditions
- ✅ **Scene Management** — Multi-scene architecture, scene loading/unloading, preload scenes
- ✅ **Singleton Pattern** — `DontDestroyOnLoad` for persistent managers, proper cleanup on scene transitions
- ✅ **Coroutines** — Asynchronous operations, timed events, animation sequences, Firebase async calls
- ✅ **UI Development** — Canvas system, TextMeshPro, button events, panel toggling, HUD elements
- ✅ **Audio Management** — AudioSource, AudioClip, `PlayOneShot()`, background music control
- ✅ **Power-Up Systems** — Grow/shrink mechanics, invincibility with visual effects, inventory management
- ✅ **Collision Detection** — Tags, layers, `OnCollisionEnter2D`, `OnTriggerEnter2D`, layer-based filtering
- ✅ **Camera Systems** — Camera boundary clamping, screen-to-world point conversion
- ✅ **Spawner Systems** — Random obstacle/enemy spawning with configurable intervals
- ✅ **Data Persistence** — `PlayerPrefs` for saving scores, coins, power-ups, and high scores

### Backend & Cloud Services
- ✅ **Firebase Authentication** — Email/password sign-in, user registration with validation, password reset, session management
- ✅ **Firebase Realtime Database** — User profile CRUD operations
- ✅ **Asynchronous Programming** — Task-based async patterns, `ContinueWith`, `WaitUntil`

### Software Engineering
- ✅ **Design Patterns** — Singleton, Observer (event-driven), State Machine, Component-based architecture
- ✅ **Object-Oriented Programming** — Inheritance (GhostBehavior → GhostChase/Scatter/Frightened/Eyes), polymorphism, encapsulation
- ✅ **Code Organization** — Modular scripts, separation of concerns (managers, controllers, UI, data)
- ✅ **Version Control** — Git/GitHub with proper `.gitignore` for Unity projects
- ✅ **Mobile Development** — Screen orientation locking, touch input handling, frame rate targeting

---

## 📚 Skills Currently Learning

- 🔄 **Multiplayer Networking** — ParrelSync package is already integrated for testing
- 🔄 **Advanced AI Pathfinding** — A* Pathfinding Project is integrated (learning advanced navigation)
- 🔄 **NavMesh 2D** — NavMeshPlus package included for 2D navigation mesh support
- 🔄 **Advanced Camera Systems** — Cinemachine integration for dynamic camera behaviors
- 🔄 **Visual Scripting** — Unity Visual Scripting package enabled for prototyping
- 🔄 **Game Monetization** — In-game economy system (coin shop) as foundation for future monetization
- 🔄 **Level Design** — Expanding from single-level per game to multi-level progressions
- 🔄 **Performance Optimization** — Frame rate targeting (60/120 FPS), object pooling concepts

---

## 🚀 Getting Started

### Prerequisites

- **Unity Hub** with **Unity 2022.3.62f3** (LTS) installed
- **Android SDK** (for mobile builds)
- **Firebase project** configured with:
  - Authentication (Email/Password enabled)
  - Realtime Database
  - `google-services.json` in `Assets/StreamingAssets/`

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Vedant241/Mario-Multiverse.git
   ```

2. **Open in Unity Hub**
   - Add the project folder in Unity Hub
   - Open with Unity **2022.3.62f3**

3. **Firebase Setup**
   - Create a Firebase project at [console.firebase.google.com](https://console.firebase.google.com)
   - Enable **Email/Password Authentication**
   - Enable **Realtime Database**
   - Download `google-services.json` and place it in `Assets/StreamingAssets/`

4. **Resolve Dependencies**
   - Unity will automatically resolve packages from `manifest.json`
   - Firebase dependencies resolve via External Dependency Manager (EDM4U)

---

## 🏗️ Build & Run

### Editor (PC)
1. Open any scene from `Assets/Scenes/`
2. Set the build scene order in **File → Build Settings** (already configured):
   - LoginSystem → MainMenu → GameSelectionScene → MarioPreload → Mario → DinoGame → FlappyBirdGame → Preload → Level 1 → Pacman → MultiversePreload → MultiVerse
3. Press **Play** in the Unity Editor

### Android Build
1. **File → Build Settings → Android**
2. Switch platform
3. Configure Player Settings (package name, minimum API level)
4. **Build and Run**

---

## 🗺️ Roadmap

- [ ] Add more levels to Super Mario Bros
- [ ] Implement Donkey Kong level progression
- [ ] Add online leaderboards via Firebase
- [ ] Implement multiplayer mode (ParrelSync foundation ready)
- [ ] Add more enemy types and boss battles
- [ ] Create a level editor
- [ ] Add achievements and unlock system
- [ ] iOS build support

---

## 🤝 Contributing

Contributions are welcome! If you'd like to contribute:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is for educational purposes. All Mario, Pac-Man, Donkey Kong, Flappy Bird, and Chrome Dino game elements are the intellectual property of their respective owners (Nintendo, Bandai Namco, Dong Nguyen, Google). This project is a fan-made recreation for learning game development.

---

<div align="center">

**Made with ❤️ and Unity**

*A passion project exploring game development through classic arcade recreation*

</div>
