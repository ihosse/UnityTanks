using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(PlayerInputManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField]
    private Transform[] spawnPositions;

    [SerializeField]
    private UI gameUI;

    [SerializeField]
    private Color[] colors;
    
    private PlayerInputManager playerInputManager;
    private List<PlayerManager> playerManagers;
    private int currentNumberOfPlayers;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager> ();
        playerManagers = new List<PlayerManager> ();
        currentNumberOfPlayers = 0;

        foreach (Transform spawnPosition in spawnPositions)
        {
            cinemachineTargetGroup.AddMember(spawnPosition, 1, 7);
        }

        gameUI.ShowWelcomeScreen(playerInputManager.maxPlayerCount ,currentNumberOfPlayers);
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        var currentPlayer = input.gameObject.GetComponent<PlayerManager>();
        int currentPlayerNumber = currentNumberOfPlayers;

        currentPlayer.gameObject.transform.SetPositionAndRotation(
            spawnPositions[currentPlayerNumber].position, 
            spawnPositions[currentPlayerNumber].rotation
            );

        cinemachineTargetGroup.RemoveMember(spawnPositions[0]);
        cinemachineTargetGroup.AddMember(currentPlayer.transform, 1, 7);

        playerManagers.Add(currentPlayer);

        currentPlayer.Initilize(colors[currentPlayerNumber]);
        currentPlayer.OnKill += OnPlayerKill;

        currentNumberOfPlayers++;

        gameUI.UpdateWaitingForPlayersMessage(playerInputManager.maxPlayerCount, currentNumberOfPlayers);

        if (currentNumberOfPlayers >= playerInputManager.maxPlayerCount)
            StartCoroutine(gameUI.ShowCountDown(3, OnStartGame));
    }

    private void OnStartGame()
    {
        foreach (var player in playerManagers)
            player.playerShot.CanShot = true;
    }

    public void OnPlayerKill()
    {
        foreach (var player in playerManagers)
            player.playerShot.CanShot = false;

        if (CheckWinner(out int winnerNumber))
        {
            playerManagers[winnerNumber -1].playerMovement.CanMove = false;
            StartCoroutine(gameUI.ShowEndGame(winnerNumber, OnEndGame));
        }
    }

    private bool CheckWinner(out int winnerNumber)
    {
        int numbersOfAlivePlayers = 0;
        winnerNumber = 0;

        for (int i = 0; i < playerManagers.Count; i++)
        {
            if (playerManagers[i].gameObject.activeSelf)
            {
                numbersOfAlivePlayers++;
                winnerNumber = i + 1;
            }
        }

        if (numbersOfAlivePlayers == 1)
            return true;
        else
            return false;
    }

    private void OnEndGame() => SceneManager.LoadScene(0);
}
