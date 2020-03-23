using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMap : GUIScreen
{
    [SerializeField]
    Button[] levelBtn;
    int levels;

    private void Start()
    {
        
        int i = 0;
        foreach(Button btn in levelBtn)
        {
            int index = i;
            btn.onClick.AddListener(() => StartButtonClick(index));
            i++;
        }
    }

    void StartButtonClick(int id)
    {
        if(id <= GameController.Instance.UnlockedLevels)
        {
            GameController.Instance.SetMap(id);
            GameController.Instance.StartGame();
        }
    }

    protected override void OnShow()
    {
        levels = GameController.Instance.UnlockedLevels;
        for(int i = 0; i < levelBtn.Length; i++)
        {
            if(i < levels)
            {
                levelBtn[i].GetComponent<Image>().color = Color.green;
            }else if(i > levels)
            {
                levelBtn[i].GetComponent<Image>().color = Color.red;
            }else if(i == levels)
            {
                levelBtn[i].GetComponent<Image>().color = Color.yellow;
            }
        }
    }

    
}
