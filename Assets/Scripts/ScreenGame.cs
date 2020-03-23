using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGame : GUIScreen
{
    [SerializeField]
    GameObject[] lives;
    [SerializeField]
    Text txtScore;

    private void Start()
    {
        GameController.Instance.AddChangeScore(OnScoreChanged);
    }

    protected override void OnShow()
    {
        txtScore.text = GameController.Instance.Score.ToString();
    }

    protected override void OnHide()
    {
        
    }

    private void OnDestroy()
    {
        GameController.Instance.RemoveChangeScore(OnScoreChanged);
    }

    void OnScoreChanged(int score)
    {
        txtScore.text = score.ToString();
    }

    public void UpdLives(int _lives)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < _lives)
            {
                lives[i].SetActive(true);
            }
            else
            {
                lives[i].SetActive(false);
            }

        }
    }


}