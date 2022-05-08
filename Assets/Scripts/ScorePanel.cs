using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {

    [SerializeField] private GameObject scorePanelP1, scorePanelP2;
    
    public int UpdateScore(int player, List<int> score) {
        int total = 0;
        int subTotal = 0;
        bool canGetBonus = false;
        for(int i = 0; i < score.Count; i++){
            if(score[i] < 0) continue;
            UpdateScoreUI(player, i, score[i]);
            total += score[i];
            if(i < 6 && score[i] >= 0) subTotal += score[i];
        }
        if(subTotal >= 63) canGetBonus = true;
        if(canGetBonus) total += 35;
        UpdateScoreUI(player, 12, subTotal);
        UpdateScoreUI(player, 13, canGetBonus ? 35 : 0);
        UpdateScoreUI(player, 14, total);
        return total;
    }

    public void ResetScore() {
        for(int i = 1; i <= 2; i++) {
            for(int j = 0; j < 15; i++) {
                UpdateScoreUI(i, j, -1);
            }
        }
    }

    private void UpdateScoreUI(int player, int category, int score) {
        if(score < 0) return;
        GameObject targetPanel = player == 1 ? scorePanelP1 : scorePanelP2;
        switch(category) {
            case 0:
                targetPanel.transform.Find("Ace").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 1:
                targetPanel.transform.Find("Deuces").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 2:
                targetPanel.transform.Find("Threes").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 3:
                targetPanel.transform.Find("Fours").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 4:
                targetPanel.transform.Find("Fives").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 5:
                targetPanel.transform.Find("Sixes").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 6:
                targetPanel.transform.Find("Choice").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 7:
                targetPanel.transform.Find("FourOfAKind").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 8:
                targetPanel.transform.Find("FullHouse").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 9:
                targetPanel.transform.Find("SmallStraight").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 10:
                targetPanel.transform.Find("BigStraight").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 11:
                targetPanel.transform.Find("Yacht").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 12:
                targetPanel.transform.Find("Subtotal").gameObject.GetComponent<Text>().text = score == -1 ? "-/63" : score.ToString() + "/63";
                break;
            case 13:
                targetPanel.transform.Find("Bonus").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
            case 14:
                targetPanel.transform.Find("Total").gameObject.GetComponent<Text>().text = score == -1 ? "-" : score.ToString();
                break;
        }
    }

}
