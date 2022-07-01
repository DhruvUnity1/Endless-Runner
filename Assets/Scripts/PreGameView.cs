using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreGameView : View
{
    [SerializeField] private Button playBtn;

    public override void Initialize()
    {
        playBtn.onClick.AddListener(() =>
        {
           /* UIManager.Show<GameView>();
            GameManager.Instance.StartGamee();*/
        });

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UIManager.Show<GameView>();
            GameManager.Instance.StartGame();
        }
    }
}
