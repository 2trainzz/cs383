using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //[SerializeField]
    float speed;
    float height;

    //for user input to move paddles
    string input;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        speed = 8f; 
    }

    public void Init(bool isRightPaddle){

        isRight = isRightPaddle;

        Vector2 pos = Vector2.zero;

        if (isRightPaddle) {
            //place paddle on right
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;  //move a bit to left

            input = "PaddleRight";
        }
        else {
            //place paddle on left
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;  //move a bit to right
        
            input = "PaddleLeft";
        }

        //update the paddle's position
        transform.position = pos;

        //set up inputs
        transform.name = input;
    }
    // Update is called once per frame
    void Update()
    {
        //moving the paddle
        //GetAxis is a number between -1 (down) to 1 (up)
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        //keep paddle from going off screen
        //stop paddle from going too low
        if (transform.position.y < (GameManager.bottomLeft.y + height/2) && move < 0){
            move = 0;
        }
        //stop paddle from going too high
        if (transform.position.y > (GameManager.topRight.y - height/2) && move > 0){
            move = 0;
        }

        //actually move the paddle
        transform.Translate(move * Vector2.up);
    }

    //just resets paddles to the middle of their sides
    public void restartPaddle(bool isRightPaddle){
        Vector2 pos = Vector2.zero;

        if (isRightPaddle) {
            //move right paddle to center right
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;  //move a bit to left

        }
        else {
            //move left paddle to center left
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;  //move a bit to right
   
        }

        //update the paddle's position
        transform.position = pos;
    }
}
