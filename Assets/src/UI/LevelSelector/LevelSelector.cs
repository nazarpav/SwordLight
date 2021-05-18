using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public LevelStorage levels;
    private List<GameObject> levelSelectorItems;
    [SerializeField]
    private GameObject levelSelectorItem;
   // private RuntimeAnimatorController animator;

    private float currentXPosition = 0;
    private int currentSelectedItem;
    private float levelSelectorItemWidth;
    private float itemBoundsLeft = 0;
    private float itemBoundsRight = 0;
    private float spacing;
    void Start()
    {
        CreateLevelSelectorItems();
        spacing = this.GetComponent<HorizontalLayoutGroup>().spacing;
        levelSelectorItemWidth = spacing + (levelSelectorItem.GetComponent<RectTransform>().rect.width * levelSelectorItem.transform.localScale.x);
        itemBoundsLeft = levelSelectorItemWidth;
        itemBoundsRight = levelSelectorItemWidth - (levelSelectorItemWidth * 2);
        currentSelectedItem = (levelSelectorItems.Count / 2);
        levelSelectorItems[currentSelectedItem].GetComponent<Animator>().SetTrigger("UpScale");
    }

    void Update()       
    {
        currentXPosition = this.gameObject.GetComponent<Transform>().localPosition.x;
        if (currentXPosition >= itemBoundsLeft-spacing)
        {
            if (currentSelectedItem == 0)
                return;

            itemBoundsLeft = itemBoundsLeft + levelSelectorItemWidth;
                itemBoundsRight = itemBoundsRight + levelSelectorItemWidth;
            
            levelSelectorItems[currentSelectedItem].GetComponent<Animator>().SetTrigger("DefaultScale");
            levelSelectorItems[--currentSelectedItem].GetComponent<Animator>().SetTrigger("UpScale");
            Debug.Log((itemBoundsLeft + " " + itemBoundsRight));
        }
        else if (currentXPosition <= itemBoundsRight+spacing)
        {
            if (currentSelectedItem == levelSelectorItems.Count-1)
                return;

                itemBoundsLeft = itemBoundsLeft - levelSelectorItemWidth;
                itemBoundsRight = itemBoundsRight - levelSelectorItemWidth;

            levelSelectorItems[currentSelectedItem ].GetComponent<Animator>().SetTrigger("DefaultScale");
            levelSelectorItems[++currentSelectedItem].GetComponent<Animator>().SetTrigger("UpScale");
            Debug.Log((itemBoundsLeft + " " + itemBoundsRight));
        }

    }
    private void CreateLevelSelectorItems()
    {
        levelSelectorItems = new List<GameObject>();
        foreach (var it in levels.GetLocation("City").Levels)
        {
            levelSelectorItems.Add(Instantiate(levelSelectorItem, this.gameObject.transform).gameObject);
           // sd.transform.localPosition = new Vector2((LastXPosition += 560f), 0f);
           // var vd = sd.gameObject.GetComponent<RectTransform>();
           // vd.anchoredPosition.Set((LastXPosition += 524f), 0f);
           //levelSelectorItem.AddComponent<LevelSelectorItem>();
           //  var tmp = levelSelectorItem.GetComponent<LevelSelectorItem>();
        }
    }
}
