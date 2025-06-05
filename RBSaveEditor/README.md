## RB Save Editor (game version 1.00.2.3)
This tool is a save editor for Reality Break.  
Current version is still a proof-of-concept so its functionality is limited, only a few values can be read and write (details below).

Version: **0.3.0 (2025.06.05)**

[Releases (win-x64 compiled binary included)](https://github.com/3x3y3z3t/RBBreaker/releases)

### Usage
0. RBSE will automatically load Metagame save file. You can click "Load Metagame" to reload manually.
1. Click "Select Pilot File" to select and load a pilot save file.
2. Values with **blue** label are modifiable. Inventory and item details is also modifiable.
3. Click "Save Pilot" to create a backup save file and save your modification.

### Feature as of v0.3
- Load and save Metagame and Pilot save file up to game version 1.00.2.3.
- Edit Talent Points (Reailty Stream).
- Edit Talents Level (Reality Stream).
- Edit Credits and Fate Points.
- Edit Pilot Level and XP.
- Edit Item details. The item should be placed in Station 6 Storage.
- Export and Import item. Note that Importing an item will overwrite the currently selected item, so prepare a trash item (e.g Iron) to be overwritten.

-----
### Acknowledgement
- Thank Courtney for the game and for sharing its internal working, as always.
- Thank Belberith for create the save file structure spreadsheet, which gave me initial lead on where various data are.
