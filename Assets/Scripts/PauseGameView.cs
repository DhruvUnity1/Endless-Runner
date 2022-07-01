using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseGameView : View
{
    [SerializeField] private Button continueBtn;

    public override void Initialize()
    {
        continueBtn.onClick.AddListener(() =>
        {
            UIManager.Show<GameView>();
            Time.timeScale = 1;
        });

    }

}
