using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        var z = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var x = Input.GetAxis("Vertical") * Time.deltaTime * speed;


        transform.Rotate(0, 0, 0);
        transform.Translate(z, 0, x);
    }
}
