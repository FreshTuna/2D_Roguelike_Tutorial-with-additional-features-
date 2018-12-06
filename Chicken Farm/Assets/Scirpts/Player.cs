using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject {

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerGoldenFood = 20;
    public float restartLevelDelay = 1f;
    public Text foodText;
    
    private int food;
    private int chickens;
    private Vector2 touchOrigin = -Vector2.one;


    // Use this for initialization
    protected override void Start()
    {

        food = GameManager.instance.playerFoodPoints;

        foodText.text = "Food : " + food;

        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;

    }

	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

#if  UNITY_EDITOR ||UNITY_STANDALONE || UNITY_WEBPLAYER

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

#else 

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if  (myTouch.phase ==  TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
            }
        }
#endif 

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);

	}

    protected override void AttemptMove <T> (int xDir, int yDir)
    {

        food-=2;

        foodText.text = " Food : " + food;

        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        CheckIfGameOver();

        GameManager.instance.playersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        
       
       
        Wall locationWall = component as Wall;
        Vector2 playerPosition = gameObject.transform.position;
        locationWall.DamageWall(playerPosition,wallDamage );
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Exit")
        {
            
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            chickens++;
            food += pointsPerFood;
            foodText.text = "+" + pointsPerFood ;
            other.gameObject.SetActive(false);

        }
        else if (other.tag == "GoldenFood")
        {
            chickens++;
            food += pointsPerGoldenFood;
            foodText.text = "+" + pointsPerGoldenFood;
            other.gameObject.SetActive(false);
        }
    }

    private void Restart()
    {
        GameManager.instance.CountChickens(chickens);
        Application.LoadLevel(Application.loadedLevel);
        chickens = 0;
    }

    public void LoseFood(int loss)
    {
        food -= loss;
        foodText.text = "-" + loss;
        if(food<=0f)
            GameManager.instance.CountChickens(chickens);
        CheckIfGameOver();
    } 

    private void CheckIfGameOver()
    {
        if (food <= 0f)
        {
            GameManager.instance.Gameover();
        }

        

    }
}

