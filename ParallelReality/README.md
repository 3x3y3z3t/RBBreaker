## Parallel Reality (game version 1.00.1)
This tool is a mod loader for Reality Break.

Version: **1.2.1 (2025.04.13)**

Release (compiled binary included):  
[win-x64 v1.0.1](https://github.com/3x3y3z3t/RBBreaker/releases/tag/1.0)  
[win-x64 v1.1.1](https://github.com/3x3y3z3t/RBBreaker/releases/tag/1.1)  
[win-x64 v1.2.1](https://github.com/3x3y3z3t/RBBreaker/releases/tag/1.2)  


### Usage
1. Select one mod in "Found Mods" list.
2. Click `->` button to add mod to "Selected Mods" list.
   Click `<-` button to move mod back to "Found Mods" list.
3. Repeat steps 1 and 2 to add/remove multiple mods.
4. Click `^` or `v` button to change load order. Mods will be load in the order shown in "Selected Mods" list, top-down.
5. Click `Apply Selected Mod(s)` to apply mods.

Remember to click `Restore Base Game` to restore base game data before exiting tool.

### Mod Format
(Mod format should be describe in another place, but since that place is not existed yet, I'll put it here for now.)

**Currently supported mod:** xml data mod.

```
Parent Folder (typically mod name)
|---Reality Break_Data
|   |---StreamingAssets
|       |---Config (same structure as base game)
|       |---GalaxySteamWrapper (same structure as base game)
|       |---Proto (same structure as base game)
|       
|---readme.txt (optional)
```

The tool will look for certain data in the mod's readme file. If a line with the following format is found, the correspond data will be set.
- `Name: ABC`: will set Mod Name to "ABC". Default to using Parent Folder's name.
- `Author: ABC`: will set Mod Author to "ABC". Default to empty string.
- `Authors: ABC`: plural version of `Author`, behavior is the same.
- `ModVersion: ABC`: will set Mod Version to "ABC". Default to empty string.
- `GameVersion: ABC`: will set (required) Game Version to "ABC". Tool will not check for compatible game version. Default to empty string.

-----
### Acknowledgement
- Thank Courtney for the game and for sharing its internal working, as always.
- Thank Belberith for pushing the modding effort, which give me the reason to make this tool.
