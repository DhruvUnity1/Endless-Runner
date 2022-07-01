using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverView : View
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button mainMenuBtn;


    public override void Initialize()
    {
        restartBtn.onClick.AddListener(() =>
        {
            UIManager.Show<GameView>();
            GameManager.Instance.StartGame();
        });
        mainMenuBtn.onClick.AddListener(() =>
        {
            UIManager.Show<MainMenuView>();
        });
    }
}
