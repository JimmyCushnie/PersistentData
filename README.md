# PersistentData
Easy cross-platform data saving and loading for Unity

### The Problem

I like to make [games](https://itch.io/c/331409/game-jam-games) that work both on desktop and in WebGL.

My games usually need to save data of some kind, like user settings or highscores. On desktop, I like to use [SUCC](https://github.com/JimmyCushnie/SUCC), because it generates files in a convenient location and formatted in a beautiful way.

However, SUCC doesn't work in WebGL, so my cross-platform games can't use it.

### The Solution

PersistentData is a clean API for saving c# data. On desktop platforms, the data is saved as [SUCC](https://github.com/JimmyCushnie/SUCC). On all other platforms, the data is serialized with [Newtonsoft.Json](https://www.newtonsoft.com/json), then stored in [PlayerPrefs](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html).

This means the desktop versions of your apps will have beautiful convenient SUCC data files, but your apps will still work perfectly on other platforms without you needing to write any platform-dependent code.

### Installing

1. Install SUCC via the package manager - [guide](https://github.com/JimmyCushnie/SUCC/wiki/Installing#as-unity-package)
2. Install Newtonsoft.Json-for-Unity via the package manager - [guide](https://github.com/jilleJr/Newtonsoft.Json-for-Unity/wiki/Installation-via-UPM)
3. Install PersistentData by adding the following to your `package.json`'s `dependencies` array:
   `"com.jimmycushnie.persistentdata":""https://github.com/JimmyCushnie/PersistentData"`

### Usage

All of the functions from this library are in the `PersistentData.GameValues` class. They all have xml documentation.

PersistentData's API is very similar to [SUCC's DataFile API](https://github.com/JimmyCushnie/SUCC/wiki/Getting-Started#get-and-set-values-in-the-file).

#### Example

```csharp
using PersistentData;
...

const string highscoreKey = "highscore";

void SaveHighscore(int score) 
    => GameValues.Set(highscoreKey, score);

int LoadHighscore() 
    => GameValues.Get(highscoreKey, defaultValue: 0);
```

#### Notes on complex type serialization

When you're serializing a [Complex Type](https://github.com/JimmyCushnie/SUCC/wiki/Complex-Types), both the desktop and non-desktop serializers use attributes to determine what to serialize.

Desktop (SUCC):

* public fields and properties are serialized unless marked with the `[SUCC.DontSave]` attribute
* private fields and properties are serialized if and only if marked with the `[SUCC.DoSave]` attribute

WebGL/other (Newtonsoft.Json):

* public fields and properties are serialized unless marked with the `[Newtonsoft.Json.JsonIgnore]` attribute
* private fields and properties are serialized if and only if marked with the `[Newtonsoft.Json.JsonProperty]` attribute

In order to keep serialization consistent between platforms, it is recommended that you only save complex types without any of the above attributes on any members.

#### Notes on disk writes

Desktop serialization uses SUCC's default behavior of saving every change to disk instantly.

Non-desktop serialization uses PlayerPref's default behavior of saving only when the game closes. However, you can force a mid-game save (to prevent data loss if a crash occurs) by calling `PlayerPrefs.Save()`.

If you need a modification to the above behavior, you should fork this repo.