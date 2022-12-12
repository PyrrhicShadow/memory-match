using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public enum GameState {
    move, pause, end 
}

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject[] cardPrefabs;
    [SerializeField] GameObject layoutGroup; 
    [SerializeField] int boardSize; 
    [SerializeField] List<Card> _hand; 
    public List<Card> hand { get { return _hand; } private set { _hand = value; } }
    [SerializeField] GameState _state = GameState.pause; 
    public GameState state { get { return _state; } private set { _state = value; } }
    [SerializeField] Image clock; 
    [SerializeField] Text time; 
    private int matches; 
    private float timer; 
    [SerializeField] int timeGoal; 
    private int counter; 
    [SerializeField] GameObject startPanel; 
    [SerializeField] Animator endAnimator; 
    [SerializeField] Animator pauseAnimator; 

    // Start is called before the first frame update
    void Start() {
        matches = 0; 
        counter = timeGoal; 
        hand = new List<Card>(); 
        startPanel.SetActive(true); 
        SetUp(); 
    }

    void SetUp() {
        int randCard = 0; 
        GameObject card = null; 
        List<GameObject> board = new List<GameObject>(); 

        // Add cards to the board in pairs
        for (int i = 0; i < cardPrefabs.Length; i++) {
            // card 1
            card = Instantiate(cardPrefabs[i]); 
            card.GetComponent<Card>().SetButton(this); 
            board.Add(card); 
            // card 2
            card = Instantiate(cardPrefabs[i]); 
            card.GetComponent<Card>().SetButton(this); 
            board.Add(card); 
        }

        // Randomly place the cards into the layout
        for (int k = 0; k < (cardPrefabs.Length * 2); k++) {
            randCard = Random.Range(0, board.Count); 
            card = board[randCard]; 
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
        if (hand[0].CompareTag(hand[1].tag)) {
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
        if (matches == cardPrefabs.Length) {
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
            // pauseAnimator.SetTrigger("unpause");
        }
        else {
            state = GameState.pause; 
            // pauseAnimator.SetTrigger("pause"); 
        }
    }

    private void WinGame() {
        Debug.Log("You win!"); 
        // menuAnimator.SetTrigger("win"); 
        state = GameState.end; 
    }

    private void LoseGame() {
        Debug.Log("You Lose :("); 
        // menuAnimator.SetTrigger("lose"); 
        state = GameState.end; 
    }

    private void UpdateTimer() {
        if (state == GameState.move) {
            counter--; 
            clock.fillAmount = (float)counter / (float)timeGoal; 
            time.text = counter.ToString(); 
        }

        if (counter <= 0) {
            LoseGame(); 
        }
    }

}
