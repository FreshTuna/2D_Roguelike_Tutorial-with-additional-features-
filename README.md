# 2D_Roguelike_Tutorial-with-additional-features-
Followed 2D Roguelike tutorial, and made some additional features. 

I made a game named ChickenFarm by following a tutorial at Unity homepage

Url: 
https://unity3d.com/kr/learn/tutorials/s/2d-roguelike-tutorial

I made some changes to the original game

***First of all,*** I made two kinds of Walls

One is breakable as normal ,
and the other one is movable.


```c#
private bool CheckWall(Vector3 CheckingLocation)
    {
       
        Walls = GameObject.FindGameObjectsWithTag("Wall");
        for(int i=0; i<Walls.Length; i++)
        {
            if (Vector3.Distance(Walls[i].transform.position,CheckingLocation)==0)
            {
                return false;
            }
        }
```
```c#
 Vector2 locationWall = gameObject.transform.position;

        float x = pushingDir.x - locationWall.x;
        float y = pushingDir.y - locationWall.y;


        if (gameObject.transform.name == "Wall2(Clone)")
        {
            if (y == 0)
            {
                if (x > 0)
                    if (CheckWall(locationWall - new Vector2(1, 0)))
                        gameObject.transform.position = locationWall - new Vector2(1, 0);
                    else
                        gameObject.transform.position = locationWall;
                else
                        if (CheckWall(locationWall + new Vector2(1, 0)))
                    gameObject.transform.position = locationWall + new Vector2(1, 0);
                else
                    gameObject.transform.position = locationWall;
            }
            else
            {
                if (y > 0)
                    if (CheckWall(locationWall - new Vector2(0, 1)))
                        gameObject.transform.position = locationWall - new Vector2(0, 1);
                    else
                        gameObject.transform.position = locationWall;
                else
                        if (CheckWall(locationWall + new Vector2(0, 1)))
                    gameObject.transform.position = locationWall + new Vector2(0, 1);
                else
                    gameObject.transform.position = locationWall;
            }
```




***Second,*** I made a special type of food to the game

There are two kinds of chickens a normal one and a Golden one.
The Golden one only appears after Day 4 and it gives extra food points. 



```c#
 public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        int GoldenChickenCount = (int)Mathf.Log(level, 4f);
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        LayoutObjectAtRandom(GoldenfoodTiles, GoldenChickenCount, GoldenChickenCount);
        int enemyCount = (int)Mathf.Log(level, 2f);
      
      
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        
        Instantiate(Exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
    }
```

I'm still working on making a restart button and a quit button after gameover.

Thank you for visiting my Git. 
