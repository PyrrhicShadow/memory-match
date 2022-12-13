using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuManager : MonoBehaviour {
    [SerializeField] string sceneToLoad; 
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    [ContextMenu("Play")]
    public void Play() {
        SceneManager.LoadScene(sceneToLoad);
    }
}
