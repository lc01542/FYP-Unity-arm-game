# FYP-Unity-arm-game![image](https://github.com/lc01542/FYP-Unity-arm-game/assets/169056993/e3b7e1cf-65e6-447b-821e-200549468371)
# My Unity Project

## Overview
This project is designed to run in Unity, and it contains multiple game scenes. To get started with the project, ensure you have the appropriate Unity version and follow the instructions below.

## Requirements
- **Unity Version**: 2022.3.12f1 or above
- **Unity Hub**: Version 3.8.0 or above

## Installation and Setup
1. **Download and Install Unity Hub**:
   - If you haven't already, download and install [Unity Hub](https://unity3d.com/get-unity/download).
   - Ensure your Unity Hub version is 3.8.0 or above.

2. **Install Unity Editor**:
   - Open Unity Hub.
   - Go to the "Installs" tab.
   - Click "Add" and install Unity Editor version 2022.3.12f1 or any version above it.

3. **Clone the Repository**:
   - Clone this repository to your local machine using the following command:
     ```sh
     git clone https://github.com/lc01542/FYP-Unity-arm-game.git
     ```
   - Alternatively, you can download the ZIP file and extract it to your preferred location.

4. **Add the Project to Unity Hub**:
   - Open Unity Hub.
   - Click the "Projects" tab.
   - Click the "Add" button.
   - Navigate to the cloned repository folder and select the `FINAL` folder.

## Running the Project
1. **Open the Project**:
   - In Unity Hub, under the "Projects" tab, find the newly added project and click to open it.

2. **Selecting a Scene**:
   - Once the project is open in Unity, go to the `Project` window.
   - Navigate to the `Scenes` folder.
   - Double-click on the scene file you want to run. This will open the scene in the editor.

3. **Play the Scene**:
   - With the desired scene open, click the "Play" button at the top of the Unity editor to run the scene.

## Key Bindings for Joint Rotation
Each joint in the project is associated with two key binds: one for positive rotation and one for negative rotation. To change the key binds:

1. **Select the ARM GameObject**:
   - In the Hierarchy window, find and select the ARM GameObject for which you want to change the key binds.

2. **Change Key Binds in the Inspector**:
   - With the ARM GameObject selected, go to the Inspector window.
   - Locate the `ArticulationJointController` script component.
   - Within the `ArticulationJointController` script, you will find options to change the key binds for positive and negative rotation.

