using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStarSpawn : MonoBehaviour
{
    public GameObject star;
    public GameObject Camera;

    float timeTillSpawn = 7;
    float starSpeed = 2;

    private void Start()
    {
        StartCoroutine(spawnStar());
    }
    IEnumerator spawnStar()
    {
        while (true)
        {
            int whichX = Random.Range(0, 2);
            float xpos = -9;
            if (whichX == 0) xpos = 9;

            float ypos = Random.Range(-15.04f, 15.04f) + Camera.transform.position.y;
            Vector3 spawnPos = new Vector3(xpos, ypos, 20); 
            GameObject curStar = Instantiate(star, spawnPos, Quaternion.identity);

            curStar.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xpos, Random.Range(-10, 10)) * Random.Range(1.3f, 2.2f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(Random.Range(4, 14));
        }
    }
}
