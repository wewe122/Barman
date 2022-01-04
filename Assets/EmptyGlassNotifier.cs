using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGlassNotifier : MonoBehaviour
{
    public string AnimationName;

    private Animator animator;
    private float timer = 0;
    private GameObject Drunk;
    private float DrinkingTime = 42f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // search for the drunk who`s holding the beer
        foreach (var drunk in GameObject.FindGameObjectsWithTag("Drunk"))
        {
            if (drunk.transform.position == transform.position)
            {
                Drunk = drunk;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.enabled)
        {
            timer += Time.deltaTime;
            if (Drunk == null)
            {
                // search for the drunk who`s holding the beer
                foreach (var drunk in GameObject.FindGameObjectsWithTag("Drunk"))
                {
                    if (drunk.transform.position == transform.position)
                    {
                        Drunk = drunk;
                        break;
                    }
                }
            }
        }
        if(timer > DrinkingTime && GameObject.FindGameObjectWithTag("instructor") == null)
        {
            Drunk.GetComponent<DrunkGoHome>().SendDrunkHome();
            Destroy(this.gameObject);
        }
    }
}
