using Hints;
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
    internal List<CustomHint> CustomHints = new();
    internal NebuliPlayer player;
    internal StringBuilder hintMessage = new();

    public void AddHint(string message, float duration = 5f)
    {
        CustomHints.Add(new CustomHint(message, duration));
    }

    public void Update()
    {
        UpdateHints();
    }

    internal void UpdateHints()
    {
        try
        {
            if (player == null || player.ReferenceHub == null || player.ReferenceHub == Server.HostHub || !NetworkServer.active)
                return;

            hintMessage.Clear();
            for (int i = CustomHints.Count - 1; i >= 0; i--)
            {
                CustomHint customHint = CustomHints[i];
                if (customHint.Duration <= 0)
                {
                    CustomHints.RemoveAt(i);
                    continue;
                }

                customHint.Duration -= Time.deltaTime;
                hintMessage.Append(customHint.Content);
            }

            if (string.IsNullOrEmpty(hintMessage.ToString()))
                return;

            if (!HintDisplay.SuppressedReceivers.Contains(player.ReferenceHub.connectionToClient))
            {
                player.ReferenceHub.connectionToClient.Send(new HintMessage(new TextHint(hintMessage.ToString())));
            }
        }
        catch (Exception e)
        {
            Log.Error($"Error in UpdateHints: {e}");
        }
    }

}
