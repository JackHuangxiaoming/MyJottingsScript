using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态
/// </summary>
public enum State
{
    /// <summary>
    /// 游戏中状态
    /// </summary>
    Game,
    /// <summary>
    /// 暂停状态
    /// </summary>
    Pause,
    /// <summary>
    /// 开始的状态
    /// </summary>
    StartMenu,
    /// <summary>
    /// 对话状态
    /// </summary>
    Dialogue
}
public class GameMarager : Singleton<GameMarager>
{
    /// <summary>
    /// 状态改变的事件
    /// </summary>
    public event System.Action<State> OnStateChang;

    /// <summary>
    /// 状态的类型 
    /// </summary>
    private State state;

    public State State
    {
        get
        {
            return state;
        }

        set
        {
            if (value == state) return;
            state = value;
            if (OnStateChang != null) OnStateChang(state);
        }
    }

    private void Awake()
    {
        OnStateChang += GameMarager_OnStateChang;

        State = State.StartMenu;
    }

    private void GameMarager_OnStateChang(State GameState)
    {
        switch (GameState)
        {
            case State.Game:
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            default:

                break;
        }
        switch (GameState)
        {
            case State.Game:
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case State.Pause:
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case State.StartMenu:
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case State.Dialogue:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    private void Update()
    {
        if (InputMarager.Escape)
        {
            State = state == State.Game ? State.Pause : State.Game;
            print(state);
        }
    }
}
