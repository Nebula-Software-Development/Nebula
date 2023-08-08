using Achievements;
using Nebuli.API.Features.Player;

namespace Nebuli.API.Features;

public static class Achievements
{
    public static void GiveClientAchievement(NebuliPlayer player, AchievementName achievement) => AchievementHandlerBase.ServerAchieve(player.ReferenceHub.connectionToClient, achievement);
}
