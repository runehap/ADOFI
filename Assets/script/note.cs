using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class note : MonoBehaviour
{
    public PlayManager pm;
    public NoteManager nm;
    BoxCollider2D boxcollider;
    public float NoteWidth;
    public float NoteHeight;
    // Start is called before the first frame update
    void Awake()
    {
        nm = GameObject.Find("NoteManager").GetComponent<NoteManager>();
        pm = GameObject.Find("PlayManager").GetComponent<PlayManager>(); 
        boxcollider = GetComponent<BoxCollider2D>();
        NoteWidth = boxcollider.size.x * transform.localScale.x;
        NoteHeight = boxcollider.size.y * transform.localScale.y;

    }

    // Update is called once per frame


}
