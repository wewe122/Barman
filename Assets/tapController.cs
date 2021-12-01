using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapController : MonoBehaviour
{
    
    public GameObject[] taps;
    [SerializeField]
    public GlassManager glassManager;
    public float PouringDelay = 3f;
    private Animator animator;

    private Vector3 powerElevation = new Vector3(0f, 0.5f, 0f);
    private Vector3 decreasePower = new Vector3(0f, 1.1f, 0f);
    private int currentTap = 0;
    private bool DownArrowPressed, SpacePressed, TapsLocked;
    private float PouringDistance = 2.9f;
    
    void Start()
    {
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        DownArrowPressed = false;
        SpacePressed = false;
        TapsLocked = false;
        // lift up first tap
        taps[currentTap].transform.position += powerElevation; 
        glassManager.Get().transform.position = taps[currentTap].transform.position - decreasePower;
        glassManager.Get().GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        /*
         * if taps is locked do nothing
         */
        if (TapsLocked)
            return;

        /*
         * handle down press button 
         * switch to next beer tap every press
         * locked when there is a full glass of beer
        */
        if (Input.GetKeyDown(KeyCode.DownArrow) && !DownArrowPressed && !glassManager.isFilled())
        {
            DownArrowPressed = true;
            taps[currentTap].transform.position -= powerElevation;
            currentTap = currentTap == taps.Length -1 ? 0 : currentTap + 1;
            taps[currentTap].transform.position += powerElevation;
            glassManager.Get().transform.position = taps[currentTap].transform.position - decreasePower;
        }

        // avoid multiple actions from one long press
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DownArrowPressed = false;
        }

        /*
         * handle beer pouring
         * player needs to be close to active tap for pouring to happen
         */
        if (Input.GetKeyDown(KeyCode.Space) && !SpacePressed
            && !glassManager.isFilled()
            && Vector3.Distance(taps[currentTap].transform.position, GameObject.FindWithTag("Player").transform.position) <= PouringDistance)
        {
            SpacePressed = true;
            TapsLocked = true;// lock taps while pouring beer
            Debug.Log("pouring beer");
            StartCoroutine(PourBeer());
            
        }
        // avoid multiple actions from one long press
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpacePressed = false;
        }
    }
    IEnumerator PourBeer()
    {
        animator.SetBool("pouring", true);
        yield return new WaitForSeconds(PouringDelay);
        TapsLocked = false;// unlock taps
        glassManager.FillGlass(currentTap);
        Debug.Log("Beer is Ready");
        animator.SetBool("pouring", false);
    }
}
