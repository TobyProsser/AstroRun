using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhysicsPlayerController : MonoBehaviour
{
    public static float Gravity = 0;
    public float graviteForce = 292.6f;
    public float moveSpeed;
    public float jumpForce;
    public float rotateSpeed;

    bool onPlanet = false;

    public ParticleSystem jetpack;

    float hangTime = 1.2f;
    public TextMeshProUGUI hangText;
    public GameObject HangPanel;

    public TextMeshProUGUI scoreText;
    public static int curScore;
    public static bool respawn = false;

    bool takeOff = false;
    bool canJump = false;
    bool left = false;

    public Animator anim;
    public GameObject AstroSprite;

    public Slider OSlider;
    public GameObject cover;
    public GameObject fill;

    public TextMeshProUGUI goldText;

    private void Awake()
    {
        SetSmokeColor();
    }
    void Start()
    {
        HangPanel.SetActive(false);
        //StartCoroutine(FadeTo(0, .1f));
        anim.SetBool("isRunning", false);
        if(!respawn) curScore = 0;
        //this.GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        Gravity = -graviteForce;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            canJump = false;
            if (MenuController.soundEffects)
            {
                AudioManager.instance.Play("Jump");
                AudioManager.instance.Play("Explosion");
            }
            
            anim.SetBool("isRunning", false);
            StartCoroutine(TakeOff());
            onPlanet = false;
            Gravity = 0;
            this.GetComponent<GravityBody>().attract = false;

            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            jetpack.Play();
            StartCoroutine(HangTime());
            StartCoroutine(rotatePlayer());
        }
        else if (onPlanet)
        {
            canJump = true;
            anim.SetBool("isRunning", true);
            if(left) this.GetComponent<Rigidbody2D>().velocity = transform.right * -moveSpeed;
            else this.GetComponent<Rigidbody2D>().velocity = transform.right * moveSpeed;
        }

        goldText.text = MenuController.SpaceGold.ToString();
    }

    void IncreaseDifficulty()
    {
        if (curScore > 2 && curScore <= 10)
        {
            moveSpeed = 5f;
        }
        else if (curScore > 10)
        {
            moveSpeed = 4.8f;
        }
    }

    IEnumerator Die()
    {
        respawn = false;
        BetweenGameScript.score = curScore;
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene("BetweenMenu");
    }

    IEnumerator TakeOff()
    {
        takeOff = true; //Avoid colliding with planet youre jumping off of which will stop the jump
        yield return new WaitForSeconds(.1f);
        takeOff = false;
    }

    IEnumerator HangTime()
    {
        yield return new WaitForSeconds(.85f);
        if (!onPlanet)
        {
            float curHangTime = hangTime;
            OSlider.maxValue = curHangTime;
            hangText.text = (Mathf.Round(curHangTime * 10.0f) * 0.1f).ToString();
            OSlider.value = curHangTime;

            HangPanel.SetActive(true);
            while (true)
            {
                yield return new WaitForSeconds(.01f);
                curHangTime -= .01f;
                hangText.text = (Mathf.Round(curHangTime * 10.0f) * 0.1f).ToString();
                OSlider.value = curHangTime;

                if (onPlanet) break;
                else if (curHangTime <= 0)
                {
                    StartCoroutine(Die());
                    print("die");
                    break;
                }
            }

            //StartCoroutine(FadeTo(0, 1));
            HangPanel.SetActive(false);
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = cover.transform.GetComponent<Image>().material.color.a;
        float alpha1 = fill.transform.GetComponent<Image>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            Color newColor1 = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            cover.transform.GetComponent<Image>().material.color = newColor;
            fill.transform.GetComponent<Image>().material.color = newColor1;
            yield return null;
        }
    }

    IEnumerator rotatePlayer()
    {
        yield return new WaitForSeconds(.2f);
        print("Rotate");
        //transform.Rotate(0, 0, 180);
        
        float startRotation = transform.rotation.z;
        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 180), Time.deltaTime * rotateSpeed);
            if (onPlanet) break;
            yield return null;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet" && !takeOff)
        {
            AudioManager.instance.Stop("Explosion");
            AudioManager.instance.Stop("Jump");
            this.GetComponent<GravityBody>().attract = true;
            onPlanet = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            Gravity = -graviteForce;
            curScore++;
            scoreText.text = curScore.ToString();
            IncreaseDifficulty();
        }

        if (collision.gameObject.tag == "SpaceGold")
        {
            Destroy(collision.gameObject);
            MenuController.SpaceGold += 1;
        }

        if (collision.gameObject.tag == "MainCamera")
        {
            StartCoroutine(Die());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet") Destroy(collision.gameObject, 2);
    }

    void SetSmokeColor()
    {
        Color color1;

        color1 = new Color32(255, 255, 255, 255);

        if (MenuController.smoke == 1) color1 = new Color32(0, 188, 197, 255);
        else if (MenuController.smoke == 2) color1 = new Color32(250, 76, 0, 255);
        else if (MenuController.smoke == 3) color1 = new Color32(255, 16, 16, 255);
        else if (MenuController.smoke == 4) color1 = new Color32(85, 0, 197, 255);
        else if (MenuController.smoke == 5) color1 = new Color32(255, 0, 235, 255);
        else if (MenuController.smoke == 6) color1 = new Color32(8, 241, 31, 255);
        else if (MenuController.smoke == 7) color1 = new Color32(0, 35, 255, 255);
        else if (MenuController.smoke == 8) color1 = new Color32(255, 217, 0, 255);

        ParticleSystem.MainModule smoke = jetpack.main;
        smoke.startColor = color1;

        print("Smoke Set " + MenuController.smoke);
    }
}
