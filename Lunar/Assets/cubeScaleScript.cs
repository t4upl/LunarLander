using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScaleScript : MonoBehaviour {


    public GameObject rocket;
    public GameObject gameManager;

    int frame = 0;

    // Use this for initialization
    void Start() {
        GetComponent<Rigidbody>().mass = GetComponent<constantScript>().rocketMass;

    }

    // Update is called once per frame
    void Update() {

        if (transform.position.y <-1)
        {
            gameManager.GetComponent<MainScript>().lose();
        }

        if (GetComponent<Rigidbody>().velocity.y > 12)
        {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            velocity.y = 0;
            GetComponent<Rigidbody>().velocity = velocity;

        }

        frame++;
    }


    void OnCollisionEnter(Collision col)
    {


        if (col.gameObject.name.Equals("platform"))
        {
            gameManager.GetComponent<MainScript>().hitPlatform();

        }
        else
        {
            gameManager.GetComponent<MainScript>().lose();
        }

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;

        rocket.GetComponent<rocketScript2>().setIsControlActive(false);

    }

    public void reset()
    {
        transform.position = new Vector3(0, 6.12f, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;

    }

}
