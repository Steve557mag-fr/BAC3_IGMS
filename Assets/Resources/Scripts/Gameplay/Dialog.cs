using COL1.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [SerializeField] UIDialog ui;
    [SerializeField] DialogEvent dialogEvents;

    const string PATH_DB = "CSV/loc_texts_entries";
    bool isBusy = false;
    int currentIndex = -1;
    CSVDocument db;

    private void Awake()
    {
        db = new(PATH_DB);
        db.FilterDoc("TYPE", (string s) => { return s == "FRAG"; } );
        isBusy = false;
    }

    public void NewFragment(int id)
    {
        if (isBusy) return;
        isBusy = true;

        int index = db.FindFromColValue("ID", id.ToString());
        if (index == -1) return;
        currentIndex = index;

        string fragMessage = db.GetRawData(Game.Get().lang, index);
        ui.UploadSeq(fragMessage, 
            onUpdate: (string s) => {

                if (s.Contains("<link=\"SKIP\">")) Next(true);

            },
            onFinished: () =>
            {
                isBusy = false;
            }
        );

    }

    public void Next(bool bypass=false)
    {
        if (bypass || isBusy || currentIndex == -1) return;
        //Debug.Log($"{isBusy}");
        //Debug.Log("so goood");
        //Debug.Log($"{currentIndex}");

        Debug.Log(db.GetRawData("ARG", currentIndex));
        string[] args = db.GetRawData("ARG", currentIndex).Split(",");
        if (args.Length == 0) Close();

        NewFragment(int.Parse(args[0]));

    }

    public void Close()
    {
        currentIndex = -1;
        ui.CloseSeq();
        isBusy = false;
    }


    public static Dialog Get()
    {
        return FindAnyObjectByType<Dialog>();
    }

}

[System.Serializable]
public struct DialogEvent
{
    public int dialogID;
    public UnityEvent onEvent;
} 

