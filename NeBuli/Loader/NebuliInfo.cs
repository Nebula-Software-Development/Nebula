using System;

namespace Nebuli.Loader;

public static class NebuliInfo
{
    internal const string NebuliVersionConst = "1.3.0";

    public static Version NebuliVersion => new(NebuliVersionConst);
}