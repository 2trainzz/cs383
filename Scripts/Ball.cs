using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    //speed of ball
    public float speed = 15;

    float radius;

    //variables to hold scores
    public static int p1Score = 0, p2Score = 0;

    //to reference gameManager
    public GameManager gameManager;
    public GameObject gameManagerObject;

    //to help halt ball script
    public bool isPaused;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {

        direction = Vector2.one.normalized; // direction is (1,1) normalized
        radius = transform.localScale.x/2;  //half width

        //set up gameManager reference object
        gameManagerObject = GameObject.FindWithTag("GameController");
        
        if (gameManagerObject != null){
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        else {
            Debug.Log("uh oh! Game Manager not found");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        
        //change direction when it hits the top/bottom
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y <0){
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0){
            direction.y = -direction.y;
        }

        //add points for out of bounds
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0 && !isPaused){
            //update p1 (right player) score
            p1Score += 1;
            Debug.Log("Point for right");

            //send score to gameManager to update on screen
            gameManager.UpdateScore(p1Score, p2Score);

            //check if someone has won
            if (p1Score >= 10){
                Debug.Log("Right player wins");
                Debug.Log(string.Format("against left: {0}", p2Score));
                //specify who won to gameManager
                gameManager.GameWon(1);
                isPaused = true;  
            }
            else {
                //reset ball to center for next point
                resetBall();
            }
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0 && !isPaused){
            //update pLeft score
            p2Score += 1;
            gameManager.UpdateScore(p1Score, p2Score);
            Debug.Log("Point for Left");
            
            //check if Left won
            if (p2Score >= 10){
                Debug.Log("Left player wins");
                gameManager.GameWon(2);
                isPaused = true;
            }
            else {
                //reset ball for next point
                resetBall();
            }
        }
    }

    //for changing directions when hitting other objects (paddles)
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Paddle"){
            bool isRight = other.GetComponent<Paddle>().isRight;

            //reverse direction when hitting paddles
            if (isRight == true && direction.x > 0){
                direction.x = -direction.x;
            }
            if (isRight == false && direction.x < 0){
                direction.x = -direction.x;
            }
        }
    }

    //resets ball to center
    public void resetBall(){
        //reset direction & speed
        speed = 15f; 
        direction = Vector2.one.normalized;
        //move the ball back to center
        transform.position = new Vector2(0,0);
    }

    //resets score and ensures ball script knows it is unpaused
    public void resetScore(){
        Debug.Log("scores reset");
        p1Score = 0;
        p2Score = 0;
        isPaused = false;
    }
}
