using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLose : GUIScreen
{
    [SerializeField]
    Button btnRestart;
    [SerializeField]
    Button btnChangeMap;

    private void Start()
    {
        btnRestart.onClick.AddListener(RestartButton);
        btnChangeMap.onClick.AddListener(ChangeMapButton);
    }

    protected override void OnShow()
    {


        // ScoreController.Instance.OnCoinChanged += OnScoreChanged;

    }

    protected override void OnHide()
    {
        // ScoreController.Instance.OnCoinChanged -= OnScoreChanged;

    }

    void RestartButton()
    {
        GameController.Instance.Restart();
    }

    void ChangeMapButton()
    {
        GUIController.Instance.ShowScreen<ScreenMap>(true);
    }
}
