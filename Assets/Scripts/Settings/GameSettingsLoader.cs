using UnityEngine;

public static class GameSettingsLoader
{
    private static GameSettings _settings;

    public static GameSettings Settings
    {
        get
        {
            if (_settings == null)
            {
                _settings = Resources.Load<GameSettings>("GameSettings");
            }
            return _settings;
        }
    }
}
