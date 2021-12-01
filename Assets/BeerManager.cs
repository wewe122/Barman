using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerManager : MonoBehaviour
{
    [SerializeField]
    public GlassManager glassManager;

    [SerializeField]
    public DrunksManager drunkManager;

    private bool UpArrowPressed;
    private bool carryingBeer;
    private GameObject glass;
    private float DistanceToServe = 4.515f, liftBeerDist = 0.9f;

    void Start()
    {
        UpArrowPressed = false;
        carryingBeer = false;
    }

    void Update()
    {
        // if Bartender is carrying a beer update beer position`s to bartender position
        if (carryingBeer)
            glass.transform.position = GameObject.FindWithTag("Player").transform.position;

        // handle picking up a beer from the bar
        else if(Input.GetKeyDown(KeyCode.UpArrow) && !UpArrowPressed)
        {
            UpArrowPressed = true;
            glass = glassManager.Get();

            /*
             * we need to check that the glass that the bartender is trying to pick up is a glass filled with beer
             * also, the bartender needs to be close to the glass
             */
            if (glass.tag == "filledGlass" 
                && Vector3.Distance(GameObject.FindWithTag("Player").transform.position, glass.transform.position) <= liftBeerDist)
            {
                carryingBeer = true;
                glass.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
                glass.GetComponent<SpriteRenderer>().sortingOrder = 3;
                glass.transform.position = GameObject.FindWithTag("Player").transform.position;
            }
        }
        // avoid multiple actions from one long press
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            UpArrowPressed = false;
        }

        // handle the release of beer from bartender hands
        if (carryingBeer && Input.GetKeyDown(KeyCode.UpArrow) && !UpArrowPressed)
        {
            Debug.Log("Trying to serve beer");
            UpArrowPressed = true;
            GameObject[] goarray = GameObject.FindGameObjectsWithTag("Drunk");
            GameObject closestDrunk = null;
            float minDist = float.MaxValue;
            
            // search for the closest drunk
            foreach (var drunk in goarray)
            {
                var dist = Vector3.Distance(drunk.transform.position, transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestDrunk = drunk;
                }
            }
            Debug.Log(minDist);

           /* 
            * make sure the bartender is not too far from the drunk guy
            * if so, serve him the beer
            */
            if (minDist <= DistanceToServe)
            {
                glass.GetComponent<SpriteRenderer>().sortingLayerName = "Drunks";
                glass.GetComponent<SpriteRenderer>().sortingOrder = 1;
                glass.transform.position = closestDrunk.transform.position;
                drunkManager.Serve(glass, closestDrunk);
                carryingBeer = false;
                glassManager.ReleaseGlass();
            }
            
        }
        // avoid multiple actions from one long press
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            UpArrowPressed = false;
        }



    }
}
