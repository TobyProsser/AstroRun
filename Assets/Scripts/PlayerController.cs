using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float rotateSpeed = .1f;

    bool jump;
    // Use this for initialization
    void Start ()
    {
        //this.GetComponent<Rigidbody2D>().AddForce(transform.up * 10, ForceMode2D.Impulse);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jump = true;
            this.GetComponent<Rigidbody2D>().AddForce(transform.right * -10, ForceMode2D.Impulse);
        }
    }

    IEnumerator rotateAround(GameObject planet)
    {
        float radius = planet.GetComponent<SpriteRenderer>().bounds.size.x/2 + transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector2 center = planet.transform.position;
        Vector2 playerPos = transform.position;

        Vector2 one = new Vector2(center.x, planet.GetComponent<SpriteRenderer>().bounds.size.y / 2 + center.y);
        GameObject test = new GameObject();
        test.transform.position = one;
        Vector2 two = new Vector2(playerPos.x, playerPos.y);

        float angle = Mathf.Atan2(one.y - two.y, one.x - two.x)* Mathf.Rad2Deg;            //set angle as central angle to make player start rotating at correct position

        print(angle);
        if (angle < 0) angle += 360;
        print(angle);
        
        planet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        while (true)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
            Vector2 playerOffset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
            transform.position = center + offset;

            Vector2 targetPos = planet.transform.position;
            Vector2 thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            float lookAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));

            if (jump)
            {
                jump = false;
                break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            StartCoroutine(rotateAround(collision.gameObject));
        }
    }
}
