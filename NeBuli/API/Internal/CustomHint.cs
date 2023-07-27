namespace Nebuli.API.Internal;

public class CustomHint
{
    internal string Content;
    internal float Duration;

    public CustomHint(string content, float duration = 5f)
    {
        Content = content;
        Duration = duration;
    }
}

