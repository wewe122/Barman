using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkGoHome : MonoBehaviour
{
    public Vector2 homePoint;

    [SerializeField]
    public DrunksManager drunksManager;

    public float speed;
    private bool DrunkWantsHome;


    // Start is called before the first frame update
    void Start()
    {
        DrunkWantsHome = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(DrunkWantsHome)
            transform.position = 
                Vector2.MoveTowards(transform.position, homePoint, speed * Time.deltaTime);
    }

    public void SendDrunkHome()
    {
        DrunkWantsHome = true;
        GetComponent<Animator>().enabled = true;
        drunksManager.KillDrunk(this.gameObject);
    }
}
