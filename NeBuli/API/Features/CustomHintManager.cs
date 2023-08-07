using Hints;
using MEC;
using Mirror;
using Nebuli.API.Features.Player;
using Nebuli.API.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Nebuli.API.Features;
public class CustomHintManager : MonoBehaviour
{
    internal Dictionary<CustomHint, CoroutineHandle> CustomHints = new();
    internal NebuliPlayer player;
    internal StringBuilder hintMessage = new();

    public void AddHint(string message, float duration = 5f)
    {
        CustomHint newHint = new(message, duration);
        CustomHints.Add(newHint, Timing.CallDelayed(duration, () => CustomHints.Remove(newHint)));
    }

    public void Update()
    {
        //UpdateHints();
    }

    internal void UpdateHints()
    {
        try
        {
            if (player == null || player.ReferenceHub == null || player.ReferenceHub == Server.NebuliHost.ReferenceHub || !NetworkServer.active)
                return;

            hintMessage.Clear();

            foreach (var customHint in CustomHints.Keys)
            {
                hintMessage.AppendLine(customHint.Content);
            }

            if (string.IsNullOrEmpty(hintMessage.ToString()))
                return;

            if (!HintDisplay.SuppressedReceivers.Contains(player.ReferenceHub.connectionToClient))
            {
                
            }
        }
        catch (Exception e)
        {
            Log.Error($"Error in UpdateHints: {e}");
        }
    }
}
