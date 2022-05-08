using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour {
    
    [SerializeField] private GameObject turnText;

    public void UpdateStatusPanel(int turnPlayer, int turnCount) {
        turnText.GetComponent<Text>().text = "(P" + turnPlayer.ToString() + ") Turn " + turnCount.ToString() + "/12";
    }

}
