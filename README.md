
# Button to Shirt Sorting

A simple yet engaging mobile game where players sort colorful buttons into matching shirt slots.

## Game Overview

Button to Shirt Sorting is a casual puzzle game developed in Unity. The core game-play involves dragging and dropping buttons onto their matching colored shirt slots before time runs out. The game features:

- Multiple difficulty levels with increasing numbers of buttons
- Randomized button and slot placement
- Time-based challenge
- Hint system to help when you're stuck
- Auto-place feature (mock rewarded ad)

## Technical Details

### Unity Version
- Developed with Unity 2021 LTS

### Setup Instructions
1. Clone this repository
2. Open the project in Unity (2021 LTS or later recommended)
3. Open the main scene in `Assets/_Game/Scenes`
4. Press Play to test the game in the editor

### Controls
- **Drag & Drop**: Touch and drag buttons to place them in matching slots
- **Hint Button**: Highlights a correct button-slot match if you take too long
- **Auto-Place Button**: Automatically places one button (mock rewarded ad feature)

## Implementation Details

### Core Features
- **Randomized Button Generation**: Buttons with different colors are randomly placed each level
- **Color Matching Mechanic**: Buttons must be placed in slots of the same color
- **Time Challenge**: Complete the level before time runs out
- **Level Progression**: Increasing difficulty with more buttons in higher levels

### Technical Highlights
- **ScriptableObjects**: Used for button and level data management
- **Object Pooling**: Implemented to optimize performance, especially on mobile
- **Data-Driven Design**: Game parameters are configurable through scriptable objects
- **Modular Code Structure**: Clean separation between game logic and UI
- **Mobile Optimization**: Targeted for 60 FPS performance on mobile devices

## Additional Features
- **Hint System**: After 10 seconds of inactivity, a matching button-slot pair is highlighted
- **Auto-Place Feature**: A button that automatically places one button in its correct slot
- **Level Creation System**: Support for both predefined and randomly generated levels
- **Visual Feedback**: Particles and effects for successful button placement

## Known Issues
- None reported at this time

## Future Improvements
- Additional button designs and colors
- More complex level layouts
- Score and star rating system
- Sound effects and music
- Player progression and save system

## Credits
- Developed as part of a Unity Developer Technical Test
- Built with Unity 2021 LTS
