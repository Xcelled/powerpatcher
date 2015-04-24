Power Patcher
============

Thank you for using Power Patcher! This document will guide you through the installation and use of Power Patcher.

## Setup/Removal

### Install
1. Open your Mabinogi directory, which is located at different places depending on how you installed the game:
  - The most common place is `C:\Nexon\Mabinogi`
  - If you used Steam, it'll be in your [steamapps](https://support.steampowered.com/kb_article.php?ref=7418-YUBN-8129) folder.
  - If you used the new Nexon launcher, it'll be `C:\Nexon\Library\mabinogi\appdata` 
  
  If you see a file called `client`, you're in the right place.
2. Rename Mabinogi.exe to Mabinogi.exe.ppbak
3. Copy the files from the ZIP folder into your Mabinogi directory

### Uninstallation of Power Patcher
1. Delete Mabinogi.exe
2. Rename Mabinogi.exe.ppbak to Mabinogi.exe

### Uninstallation of Mabinogi
1. Uninstall Power Patcher using the steps above
2. Uninstall Mabinogi like you normally would


## Usage
- **NOTE**: Power Patcher records information in `powerpatcher.log`. Reading the log can be helpful for debugging, **determining why a patch failed**, or just to see what the program is doing
- **NOTE 2**: If you can't patch and you see a bunch of `Cannot access the path blah: Access is denied` errors in the log, do all of the following (Google if you don't know how): 
    - Make sure the `Administrators` group has ownership of the Mabinogi folder **AND** all subdirectories/files
    - Make sure the `Administrators` group has `Full Control` over the Mabinogi folder **AND** all subdirectories/files
    - Make sure `Read only` is **NOT** enabled for the Mabinogi folder **AND** all subdirectories/files

### Basic Usage
1. Start Mabinogi like you normally do

### Advanced Features
#### Write custom version number
- Does exactly what it says: writes the number in the box to `version.dat` in the current directory

#### Patch to version
- Does exactly what it says: patches up to the version specified in the box

#### Redownload current version in full
- Re-patches with the full packs for the current version. Semantically the same as writing a version number of 0 and patching to the current version
- _You will be asked if you would like to clean the package folder. Choosing `yes` will remove any existing .pack files, saving space, but potentially **removing** mods you may have_

#### Redownload language pack
- Redownload the language pack for the current version and overwrite the one in package.
- _This will fail on any Korean versions, as there is no language pack for Korea_

### Settings
I tried hard to make Power Patcher configurable yet user friendly. Feedback is appreciated.

#### Patch Clean Up Options
These options relate to how Power Patcher handles temporary files generated in the patching process.

##### Delete part patch files
- Controls deletion of the xxx_to_yyy.zzz files
- Default is checked, but unless you are worried about space, the recommendation is to **UNCHECK** this
- Unchecking it makes repatching faster in the event of an error because the patcher will not need to redownload the whole patch again

##### Delete generated zip files
- Controls deletion of xxx_to_yyy.zip and language.zip files
- Default is checked, uncheck it if you want the raw zips

##### Delete temporary content folder
- Controls deletion of the temporary content folder
- If you want to make/upload manual patches, **UNCHECK** this and rar/zip/tar the patch_name\content directory. Poof, you have your manual patch

#### General Patcher Options
##### Require administrator rights
- If checked, Power Patcher will check for and require elevated permissions to run
- This is usually needed, as the Mabinogi directory is only readable by the Administrators group

##### Close after starting game
- If checked, Power Patcher will auto-close after the `Start Game` button is pressed

##### Mabinogi Version
- The patch server to use. Should match your installed client version
- _Changing this setting immediately refreshes the patcher's internal information and may take **several seconds** to complete_

##### Warn if not in Mabinogi folder
- Power Patcher should be in the Mabinogi folder to run properly. This option disables the warning message if it's not.

##### Startup page url
- The url to load in the "startup" tab
- Can be anything you want
- To turn off the startup tab, simply enter `about:blank` here

#### Client Start Options
##### Custom client arguments
- If you want to connect to a private server, this option is for you. Paste the arg string the server admin gave you into this box
- Leaving this field blank automatically generates the proper args for the currently selected version
- **IT IS NOT REQUIRED TO HAVE ANYTHING IN THIS BOX IF YOU'RE CONNECTING TO AN OFFICIAL SERVER**

##### Pre- and Post- start commands 
- These textboxes contain **SHELL** commands to execute before and after starting `client.exe`
- Internally, each line is passed to the shell like this:
   `cmd.exe /c YOUR_COMMAND`
   - This means you can use shell commands like `dir` in addition to batch files and running executables
- **POWER PATCHER WILL WAIT FOR THE COMMAND TO EXIT BEFORE CONTINUING**
   - If you have a non-terminating program, like `crackshield`, use the `start` command. So your line would look like this:
    `start crackshield.exe`
      - This will return immediately, allowing the patcher to continue starting the client

#### Command Line Options
##### /?
- Displays usage

##### /noadmin
- Disables the "Require administrator right" setting, allowing Power Patcher to open on a computer where you do not have administrator rights
