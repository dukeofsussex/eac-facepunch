# eac-facepunch

EAC wrapper needs to be added by the tester.

Debugging Facepunch.Steamworks shows that [this line](https://github.com/Facepunch/Facepunch.Steamworks/blob/master/Facepunch.Steamworks/Classes/Dispatch.cs#L98) always returns false whenever the game is run through the EAC launcher.

See [this opened issue](https://github.com/Facepunch/Facepunch.Steamworks/issues/609) and [the same issue with manual dispatchers](https://github.com/rlabrecque/Steamworks.NET/issues/413) for Steamworks.NET.