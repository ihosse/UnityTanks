using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof(PlayerInputManager), typeof(BackGroundMusic))]
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
    private BackGroundMusic backGroundMusic;
    private int currentNumberOfPlayers;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager> ();
        playerManagers = new List<PlayerManager> ();
        backGroundMusic = GetComponent<BackGroundMusic> ();
        currentNumberOfPlayers = 0;

        playerInputManager.EnableJoining();

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
        {
            playerInputManager.DisableJoining();
            StartCoroutine(gameUI.ShowCountDown(3, OnStartGame));
        }
    }

    private void OnStartGame()
    {
        backGroundMusic.PlayMusic();
        backGroundMusic.ChangeSnapShot(backGroundMusic.GameSnapShot, 0);

        foreach (var player in playerManagers)
            player.playerShot.CanShot = true;
    }

    public void OnPlayerKill()
    {
        if (CheckWinner(out int winnerNumber))
        {
            var playerWinner = playerManagers[winnerNumber - 1];

            playerWinner.playerMovement.CanMove = false;
            playerWinner.playerShot.CanShot = false;
            playerWinner.isInvencible = true;

            StartCoroutine(gameUI.ShowEndGame(winnerNumber, OnEndGame));

            backGroundMusic.ChangeSnapShot(backGroundMusic.WinnerSnapShot, 1);
        }
    }

    private bool CheckWinner(out int winnerNumber)
    {
        int numbersOfAlivePlayers = 0;
        winnerNumber = 0;

        for (int i = 0; i < playerManagers.Count; i++)
        {
            if (!playerManagers[i].IsDead)
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

    private void OnEndGame()
    {
        StartCoroutine(EndGameAfterSeconds());
    }

    private IEnumerator EndGameAfterSeconds()
    {
        backGroundMusic.ChangeSnapShot(backGroundMusic.EndSnapShot, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
