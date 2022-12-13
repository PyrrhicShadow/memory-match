using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "Deck")]
public class Deck : ScriptableObject {

    public string displayName; 
    public Sprite displayImage; 
    public GameObject[] cards;

    [Header("Accessibility Tags")]
    public bool colorblind; 
}
