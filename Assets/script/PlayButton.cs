using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public NoteManager2 nm;
    public GameManger gm;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    public void OnClick()
    {
        if(gm.iseditor == true)
        {
            gm.iseditor = false;
            gm.isplay = true;

        }
        else if (gm.isplay == true)
        {
            gm.iseditor = true;
            gm.isplay = false;

        }

        for(int i=0; i < nm.NoteArr.Count; i++)
        {
            sr = nm.NoteArr[i].GetComponent<SpriteRenderer>();
            sr.color = Color.white;
        }
    }
}
