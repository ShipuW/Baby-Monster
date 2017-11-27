using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public GameObject levelManager;                   //Store a reference to our BoardManager which will set up the level.
	public GameObject piecesManager;
	public GameObject soundManager;

    //Awake is always called before any Start functions
    void Awake()
    {
		
        //Check if instance already exists
        if (instance == null)
            
            //if not, set instance to this
            instance = this;
        
        //If instance already exists and it's not this:
        else if (instance != this)
            
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
        
        //Call the InitGame function to initialize the first level 
        InitGame();
    }
    
    //Initializes the game for each level.
    void InitGame()
    {



        Instantiate(levelManager);

		if (PiecesManager.instance == null){
			//Instantiate gameManager prefab
			Instantiate(piecesManager);
		}

		if (SoundManager.instance == null){
			//Instantiate gameManager prefab
			Instantiate(soundManager);
		}
    }
    
    
    
    //Update is called every frame.
    void Update()
    {
        
    }
}