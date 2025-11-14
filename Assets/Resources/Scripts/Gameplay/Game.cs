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
        //FindAnyObjectByType<SceneSetup>().UpdateStep();

        SetStep(0);
        gameData = new();
        Goto("House");
    }
    
    public void SetStep(int step)
    {
        gameData.step = step;
        foreach (var e in FindObjectsByType<SceneSetup>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            e.UpdateStep();
        }
        Debug.Log($"STEP UPDATED to {step}");

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
