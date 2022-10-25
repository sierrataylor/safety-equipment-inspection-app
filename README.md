# Safety Equipment Inspection App

This project has been built by the John Carroll University graduating class of 2023 for the company Avery Dennison. Its purpose is perform efficient inspections of safety equipment.

## Built with:

- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Angular](https://angular.io/docs)
- [Firestore](https://firebase.google.com/support/releases)
- [Google.Cloud.Firestore 3.0](https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Firestore/latest) 

## Prerequisites

- Latest version of .NET: can be downloaded [here](https://dotnet.microsoft.com/en-us/download)
- npm: Download [here](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm) 
- TypeScript: Find instructions on how to install [here](https://www.typescriptlang.org/download)
- Google.Cloud.Firestore: a .NET library for working with FireStore documents; installed from the NuGet packages in Visual Studio. Go into Visual Studio, right click the project, choose *"Manage NuGet packages..."* and search for it.

## Getting Started

Here is a quick intro of the steps needs to get the app up and running on your desktop computer.

1. Download a zip folder containing the project, or navigate to a folder in your terminal and run this command
```
git clone https://github.com/sierrataylor/safety-equipment-inspection-app.git
```
which should clone the repository into a folder of your choice

2. Once you have installed *npm*, navigate to the folder containing the project 

3. Navigate to the folder containing the **SafetyEquipmentInspectionApp** project and you should find package.json in the root of the project. 
```
cd <folder_path>/SafetyEquipmentInspection/SafetyEquipmentInspectionApp/
```
Run 
```
npm install package.json
```
to install the necessary dependencies.
	
4. To run the app:
 - Windows: Press F5 in the Visual Studio window to start the application with debugging
 - Mac: Run the application in Visual Studio. 
 In your terminal or command prompt, navigate to the folder containing the source code for the Angular front-end application. 
 ```
 cd SafetyEquipmentInspection/SafetyEquipmentInspectionApp/ClientApp/src
 ```
 Then type this command in the terminal to start the front end before running the application in Visual Studio.
 ```
 npm start
 ```
 
 ### Configuration
To configure Visual Studio to run both the API and the front-end: 
1. Open the application in Visual Studio and right-click the solution file (SafetyEquipmentInspection.sln). 
2. Select *Select Startup Projects...* or *Properties*. 
3. In the Startup Project menu, select *Multiple startup projects* and at this point you should see both the API and Angular application projects enabled for selection. 
4. Select *Start* as the action for both and click *Apply*. 
5. Now when you run the project, two windows should open.

 ### Tests
 to be added