using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    
    public PlayManager pm;
    public NoteManager2 nm;
    public GameManger gm;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pm.NoteNum2+1 != nm.NoteArr.Count)
        {
            if (collision.CompareTag("Note") && collision == nm.NoteArr[pm.NoteNum2 + 1].GetComponent<BoxCollider2D>())
            {
                pm.istriger = true;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pm.NoteNum2+1 != nm.NoteArr.Count)
        {
            if (collision.CompareTag("Note") && collision == nm.NoteArr[pm.NoteNum2 + 1].GetComponent<BoxCollider2D>())
            {
                pm.istriger = false;
            }
        }
    }

}
