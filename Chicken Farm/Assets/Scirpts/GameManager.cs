using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public float turnDelay = .1f; 
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private GameObject gameQuit;
    private GameObject gameRestart;
    private GameObject GoldenChickens;
    private Text levelText;
    private GameObject levelImage;
    private int level = 1;
    private int chickens = 0;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    
	// Use this for initialization
	void Awake ()
    {
        

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
       
        InitGame();
	}

    private void OnLevelWasLoaded(int index)
    {
        level++;

        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;
        gameQuit = GameObject.Find("QuitButton");
        gameRestart = GameObject.Find("RestartButton");
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        gameRestart.SetActive(false);
        gameQuit.SetActive(false);
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay); 

        enemies.Clear();
        boardScript.SetupScene(level);
        

    }



    public void CountChickens(int chickensAmount)
    {
        chickens = chickens + chickensAmount;
    }

    public void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
        //enabled = false;
    }

    public void HideLevelText()
    {
        levelText.text = " ";
        gameRestart.SetActive(true);
        gameQuit.SetActive(true);

    }

    public void Gameover()
    {
        levelText.text = "You Saved " + chickens +" chickens for "+ level + " days";
        levelImage.SetActive(true);
        doingSetup = true;
        Invoke("HideLevelText", levelStartDelay);
        
        

    }
	
	// Update is called once per frame
	void Update () {
        if (playersTurn || enemiesMoving || doingSetup )
            return;
        StartCoroutine(MoveEnemies());
	}
        
    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script); 
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }
        for(int i=0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);

        }
        playersTurn = true;
        enemiesMoving = false;
    }

}
