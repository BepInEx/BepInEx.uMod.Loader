# BepInEx.uMod.Loader
uMod Framework mod loader for BepInEx

Loads mods from the [uMod Framework](https://umodframework.com/) modloader, most widely known for its Slime Rancher mods.

Since the original is closed source, this is an open-source reimplementation made from scratch. It is missing functionality relating to UI, configuration and caching, however it implements the encryption/decryption code and keys such that it can load encrypted `.umfmod` files.

Simple mods have been tested to work just fine, while more complex mods can at least load without Mono complaining about missing methods and types. Your mileage may vary.

Once installed, the associated `uModFramework` folders will be created on first launch without any installation required.