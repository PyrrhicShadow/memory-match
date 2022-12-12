using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Card : MonoBehaviour {

    [SerializeField] GameObject front; 
    [SerializeField] GameObject back; 
    [SerializeField] PlayerInput input; 
    private InputAction flipAction; 

    // Awake is called before all Start
    void Awake() {
        flipAction = input.actions["Fire"]; 
    }


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

    
    private void OnEnable() {
        flipAction.performed += _ => Flip(); 

    }

    private void OnDisable() {
        flipAction.performed -= _ => Flip(); 
    }
}
