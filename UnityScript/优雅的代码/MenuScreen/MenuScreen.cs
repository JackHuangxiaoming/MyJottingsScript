using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIForm<MenuScreen>
{
    public Button PlayButton;

    protected override void Awake()
    {
        base.Awake();
        PlayButton.onClick.AddListener(OnClickPlay);
    }

    private void OnClickPlay()
    {
        GameMarager.Single.State = State.Game;
        print("Game");
    }
}
