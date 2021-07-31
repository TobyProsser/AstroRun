using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour {

    private GameObject AttractorObject;
    private GameObject LastAttractorObject;
    private Transform myTransform;

    public bool attract = true;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        myTransform = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (AttractorObject && attract)
        {
            AttractorObject.GetComponent<GravityAttractor>().Attract(myTransform); //Sends the location and color of the player to planet that is attracting the player
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            AttractorObject = collision.gameObject;
        }
    }
}
