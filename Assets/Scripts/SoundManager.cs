using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager soundManager; 
    [SerializeField] AudioSource background; 
    
    [Header("Game Sounds")]
    [SerializeField] AudioSource flip; 
    [SerializeField] AudioSource matched; 
    [SerializeField] AudioSource win; 
    [SerializeField] AudioSource lose; 
    
    [Header("UI Sounds")]
    [SerializeField] AudioSource click; 
    [SerializeField] AudioSource select; 

    public bool muteEffects { 
        get {
            return (flip.mute && matched.mute && win.mute && lose.mute && click.mute && select.mute); 
        } 
        set {
            flip.mute = value; 
            matched.mute = value; 
            win.mute = value; 
            lose.mute = value; 
            click.mute = value; 
            select.mute = value; 
        } 
    }
    public bool muteMusic {
        get {
            return background.mute; 
        }
        set {
            background.mute = value; 
        }
    }

    void Awake() {
        if (soundManager != null) {
            Destroy(gameObject);  
        }
        soundManager = this; 
        DontDestroyOnLoad(gameObject); 
    }

    // Start is called before the first frame update
    void Start() {
        muteEffects = false; 
        muteMusic = false; 
        background.loop = true; 
        background.Play(); 
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Flip() {
        flip.Play(); 
    }

    public void Matched() {
        matched.Play(); 
    }

    public void Win() {
        win.Play(); 
    }

    public void Lose() {
        lose.Play(); 
    }

    public void Click() {
        click.Play(); 
    }
    
    public void Select() {
        select.Play(); 
    }
}
