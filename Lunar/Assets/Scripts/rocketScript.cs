using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketScript : MonoBehaviour {

    Boolean isEnteredScreen = false;
    public Camera camera;
    public GameObject constantsGameObject;

    public GameObject prawyPunktPrzylozeniaSily;
    public GameObject lewyPunktPrzylozeniaSily;

    // Use this for initialization
    void Start () {

        Vector3 punktPrzylozeniaSily = prawyPunktPrzylozeniaSily.transform.position;

        float offsetPrzylzoeniaSily = GetComponent<BoxCollider>().bounds.size.x;

        prawyPunktPrzylozeniaSily.transform.position = new 
            Vector3(punktPrzylozeniaSily.x+offsetPrzylzoeniaSily, punktPrzylozeniaSily.y, punktPrzylozeniaSily.z);
        lewyPunktPrzylozeniaSily.transform.position = new 
            Vector3(punktPrzylozeniaSily.x - offsetPrzylzoeniaSily, punktPrzylozeniaSily.y, punktPrzylozeniaSily.z);




    }

    // Update is called once per frame
    void Update () {
        if (!isEnteredScreen)
        {
            float bottomYPosition = transform.position.y + GetComponent<MeshRenderer>().bounds.extents.y;
            Vector2 pixels= camera.WorldToScreenPoint(new Vector3(0f, bottomYPosition, 0f));

            if (pixels.y - Screen.height<0)
            {
                isEnteredScreen = true;
                Debug.Log("ACTIVE!");
            }
        }
	}

    public void useUp()
    {
        GetComponent<Rigidbody>().AddForce(transform.up* constantsGameObject.GetComponent<constantScript>().thrustPower);
    }

    public void useLeft()
    {
        Debug.Log("Left used");

        GetComponent<Rigidbody>().AddTorque(transform.forward *constantsGameObject.GetComponent<constantScript>().sidePower);

        //Vector3 punktPrzylzoeniaSily = new Vector3 (transform.position.x, transform.position.y + lewyPunktPrzylozeniaSily.transform.position.y, transform.position.z);
        ///GetComponent<Rigidbody>().AddForceAtPosition(Vector3.right * constantsGameObject.GetComponent<constantScript>().sidePower, punktPrzylzoeniaSily );
    }

    public void useRight()
    {
        // GetComponent<Rigidbody>().AddTorque(-transform.forward * constantsGameObject.GetComponent<constantScript>().sidePower);
        //Vector3 punktPrzylzoeniaSily = new Vector3(transform.position.x, transform.position.y + lewyPunktPrzylozeniaSily.transform.position.y, transform.position.z);
        //GetComponent<Rigidbody>().AddForceAtPosition(-Vector3.right * constantsGameObject.GetComponent<constantScript>().sidePower, punktPrzylzoeniaSily);

        /*
        GetComponent<Rigidbody>().AddForce(-prawyPunktPrzylozeniaSily.transform.right * constantsGameObject.GetComponent<constantScript>().sidePower);
        // GetComponent<Rigidbody>().AddForce(transform.up * constantsGameObject.GetComponent<constantScript>().thrustPower);
        */
    }


}
