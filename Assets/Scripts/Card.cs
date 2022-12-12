using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI; 

public class Card : MonoBehaviour {

    [SerializeField] GameObject back; 
    [SerializeField] Button _button; 
    public Button button { get { return _button;} private set { _button = value; } }
    [SerializeField] Animator animator; 
    private InputAction flipAction; 

    // Awake is called before all Start
    void Awake() {

    }


    // Start is called before the first frame update
    void Start() {
        button.enabled = true; 
    }

    public void Flip() {
        animator.SetTrigger("flip"); 
    }

    public void Reset() {
        // play reset animation
        animator.SetTrigger("unflip"); 
    }

    public void Matched() {
        button.enabled = false; 
        // play matched effect
    }

    public void SetButton(GameManager manager) {
       button.onClick.AddListener(delegate { manager.AddCard(this); }); 
    }

}
