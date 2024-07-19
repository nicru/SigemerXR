# SigemerXR

## Project Description

SigemerXR is an Extended Reality app designed for prototyping emergency escape routes. It is aimed at security specialists who design and mandate the building of these routes.

## Features

- Placement, rotation, and scaling of a set selection of ad-hoc escape route 3D-models.
- Ability to save and load sessions.
- Ability to take and send a screenshot of placed models.

## Installation

1. Clone the project repository on your machine.
2. Add it as a valid Unity project on Unity Hub.
3. This project was developed using Unity 2022.3.34f1 with Android Build Support. If you use a different version, compatibility issues might arise. These can be resolved, but it is not advised if an easy setup is desired.
4. The rest of the dependencies should be downloaded by Unity Package Manager. 
> **⚠️ Remember:** Go to `Assets/Scenes` and click on `SampleScene` in order to import the scene as it was intended to work on the original build. Otherwise the scene will be empty and when trying to generate a build, it will not work.
5. Generate a running build from Unity build settings tab.
6. Download to an Android device that supports Google's ARCore. [List of supported devices](https://developers.google.com/ar/devices).
7. Execute, install ARCore, give permission for the app to use your camera, and you're ready.

## Usage

### 1. Planning Escape Routes in a New Metro Station

SigemerXR can be instrumental in planning escape routes for a new metro station. With such an environment’s complexity and high foot traffic, traditional prototyping methods could be time-consuming and costly. Using SigemerXR, planners can create and position digital 3D models of escape signs throughout the station. This allows rapid testing and iteration of various escape route configurations, ensuring that all areas of the station are covered effectively. The tool’s ability to visualize these routes in real time helps identify potential bottlenecks and optimize the placement of signs for maximum visibility and efficiency.

### 2. Updating Emergency Escape Protocols

Organizations often need to update their emergency escape protocols to comply with new safety regulations or improve existing plans. SigemerXR provides a flexible platform for quickly reevaluating and redesigning these protocols. Safety officers can use the tool to simulate different emergency scenarios and test new escape routes without the need for physical signs. This not only saves time and resources but also allows for comprehensive testing and refinement. The ability to easily modify and update digital models ensures that the most effective escape strategies are implemented, enhancing overall safety.

### 3. Polling Users to Improve Visibility of Emergency Signs

To ensure that emergency signs are placed effectively for maximum visibility, it is important to collect feedback from actual users. SigemerXR can facilitate this by allowing users to interact with digital prototypes of escape routes and signs. By conducting user polls and collecting data on the placement of signs that are most visible and intuitive, organizations can make informed decisions about their emergency signage. This user-centered approach ensures that escape routes are optimized based on real-world feedback, leading to improved safety and compliance with visibility standards.

## Configuration

The project should be ready to open in Unity as it is. However, you can customize the 3D models displayed by adding, removing, or editing the scriptable objects tracked by the data manager. Below are the steps for configuring the project:

### Scriptable Objects

1. To create a new scriptable object, navigate to `Assets` > `Create` > `Item`.
2. Each scriptable object should contain the following fields:
   - **Name**: The name of the item.
   - **Description**: A brief description of the item.
   - **2D Sprite**: An icon representing the item.
   - **3D Model**: The associated 3D prefab.
3. Place the scriptable objects in the `ScriptableObjects` folder within the `Assets` directory to maintain organization.

### 3D Models

1. Ensure that your 3D models are in a format supported by Unity.
2. Follow the SnakeCase naming convention for 3D model files to ensure they are descriptive and easy to locate when adding to scriptable objects.
3. Add your 3D models to the `3DModels` folder within the `Assets` directory.
4. Adjust the size of the 3D models according to the Unity cube to maintain consistency.
5. Convert the 3D models into prefabs:
   - Add the `Item` tag to the prefabs to ensure they are tracked by the raytracing function.
   - Attach a box collider to the prefabs to ensure precise touch input.
6. Place the prefabs in the `Prefabs` folder within the `Assets` directory.

### Data Manager

1. The `DataManager` is both a script and an object placed in the scene.
2. To configure the `DataManager`, select the object in the scene and view its configuration in the Inspector window.
3. In the configuration tab, specify which scriptable objects are being tracked by dragging and dropping the scriptable objects into the appropriate field.
4. For a large number of models, you can also configure the `DataManager` through scripting to automate the addition process.

Feel free to reach out if you need more detailed instructions or encounter any issues during the configuration process.

## Contributing

The project is being showcased for a short academic article, so the main purpose of this repo is not exactly receiving “open-source” contributions. However, if someone is interested in contributing, you can contact me by starting an issue on the project.

## License

This project is licensed under the GNU General Public License v3.0. See the [LICENSE](./LICENSE) file for details.

## Credits

María Luisa Claren, Nicolás Cruzat, Santiago Rodríguez  
School of Design, Pontificia Universidad Católica de Chile  

Alison Fernández Blanco, Juan Pablo Sandoval Alcocer, Leonel Merino  
School of Engineering, Pontificia Universidad Católica de Chile

## Contact

For any inquiries or feedback, please start an issue on this project repository.

## Badges

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)  
This build is up to date and fully functioning as described on the academic paper it was reported.

