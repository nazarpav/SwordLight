using System;
using UnityEngine;
[Serializable]
public enum LevelDifficulty
{ EASY, MEDIUM, HARD, SUPERHARD }

[Serializable]
[CreateAssetMenu(fileName = "LevelStorage", menuName = "ScrptObj/LevelStorage", order = 1)]
public class LevelStorage : ScriptableObject
{
    public Location[] Locations;
    public Location GetLocation(string locationName)
    {
        for (int a = 0; a < Locations.Length; a++)
        {
            if (Locations[a].LocationName == locationName)
                return Locations[a];
        }
        return null;
    }
}


[Serializable]
public class Location
{
    public string LocationName;
    public string CountOfLevels;
    public Texture2D LocationSelectorBackground;
    public Texture2D LocationBackground;
    public LevelSpecification[] Levels;

}

[Serializable]
public class LevelSpecification
{
    public int CoinsReward;
    public LevelDifficulty LevelDiff;

}
