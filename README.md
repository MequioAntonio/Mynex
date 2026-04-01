# Mynex
2D mobile puzzle game featuring dynamic dialogue generation using a Large Language Model (OpenRouter API).

## Features
• AI-generated dialogue creating unique puzzles  
• Dynamic gameplay (different every run)  
• Player interaction and UI system  
• Designed UI and gameplay for mobile devices

## Tech Stack
• Unity (C#)
• OpenRouter API (LLM integration)
• NavMesh
• Tilemap

## Demo
[https://youtube.com/tuo-video](https://youtu.be/tMHh-KGls5g)

### Demo explanation
The game starts in the main menu, where the player can select a difficulty level and begin the game.

The player controls Nex, a character searching for his master, Orwin. After an initial scripted sequence, the player gains control using a mobile-style joystick, along with UI elements such as a health bar, a healing potion button, and an attack button.

During the game, the player must defeat enemies while progressing through the environment and protecting Orwin from waves of attackers.

The core gameplay takes place in a dungeon, where the player must solve three different puzzles to obtain magical elixirs and win the game.

Each puzzle is dynamically generated using a Large Language Model (LLM) with Few-Shot Prompting, ensuring varied gameplay in each session.

### Puzzle examples
- Defeat four bosses in the correct color order  
- Place totems in the correct sequence  
- Interact with environmental elements (e.g., sound-based puzzles) in the right order  

Making a mistake in puzzle-solving results in failure.

### Difficulty system
The selected difficulty affects the checkpoint system:
- Easy: retry the puzzle immediately  
- Medium: restart from the puzzle hub  
- Hard: restart the entire game  

The game ends when the player successfully completes all puzzles and collects all elixirs.
