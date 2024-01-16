using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent (typeof(PlayerInputManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineTargetGroup cinemachinetagetGroup;

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
            cinemachinetagetGroup.AddMember(spawnPosition, 1, 7);
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

        cinemachinetagetGroup.RemoveMember(spawnPositions[0]);
        cinemachinetagetGroup.AddMember(currentPlayer.transform, 1, 7);

        playerManagers.Add(currentPlayer);
        playerManagers[currentPlayerNumber].Initilize(colors[currentPlayerNumber]);

        currentNumberOfPlayers++;

        gameUI.UpdateWaitingPlayersMessage(playerInputManager.maxPlayerCount, currentNumberOfPlayers);

        if (currentNumberOfPlayers >= playerInputManager.maxPlayerCount)
            StartCoroutine(gameUI.CountDown(3, StartGame));
    }

    private void StartGame()
    {
        print("começou!");
    }
}
