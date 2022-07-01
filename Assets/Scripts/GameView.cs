using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameView : View
{
    [SerializeField] private Button pauseBtn;

    public override void Initialize()
    {
        pauseBtn.onClick.AddListener(() =>
        {
            UIManager.Show<PauseGameView>();
            Time.timeScale = 0;
        });

    }
}
