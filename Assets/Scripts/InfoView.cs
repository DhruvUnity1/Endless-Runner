using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoView : View
{
    [SerializeField] private Button backBtn;

    public override void Initialize()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAll();
            UIManager.Show<MainMenuView>();
        });

    }
}
