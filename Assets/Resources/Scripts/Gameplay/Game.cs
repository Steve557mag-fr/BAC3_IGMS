using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public string lang = "fr/FR";
    [SerializeField] UIGame ui;
    
    bool isBusy = false;

    GameData gameData;

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

    public void Quit()
    {
        Application.Quit();
    }


    public static Game Get()
    {
        return FindAnyObjectByType<Game>();
    }

}

public struct GameData
{
    public int step;

}
