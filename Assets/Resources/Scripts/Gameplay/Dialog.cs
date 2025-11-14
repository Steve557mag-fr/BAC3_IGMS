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
        Singleton.Make(this);

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

        Singleton.Get<PlayerController>().DisableCharacter();

        string fragMessage = db.GetRawData(Singleton.Get<Game>().lang, index);
        ui.UploadSeq(fragMessage, 
            onUpdate: (string s) => {

                if (s.Contains("£")) Next(true);

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

        string[] args = db.GetRawData("ARG", currentIndex).Split(",");
        //Debug.Log(db.GetRawData("ARG", currentIndex));
        //print($"args:(${args.Length}) {string.Join(", ", args)}");

        int nextFrag = -1;
        try
        {
            NewFragment(int.Parse(args[0]));
        }
        catch {
            Close();
        }

    }

    public void Close()
    {
        currentIndex = -1;
        ui.CloseSeq();
        isBusy = false;
        Singleton.Get<PlayerController>().EnableCharacter();
        //Debug.Log("END!");
    }



}

[System.Serializable]
public struct DialogEvent
{
    public int dialogID;
    public UnityEvent onEvent;
} 

