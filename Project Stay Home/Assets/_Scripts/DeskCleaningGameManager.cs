using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskCleaningGameManager : MonoBehaviour
{
    public GameObject Desk;
    public GameObject GarbageBin;
    public GameObject PencilHolder;
    public GameObject StorageBin;

    public Image GBCircle;
    public Image GBCheckMark;
    public Image PHCircle;
    public Image PHCheckMark;
    public Image SBSquare;
    public Image SBCheckMark;
    public Text Exit;

    public List<GameObject> GBObjs;
    public List<GameObject> PHObjs;
    public List<GameObject> SBObjs;

    static public bool showGBCircle = false;
    static public bool showPHCircle = false;
    static public bool showSBSquare = false;

    Vector3 GBCheckMarkOriginalScale;
    Vector3 PHCheckMarkOriginalScale;
    Vector3 SBCheckMarkOriginalScale;

    // Start is called before the first frame update
    void Start()
    {
        GBCheckMarkOriginalScale = GBCheckMark.gameObject.transform.localScale;
        PHCheckMarkOriginalScale = GBCheckMark.gameObject.transform.localScale;
        SBCheckMarkOriginalScale = GBCheckMark.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (showGBCircle)
            GBCircle.enabled = true;
        else
            GBCircle.enabled = false;

        if (showPHCircle)
            PHCircle.enabled = true;
        else
            PHCircle.enabled = false;

        if (showSBSquare)
            SBSquare.enabled = true;
        else
            SBSquare.enabled = false;

        if (Desk.GetComponent<DeskInteraction>().interactableDesk)
        {
            bool GBTmp = false;
            bool PHTmp = false;
            bool SBTmp = false;

            if (IsReady(GBObjs))
            {
                GBCheckMark.gameObject.SetActive(true);
                GBTmp = true;
            }
            else
                GBCheckMark.gameObject.SetActive(false);

            if (IsReady(PHObjs))
            {
                PHCheckMark.gameObject.SetActive(true);
                PHTmp = true;
            }
            else
                PHCheckMark.gameObject.SetActive(false);

            if (IsReady(SBObjs))
            {
                SBCheckMark.gameObject.SetActive(true);
                SBTmp = true;
            }
            else
                SBCheckMark.gameObject.SetActive(false);

            if (GBTmp && PHTmp && SBTmp)
                Desk.GetComponent<DeskInteraction>().interactableDesk = false;
        }
        else
        {
            Exit.enabled = true;
            GBCheckMark.gameObject.transform.localScale = Vector3.Lerp(GBCheckMark.gameObject.transform.localScale, GBCheckMarkOriginalScale * 0, Time.deltaTime * 5f);
            PHCheckMark.gameObject.transform.localScale = Vector3.Lerp(PHCheckMark.gameObject.transform.localScale, PHCheckMarkOriginalScale * 0, Time.deltaTime * 5f);
            SBCheckMark.gameObject.transform.localScale = Vector3.Lerp(SBCheckMark.gameObject.transform.localScale, SBCheckMarkOriginalScale * 0, Time.deltaTime * 5f);
        }

        if (!DragObject.Dragble)
        {
            GBCheckMark.gameObject.SetActive(false);
            PHCheckMark.gameObject.SetActive(false);
            SBCheckMark.gameObject.SetActive(false);
            Exit.enabled = false;
        }
    }

    bool IsReady(List<GameObject> list)
    {
        bool ready = true;

        foreach(GameObject a in list){
            if (!a.GetComponent<DragObject>().inRightPlace)
                ready = false;
        }

        return ready;
    }
}
