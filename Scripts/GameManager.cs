using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //instances for ball and paddle
    public Ball ball;
    public Paddle paddle;

    //actual paddle instances
    private Paddle paddle1;
    private Paddle paddle2;

    //vectors for screen position references
    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    //texts to show the score
    public TextMeshProUGUI scoreTextLeft, scoreTextRight;

    //screen and text for win message
    public GameObject WinPanel;
    public TextMeshProUGUI winMessage;



    // Start is called before the first frame update
    void Start()
    {
        //set the bottom left point
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0,0)); 
        //set the top right point
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        //Create the ball
        ball = Instantiate(ball) as Ball;

        //Create two paddles
        paddle1 = Instantiate(paddle) as Paddle;
        paddle2 = Instantiate(paddle) as Paddle;
        paddle1.Init(true); //right paddle
        paddle2.Init(false); //left paddle

        //make sure winPanel is not on
        WinPanel.SetActive(false);
    }

    void Update(){
        //lets player quit at anytime with the escape key
         //if (Input.GetKey("escape"))
         if (Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 0;
            Application.Quit();
            enabled = false;
         }
    }

    //updates the text score on screen
    //called in ball script whenever there is a score
    public void UpdateScore(int p1Score, int p2Score){
        scoreTextRight.text=p1Score.ToString();
        scoreTextLeft.text=p2Score.ToString();
    }

    //pauses game, puts WinPanel up when someone wins. Called from ball script
    public void GameWon(int winner){
        //pull panel up
        WinPanel.SetActive(true);
        //make sure all objects stop
        ball.speed = 0;
        ball.gameObject.SetActive(false);
        paddle1.gameObject.SetActive(false);
        paddle2.gameObject.SetActive(false);
        //freeze game
        Time.timeScale = 0;

        //print the correct winner message
        if (winner == 1){
            winMessage.text = "Right wins!";
        } else {
            winMessage.text = "Left wins!";
        }

        //user is able to press button to restart
        
    }

    //set up in Unity, user can hit button to call Restart()
    public void restartButton(){
        Restart();
    }

    //restarts the game
    void Restart(){
        //take away winner panel
        WinPanel.SetActive(false);
        winMessage.text = "";

        //reset the scores (and unpause in ball script)
        ball.resetScore();
        //clear score texts
        scoreTextLeft.text = "0";
        scoreTextRight.text = "0";

        //re enable the objects
        ball.gameObject.SetActive(true);
        paddle1.gameObject.SetActive(true);
        paddle2.gameObject.SetActive(true);

        //reset positions of the objects
        ball.resetBall();
        paddle1.restartPaddle(true);
        paddle2.restartPaddle(false);

        //unfreeze the game
        Time.timeScale = 1;
    }

}
