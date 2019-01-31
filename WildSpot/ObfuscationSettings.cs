using System;
using System.Reflection;

[assembly: Obfuscation(Feature = "ignore error EF-3035", StripAfterObfuscation = false)]
[assembly: Obfuscation(Feature = "merge with SKGL.dll", Exclude = false)]
[assembly: Obfuscation(Feature = "merge with [internalization=auto] Elysium.dll", Exclude = false)]
[assembly: Obfuscation(Feature = "merge with [internalization=auto] Elysium.Notifications.dll", Exclude = false)]
[assembly: Obfuscation(Feature = "merge with [satellites] Microsoft.Expression.Drawing.dll", Exclude = false)]
[assembly: Obfuscation(Feature = @"merge with [satellites] ru\Microsoft.Expression.Drawing.resources.dll", Exclude = false)]
[assembly: Obfuscation(Feature = @"merge with ru\StarSpot.resources.dll", Exclude = false)]
[assembly: Obfuscation(Feature = "Apply to type Elysium.*: all", Exclude = true, ApplyToMembers = true)]