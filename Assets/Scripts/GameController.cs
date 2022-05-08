using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // DICE
    [SerializeField] private GameObject diceObj1, diceObj2, diceObj3, diceObj4, diceObj5;
    private Dice dice1, dice2, dice3, dice4, dice5;
    private bool toBeConfimred1, toBeConfimred2, toBeConfimred3, toBeConfimred4, toBeConfimred5;
    private bool isConfimred1, isConfimred2, isConfimred3, isConfimred4, isConfimred5;

    // DICE RESULT PANEL
    [SerializeField] private GameObject statusPanel, initialRollPanel, diceResultPanel, scorePanel, winnerPanel;

    // TURN MANAGEMENT
    private int turnCount, turnPlayer;

    // PLAYER
    private List<int> player1ScoreList, player2ScoreList;
    private int player1TotalScore, player2TotalScore;

    // GAME
    private bool hasRolled, isFinishedRolling;
    private List<int> currentDiceResult;
    private int currentTryCount;

    private void Start() {
        turnCount = 1;
        turnPlayer = 1;
        player1ScoreList = new List<int>();
        player2ScoreList = new List<int>();
        for(int i = 0; i < 12; i++) {
            player1ScoreList.Add(-1);
            player2ScoreList.Add(-1);
        }
        dice1 = diceObj1.GetComponent<Dice>();
        dice2 = diceObj2.GetComponent<Dice>();
        dice3 = diceObj3.GetComponent<Dice>();
        dice4 = diceObj4.GetComponent<Dice>();
        dice5 = diceObj5.GetComponent<Dice>();
        InitStartTurnState();
    }

    private void Update() {
        if(hasRolled && !isFinishedRolling) {
            for(int i = 0; i < 5; i++) { 
                if(GetTargetDiceObject(i).activeInHierarchy) currentDiceResult[i] = GetTargetDiceScript(i).GetFinalNumber();
            }
            if(currentDiceResult.IndexOf(-1) != -1) return;
            isFinishedRolling = true;
            currentDiceResult.Sort();
            UpdateDiceResultPanel();
        }
    }

    private void InitStartTurnState() {
        currentDiceResult = new List<int>{ -1, -1, -1, -1, -1 };
        currentTryCount = 0;
        hasRolled = false;
        isFinishedRolling = false;
        initialRollPanel.SetActive(true);
        initialRollPanel.GetComponent<InitialPanel>().UpdateUI(turnPlayer, turnCount);
        statusPanel.GetComponent<StatusPanel>().UpdateStatusPanel(turnPlayer, turnCount);
    }

    private void UpdateDiceResultPanel() {
        List<int> possibleScore = gameObject.GetComponent<ScoreCalculator>().GetPossibleScore(currentDiceResult);
        diceResultPanel.GetComponent<ResultPanel>().UpdatePanel(currentDiceResult, turnPlayer == 1 ? player1ScoreList : player2ScoreList, possibleScore, currentTryCount);
        diceResultPanel.SetActive(true);
    }

    private GameObject GetTargetDiceObject(int i) {
        switch(i) {
            case 0: return diceObj1;
            case 1: return diceObj2;
            case 2: return diceObj3;
            case 3: return diceObj4;
            case 4: return diceObj5;
            default: return null;
        }
    }

    private Dice GetTargetDiceScript(int i) {
        switch(i) {
            case 0: return dice1;
            case 1: return dice2;
            case 2: return dice3;
            case 3: return dice4;
            case 4: return dice5;
            default: return null;
        }
    }

    public void RollDice() {
        currentTryCount++;
        isFinishedRolling = false;
        hasRolled = true;
        initialRollPanel.SetActive(false);
        diceResultPanel.SetActive(false);
        for(int i = 0; i < 5; i++) {
            if(currentDiceResult[i] == -1) {
                GetTargetDiceObject(i).SetActive(true);
                GetTargetDiceScript(i).Roll(i + 1);
            } else {
                GetTargetDiceObject(i).SetActive(false);
            }
        }
    }

    public void Reroll(List<int> newDiceResult){
        currentDiceResult = newDiceResult;
        RollDice();
    }

    public void EndTurn(List<int> newPoint) {
        if(turnPlayer == 1) {
            player1ScoreList = newPoint;
            player1TotalScore = scorePanel.GetComponent<ScorePanel>().UpdateScore(turnPlayer, newPoint);
            turnPlayer = 2;
            diceResultPanel.SetActive(false);
            InitStartTurnState();
        } else if (turnPlayer == 2){
            player2ScoreList = newPoint;
            player2TotalScore = scorePanel.GetComponent<ScorePanel>().UpdateScore(turnPlayer, newPoint);
            turnPlayer = 1;
            diceResultPanel.SetActive(false);
            if(turnCount < 12) {
                turnCount++;
                InitStartTurnState();
            } else {
                ShowWinner();
            }
        }
    }

    public void ShowWinner() {
        winnerPanel.GetComponent<WinnerPanel>().UpdatePanel(player1TotalScore, player2TotalScore);
        winnerPanel.SetActive(true);
    }

}
