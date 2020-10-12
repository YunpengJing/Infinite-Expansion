using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject HeroMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowHeroMenu()
    {
        Time.timeScale = 0;
        GameObject menu = Instantiate(HeroMenu);
        menu.name = "HeroMenu";
    }

    public void HideHeroMenu()
    {
        GameObject menu = GameObject.Find("HeroMenu");
        Debug.Log(menu);
        Destroy(menu);
        Time.timeScale = 1;
    }
}
