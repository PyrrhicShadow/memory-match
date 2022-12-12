using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject[] cardPrefabs;
    [SerializeField] GameObject layoutGroup; 
    [SerializeField] int boardSize; 
    [SerializeField] List<Card> _hand; 
    public List<Card> hand { get { return _hand; } private set { _hand = value; } }
    private int matches; 

    // Start is called before the first frame update
    void Start()
    {
        matches = 0; 
        hand = new List<Card>(); 
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
    void Update()
    {

    }

    public void AddCard(Card newCard) {
        if (hand.Count < 2) {
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
            Debug.Log("You win!"); 
        }
    }

    private void ResetHand() {
        foreach (Card c in hand) {
            c.Reset(); 
        }
    }

}
