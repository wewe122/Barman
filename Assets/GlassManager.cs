using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{

    public GameObject[] glasses;
    public GameObject[] smallFilledGlasses;
    public GameObject[] largeFilledGlasses;

    private int current = 0;
    private bool pressed, filled;
    private GameObject currentObject;

    void Start()
    {
        pressed = false;
        filled = false;
        currentObject = glasses[current];
        currentObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        if (!filled && Input.GetKeyDown(KeyCode.RightControl) && !pressed)
        {
            pressed = true;
            currentObject.GetComponent<SpriteRenderer>().enabled = false;
            Vector3 prevpos = currentObject.transform.position;
            current = current == glasses.Length - 1 ? 0 : current + 1;
            currentObject = glasses[current];
            currentObject.transform.position = prevpos;
            currentObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.RightControl))
        {
            pressed = false;
        }
    }

    public void FillGlass(int type)
    {
        filled = true;
        currentObject = Instantiate(current == 0 ? smallFilledGlasses[type] : largeFilledGlasses[type], 
                                    glasses[current].transform.position, 
                                    glasses[current].transform.rotation); 
        glasses[current].GetComponent<SpriteRenderer>().enabled = false;
        currentObject.transform.position = glasses[current].transform.position;
        currentObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    public GameObject Get() 
    {
        return currentObject;
    }
    public void ReleaseGlass()
    {
        filled = false;
        currentObject = glasses[current];
        currentObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public bool isFilled() { return filled; }
}