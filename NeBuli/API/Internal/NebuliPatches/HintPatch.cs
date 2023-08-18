/*using HarmonyLib;
using Hints;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.API.Internal.NebuliPatches;

//[HarmonyPatch(typeof(HintDisplay), nameof(HintDisplay.Show))]
public class ShowHint
{
    [HarmonyPrefix]
    public static bool SendHint(HintDisplay __instance, Hint hint)
    {
        Type type = hint.GetType();

        if (type == typeof(TranslationHint))
            return false;

        if(type == typeof(TextHint))
        {
            TextHint textHint = hint as TextHint;
            NebuliPlayer.Get(__instance.gameObject).CustomHintManager.AddHint(textHint.Text, textHint.DurationScalar);
            return false;
        }

        return true;
    }
}
*/
