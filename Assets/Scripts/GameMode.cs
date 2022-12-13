using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "World", menuName = "Game Mode")]
public class GameMode : ScriptableObject {
    
    public string displayName; 
    public int boardSize; 
    public int time; 
}
