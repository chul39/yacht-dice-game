using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Rigidbody rigidBody;
    private Vector3 diceVelocity;
    [SerializeField] private int finalNumber = -1;

    private void Update() {
        if(rigidBody != null) diceVelocity = rigidBody.velocity;
    }

    public Vector3 GetDiceVelocity() => diceVelocity;
    
    public void SetFinalNumber(int num) => finalNumber = num;
    public int GetFinalNumber() => finalNumber;

    public void Roll(int diceNum) {
        finalNumber = -1;
        rigidBody = GetComponent<Rigidbody>();
        float dirX = Random.Range (0, 500);
        float dirY = Random.Range (0, 500);
        float dirZ = Random.Range (0, 500);
        transform.position = GetStartPosition(diceNum);
        transform.rotation = Random.rotation;
        rigidBody.AddForce(transform.up * 500);
        rigidBody.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    private Vector3 GetStartPosition(int diceNum) {
        switch(diceNum) {
            case 1: return new Vector3(-1.5f, 3, 1.5f);
            case 2: return new Vector3(1.5f, 3, -1.5f);
            case 3: return new Vector3(1.5f, 3, 1.5f);
            case 4: return new Vector3(-1.5f, 3, -1.5f);
            default: return new Vector3(0, 3, 0);
        }
    } 

}
