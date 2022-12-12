using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI; 

[System.Serializable]
public enum CardType {
    heart, cushion, brilliant, other, eye
}

public class Card : MonoBehaviour {

    [SerializeField] GameObject back; 
    [SerializeField] CardType type; 
    [SerializeField] int value; 
    public string cardName { get { return type.ToString() + " " + value.ToString(); } }
    [SerializeField] Button button; 
    [SerializeField] Animator animator; 
    private InputAction flipAction; 
    private SoundManager sound; 

    // Awake is called before all Start
    void Awake() {

    }


    // Start is called before the first frame update
    void Start() {
        button.enabled = true; 
    }

    public void Flip() {
        button.enabled = false; 
        animator.SetTrigger("flip"); 
        if (sound != null) {
            sound.Flip(); 
        }
    }

    public void Reset() {
        button.enabled = true; 
        // play reset animation
        animator.SetTrigger("unflip"); 
        if (sound != null) {
            sound.Flip(); 
        }
    }

    public void Matched() {
        button.enabled = false; 
        // play matched effect
        if (sound != null) {
            sound.Matched(); 
        }
    }

    public bool Compare(Card other) {
        if (this.type == other.type) {
            if (this.value == other.value) {
                return true; 
            }
            return false; 
        }
        return false; 
    }

    public void SetButton(GameManager manager) {
       button.onClick.AddListener(delegate { manager.AddCard(this); }); 
    }

    public void SetSound(SoundManager manager) {
        sound = manager; 
    }

}
