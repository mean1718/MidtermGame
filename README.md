# Coin Ball
 
A 3D physics obstacle-course game made in Unity 6 for the Game Development mid-term group project (Cambodia Academy of Digital Technology, Department of Computer Sciences).
 
Roll the ball across the platforms, avoid the hazards, collect all the coins and reach the green finish line before you lose your 3 lives.
 
**Group:** <GROUP2>   (CADT)
 
## Links
 
| Item | Link |
|---|---|
| Git repository | <https://github.com/mean1718/MidtermGame.git> |
| Gameplay video | <https://youtu.be/NMSyqJmwtug?feature=shared> |

## Objective

Guide the ball from the start platform to the green **Finish Line**. Collect all 8 gold coins on the way, avoid the hazards, and do not run out of lives. Finish as fast as you can to get a better score and a better star rating.

## How to play

1. Click **PLAY** on the main menu.
2. Move the ball with **W A S D** or the **arrow keys**.
3. Press **Space** to jump over obstacles and across gaps.
4. Roll through the **gold coins** to collect them (10 points each).
5. Avoid the red blocks and the spinning bar. They knock the ball back.
6. Use the **bounce pad** to launch yourself over the gap.
7. Touch the **green checkpoint** to save your place. If you fall, you restart from there.
8. Reach the **green Finish Line** to win.

## Rules

- You have **3 lives**. Falling off the course costs 1 life.
- After a fall you respawn at the last checkpoint you touched (or at the start).
- If you lose all 3 lives, the game is over. Press **R** to try again.
- You can press **R** at any time to restart, and **Esc** to pause.

## Score and rating

| Item | Points / reward |
|---|---|
| Each coin (8 in total) | +10 points (maximum 80) |
| Time bonus | Extra points for finishing quickly |
| 1 star | Reach the Finish Line |
| 2 stars | Finish and collect all 8 coins |
| 3 stars | Finish, collect all 8 coins and lose no lives |

Your best time is saved between plays.
## Controls
 
| Input | Action |
|---|---|
| W A S D / Arrow keys | Move |
| Space | Jump |
| R | Restart |
| Esc / P | Pause |
 
## Requirements
 
- **Unity Editor 6000.3.8f1** (Unity 6.3 LTS), with the Universal Render Pipeline and Input System
- Git or GitHub Desktop (Git LFS is not needed)
Use the exact Unity version. A different version can upgrade the project files and cause errors.
 
## Open and run the project
 
1. Clone the repository: `git clone <https://github.com/mean1718/MidtermGame.git>`
2. In Unity Hub choose **Add > Add project from disk** and select the `MidtermGame` folder (the one containing `Assets`, `Packages` and `ProjectSettings`).
3. Open the project and wait for the first import to finish.
4. Open `Assets/Scenes/MainMenu.unity` and press **Play**.
    
## Project structure
 
```
MidtermGame/
├── Assets/
│   ├── Audio/       Music and sound effects
│   ├── Materials/   Colour materials and sky
│   ├── Particles/   Particle effects
│   ├── Prefabs/     Prefabs (coin sparkle)
│   ├── Scenes/      MainMenu.unity, MainLevel.unity
│   └── Scripts/     All C# scripts
├── Packages/
├── ProjectSettings/
└── README.md
```
 
## How it works
 
| Script | Purpose |
|---|---|
| GameManager | Score, lives, timer, checkpoints, respawn, win, game over, pause, restart |
| BallController | Rigidbody movement, jump, ground check |
| CameraFollow | Follow camera with shake and speed-based field of view |
| Coin, Checkpoint, FallDetector, FinishLine | Trigger colliders for collecting, saving, falling and winning |
| Hazard, BouncePad | Collision events: knock-back and launch |
| MovingObstacle, RotatingObstacle | Moving and spinning obstacles |
| AudioManager, MainMenu | Sound and music, menu buttons |


## Credits
 
| Item | Source | 
|---|---|
| Sound effects | <https://pixabay.com/> | 
 
## Team members and contributions
- Lenh Sokmean
- Sou Pin
- Ngin Sinbe
- Chea Meiya
  
- We are year 3 students at CADT
## Lecturer
- Va Hongly
  

