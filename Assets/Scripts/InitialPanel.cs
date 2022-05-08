using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialPanel : MonoBehaviour {

    [SerializeField] private GameObject turnText, playerText;

    public void UpdateUI(int playerNum, int turnNum) {
        playerText.GetComponent<Text>().text = "P" + playerNum.ToString() + " Turn";
        turnText.GetComponent<Text>().text = "Turn " + turnNum.ToString() + "/12";
    }

}
