using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {

    // Game Controller
    [SerializeField] private GameController gameController;

    // Images
    [SerializeField] private GameObject diceImg1, diceImg2, diceImg3, diceImg4, diceImg5;
    private bool selected1, selected2, selected3, selected4, selected5;
    private bool locked1, locked2, locked3, locked4, locked5;

    // Buttons
    [SerializeField] private GameObject btn1, btn2, btn3, btn4, btn5, btn6, btnCh, btn4ok, btnFh, btnSs, btnBs, btnY, btnReroll;

    private List<int> currentDiceResult = new List<int>();
    private List<int> possibleScoreList = new List<int>();
    private List<int> currentPlayerScore = new List<int>();
    private List<int> confirmedFaceList = new List<int>();

    private int tryLeft;

    public void Reroll() {
        if(!selected1) currentDiceResult[0] = -1;
        else confirmedFaceList.Add(currentDiceResult[0]);
        if(!selected2) currentDiceResult[1] = -1;
        else confirmedFaceList.Add(currentDiceResult[1]);
        if(!selected3) currentDiceResult[2] = -1;
        else confirmedFaceList.Add(currentDiceResult[2]);
        if(!selected4) currentDiceResult[3] = -1;
        else confirmedFaceList.Add(currentDiceResult[3]);
        if(!selected5) currentDiceResult[4] = -1;
        else confirmedFaceList.Add(currentDiceResult[4]);
        gameController.GetComponent<GameController>().Reroll(currentDiceResult);
    }

    public void UpdateScore(int x) {
        currentPlayerScore[x] = possibleScoreList[x];
        gameController.GetComponent<GameController>().EndTurn(currentPlayerScore);
    }

    public void ToggleSelect(int slot) {
        if(slot > 4) return;
        GameObject targetGameObject = null;
        bool newState = false;
        switch(slot) {
            case 0:
                if(locked1) return;
                targetGameObject = diceImg1;
                selected1 = !selected1;
                newState = selected1;
                break;
            case 1:
                if(locked2) return;
                targetGameObject = diceImg2;
                selected2 = !selected2;
                newState = selected2;
                break;
            case 2:
                if(locked3) return;
                targetGameObject = diceImg3;
                selected3 = !selected3;
                newState = selected3;
                break;
            case 3:
                if(locked4) return;
                targetGameObject = diceImg4;
                selected4 = !selected4;
                newState = selected4;
                break;
            case 4:
                if(locked5) return;
                targetGameObject = diceImg5;
                selected5 = !selected5;
                newState = selected5;
                break;
        }
        Image image = targetGameObject.transform.Find("Face" + currentDiceResult[slot]).gameObject.GetComponent<Image>();
        float newAlpha = newState ? 0.2f : 1f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
        btnReroll.GetComponent<Button>().interactable = (tryLeft > 0) && !(selected1 && selected2 && selected3 && selected4 && selected5);
    }

    private void ResetSelectedDiceFace() {
        selected1 = false;
        selected2 = false;
        selected3 = false;
        selected4 = false;
        selected5 = false;
        locked1 = false;
        locked2 = false;
        locked3 = false;
        locked4 = false;
        locked5 = false;
        for(int i = 0; i < 6; i++) {
            Image image1 = diceImg1.transform.Find("Face" + (i + 1)).gameObject.GetComponent<Image>();
            image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 1f);
            Image image2 = diceImg2.transform.Find("Face" + (i + 1)).gameObject.GetComponent<Image>();
            image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, 1f);
            Image image3 = diceImg3.transform.Find("Face" + (i + 1)).gameObject.GetComponent<Image>();
            image3.color = new Color(image3.color.r, image3.color.g, image3.color.b, 1f);
            Image image4 = diceImg4.transform.Find("Face" + (i + 1)).gameObject.GetComponent<Image>();
            image4.color = new Color(image4.color.r, image4.color.g, image4.color.b, 1f);
            Image image5 = diceImg5.transform.Find("Face" + (i + 1)).gameObject.GetComponent<Image>();
            image5.color = new Color(image5.color.r, image5.color.g, image5.color.b, 1f);
        }
        btnReroll.GetComponent<Button>().interactable = false;
    }

    public void UpdatePanel(List<int> diceResult, List<int> currentScore, List<int> potentialScore, int tryCount) {
        ResetSelectedDiceFace();
        currentDiceResult = diceResult;
        currentPlayerScore = currentScore;
        possibleScoreList = potentialScore;
        UpdateRerollButton(tryCount);
        UpdateScoreButtons();
        SelectDisplayFace(diceImg1, diceResult[0]);
        SelectDisplayFace(diceImg2, diceResult[1]);
        SelectDisplayFace(diceImg3, diceResult[2]);
        SelectDisplayFace(diceImg4, diceResult[3]);
        SelectDisplayFace(diceImg5, diceResult[4]);
        UpdateFaceDisplay();
        UpdateButtonInteractability();
    }

    private void UpdateFaceDisplay() {
        if(confirmedFaceList.IndexOf(currentDiceResult[0]) != -1) {
            ToggleSelect(0);
            locked1 = true;
            confirmedFaceList.Remove(currentDiceResult[0]);
        }
        if(confirmedFaceList.IndexOf(currentDiceResult[1]) != -1) {
            ToggleSelect(1);
            locked2 = true;
            confirmedFaceList.Remove(currentDiceResult[1]);
        }
        if(confirmedFaceList.IndexOf(currentDiceResult[2]) != -1) {
            ToggleSelect(2);
            locked3 = true;
            confirmedFaceList.Remove(currentDiceResult[2]);
        }
        if(confirmedFaceList.IndexOf(currentDiceResult[3]) != -1) {
            ToggleSelect(3);
            locked4 = true;
            confirmedFaceList.Remove(currentDiceResult[3]);
        }
        if(confirmedFaceList.IndexOf(currentDiceResult[4]) != -1) {
            ToggleSelect(4);
            locked5 = true;
            confirmedFaceList.Remove(currentDiceResult[4]);
        }
    }

    private void UpdateRerollButton(int num) {
        tryLeft = 3 - num;
        btnReroll.GetComponent<Button>().interactable = tryLeft > 0;
        btnReroll.GetComponentInChildren<Text>().text = "Reroll (" + tryLeft + " left)";
    }

    private void SelectDisplayFace(GameObject obj, int num) {
        for(int i = 0; i < 6; i++) {
            GameObject targetImageObject = obj.transform.Find("Face" + (i + 1)).gameObject;
            bool targetState = ((i+1) == num);
            targetImageObject.SetActive(targetState);
        }
    }

    private void UpdateButtonInteractability() {
        btn1.GetComponent<Button>().interactable = currentPlayerScore[0] < 0;
        btn2.GetComponent<Button>().interactable = currentPlayerScore[1] < 0;
        btn3.GetComponent<Button>().interactable = currentPlayerScore[2] < 0;
        btn4.GetComponent<Button>().interactable = currentPlayerScore[3] < 0;
        btn5.GetComponent<Button>().interactable = currentPlayerScore[4] < 0;
        btn6.GetComponent<Button>().interactable = currentPlayerScore[5] < 0;
        btnCh.GetComponent<Button>().interactable = currentPlayerScore[6] < 0;
        btn4ok.GetComponent<Button>().interactable = currentPlayerScore[7] < 0;
        btnFh.GetComponent<Button>().interactable = currentPlayerScore[8] < 0;
        btnSs.GetComponent<Button>().interactable = currentPlayerScore[9] < 0;
        btnBs.GetComponent<Button>().interactable = currentPlayerScore[10] < 0;
        btnY.GetComponent<Button>().interactable = currentPlayerScore[11] < 0;
    }

    private void UpdateScoreButtons() {
        btn1.GetComponentInChildren<Text>().text = "+" + possibleScoreList[0];
        btn2.GetComponentInChildren<Text>().text = "+" + possibleScoreList[1];
        btn3.GetComponentInChildren<Text>().text = "+" + possibleScoreList[2];
        btn4.GetComponentInChildren<Text>().text = "+" + possibleScoreList[3]; 
        btn5.GetComponentInChildren<Text>().text = "+" + possibleScoreList[4];
        btn6.GetComponentInChildren<Text>().text = "+" + possibleScoreList[5]; 
        btnCh.GetComponentInChildren<Text>().text = "+" + possibleScoreList[6]; 
        btn4ok.GetComponentInChildren<Text>().text = "+" + possibleScoreList[7]; 
        btnFh.GetComponentInChildren<Text>().text = "+" + possibleScoreList[8];
        btnSs.GetComponentInChildren<Text>().text = "+" + possibleScoreList[9];
        btnBs.GetComponentInChildren<Text>().text = "+" + possibleScoreList[10];
        btnY.GetComponentInChildren<Text>().text = "+" + possibleScoreList[11];
    }

}
