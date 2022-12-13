using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

[System.Serializable]
public enum GameState {
    move, pause, end 
}

public class GameManager : MonoBehaviour {

    [SerializeField] SoundManager sound; 
    [SerializeField] World world; 

    [Header("Board")]
    [SerializeField] GameObject[] cardPrefabs;
    [SerializeField] int boardSize; 
    [SerializeField] GameObject layoutGroup; 
    [SerializeField] List<Card> _hand; 
    public List<Card> hand { get { return _hand; } private set { _hand = value; } }
    [SerializeField] GameState _state = GameState.pause; 
    public GameState state { get { return _state; } private set { _state = value; } }
    [SerializeField] Image clock; 
    [SerializeField] Text time; 
    [SerializeField] Text highScore; 
    private int matches; 
    private float timer; 
    [SerializeField] int timeGoal; 
    private int counter; 
    private float menuDelay = 1f; 

    [Header("Menu")]
    [SerializeField] string menuSceneName; 
    [SerializeField] GameObject startPanel; 
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject winPanel; 
    [SerializeField] GameObject losePanel;  
    [SerializeField] GameObject pausePanel; 
    [SerializeField] Text finalScore; 
    [SerializeField] GameObject newHighScore; 

    void Awake() {
        if (world != null) {
            GameMode mode = world.gameModes[PlayerPrefs.GetInt("game mode", 0)]; 
            timeGoal = mode.time; 
            boardSize = mode.boardSize; 
            cardPrefabs = world.decks[PlayerPrefs.GetInt("deck", 0)].cards; 
        }
    }

    // Start is called before the first frame update
    void Start() {
        matches = 0; 
        counter = timeGoal; 
        hand = new List<Card>(); 

        startPanel.SetActive(true); 
        winPanel.SetActive(false); 
        losePanel.SetActive(false);
        endPanel.SetActive(false); 
        pausePanel.SetActive(false); 
        newHighScore.SetActive(false); 

        time.text = counter.ToString(); 
        highScore.text = PlayerPrefs.GetInt("Memory " + world.gameModes[PlayerPrefs.GetInt("game mode", 0)].displayName, 0).ToString(); 
        SetUp(); 
    }

    void SetUp() {
        int randCard = 0; 
        List<GameObject> prefabsCopy = new List<GameObject>(cardPrefabs);
        List<GameObject> board = new List<GameObject>(); 

        // Add cards to the board in pairs
        for (int i = 0; i < boardSize; i++) {
            // pick a random card from the list of cards 
            randCard = Random.Range(0, prefabsCopy.Count); 
            GameObject prefab = prefabsCopy[randCard]; 

            // create a pair of cards
            GameObject card1 = Instantiate(prefab); 
            GameObject card2 = Instantiate(prefab); 

            // set up cards 
            card1.GetComponent<Card>().SetButton(this); 
            card2.GetComponent<Card>().SetButton(this); 

            card1.GetComponent<Card>().SetSound(sound); 
            card2.GetComponent<Card>().SetSound(sound); 

            // add the pair to the temp board
            board.Add(card1); 
            board.Add(card2); 

            // remove this card from the prefab copy
            prefabsCopy.Remove(prefab); 
        }

        // Randomly place the cards into the layout
        for (int k = 0; k < (boardSize * 2); k++) {
            randCard = Random.Range(0, board.Count); 
            GameObject card = board[randCard]; 
            card.transform.SetParent(layoutGroup.transform); 
            card.transform.localScale = Vector3.one; 
            board.Remove(card); 
        }


    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime; 
        if (timer <= 0) {
            UpdateTimer(); 
            timer = 1; 
        }
    }

    public void AddCard(Card newCard) {
        if (hand.Count < 2 && state == GameState.move) {
            hand.Add(newCard); 
            newCard.Flip();  
        }

        if (hand.Count >= 2) {
            CompareCards(); 
        }
    }

    private bool CompareCards() {
        bool matched = false; 
        if (hand[0].Compare(hand[1])) {
            matched = true; 
            MatchedHand(); 
        }
        else {
            ResetHand(); 
        }
        hand.Clear(); 
        return matched; 
    }

    private void MatchedHand() {
        foreach (Card c in hand) {
            c.Matched(); 
        }
        matches++; 
        if (matches == boardSize) {
            // You win!
            WinGame(); 
        }
    }

    private void ResetHand() {
        foreach (Card c in hand) {
            c.Reset(); 
        }
    }

    [ContextMenu("Play")]
    public void PlayGame() {
        startPanel.SetActive(false); 
        state = GameState.move; 
    }

    [ContextMenu("Pause")]
    public void PauseGame() {
        if (state == GameState.pause) {
            state = GameState.move; 
            pausePanel.SetActive(false);
        }
        else {
            state = GameState.pause; 
            pausePanel.SetActive(true); 
        }
    }

    private void WinGame() {
        Debug.Log("You win!"); 
        HighScore(); 
        state = GameState.end; 
        sound.Win(); 
        winPanel.SetActive(true); 
        StartCoroutine(SummonPanel(endPanel)); 
    }

    private void LoseGame() {
        Debug.Log("You Lose :("); 
        state = GameState.end; 
        sound.Lose(); 
        losePanel.SetActive(true); 
        StartCoroutine(SummonPanel(endPanel)); 
    }

    private void UpdateTimer() {
        if (state == GameState.move) {
            counter--; 
            clock.fillAmount = (float)counter / (float)timeGoal; 
            time.text = counter.ToString(); 
            if (counter <= 0) {
                LoseGame(); 
            }
        }
    }

    [ContextMenu("Play Again")]
    public void PlayAgain() {
        // reload this level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    [ContextMenu("Main Menu")]
    public void MainMenu() {
        SceneManager.LoadScene(menuSceneName); 
    }

    private IEnumerator SummonPanel(GameObject panel) {
        yield return new WaitForSeconds(menuDelay); 
        panel.SetActive(true); 
    }

    private void HighScore() {
        int prevScore = PlayerPrefs.GetInt("Memory " + world.gameModes[PlayerPrefs.GetInt("game mode", 0)].displayName, 100000);
        int currentScore = timeGoal - counter; 

        if (currentScore < prevScore) {
            PlayerPrefs.SetInt("Memory " + world.gameModes[PlayerPrefs.GetInt("game mode", 0)].displayName, currentScore); 
            newHighScore.SetActive(true); 
        }

        finalScore.text = currentScore.ToString(); 

    }
}
