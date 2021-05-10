using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    private List<LevelSelectorItem> levels;
    private List<GameObject> levelSelectorItems;
    [SerializeField]
    private GameObject levelSelectorItem;
   // private RuntimeAnimatorController animator;

    private float currentXPosition = 0;
    private int currentSelectedItem;
    private float levelSelectorItemWidth;
    private float itemBounds = 0;
    void Start()
    {
        LoadLevelsInfo();
        CreateLevelSelectorItems();
        levelSelectorItemWidth = levelSelectorItem.GetComponent<RectTransform>().rect.width * levelSelectorItem.transform.localScale.x;
        itemBounds = levelSelectorItemWidth;
        currentSelectedItem = (levelSelectorItems.Count / 2);
        levelSelectorItems[currentSelectedItem--].GetComponent<Animator>().SetTrigger("UpScale");
    }

    void Update()   
    {
        currentXPosition = this.gameObject.GetComponent<Transform>().localPosition.x;
        if(currentXPosition>= itemBounds)
        {
            itemBounds += levelSelectorItemWidth;
            levelSelectorItems[currentSelectedItem+1].GetComponent<Animator>().SetTrigger("DefaultScale");
            levelSelectorItems[currentSelectedItem--].GetComponent<Animator>().SetTrigger("UpScale");
            Debug.Log("BOUNDS");
        }

    }
    private void CreateLevelSelectorItems()
    {
        levelSelectorItems = new List<GameObject>();
        foreach (var it in levels)
        {
            levelSelectorItems.Add(Instantiate(levelSelectorItem, this.gameObject.transform).gameObject);
           // sd.transform.localPosition = new Vector2((LastXPosition += 560f), 0f);
           // var vd = sd.gameObject.GetComponent<RectTransform>();
           // vd.anchoredPosition.Set((LastXPosition += 524f), 0f);
           //levelSelectorItem.AddComponent<LevelSelectorItem>();
           //  var tmp = levelSelectorItem.GetComponent<LevelSelectorItem>();
        }
    }
    private void LoadLevelsInfo()
    {
        levels = new List<LevelSelectorItem>();
        for(int a=0;a<15;a++)
        {
            levels.Add(new LevelSelectorItem(25, LevelDifficulty.HARD));
        }
    }
}
