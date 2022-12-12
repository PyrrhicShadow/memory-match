using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject[] cardPrefab;
    [SerializeField] GameObject[] _cards;
    public GameObject[] cards { get { return _cards;} private set { _cards = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
