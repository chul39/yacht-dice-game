using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerPanel : MonoBehaviour {

    [SerializeField] private GameObject textObject;
    
    public void UpdatePanel(int p1Score, int p2Score) {
        string displayText = "";
        if(p1Score > p2Score) displayText = "P1 Won!";
        else if(p1Score < p2Score) displayText = "P2 Won!";
        else displayText = "DRAW";
        textObject.GetComponent<Text>().text = displayText;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
