# CSF24

## Overview
CSF24 is a project aimed at enhancing accessibility in technology through research and development. This repository contains the codebase, documentation, and resources related to the CSF24 project.

## Branching
The last finshed project can be found in the Final Hallway Dev branch

## Features
- **Accessibility Research**: Conducting studies and experiments to improve accessibility features.
- **Development**: Building tools and applications that enhance accessibility.
- **Community Engagement**: Collaborating with the community to gather feedback and improve accessibility solutions.

## Installation

### Unity
1. **Download and Install Unity**:
   - Download the Unity Hub from the [Unity Download Page](https://unity3d.com/get-unity/download).
   - Install Unity Hub and open it.
   - In Unity Hub, install the Unity Editor version used in this project: **2022.3.47f1**.
   - Once installed, open the project through Unity Hub.

2. **Open the Project**:
   - Click the "Add" button in Unity Hub.
   - Navigate to the cloned repository directory and select it.
   - Click "Add Project".

### Wwise
1. **Download and Install Wwise**:
   - Download the Wwise launcher from the [Audiokinetic website](https://www.audiokinetic.com/downloads/).
   - Install the Wwise launcher and open it.

2. **Install Wwise**:
   - In the Wwise launcher, go to the "Wwise" tab.
   - Click "Install New Version" and select the version used in this project: **2023.1.7.8574.3182**.
   - Follow the installation steps provided by the launcher.

3. **Integrate Wwise with Unity**:
   - In the Wwise launcher, navigate to the "Unity" tab.
   - Click "Integrate Wwise into Project" and select the Unity project directory.
   - Follow the integration steps provided.

## Resources

### Unity
There are countless resources online for Unity. Here would be a great start.
- [Unity Tutorial for Beginners - YouTube](https://www.youtube.com/watch?v=X367yDYmogo)

### Wwise
The fall 2024 team utlized a Youtube resources and documentation for the project. One notable and great resource was Micheal G Wagner on youtube. Make sure you watch all of Michael's videos up to part 6 and reverb zones, if you want to continue after part 6 feel free to. Michaels series of videos will teach you the basics of Wwise. After that you can watch the video on speaker panning which will be helpful when trying to replicate realistic sounds cue's with the bone conduction headset. The video after that will walk you through effects like delay, echo, and sound that you can change which will be important when you want to add another component that trys to match a realisitc sound cue. 
- [Michael G Wagner - YouTube](https://www.youtube.com/watch?v=OchYfH0wb0U&list=PLzlEBXWjqM97U5rHMERc82sTXRBoSB_Fu)
- [Wwise-101- Lesson 4 - Understanding Balance-Fade Speaker Panning - Youtube](https://www.youtube.com/watch?v=w74ENOAoErs)
- [Wwise-101 - Lesson 5 - Working with Effects](https://www.youtube.com/watch?v=bnczDgQoyJI)

There is already panning that is being done in the FirstPersonController script along with changes to volume and reverb/echo as you get closer to obstacles in the UpdateWallDistance method. If you need to add or edit anything with regards to effects or speaker panning can be found in the UpdateWallDistance method in the FirstPersonController script. Footsteps and echolocation are also performed in the FirstPersonController script. If you want to change any collsion sounds, navigate to the Collision Test script which is on the FirstPersonController object. You assign your collision sounds in the FirstPersonController object inspector at the Collsion Test script. 

Make sure when working on Wwise with Unity that you have the Wwise project open from the Wwise launcher. This will allow the Wwise picker in your Unity project to connect to your Wwise and will get rid of any unnecessary and confusing errors, It will also make testing and debugging way easier. 


## Usage
Provide detailed instructions on how to use the project. This can include examples, code snippets, and explanations.



### Logging
**When running the project, note that files will be created on your computer.** These log files can be found under `\C:\Users\{usr}\AppData\LocalLow\Accessibility Research\EchoNav (idk)\Experiments`, or under `library\Accessibility Research\EchoNav (idk)\Experiments` for our Mac friends. </br>
It is highly recommended that you disable logging when doing testing, especially if said testing involves work with collisions or level resets. (There were 13,000 .csv's on my laptop...) This can be done by changing the `enableLogging` bool in `LogManager` to `false`.</br>

> [!NOTE]
> This is only present on the [VRIntegrationTest branch](https://github.com/AccessibilityResearch/CSF24/tree/VR-Integration-Test), you will have to comment out `TrialLogger.WriteLog();` under `FixedUpdate` in `LogManager` and `LogManager.Instance.NewTrial();` under `LoadScene` in `JSONSerializer` if the bool is not present on yours.

There are also some logging performance realated settings in the `TrialLogger` class:
- `flushInterval` Controls how often the stream writer flushes to the file. (This is based on `fixedUpdate`) | `FrameCount` is the itterator that keeps track of said intervals.
- `SBPool` is a "**S**tring **B**uilder **Pool**". While pooliong is mostly for multithreading (which we are not doing) it's good to have.
You will also notice in the constructors of `TrialLogger` there are two more *possibly important* fields:
- `_BufferSize` is the number of bytes allocated to the stream writer. The default it 8kb
- `_Encoding` believe it or not this is the kind of encoding used... Default is UTF8
  </br>

### Virtual Reality
It should be mentioned that there is a [BRANCH](https://github.com/AccessibilityResearch/CSF24/tree/VR-Integration-Test) of this project that has VR basics integrated. This implementation has only been tested via a simulator, but seems to work fine. 

## Contributing
We welcome contributions from the community! To contribute, follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes and commit them (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.


## Contact
For any questions or inquiries, please contact the project maintainer at [flint.million2@mnsu.edu](mailto:flint.million2@mnsu.edu).

## Acknowledgments
We would like to thank all contributors and supporters of this project.
