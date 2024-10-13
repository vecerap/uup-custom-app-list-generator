# Description

Generates CustomAppsList.txt with minimal Win Store applications required to run Windows properly. Basically the stripped down version without bloatware apps. The script is for [UUP dump](https://uupdump.net/) for Windows ISO creation and customization.

The reason of this script is to prevent invalid package names in case of package ID after the package name changes from time to time.

Script also enables custom apps list during uup dump generation (it will alter the config file)

# Usage

Simply copy generate-uup-custom-app-list.ps1 inside the unzipped UUP zip folder for ISO creation in the same directory as CustomApplsList.txt file is located, open powershell terminal and run the script. **Script relies on correct working directory. You need to run the script with working directory same as the folder it is located, otherwise it will not work**

# Customization

Inside script, there is an array of names of the apps, these apps will be included in final generated file, other programs will be commented out. In case of exclusion or addition of some apps, simply change the array with programs. Beware of format with dots "." as they need to be escaped (\\\.), because script uses regex for filtering.
