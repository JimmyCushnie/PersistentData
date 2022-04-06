# PersistentData
Easy cross-platform data saving and loading for Unity

### The Problem

I like to make [games](https://itch.io/c/331409/game-jam-games) that work both on desktop and in WebGL.

My games usually need to save data of some kind, like user settings or highscores. On desktop, I like to use [SUCC](https://github.com/JimmyCushnie/SUCC), because it generates files in a convenient location and formatted in a beautiful way.

However, SUCC doesn't work in WebGL, so my cross-platform games can't use it.

### The Solution

PersistentData is a clean API for saving c# data. On desktop platforms, the data is saved as [SUCC](https://github.com/JimmyCushnie/SUCC). On all other platforms, the data is serialized with [Newtonsoft.Json](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.0/manual/index.html), then stored in [PlayerPrefs](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html).

This means the desktop versions of your apps will have beautiful convenient SUCC data files, but your apps will still work perfectly on other platforms without you needing to write any platform-dependent code.

## Installing

1. Install [SUCC](https://github.com/JimmyCushnie/SUCC) via the package manager (Add package from git URL -> `https://github.com/JimmyCushnie/SUCC.git#unity`)
2. Install [Newtonsoft.Json](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.0/manual/index.html) via the package manager (Add package by name -> `com.unity.nuget.newtonsoft-json`)
3. Install PersistentData via the package manager (Add package from git URL -> `https://github.com/JimmyCushnie/PersistentData.git`)

## Usage

All of the functions from this library are in the `PersistentData.GameValues` class. They all have xml documentation.

PersistentData's API is very similar to [SUCC's DataFile API](https://github.com/JimmyCushnie/SUCC/wiki/Getting-Started#get-and-set-values-in-the-file).

### Example

```csharp
using PersistentData;
...

const string KEY_HIGHSCORE = "highscore";

void SaveHighscore(int score) 
    => GameValues.Set(KEY_HIGHSCORE, score);

int LoadHighscore() 
    => GameValues.Get(KEY_HIGHSCORE, defaultValue: 0);
```

### Notes on complex type serialization

When you're serializing a [Complex Type](https://github.com/JimmyCushnie/SUCC/wiki/Complex-Types), both the desktop and non-desktop serializers use attributes to determine what to serialize.

Desktop (SUCC):

* public fields and properties are serialized unless marked with the `[SUCC.DontSaveThis]` attribute
* private fields and properties are serialized if and only if marked with the `[SUCC.SaveThis]` attribute

WebGL/other (Newtonsoft.Json):

* public fields and properties are serialized unless marked with the `[Newtonsoft.Json.JsonIgnore]` attribute
* private fields and properties are serialized if and only if marked with the `[Newtonsoft.Json.JsonProperty]` attribute

In order to keep serialization consistent between platforms, it is recommended that you only save complex types without any of the above attributes on any members.
