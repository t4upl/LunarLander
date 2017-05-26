using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketScript2 : MonoBehaviour {

    public GameObject torquePoint;
    public GameObject gameManager;

    float fuel;

    bool isControlActive = false;
    bool isControlActiveFirstTime = true;


    // Use this for initialization
    void Start () {

        isControlActive = false;

    }
	
	// Update is called once per frame
	void Update () {

        float speed = torquePoint.GetComponent<Rigidbody>().velocity.y;
        float angle = torquePoint.transform.localEulerAngles.z;

        gameManager.GetComponent<MainScript>().updateSpeedUI(speed,angle);

        if (isControlActiveFirstTime)
        {
            if (torquePoint.transform.position.y < 5)
            {
                isControlActive = true;
                isControlActiveFirstTime = false;
            }
        }

	}

    public void useLeft()
    {


        if (fuel > 0 && isControlActive)
        {
            Vector3 newRotation = torquePoint.transform.localEulerAngles;

            newRotation.z = newRotation.z + GetComponent<constantScript>().sidePower;

            torquePoint.transform.localEulerAngles = newRotation;

            fuelStep();
        }


    }

    public void useRight()
    {

        if (fuel > 0 && isControlActive)
        {


            Vector3 newRotation = torquePoint.transform.localEulerAngles;

            newRotation.z = newRotation.z - GetComponent<constantScript>().sidePower;

            torquePoint.transform.localEulerAngles = newRotation;

            fuelStep();
        }

    }


    public void useUp()
    {
        if (fuel > 0 && isControlActive)
        {

            Vector3 force = new Vector3();
            Vector3 position = new Vector3();

            force = torquePoint.transform.up * GetComponent<constantScript>().thrustPower;
            position = transform.position;


            torquePoint.GetComponent<Rigidbody>().AddForceAtPosition(force, position);

            fuelStep();

        }

    }


    //reset zarowno siebie jak i torquePoint
    public void reset()
    {
        

        fuel = GetComponent<constantScript>().fuelMax;
        gameManager.GetComponent<MainScript>().updateFuelBar(fuel);


        torquePoint.GetComponent<cubeScaleScript>().reset();

        isControlActive = false;
        isControlActiveFirstTime = true;
    }

    void fuelStep()
    {
        gameManager.GetComponent<MainScript>().updateFuelBar(fuel);
        fuel -= GetComponent<constantScript>().fuelUsage;
        fuel = Mathf.Max(0, fuel);

        
    }


    public void setIsControlActive(bool b1)
    {
        isControlActive = b1;
    }




}
