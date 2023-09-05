using NorthwoodLib.Pools;
using PlayerRoles;
using Respawning;
using System.Text;

namespace Nebuli.API.Features.Map;

/// <summary>
/// Class for interacting with C.A.S.S.I.E easier.
/// </summary>
public static class CASSIE
{
    /// <summary>
    /// Gets if C.A.S.S.I.E is currently speaking.
    /// </summary>
    public static bool IsSpeaking => NineTailedFoxAnnouncer.singleton.queue.Count is not 0;

    /// <summary>
    /// Sends a message that C.A.S.S.I.E will announce.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="Hold">If C.A.S.S.I.E should hold.</param>
    /// <param name="Noisy">If the announcement is silent or not.</param>
    /// <param name="Subtitles">If subtitles display or not.</param>
    public static void SendMessage(string message, bool Hold = false, bool Noisy = true, bool Subtitles = false) => RespawnEffectsController.PlayCassieAnnouncement(message, Hold, Noisy, Subtitles);

    /// <summary>
    /// Reproduces a non-glitched C.A.S.S.I.E message with optional customization of subtitles.
    /// </summary>
    /// <param name="originalMessage">The original message to be delivered by C.A.S.S.I.E.</param>
    /// <param name="subtitleTranslation">The translated text to be displayed as subtitles.</param>
    /// <param name="Hold">Indicates whether C.A.S.S.I.E should hold the message.</param>
    /// <param name="Noisy">Indicates whether C.A.S.S.I.E should include sound effects during the message.</param>
    /// <param name="Subtitles">Indicates whether subtitles should be displayed for the message.</param>
    public static void SendCustomCaptionMessage(string originalMessage, string subtitleTranslation, bool Hold = false, bool Noisy = true, bool Subtitles = true)
    {
        StringBuilder announcementBuilder = new();
        string[] cassieMessages = originalMessage.Split('\n');
        string[] subtitleTranslations = subtitleTranslation.Split('\n');

        for (int i = 0; i < cassieMessages.Length; i++)
            announcementBuilder.Append($"{subtitleTranslations[i].Replace(' ', ' ')}<size=0> {cassieMessages[i]} </size><split>");

        SendMessage(announcementBuilder.ToString(), Hold, Noisy, Subtitles);
    }

    /// <summary>
    /// Clears all current and queued C.A.S.S.I.E messages.
    /// </summary>
    public static void Clear() => RespawnEffectsController.ClearQueue();

    /// <summary>
    /// Sends a glitchy message that C.A.S.S.I.E will announce.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="glitchAmount">The amount that C.A.S.S.I.E will glitch.</param>
    /// <param name="jamChance">The chance C.A.S.S.I.E has to jam.</param>
    public static void SendGlitchyMessage(string message, float glitchAmount, float jamChance) => NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase(message, glitchAmount, jamChance);

    /// <summary>
    /// Converts a team to a team message that C.A.S.S.I.E reconignizes.
    /// </summary>
    /// <param name="team">The team to convert.</param>
    /// <param name="unitName">If foundation forces, the Units name.</param>
    public static void ConvertTeam(Team team, string unitName) => NineTailedFoxAnnouncer.ConvertTeam(team, unitName);

    /// <summary>
    /// Gets the amount of time it would take to speak the words given.
    /// </summary>
    /// <param name="text">The text to calculate the time with.</param>
    /// <param name="rawNumber">If it should be a raw number.</param>
    /// <param name="speed">The speed at which to calculate at.</param>
    public static float TimeToSpeak(string text, bool rawNumber = false, float speed = 1) => NineTailedFoxAnnouncer.singleton.CalculateDuration(text, rawNumber, speed);

}
