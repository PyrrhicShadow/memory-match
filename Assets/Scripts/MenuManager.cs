using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class MenuManager : MonoBehaviour {
    [SerializeField] string sceneToLoad; 
    [SerializeField] World world; 
    private SoundManager sound; 

    [Header("Start Page Objects")]
    [SerializeField] Text difficultyText; 
    [SerializeField] Image deckImage; 
    [SerializeField] Text deckText; 
    private int currentMode = 0; 
    private int currentDeck = 0; 

    // Start is called before the first frame update
    void Start() {
        sound = GameObject.FindWithTag("SoundController").GetComponent<SoundManager>(); 

        currentMode = PlayerPrefs.GetInt("game mode", 0); 
        currentDeck = PlayerPrefs.GetInt("deck", 0); 
        SetMode(1); 
        SetDeck(1);  
    }

    // Update is called once per frame
    void Update() {
        
    }

    [ContextMenu("Play")]
    public void Play() {
        if (sound != null) {
            sound.Select(); 
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    /**     Difficulty Scroll Bar     **/
    // cycle between easy-medium-hard-easy-etc 

    public void PrevMode() {
        if (currentMode == 0) {
            currentMode = world.gameModes.Length - 1; 
        }
        else {
            currentMode--; 
        }

        SetMode(-1); 
        SetDeck(-1); 
    }

    public void NextMode() {
        if (currentMode == world.gameModes.Length - 1) {
            currentMode = 0; 
        }
        else { 
            currentMode++; 
        }

        SetMode(1); 
        SetDeck(1); 
    }

    /**     Deck Scroll Bar     **/
    // cycle betwween available decks for this diffuculty 

    public void PrevDeck() {
        if (currentDeck == 0) {
            currentDeck = world.decks.Length - 1; 
        }
        else { 
            currentDeck--; 
        }

        SetDeck(-1); 
    }

    public void NextDeck() {
        if (currentDeck == world.decks.Length - 1) {
            currentDeck = 0; 
        }
        else { 
            currentDeck++; 
        }

        SetDeck(1); 
    }

    private void SetMode(int dir) {
        // play button sound 
        if (sound != null) {
            sound.Click(); 
        }
        // change text 
        difficultyText.text = world.gameModes[currentMode].displayName; 
        // save difficulty 
        PlayerPrefs.SetInt("game mode", (int)currentMode); 
    }

    private void SetDeck(int dir) {
        if (world.gameModes[currentMode].boardSize > world.decks[currentDeck].cards.Length - 1) {
            if (dir > 0) {
                NextDeck(); 
            }
            else {
                PrevDeck(); 
            }
        }
        else {
            // play button sound 
            if (sound != null) {
                sound.Click(); 
            }
            // change text 
            deckText.text = world.decks[currentDeck].displayName; 
            // change image 
            // deckImage.sprite = world.decks[currentDeck].displayImage; 
            // save deck 
            PlayerPrefs.SetInt("deck", currentDeck); 
        }
    }

}
