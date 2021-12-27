using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string NextLevelName;
    public string CurrentLevelName;
    

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(NextLevelName, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitForSceneLoad());

    }
    public void QuitGame()// להוסיף חלונית האם אתה בטוח וכו
    {
        Debug.Log("quit game");
        Application.Quit();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(CurrentLevelName, LoadSceneMode.Single);
    }
}
