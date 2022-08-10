using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏状态
/// </summary>
public enum GameState
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    Game,
    /// <summary>
    /// 暂停状态
    /// </summary>
    Pause,
    /// <summary>
    /// 主菜单状态
    /// </summary>
    MainMeun,
    /// <summary>
    /// 对话状态
    /// </summary>
    Dialogue,
    /// <summary>
    /// 背包状态
    /// </summary>
    Inventory,
    /// <summary>
    /// 结束状态
    /// </summary>
    Finish
}
/// <summary>
/// 游戏管理器
/// </summary>
public class GameMarager : Singleton<GameMarager>
{
    private GameState _gameState;

    /// <summary>
    /// 游戏状态
    /// </summary>
    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            if (value == _gameState) return;
            _gameState = value;
            if (OnStateChang != null) OnStateChang(_gameState);
        }
    }

    /// <summary>
    /// 游戏状态事件
    /// </summary>
    public event System.Action<GameState> OnStateChang;

    private void Awake()
    {
        OnStateChang += GameMarager_OnStateChang;
    }

    /// <summary>
    /// 状态改变事件 执行方法
    /// </summary>
    /// <param name="state"></param>
    private void GameMarager_OnStateChang(GameState state)
    {
        switch (state)
        {
            case GameState.Game:
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case GameState.MainMeun:
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case GameState.Dialogue:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Inventory:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Finish:
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
        }
    }

    private void Start()
    {
        GameState = GameState.MainMeun;
    }

    private void Update()
    {
        if (InputController.Escape)
        {
            GameState = GameState == GameState.Game ? GameState.Pause : GameState.Game;
        }
        if (InputController.Inventory)
        {
            GameState = GameState == GameState.Inventory ? GameState.Game : GameState.Inventory;
        }
    }
}
