# XML Formatter
[![Build status](https://ci.appveyor.com/api/projects/status/od60o55s004tdjnn/branch/master?svg=true)](https://ci.appveyor.com/project/XanatosX/xmlformatter/branch/master)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/d82f0208f13547ff890deff9207bb4c4)](https://www.codacy.com/manual/simonaberle/XmlFormatter?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=XanatosX/XmlFormatter&amp;utm_campaign=Badge_Grade)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FXanatosX%2FXmlFormatter.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FXanatosX%2FXmlFormatter?ref=badge_shield)


This project is a lightweight tool to convert xml files from a flat to a formatted format or the other way round.


## How to use this application

### Select a file to format

There are different ways to select the file you want to format

#### 1. Use the select button

To "open" an xml file use the `Select XML`



![SelectButton.png](/.github/Screenshots/SelectButton.png)



This will open up a file selector where you can choose the file for converting





![FileSelector.png](/.github/Screenshots/FileSelector.png)



#### 2. Write the path to the into the Textbox

You can write the path to the file into the textbox to select it for the formatting task



![AddPathToFile.png](/.github/Screenshots/AddPathToFile.png)



Please make sure that the file is a valid xml file and is located at the path you did provide in the textbox



#### 3. Drag the file onto the window

The last option is to simple drag and drop the file into the window, please keep in mind that you can only drag one file for converting.

This will automatically fill the textbox with the correct file path.

### Select the formatting mode

There are two different modes to format one to flat out the file and one for proper formatting. You can select the mode on the dropdown near the save button.

![ModeSelection.png](/.github/Screenshots/ModeSelection.png)



* Formatted -> Format the file in a human readable format
* Flat -> Flat the file so it will need less space on your disk

### Save the file

To finally save the file just click on the `Save` button. This will load the file into your ram convert it and write it back to the disk. While this action is in progress the window will be locked for other interactions.

## Pre-requirements for creating a build

To get started with the project you should check the following requirements

1. You will need a program which allows you to write C# and can compile and build Windows forms applications.
   * Check out [Visual Studio Community](https://visualstudio.microsoft.com/de/vs/community/)

## Getting a working build

You should fulfill the pre-requirements for these steps first.

1. Clone this project `git clone git@github.com:XanatosX/XmlFormatter.git`
2. Download the NuGet dependencies before compiling. If you IDE is not doing this on loading the project file use `nuget restore` in the root folder
3. Start the build in your IDE


## Contributions

If you have found a bug or a feature idea feel free to create an issue [here](https://github.com/XanatosX/XmlFormatter/issues). Before you open up a new issue, please search to see if it was already reported. In your issue try to be as detailed as possible.

If you want to contribute fixes or features to this project, please read our [contributors guide](CONTRIBUTING.MD) first.


## Using the Source Code

The full source code and all the required assets are available here on GitHub.

## License

This project is using the [MIT License](LICENSE).

[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FXanatosX%2FXmlFormatter.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FXanatosX%2FXmlFormatter?ref=badge_large)




