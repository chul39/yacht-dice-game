using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceChecker : MonoBehaviour {
    
    [SerializeField] private GameObject diceObj1, diceObj2, diceObj3, diceObj4, diceObj5;
    private Dice dice1, dice2, dice3, dice4, dice5;

    private void Start() {
        dice1 = diceObj1.GetComponent<Dice>();
        dice2 = diceObj2.GetComponent<Dice>();
        dice3 = diceObj3.GetComponent<Dice>();
        dice4 = diceObj4.GetComponent<Dice>();
        dice5 = diceObj5.GetComponent<Dice>();
    }

    private void SetDiceNumber(int diceNum, int val) {
        Vector3 diceVelocity = new Vector3();
        switch(diceNum) {
            case 1:
                diceVelocity = dice1.GetDiceVelocity();
                if(diceVelocity.x == 0 && diceVelocity.y == 0 && diceVelocity.z == 0) dice1.SetFinalNumber(val);
                break;
            case 2:
                diceVelocity = dice2.GetDiceVelocity();
                if(diceVelocity.x == 0 && diceVelocity.y == 0 && diceVelocity.z == 0) dice2.SetFinalNumber(val);
                break;
            case 3:
                diceVelocity = dice3.GetDiceVelocity();
                if(diceVelocity.x == 0 && diceVelocity.y == 0 && diceVelocity.z == 0) dice3.SetFinalNumber(val);
                break;
            case 4:
                diceVelocity = dice4.GetDiceVelocity();
                if(diceVelocity.x == 0 && diceVelocity.y == 0 && diceVelocity.z == 0) dice4.SetFinalNumber(val);
                break;
            case 5:
                diceVelocity = dice5.GetDiceVelocity();
                if(diceVelocity.x == 0 && diceVelocity.y == 0 && diceVelocity.z == 0) dice5.SetFinalNumber(val);
                break;
        }
    }

    private int GetDiceSideNumber(int num) {
        switch(num) {
            case 1: return 6;
            case 2: return 5;
            case 3: return 4;
            case 4: return 3;
            case 5: return 2;
            case 6: return 1;
            default: return -1;
        }
    }

    private void OnTriggerStay(Collider col){
        if(col.gameObject.tag == "DiceSide") {
            GameObject parentObject = col.gameObject.transform.parent.gameObject;
            int diceObjNum = int.Parse(parentObject.name.Substring(parentObject.name.Length - 1));
            int collidedSideNum = int.Parse(col.gameObject.name.Substring(col.gameObject.name.Length - 1));
            int sideNum = GetDiceSideNumber(collidedSideNum);
            SetDiceNumber(diceObjNum, sideNum);
        }
    }

}
