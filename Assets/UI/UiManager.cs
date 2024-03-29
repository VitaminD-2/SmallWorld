using Fusion;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private enum ActiveUi
    {
        MainMenu,
        Lobby,
        Game
    }

    public GameObject LobbyUi;
    public GameObject MainMenuUi;
    public GameObject RectLayoutUi;
    public GameObject SquareLayoutUi;

    public GameLogic GameLogic;
    private GameLogic.GameState _lastGameState;

    private ActiveUi UiState;

    void Awake()
    {
        GoToMainMenu();
    }

    void Update()
    {
        if (!GameLogic.IsNetworkActive) return;

        if (GameLogic.State == GameLogic.GameState.GameStarted && _lastGameState != GameLogic.GameState.GameStarted)
        {
            GoToGame(GameLogic.PlayerCount);
        }
        _lastGameState = GameLogic.State;
    }

    public void RefreshUi()
    {
        if (UiState == ActiveUi.Game)
        {
            foreach (var gameUi in FindObjectsOfType<GameUi>())
            {
                gameUi.RefreshUi();
            }
        }
    }

    public void GoToMainMenu()
    {
        SetActiveUi(ActiveUi.MainMenu);
    }

    public void GoToLobby(bool isHost)
    {
        SetActiveUi(ActiveUi.Lobby);
        LobbyUi.GetComponent<LobbyUi>().Initialise(isHost);
    }

    public void GoToGame(int playerCount)
    {
        Debug.Log($"GoToGame({playerCount})");
        SetActiveUi(ActiveUi.Game, playerCount);
        if (playerCount == 1 || playerCount == 2 || playerCount == 3)
            RectLayoutUi.GetComponent<GameUi>().Initialise();
        else if (playerCount == 4 || playerCount == 5)
            SquareLayoutUi.GetComponent<GameUi>().Initialise();
    }

    private void SetActiveUi(ActiveUi targetState, int playerCount = 0)
    {
        if (playerCount == 1) playerCount = 2; // Cheeky override to help solo testing

        switch (targetState)
        {
            case ActiveUi.MainMenu:
                LobbyUi.gameObject.SetActive(false);
                MainMenuUi.gameObject.SetActive(true);
                break;
            case ActiveUi.Lobby:
                MainMenuUi.gameObject.SetActive(false);
                LobbyUi.gameObject.SetActive(true);
                break;
            case ActiveUi.Game:
                MainMenuUi.gameObject.SetActive(false);
                LobbyUi.gameObject.SetActive(false);
                RectLayoutUi.gameObject.SetActive(playerCount == 2 || playerCount == 3);
                SquareLayoutUi.gameObject.SetActive(playerCount == 4 || playerCount == 5);
                break;
        }

        UiState = targetState;
    }

    /*private GameObject GetGameBoard(int playerCount)
    {

    }*/

    public void InformPlayerChange()
    {
        // Don't currently need to do this as lobby UI just updates the player list every second from it's local gameobjects
        //LobbyUi.GetComponent<LobbyUi>().UpdatePlayers();
    }
}
