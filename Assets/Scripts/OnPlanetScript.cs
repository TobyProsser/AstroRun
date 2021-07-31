using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanetScript : MonoBehaviour {

    public GameObject Player;
    public Color onColor;
    public Color PlanetColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color = onColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color = PlanetColor;
        }
    }
}
