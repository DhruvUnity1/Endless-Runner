using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuView : View
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button infoBtn;


    public override void Initialize()
    {
        playBtn.onClick.AddListener(() =>
         {
             UIManager.Show<PreGameView>();
         });
        infoBtn.onClick.AddListener(() =>
        {
            UIManager.ShowBoth<MainMenuView, InfoView>();
        });
    }
}

