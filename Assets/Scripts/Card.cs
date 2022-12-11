using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    [SerializeField] GameObject front; 
    [SerializeField] GameObject back; 

    // Start is called before the first frame update
    void Start() {
        Reset(); 
    }

    public void Flip() {
        back.SetActive(false); 
        front.SetActive(true); 
    }

    public void Reset() {
        back.SetActive(true); 
        front.SetActive(false); 
    }

}
