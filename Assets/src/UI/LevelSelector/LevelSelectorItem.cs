using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum LevelDifficulty
{ EASY, MEDIUM, HARD, SUPERHARD }

public class LevelSelectorItem : MonoBehaviour
{
    public int CoinsReward { get; set; }
    public LevelDifficulty LevelDiff { get; set; }
    public LevelSelectorItem(int _coinsReward, LevelDifficulty _levelDiff)
    {
        CoinsReward = _coinsReward;
        LevelDiff = _levelDiff;
    }
    private void Awake()
    {
       // this.gameObject.GetComponentInChildren<TextElement>().text = "asd"; 
    }
}
