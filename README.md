# FISHBOT

Fishbot is a 2D side-scrolling platform game that seeks to address issues surrounding environmental pollution, particulatly water pollution, though original gameplay with dramatic elements. Players control an initially healthy fish in pursuit of her school that abandoned her, only to realize that they had evolved into mechanical fish to adapt to their now noxious, changing environment. By advancing through progressively darkening levels in which players have to avoid falling obstacles from the wold above, we seek to evoke sensitivity in players to the negative impacts of environmental pollution.  

# Obtaining the source  
The source for this project can be downloaded as follows:

1. On the command line, input the following command:
```
git clone git@github.swarthmore.edu:cs71-s19/project-ilomeli1-rmuniu1-yqaddur1.git  
```
> Note that this requires sharing access to the repository.  


Within our overarching project folder (project-ilomeli1-rmuniu1-yqaddur1), our project is in two parts:  

## *FishBot* 
This contains the Unity application and its dependencies (Assets, Scripts, Resources, etc.). This folder can be opened up/loaded in Unity.

## *FishBotClasses*
This directory contains our object classes and unit tests. There are two important sub-folders:  
#### FishBot-App
This directory contains our main code for the following classes:  
* Option
* SaveIO
* User
* UserManager
#### FishBot-Test
This directory contains unit tests for the aforementioned classes, as well as subdirectories containing test examples for various test scenarios and cases. There are 82 unit tests in total, and testing strategies are documented in the code itself as comments.

# How to build
> Note: There are two buildable parts; one in *FishBotClasses* and one in *FishBot*. Both require applications to build them, ie. Visual Studio Code and Unity Engine. Our project is developed in (and thus best compatible with) Unity 2018.3.0f2.  
> They can be downloaded here:  
> * https://code.visualstudio.com/  
> * https://unity3d.com/get-unity/download/archive  

> See further build instructions below:

## *FishBotClasses*
The folder FishBotClasses can be open in Visual Studio Code, where a user can find solutions for *FishBot-App* and *FishBot-Test* in the Solution Explorer. Each of these solutions can be right-clicked to build. One can right-click *FishBot-App* and select "Run" to run the main application, or right-click *FishBot-Test* and select "Test" to run the unit tests.  

#### A few things to note:  
* FishBot-App  
  1. The main executable of this solution has no code in it, as it serves very little purpose. It would be better to run *FishBot-Test* (instructions above), which puts our Classes through rigorous testing.  
* FishBot-Test  
  2. This is where the money is. We have 82 unit tests spread across four files that test each of the four classes; however, the current build only allows users to run 70 out of these tests, as 12 of these tests were originally machine-specific. This is because the sub-directories containing the test files had their folder paths hard-coded into the path string variable in each of these tests. However, users are free to modify the code to specify the folder paths to their test files on their individual computers.  
  The following are the skipped tests:
    * *SaveFileDoesNotExist()*  
    * *SaveFileEmpty()*  
    * *SaveFileNonEmpty()*  
    * *SaveFileReadOnly()*  
    * *SaveAllUsersNothingToSave()*  
    * *SaveAllUsersSaveToTestManagerNonEmpty()*  
    * *ReadFileWellFormatted()*  
    * *ReadFileUserNameIllFormatted()*  
    * *ReadAllUsersNothingToRead()*  
    * *ReadAllUsersTestManagerReadFolder()*  
    * *ReadFileSomeOptionIsIllFormatted()*  
   
 ## *FishBot*
 This is the Unity Application. After launching Unity and opening the folder "FishBot" this application can be built/ran in two ways:  
 1. In Unity's engine, you can hit the "Play" button. Preferred aspect ratio is 16:9.
 2. Alternatively, in Unity, you can click on "File", then "Build Settings". Select your your platform of choice (win/mac/linux/xbox/etc etc), then click on "Build".

