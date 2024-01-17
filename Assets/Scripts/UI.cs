using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI gameInfoText;

    [SerializeField]
    private TextMeshProUGUI instructionsText;

    [Space(10)]

    [SerializeField]
    private string gameTitle = "TANKS";

    [SerializeField]
    private string gameWinner = "WINNER: Player";

    [SerializeField]
    private string waitForPlayers = "WAITING FOR PLAYERS"; 

    [SerializeField]
    private string instructions = "(Press any button or key to enter)";

    [SerializeField]
    private string startGameInfo = "Destroy your enemy!";

    public void ShowWelcomeScreen(int totalNumberOfPlayers, int activePlayers)
    {
        gameInfoText.gameObject.SetActive(false);
        
        titleText.text = gameTitle;
        UpdateWaitingForPlayersMessage(totalNumberOfPlayers, activePlayers);
    }

    public void UpdateWaitingForPlayersMessage(int totalNumberOfPlayers, int activePlayers)
    {
        string activePlayersInfo = string.Format(" ({0}/{1})", activePlayers, totalNumberOfPlayers);
        instructionsText.text = waitForPlayers + activePlayersInfo + "\n" + instructions;
    }

    public IEnumerator ShowCountDown(int seconds, Action CallbackMethod)
    {
        for (int i = 0; i < 10; i++)
        {
            instructionsText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.1f);
            instructionsText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }

        gameInfoText.gameObject.SetActive(true);
        titleText.gameObject.SetActive(false);
        instructionsText.gameObject.SetActive(false);

        for (int i = 0; i< seconds; i++)
        {
            gameInfoText.text = (seconds - i).ToString();
            yield return new WaitForSeconds(1);
        }

        gameInfoText.text = startGameInfo;
        CallbackMethod?.Invoke();

        yield return new WaitForSeconds(1f);
        gameInfoText.gameObject.SetActive(false);
    }

    public IEnumerator ShowEndGame(int winner, Action CallbackMethod)
    {
        yield return new WaitForSeconds(2f);
        gameInfoText.gameObject.SetActive(true);
        titleText.gameObject.SetActive(false);
        instructionsText.gameObject.SetActive(false);

        gameInfoText.text = gameWinner + " " + winner;

        yield return new WaitForSeconds(2f);
        CallbackMethod!.Invoke();
    }
}
