using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlassManager : MonoBehaviour
{

    public GameObject[] glasses;
    public GameObject[] smallFilledGlasses;
    public GameObject[] largeFilledGlasses;
    public TextMeshProUGUI smallGlassTmp;
    public TextMeshProUGUI largeGlassTmp;
    [SerializeField]
    public GameManager gameManager;

    private int largeGlassQuantity;
    private int smallGlassQuantity;
    private bool SpacePressed;
    private float MinDistToLiftGlass = 10.146f;
    private float distToReleaseGlass = 10.3f;
    private GameObject EmptyGlassObject = null;
    private GameObject FullGlassObject = null;
    private Transform PlayerTransform = null;
    private static bool instructionsMode;
    [SerializeField]
    public UIManager uiManager;

    void Awake() 
    {
        instructionsMode = false;
    }
    void Start()
    {
        smallGlassQuantity = gameManager.smallGlassQuantity;
        largeGlassQuantity = gameManager.largeGlassQuantity;
        uiManager.SetSmallGlassQuantity(smallGlassQuantity);
        uiManager.SetLargeGlassQuantity(largeGlassQuantity);
        SpacePressed = false;
        PlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (EmptyGlassObject != null) // demonstrate the bartender holding the empty glass
            EmptyGlassObject.transform.position = PlayerTransform.position;
        
        if (FullGlassObject != null) // demonstrate the bartender holding the full glass
            FullGlassObject.transform.position = PlayerTransform.position;
        // bartender wants to take an empty glass
        else if (Input.GetKeyDown(KeyCode.Space) && !SpacePressed)
        {
            SpacePressed = true;
            var distToSmallGlass = Vector3.Distance(Camera.main.ScreenToWorldPoint(
                GameObject.Find("UI Canvas/Porto_0").GetComponent<RectTransform>().position),
                PlayerTransform.position);
            var distToLargeGlass = Vector3.Distance(
                Camera.main.ScreenToWorldPoint(GameObject.Find("UI Canvas/chop roja_0").GetComponent<RectTransform>().position),
                PlayerTransform.position);
            Debug.Log("distToLarge: " + distToLargeGlass + ", distToSmall: " + distToSmallGlass);
            if (EmptyGlassObject == null)
            {
                if (distToLargeGlass <= MinDistToLiftGlass && (largeGlassQuantity > 0 || instructionsMode))
                {
                    EmptyGlassObject = Instantiate(glasses[1], PlayerTransform.position, PlayerTransform.rotation);
                    EmptyGlassObject.GetComponent<SpriteRenderer>().enabled = true;
                    uiManager.SetLargeGlassQuantity(--largeGlassQuantity);
                    Debug.Log("picking up large glass");
                }
                else if (distToSmallGlass <= MinDistToLiftGlass && (smallGlassQuantity > 0 ||  instructionsMode))
                {
                    EmptyGlassObject = Instantiate(glasses[0], PlayerTransform.position, PlayerTransform.rotation);
                    EmptyGlassObject.GetComponent<SpriteRenderer>().enabled = true;
                    uiManager.SetSmallGlassQuantity(--smallGlassQuantity);
                    Debug.Log("picking up small glass");
                }
            }
            // bartender wants to release the empty glass
            else if(distToLargeGlass <= distToReleaseGlass || distToSmallGlass <= distToReleaseGlass)
            {
                //if current empty glass is a small one
                if (EmptyGlassObject.GetComponent<SpriteRenderer>().sprite.name == glasses[0].GetComponent<SpriteRenderer>().sprite.name)
                    uiManager.SetSmallGlassQuantity(++smallGlassQuantity);
                else //if current empty glass is a large one
                    uiManager.SetLargeGlassQuantity(++largeGlassQuantity);
                Destroy(EmptyGlassObject);
                EmptyGlassObject = null;
            }

        }

        // avoid multiple actions from one long press
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpacePressed = false;
        }

    }
    public GameObject Get()
    {
        return FullGlassObject;        
    }
    public void ReleaseEmptyGlass()
    {
        EmptyGlassObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void ReleaseFullGlass()
    {
        FullGlassObject = null;
    }
    public bool isHoldingEmptyGlass()
    {
        return EmptyGlassObject != null;
    }
    public bool isHoldingFullGlass()
    {
        return FullGlassObject != null;
    }
    public void FillGlass(int type)
    {
        FullGlassObject = EmptyGlassObject.GetComponent<SpriteRenderer>().sprite.name == "Porto_0" ?
            Instantiate(smallFilledGlasses[type], PlayerTransform.position, PlayerTransform.rotation) :
            Instantiate(largeFilledGlasses[type], PlayerTransform.position, PlayerTransform.rotation);
        
        Destroy(EmptyGlassObject);// destroy empty glass object
        EmptyGlassObject = null;
        //build new filled glass object
        FullGlassObject.GetComponent<SpriteRenderer>().enabled = true;

    }
    public static void ToggleInstructionsMode()
    {
        instructionsMode = !instructionsMode;
    }
}