using System.Collections.Generic;

[System.Serializable]
public class HighScore
{
    public string playerName;
    public string time;

    public HighScore(string playerName, string time)
    {
        this.playerName = playerName;
        this.time = time;
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "playerName", playerName },
            { "time", time }
        };
    }
}