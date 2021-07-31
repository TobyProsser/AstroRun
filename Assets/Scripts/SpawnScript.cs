using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    private float Yvar = 3f;
    public GameObject Planet;
    private GameObject CurPlanet;

    public GameObject spaceGold;

    float minSize = .7f;
    float MaxSize = .9f;
    float score =0;

    GameObject player;

    public List<Sprite> planetSprites = new List<Sprite>();
    public List<Color> planetColors = new List<Color>();
    public Color OGplanetColor;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start ()
    {
        StartCoroutine(SpawnPlanets(1));
    }
	
	// Update is called once per frame
	void LateUpdate()
    {
        MakeHarder();
    }

    void MakeHarder()
    {
        score = PhysicsPlayerController.curScore;
        if (score > 5 && score <= 8)
        {
            MaxSize = .9f;
            minSize = .6f;
        }
        else if (score > 8 && score <= 40)
        {
            MaxSize = .7f;
            minSize = .55f;
        }
        else if (score > 40)
        {
            MaxSize = .7f;
            minSize = .4f;
        }
    }

    private IEnumerator SpawnPlanets(float waitTime)
    {
        int curColor = 0;
        while (true)
        {
            Yvar += 15f;
            float Xvar = Random.Range(-4f, 4f);
            Vector3 spawnPos = new Vector3(Xvar, Yvar, 0);
            CurPlanet = Instantiate(Planet, spawnPos, Planet.transform.rotation);

            int choosePlanet = Random.Range(0, planetSprites.Count);
            CurPlanet.GetComponent<SpriteRenderer>().sprite = planetSprites[choosePlanet];

            if (MenuController.newColors)
            {
                if (curColor >= planetColors.Count) curColor = 0;

                Color planetColor = planetColors[curColor];

                CurPlanet.GetComponent<SpriteRenderer>().color = planetColor;
                CurPlanet.GetComponent<OnPlanetScript>().PlanetColor = planetColor;
                CurPlanet.GetComponent<OnPlanetScript>().onColor = new Color(planetColor.r * 1.2f, planetColor.g * 1.2f, planetColor.b * 1.2f);

                curColor++;
            }
            else
            {
                CurPlanet.GetComponent<SpriteRenderer>().color = OGplanetColor;
                CurPlanet.GetComponent<OnPlanetScript>().PlanetColor = OGplanetColor;
            }

            float scale = Random.Range(minSize, MaxSize);
            CurPlanet.transform.localScale = new Vector3(scale, scale);

            while (player.transform.position.y + 15 < Yvar && player != null)
            {
                if (player == null) break;
                if (player.transform.position.y + 15 < Yvar)
                {
                    yield return null;
                }
                else
                {
                    break;
                }
            }

            int ChanceToSpawnGold = Random.Range(0, 8);

            if (ChanceToSpawnGold == 0)
            {
                spawnPos = new Vector3(Random.Range(-3f, 3f), Yvar + 7, 0);
                GameObject curGold = Instantiate(spaceGold, spawnPos, Planet.transform.rotation);
            }

            yield return null;
        }
    }
}
