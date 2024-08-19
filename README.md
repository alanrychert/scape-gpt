# Scape-GPT

## Overview

Scape-GPT is my final project for the completion of a Bachelor's degree in Software Engineering at Universidad Nacional del Sur. The primary objective of this project was to assess whether ChatGPT could solve, or assist a user in solving, the types of problems typically encountered in an escape room. These problems often require a combination of environmental awareness and logical reasoning to identify patterns that lead to solutions.

The project also explores the integration of low-cost Virtual Reality (VR) with ChatGPT, enabling cooperation between the user and the AI assistant through voice commands. The project was developed using C# in the Unity 3D platform, with Google Cardboard used for VR implementation.

For voice-based communication between the user and the AI assistant, the project makes use of the "Speech And Text in Unity iOS and Unity Android" package, developed by khanhuitse05. Additionally, a Bluetooth controller with a joystick and at least 1 button is required for the player to move and interact with objects within the virtual environment.

## Features

-  ChatGPT Integration: Implements ChatGPT as an in-game assistant to help solve escape room challenges.
-  Voice Commands: Allows players to interact with the AI assistant using voice input.
-  Virtual Reality: Utilizes Google Cardboard to create an immersive low cost VR experience.
-  Logical Problem Solving: Focuses on problems that require pattern recognition and logical reasoning, typical of escape room scenarios.

## Installation and Setup

Clone the repository:
-  git clone https://github.com/yourusername/scape-gpt.git
-  Open the project in Unity 3D.
-  In the ChatGPT.cs file, located at Assets/Scripts/ChatGPT.cs, replace YOUR_API_KEY at line 18 with your actual OpenAI API key
-    private string apiKey = "YOUR_API_KEY";
-  Build and run the project on a compatible Android or iOS device with Google Cardboard.

## Dependencies

Unity 3D: Version 2020.3 or later.
Google Cardboard SDK: For VR support.
Speech And Text in Unity iOS and Unity Android: For voice interaction.

## Author
Name: Alan Rychert
University: Universidad Nacional del Sur
Year: 2024

## Acknowledgments
Special thanks to my proffessors Dr. Matias Selzer and Dr. María Lujan Ganuza, and to khanhuitse05 for the development of the "Speech And Text in Unity iOS and Unity Android" package used in this project.
