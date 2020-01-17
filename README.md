# PersistentData
Easy cross-platform data saving and loading for Unity













## Notes on complex type serialization

When you're serializing a [Complex Type](https://github.com/JimmyCushnie/SUCC/wiki/Complex-Types), both the desktop and non-desktop serializers use attributes to determine what to serialize.

Desktop (SUCC):

* public fields and properties are serialized unless marked with the `[SUCC.DontSave]` attribute
* private fields and properties are serialized if and only if marked with the `[SUCC.DoSave]` attribute

WebGL/other (Newtonsoft.Json):

* public fields and properties are serialized unless marked with the `[Newtonsoft.Json.JsonIgnore]` attribute
* private fields and properties are serialized if and only if marked with the `[Newtonsoft.Json.JsonProperty]` attribute

In order to keep serialization consistent between platforms, it is recommended that you only save complex types without any of the above attributes on any members.