# XML Formatter
![Live build](https://github.com/XanatosX/XmlFormatter/workflows/Live%20build/badge.svg)
![Latest unstable build](https://github.com/XanatosX/XmlFormatter/workflows/Latest%20unstable%20build/badge.svg)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/e5160ad2aa1641f3af76a100c0d042f0)](https://www.codacy.com/gh/XanatosX/XmlFormatter/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=XanatosX/XmlFormatter&amp;utm_campaign=Badge_Grade)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FXanatosX%2FXmlFormatter.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FXanatosX%2FXmlFormatter?ref=badge_shield)

This project is a lightweight tool to convert xml files from a flat to a formatted format or the other way round.

## How to "install" this application

### Pre-Requirements

You will need the .NET Framework 4.7 or newer installed on your machine. This is installed on default on Windows 10.

### Installation

1. Download the newest zip file from the [Release](https://github.com/XanatosX/XmlFormatter/releases) tab.
   1. Click on the topmost entry in the list
   2. Click on the zip file in the `Assets` area
2. Unzip the content of the file somewhere on your computer
   * You do not need the `LICENSE` file.
3. Start the unzipped `XmlFormatter.exe` file.

This application is portable, just copy the zip content to a portable disk.

## How to use this application

### Select a file to format

There are different ways to select the file you want to format

#### 1. Use the select button

To "open" an file use the `Select` button, keep in mind that the text of the button is related to the current converter you do use

![SelectButton.png](/.github/Screenshots/SelectButton.png)

This will open up a file selector where you can choose the file for converting

![FileSelector.png](/.github/Screenshots/FileSelector.png)

#### 2. Write the path to the into the Textbox

You can write the path to the file into the textbox to select it for the formatting task

![AddPathToFile.png](/.github/Screenshots/AddPathToFile.png)

Please make sure that the file is a valid file and is located at the path you did provide in the textbox.

#### 3. Drag the file onto the window

The last option is to simple drag and drop the file into the window, please keep in mind that you can only drag one file for converting.

This will automatically fill the textbox with the correct file path.

### Select the converter

You can select different converter if there are multiple plugins installed into the application.

**Important** If this selection is not visible for you there is only one converter available

![ConverterSelection.png](/.github/Screenshots/ConverterSelection.png)

Changing this setting will change different texts in the GUI. It will change some filters like which files you can select in the background as well.
Changing this selectbox will change the filetype you can convert.

![JsonExample.png](/.github/Screenshots/JsonExample.png)

### Select the formatting mode

There are two different modes to format one to flat out the file and one for proper formatting. You can select the mode on the dropdown near the save button.

![ModeSelection.png](/.github/Screenshots/ModeSelection.png)

* Formatted -> Format the file in a human readable format
* Flat -> Flat the file so it will need less space on your disk

### Save the file

To finally save the file just click on the `Save` button. This will load the file into your ram convert it and write it back to the disk. While this action is in progress the window will be locked for other interactions.

### Change the settings

You can change the settings of this application by clicking on `File->Settings`

![OpenSettings.png](/.github/Screenshots/OpenSettings.png)

Do not forget to click on `Save and close` to apply your settings.

### Hotfolder Support

**Important** This functionality is not tested deeply so it is possible that the application will crash or bug out.

The hotfolder support will, if activated, run in the background while the application is started and will try to watch all the configured folders and convert the files as configured.

To use this functionality go to the settings and access the hotfolder tab, check the `Hotfolder active` flag first.

![ActivateHotfolder.png](/.github/Screenshots/ActivateHotfolder.png)

Click on the `Add` button to add a new hotfolder. This will bring up a new window, where you need to select the `provider` (Converter to use) and the `mode` if the files should be converted to flat or formatted.

![GeneralHotfolderSettings.png](/.github/Screenshots/GeneralHotfolderSettings.png)

After adding this information you should add the folder to watch. Keep in mind that the folder should be readable. Best practice is to set a Filter here in case of the xml formatter the filter should be `*.xml`. The output folder is filled automatically but can be changed if needed. It is not possible to select the same output folder as the input folder.

The output file scheme will define how the file will be named after the conversion. At the moment there are the following variables

* `{inputfile}` -> Name of the input file without extension and path.
* `{format}` -> The format (Formatted, Flat) the file was converted to.
* `{extension}` -> The extension of the converter.

You can check the additional triggers if you want that the input file is deleted or if the process should be triggered on file rename as well.

## Plugins

**Important** Using plugins of any kind can cause harm to your computer be absolutely sure that the plugin author is trustworthy.

### Install or Remove a Plugin

To install or remove a plugin close the application first. After closing the application navigate to the installation folder.

![InstallationFolder.png](/.github/Screenshots/InstallationFolder.png)

The folder should look like this, you need to open the `Plugins` folder. You can add plugins for this application by dropping them into the folder, don't forget to put in the required dll's as well. To Remove a plugin just delete the dll file and there requirements.

![PluginFolder.png](/.github/Screenshots/PluginFolder.png)

Do not delete the `CorePlugin.dll` if you do not know what you are doing. The application will still start but if there is no replacement it will not function properly.

### View Plugins and Manage there Settings

If you want to take a look which plugins are loaded right now open the plugin manager by clicking on `Help->Plugins`

![OpenPluginManager.png](/.github/Screenshots/OpenPluginManager.png)

If you extend the tree view to the left you will see two root nodes

* Formatter -> Entries listed under this node are converter for files
* Updater -> Entries listed under this node are updater, those are strategies you can use to update the application.

You can select a plugin in this view to see the details of it, if there are settings available for the plugin there will be an additional tab to the right.

![PluginSelected.png](/.github/Screenshots/PluginSelected.png)

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
