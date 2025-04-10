## Parallel Reality (game version 1.00.1)
This tool is a mod loader for Reality Break.

Version: **1.0 (2025.04.10)**


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
- `Name: [Mod name here]` (default to using Parent Folder's name)
- `Author: [Author name here]`
- `Mod Version: [Mod version here]`
- `Game Version: [Game version here]`

Currently all of the above are optional (values default to empty string, with exception of Mod name).

-----
### Acknowledgement
- Thank Courtney for the game and for sharing its internal working, as always.
- Thank Belberith for pushing the modding effort, which give me the reason to make this tool.
