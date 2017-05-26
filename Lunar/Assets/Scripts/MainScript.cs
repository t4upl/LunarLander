using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    public GameObject objectsContainer;
    public GameObject rocket;

    public GameObject FuelSlider;
    public GameObject UIspeedtext;
    public GameObject UIangletext;

    public GameObject scoreText;
    public GameObject hiscoreText;

    public GameObject resultText;

    public GameObject platform;


    int score = 0;

    //mowi czy gra nadal trwa (statek w powiertrzu
    //zapobiega zmianie UI po zkaonczneiu gry
    bool isGameOn = true;


    // Use this for initialization
    void Start () {
        Screen.SetResolution(225, 400, false);

        scaleBackground();
        intializeObjects();
    }

    void intializeObjects()
    {
        rocket.GetComponent<rocketScript2>().reset();
        UIupdateScore();

        isGameOn = true;
        resultText.GetComponent<Text>().text = "";

        resetPlatform();


    }

    void resetPlatform()
    {
        Vector3 position = platform.transform.position;
        Vector3 scale = platform.transform.localScale;


        if (score == 0)
        {
            position.x = 0;
            scale.x = GetComponent<constantScript>().platformWidth;
        }
        else
        {

            Debug.Log("reset");

            position.x = Random.Range(-GetComponent<constantScript>().platformRange, GetComponent<constantScript>().platformRange);

            Debug.Log(position.x);

            scale.x = GetComponent<constantScript>().platformWidth*Mathf.Pow(GetComponent<constantScript>().platformDecreasePercent,score);
        }

        platform.transform.position = position;
        platform.transform.localScale = scale;
    }

    public void updateFuelBar(float fuel)
    {    
        float scaleValue = fuel / GetComponent<constantScript>().fuelMax;

        Vector3 v1 = FuelSlider.GetComponent<RectTransform>().localScale;
        v1.x = scaleValue;
        FuelSlider.GetComponent<RectTransform>().localScale = v1;
        
    }

    public void updateSpeedUI(float speed, float angle)
    {

        if (isGameOn)
        {
            UIspeedtext.GetComponent<Text>().color = Color.black;
            UIspeedtext.GetComponent<Text>().text = string.Format("{0:N2}", speed);// speed;

            if (angle > 90)
            {
                angle = 360 - angle;
            }

            UIangletext.GetComponent<Text>().color = Color.black;
            UIangletext.GetComponent<Text>().text = string.Format("{0:N2}", angle);

            if (Mathf.Abs(speed) > GetComponent<constantScript>().winSpeed)
            {
                UIspeedtext.GetComponent<Text>().color = Color.red;
            }

            if (angle > GetComponent<constantScript>().winAngle)
            {
                UIangletext.GetComponent<Text>().color = Color.red;
            }
        }

        

    }

    void UIupdateScore()
    {
        scoreText.GetComponent<Text>().text = string.Format("{0}", score);

        hiscoreText.GetComponent<Text>().text = string.Format("{0}", GetComponent<constantScript>().highScore);

    }


    void scaleBackground()
    {
        GameObject backGround = GameObject.Find("BackgroundX");

        double widthOfBackGround = backGround.GetComponent<SpriteRenderer>().bounds.size.y;

        backGround.transform.localScale = new Vector3(1, 1, 1);

        var sr = backGround.GetComponent<SpriteRenderer>();
        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        backGround.transform.localScale = new Vector3((float)worldScreenWidth / width, (float)worldScreenHeight / height, 1);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("a"))
        {

            rocket.GetComponent<rocketScript2>().useLeft();

        }


        if (Input.GetKey("d"))
        {

            rocket.GetComponent<rocketScript2>().useRight();
        }


        if (Input.GetKey("w"))
        {
            rocket.GetComponent<rocketScript2>().useUp();
        }

    }

    public void lose()
    {
        if (isGameOn)
        {
            resultText.GetComponent<Text>().text = "LOSE";
            score = 0;
        }
        reloadScene();

    }

    public void hitPlatform()
    {
        if (isGameOn)
        {
            if (UIspeedtext.GetComponent<Text>().color == Color.black && UIangletext.GetComponent<Text>().color == Color.black)
            {
                win();
            }
            else
            {
                lose();
            }

            isGameOn = false;
        }

        reloadScene();


    }

    void win()
    {
        resultText.GetComponent<Text>().text = "WIN";

        Debug.Log("win");
        score++;

        Debug.Log("Score after win " + score);

        if (score > GetComponent<constantScript>().highScore)
        {
            GetComponent<constantScript>().highScore = score;
            GetComponent<constantScript>().saveHIscore();
        }
    }

    void reloadScene()
    {
        Debug.Log("reload scene");
        Invoke("intializeObjects", 2);
    }




}
