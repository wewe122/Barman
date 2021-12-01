using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class instructions : MonoBehaviour
{
    public Button continueBtn;
    public Button nextBtn;
    public Text[] instructionsTexts;
    public Text[] missionTexts;
    public Text greatText;
    public Text tryAgainText;

    private int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        instructionsTexts[current].gameObject.SetActive(true);
        Time.timeScale = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() 
    {
        instructionsTexts[current++].gameObject.SetActive(false);
        if(current < instructionsTexts.Length)
            instructionsTexts[current].gameObject.SetActive(true);
        else 
        {
            continueBtn.gameObject.SetActive(false);
            Time.timeScale = 1;
            current = 0;
            missionTexts[current].gameObject.SetActive(true);
        }
    }
    public void Serve(GameObject glass, GameObject drunk)
    {
        if (glass.GetComponent<SpriteRenderer>().sprite.name
            == drunk.transform.Find("dialogo/BeerDrunkWants").GetComponent<SpriteRenderer>().sprite.name)
        {
            greatText.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(true);
            Destroy(drunk.transform.Find("dialogo").gameObject);
            Debug.Log("good serve");
        }
        else
        {
            StartCoroutine(displaytext());
            Destroy(glass);
            Debug.Log("bad serve");
        }
    }
    public void Next()
    {
        greatText.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
        missionTexts[current++].gameObject.SetActive(false);
        if(current < missionTexts.Length)
            missionTexts[current].gameObject.SetActive(true);
        else 
        {
            SceneManager.LoadScene("firstScene");
            Debug.Log("next scene");
        }
    }
    public IEnumerator displaytext()
    {
        tryAgainText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        tryAgainText.gameObject.SetActive(false);

    }

}
