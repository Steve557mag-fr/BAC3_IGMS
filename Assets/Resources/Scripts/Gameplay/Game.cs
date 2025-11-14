using COL1.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public string lang = "fr/FR";
    [SerializeField] UIGame ui;
    
    bool isBusy = false;

    internal GameData gameData;

    public void Goto(string sceneName)
    {
        if (isBusy) return;
        isBusy = true;

        ui.MakeTransition(() =>
        {
            SceneManager.LoadScene(sceneName);
        }, () =>
        {
            isBusy = false;
        });

    }

    public void StartGame()
    {
        gameData.step = 0;
        Goto("Game");
    }
    
    public void SetLang(string val)
    {
        lang = val;
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void Awake()
    {
        Singleton.Make(this);
    }

}

public struct GameData
{
    public int step;

}
