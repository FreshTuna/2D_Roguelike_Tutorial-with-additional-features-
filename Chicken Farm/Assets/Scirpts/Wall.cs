using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour {

    public Sprite dmgSprite;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;
    private int index = 0;
    private GameObject[] Walls;
    private GameObject whichWall;

    public LayerMask blockingLayer;

    void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();	
	}
	

	// Update is called once per frame
	public void DamageWall (Vector2 pushingDir,int  loss)
    {

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
        }
        else if (gameObject.transform.name == "Wall1(Clone)")
        {
            hp -= loss;
            if (hp <= 0)
                gameObject.SetActive(false);
        }

        spriteRenderer.sprite = dmgSprite;
        

    }

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
        return true;
        
        
    }
}
