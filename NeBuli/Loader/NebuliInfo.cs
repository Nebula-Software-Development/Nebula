using System;

namespace Nebuli.Loader;

public static class NebuliInfo
{
    internal const string NebuliVersionConst = "1.3.3";

    public static Version NebuliVersion { get; } = new(NebuliVersionConst);
}