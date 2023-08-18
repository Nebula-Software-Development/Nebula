using System;

#pragma warning disable CS1591

namespace Nebuli.Loader;

public static class NebuliInfo
{
    internal const string NebuliVersionConst = "1.1.8";

    public static Version NebuliVersion => new(NebuliVersionConst);
}