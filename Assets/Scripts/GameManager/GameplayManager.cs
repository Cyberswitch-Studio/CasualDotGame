using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenuUI, OptionsMenuUI, PauseMenuUI, GameUI;
    public GameObject mainCamera;
    public GameObject Player;

    public enum GameState
    {
        MainMenu,
        OptionsMenu,
        PauseMenu,
        Game
    }

    public GameState currentState = GameState.MainMenu;

    void Start()
    {
        //MainMenuCanvas.transform.DOScale(2, 1);
        //mainCamera.transform.DOLocalMoveY(5, 5);
    }

    void Update()
    {
        if (currentState == GameState.Game)
        {
            
        }
    }

    IEnumerator MainMenuState()
    {
        while (currentState == GameState.MainMenu)
        {
            yield return null;
        }
    }
}
