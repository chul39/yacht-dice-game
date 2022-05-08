using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour {

    private List<int> diceResult = new List<int>{ 0, 0, 0, 0, 0 };
    private List<int> diceResultCount = new List<int>{ 0, 0, 0, 0, 0, 0 };

    public List<int> GetPossibleScore(List<int> inputResult) {
        ResetToInitState();
        diceResult = inputResult;
        foreach(int num in diceResult) { 
            if(num <= 0) break;
            diceResultCount[num - 1]++; 
        }
        List<int> tmp = new List<int>();
        tmp.Add(GetAceScore());
        tmp.Add(GetDeucesScore());
        tmp.Add(GetThreesScore());
        tmp.Add(GetFoursScore());
        tmp.Add(GetFivesScore());
        tmp.Add(GetSixesScore());
        tmp.Add(GetChoiceScore());
        tmp.Add(GetFourOfAKindScore());
        tmp.Add(GetFullHouseScore());
        tmp.Add(GetSmallStraightScore());
        tmp.Add(GetBigStraightScore());
        tmp.Add(GetYachtScore());
        return tmp;
    }

    private void ResetToInitState() {
        diceResult = new List<int>{ 0, 0, 0, 0, 0 };
        diceResultCount = new List<int>{ 0, 0, 0, 0, 0, 0 };
    }

    private int GetAceScore() => diceResultCount[0];
    private int GetDeucesScore() => diceResultCount[1] * 2;
    private int GetThreesScore() => diceResultCount[2] * 3;
    private int GetFoursScore() => diceResultCount[3] * 4;
    private int GetFivesScore() => diceResultCount[4] * 5;
    private int GetSixesScore() => diceResultCount[5] * 6;
    private int GetChoiceScore() => diceResult.Sum();

    private int GetFourOfAKindScore() {
        int targetIndex = diceResultCount.IndexOf(4);
        return targetIndex != -1 ? (targetIndex + 1) * 4 : 0;
    }

    private int GetSmallStraightScore() {
        bool checkOneToFour = diceResultCount[0] >= 1 && diceResultCount[1] >= 1 && diceResultCount[2] >= 1 && diceResultCount[3] >= 1;
        bool checkTwoToFive = diceResultCount[1] >= 1 && diceResultCount[2] >= 1 && diceResultCount[3] >= 1 && diceResultCount[4] >= 1;
        bool checkThreeToSix = diceResultCount[2] >= 1 && diceResultCount[3] >= 1 && diceResultCount[4] >= 1 && diceResultCount[5] >= 1;
        return checkOneToFour || checkTwoToFive || checkThreeToSix ? 15 : 0;
    }

    private int GetBigStraightScore() {
        bool checkOneToFive = diceResultCount[0] >= 1 && diceResultCount[1] >= 1 && diceResultCount[2] >= 1 && diceResultCount[3] >= 1 && diceResultCount[4] >= 1;
        bool checkTwoToSix = diceResultCount[1] >= 1 && diceResultCount[2] >= 1 && diceResultCount[3] >= 1 && diceResultCount[4] >= 1 && diceResultCount[5] >= 1;
        return checkOneToFive || checkTwoToSix ? 30 : 0;
    }

    private int GetFullHouseScore() {
        int countTwoIndex = diceResultCount.IndexOf(2);
        int countThreeIndex = diceResultCount.IndexOf(3);
        return countTwoIndex != -1 && countThreeIndex != -1 ? ((countTwoIndex + 1)) * 2 + ((countThreeIndex + 1) * 3) : 0;
    }

    private int GetYachtScore() {
        int targetIndex = diceResultCount.FindIndex(x => x == 5);
        return targetIndex != -1 ? 50 : 0;
    }

}
