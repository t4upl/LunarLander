using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


public class constantScript : MonoBehaviour {

    public float thrustPower=100;
    public float fuelMax=100;

    //uaage per frame of pushed button (both for up and left/right)
    public float fuelUsage;

    [HideInInspector]
    public float sidePower;

    public float winSpeed;
    public float winAngle;
    public float highScore;

    public float gravity;
    public float rocketMass;

    string jsonFileName= "standardSettings";

    public float platformWidth;
    public float platformRange;

    //with each score platform width = widthAtTheBegining * platformDecreasePercent^score
    public float platformDecreasePercent;

    private void Awake()
    {
        highScore = 0;

        thrustPower = 20;
        fuelMax = 100;
        sidePower = 1f;
        fuelUsage = 0.05f;

        winSpeed = 4f;
        winAngle = 12f;

        gravity = 4;
        rocketMass = 1;

        platformWidth = 2;
        platformRange = 1.6f;
        platformDecreasePercent = 0.75f;



        loadJSON();
        LoadHIScore();

        //createJSONFromCurrentSettings();

        Physics.gravity = new Vector3(0, -gravity, 0);
    }

    void loadJSON()
    {
        TextAsset txt = (TextAsset)Resources.Load(jsonFileName, typeof(TextAsset));
        string content = txt.text;

        jsonSettings json = JsonUtility.FromJson<jsonSettings>(content);

        gravity = json.grawitacja;

        fuelMax= json.iloscPaliwa ;
        thrustPower = json.silaCiagu;
        sidePower =json.silaPrzechylania;


    }

    void createJSONFromCurrentSettings()
    {
        jsonSettings json = new jsonSettings();

        json.grawitacja = gravity;

        json.iloscPaliwa = fuelMax;
        json.silaCiagu = thrustPower;
        json.silaPrzechylania = sidePower;

        json.masaRakiety = rocketMass;
        
        string s = JsonUtility.ToJson(json);
        writeFile(s);
    }

    void writeFile(string s)
    {
        var fileName = "MyFile.json";
        if(File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(fileName);

        sr.Write(s);

        sr.Close();

    }


    public void saveHIscore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");

        PlayerData playerDaya = new PlayerData();

        Debug.Log("saveHIscore " + highScore);

        playerDaya.highScore = highScore;

        bf.Serialize(file, playerDaya);
        file.Close();
    }

    public void LoadHIScore()
    {
        highScore = 0;

        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            PlayerData pl = (PlayerData)bf.Deserialize(file);

            highScore = pl.highScore;


            file.Close();
        }
    }


}

[System.Serializable]
class PlayerData
{
   public float  highScore;
}
