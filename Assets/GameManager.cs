using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class GameManager : MonoBehaviour
{
    [Header("Game Options")]
    public int numberOfPlayers = 2;

    [Header("Debug States")]
    [SerializeField]
    GameState currentState;

    public static GameManager instance;

    public List<PlayerData> players = new List<PlayerData>();

    public int currentPlayer = 0;

    public static int CurrentPlayer
    {
        get
        {
            if (GameManager.instance != null)
                return GameManager.instance.currentPlayer;
            else
                return 0;
        }
        set
        {
            if (GameManager.instance != null)
                GameManager.instance.currentPlayer = value;
        }
    }

    private static DiceRoller diceRoller;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    //Gameplay Loop
        // 1. Initialise the players with their amounts
        // 2. Dice Roll (Player 1) -> Player 1 Land -> Options -> (Repeat with Player 2)

    private void Start()
    {
        diceRoller = GameObject.FindObjectOfType<DiceRoller>();

        if (!diceRoller) Debug.LogError("Dependency of Dice Roller missing");

        SetupPlayers();
    }

    private void SetupPlayers ()
    {
        currentState = GameState.Setup;
        for (int i = 0; i < numberOfPlayers; i++)
        {
            PlayerData data = new PlayerData($"Player {i}",1500,1);
            players.Add(data);
        }

        currentPlayer = 0;

        UIManager.Instance.currentPlayerNumber.text = (currentPlayer + 1).ToString();

        BeginDiceRoll();
    }

    public static void IncrementPlayer ()
    {
        GameManager.CurrentPlayer += 1;

        if (GameManager.instance.numberOfPlayers == GameManager.CurrentPlayer)
        {
            GameManager.CurrentPlayer = 0;
        }

        UIManager.Instance.currentPlayerNumber.text = (GameManager.CurrentPlayer + 1).ToString();
    }

    //End game when all players have passed GO 3 Times
    public static bool GameHasFinished()
    {
        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            if (GameManager.instance.players[i].passedGo < 2)
            {
                return false;
            }
        }

        return true;
    }

    //Game Stage 1 
    public void BeginDiceRoll ()
    {
        if (GameHasFinished())
        {
            HandleGameCompletion();
            return;
        }

        diceRoller.BeginRolling(new System.Action<int>(HandlePlayerMovement));

    }

    //Game Stage 2
    public void HandlePlayerMovement (int rollResult)
    {
        print($"Player {currentPlayer} rolled a {rollResult}");
    }


    public static void HandleGameCompletion ()
    {

    }

}

public enum GameState
{
    Setup,
    Rolling,
    PlayerDecision,
    Ending
}