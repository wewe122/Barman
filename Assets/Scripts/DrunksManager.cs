using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunksManager : MonoBehaviour
{
    public GameObject drunkToSpawn;
    public Transform[] positions;
    public Sprite[] beersSprites;
    public Transform startPosition;
    public float delayBetweenDrunks = 10f;
    public float DrunksSpeed;
    public bool instructionsMode = false;

    [SerializeField]
    public instructions instructions;

    [SerializeField]
    public GameManager gameManager;

    private Dictionary<int, GameObject> drunks;
    private int currentPosition = -1;
    private float timer = 0;
    private int drunksCount = 0;
    private float KilingwaitingTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        if (instructionsMode)
            return;
        drunks = new Dictionary<int, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instructionsMode)
            return;
        timer += Time.deltaTime;
        if (currentPosition > -1 && drunks[currentPosition].transform.position != positions[currentPosition].position)
            moveDrunk();
        else if ((timer > delayBetweenDrunks || currentPosition == -1 && timer > delayBetweenDrunks / 2)
                    && drunksCount < gameManager.NumOfDrunks && drunks.Count < positions.Length)
        {
            // init new drunk
            currentPosition = Random.Range(0, positions.Length);
            while (drunks.ContainsKey(currentPosition))
                currentPosition = Random.Range(0, positions.Length);
            timer = 0;
            drunksCount++;
            GameObject go = Instantiate(drunkToSpawn, startPosition.position, transform.rotation);
            go.GetComponent<DrunkGoHome>().drunksManager = this;
            drunks.Add(currentPosition, go);
            var t = Random.Range(0, beersSprites.Length);
            go.transform.Find("dialogo/BeerDrunkWants").GetComponent<SpriteRenderer>().sprite = 
                beersSprites[t];
        }
    }

    void moveDrunk()
    {
        drunks[currentPosition].transform.position = Vector2.MoveTowards(drunks[currentPosition].transform.position,
                                                                 positions[currentPosition].position,
                                                                 DrunksSpeed * Time.deltaTime);
    }
    public bool Serve(GameObject glass, GameObject drunk)
    {
        if (instructions != null)
        {
            instructions.Serve(glass, drunk);
            return true;
        }

        Debug.Log(glass.GetComponent<SpriteRenderer>().sprite.name + ", " + drunk.transform.Find("dialogo/BeerDrunkWants").GetComponent<SpriteRenderer>().sprite.name);
        if (glass.GetComponent<SpriteRenderer>().sprite.name
            == drunk.transform.Find("dialogo/BeerDrunkWants").GetComponent<SpriteRenderer>().sprite.name)
        {
            Destroy(drunk.transform.Find("dialogo").gameObject);
            Debug.Log("good serve");
            gameManager.AddScore();
            return true;
        }
        gameManager.DecreaseScore();
        Debug.Log("bad serve");
        return false;
      
    }
    public void KillDrunk(GameObject drunk)
    {
        StartCoroutine(DestroyDrunk(drunk));
    }
    public IEnumerator DestroyDrunk(GameObject drunk)
    {
        yield return new WaitForSeconds(KilingwaitingTime);
        foreach (var item in drunks)
        {
            if(item.Value == drunk)
            {
                drunks.Remove(item.Key);
                break;
            }    
        }

        Destroy(drunk);
        Debug.Log("drunk is out");
    }
}
