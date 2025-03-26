using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class NoteManager2 : MonoBehaviour
{
    [SerializeField]
    public maincamera cam;

    public GameObject parent;
    public float CurZ = 0f;
    public float NextRotation = 0f;
    public GameManger gm;

    public GameObject Curnote;
    public GameObject SpawnNote;
    public int CurNoteNum = 0;
    public int SpawnNoteNum = 0;
    public GameObject Note;
  
    public GameObject Note180;
    public GameObject Note90;
    public GameObject Note270;
    public GameObject NoteTurn;

    public GameObject Notepre;

    public float P90 = 2.193f;
    public float PX = 0.25f * 9f;
    public float NoteLength = 0.25f * 9f;
    bool is90 = false;

    Vector2 CurSpawn;
    float CurRotat;
    Vector2 PreSpawn;

    Vector3 Spawnpos; //노트 생성 위치
    Vector3 SpawnRo; // 노트 생성 각도


    public List<GameObject> NoteArr = new List<GameObject>();
    public List<float> NoteDicArr = new List<float>();

    bool ReGeneration(float Rotat)
    {
        if (Rotat == 90f || Rotat == -270f)
        {
            Curnote = Note90;
            is90 = true;
            return true;
        }
        else if (Rotat == -90f || Rotat == 270f)
        {
            Curnote = Note270;
            is90 = true;
            return true;
        }
        else if (Rotat == 0f)
        { 
                Curnote = Note180;
                return true;
        }
        else if (Mathf.Abs(Rotat) == 180f)
        {
            Curnote = NoteTurn;
            return true;
        }

            return false;
    }

    Vector2 CurPos(Vector2 p,Vector2 c)
    {
        Vector2 c2 = p;
        if(p.x < c.x) c2.x += P90;
        else if(p.x > c.x) c2.x -= P90;

        if (p.y < c.y) c2.y += P90;
        else if (p.y > c.y) c2.y -= P90;

        return c2; 
    }

    void SelSpawnpos(float Dic)
    {
        if (is90)
        {
            switch(Dic) {
                case 90f: 
                    Spawnpos = new Vector3(CurSpawn.x, CurSpawn.y + P90, CurZ);
                    break;
                case 180f:
                    Spawnpos = new Vector3(CurSpawn.x - P90, CurSpawn.y , CurZ);
                    break;
                case 270f:
                    Spawnpos = new Vector3(CurSpawn.x, CurSpawn.y - P90, CurZ);
                    break;
                case 360f:
                    Spawnpos = new Vector3(CurSpawn.x + P90, CurSpawn.y, CurZ);
                    break;
            }
        }
        else
        {
            switch (Dic)
            {
                case 90f:
                    Spawnpos = new Vector3(CurSpawn.x, CurSpawn.y + PX, CurZ);
                    break;
                case 180f:
                    Spawnpos = new Vector3(CurSpawn.x - PX, CurSpawn.y, CurZ);
                    break;
                case 270f:
                    Spawnpos = new Vector3(CurSpawn.x, CurSpawn.y - PX, CurZ);
                    break;
                case 360f:
                    Spawnpos = new Vector3(CurSpawn.x + PX,CurSpawn.y, CurZ);
                    break;
            }
        }
    }

    void notemanage(float Dic)
    {
        is90 = false;
        if (Mathf.Abs(Dic - CurRotat) == 180f)
        {

            if (CurNoteNum >= 1)
            {
                Destroy(NoteArr[CurNoteNum]);
                NoteArr.RemoveAt(CurNoteNum);
                NoteDicArr.RemoveAt(CurNoteNum);
                --CurNoteNum;

                if (CurNoteNum+1 != NoteArr.Count)//생성할 노트와 다음 노트 연결
                {
                    if (ReGeneration(NoteDicArr[CurNoteNum + 1] - NoteDicArr[CurNoteNum])) { }
                }
                else
                {
                    Curnote = Note180;
                }

                if (CurNoteNum == 0)
                {
                    Curnote = Note180;

                    if (NoteArr.Count >= 1)
                    {
                        NoteDicArr[CurNoteNum] = NoteDicArr[CurNoteNum + 1];
                        CurRotat = NoteDicArr[CurNoteNum + 1]; ;
                    }
                    else
                    {
                        NoteDicArr[CurNoteNum] = 360f;
                        CurRotat = 360f;
                    }
                }

                CurSpawn = NoteArr[CurNoteNum].transform.position;

                Destroy(NoteArr[CurNoteNum]);

                Spawnpos = new Vector3(CurSpawn.x, CurSpawn.y, CurZ);
                SpawnRo = new Vector3(0f, 0f, NoteDicArr[CurNoteNum]);
                Notepre = Instantiate(Curnote, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                NoteArr[CurNoteNum] = Notepre;

                CurZ -= 0.01f;
            }
        }
        else
        {

            if (ReGeneration(Dic - CurRotat))
            {
                Destroy(NoteArr[CurNoteNum]); //직선노트 파괴

                if(CurNoteNum == 0)
                {
                    Curnote = Note180;
                    NoteDicArr[CurNoteNum] = Dic;
                    CurRotat = Dic;
                }

                if (CurNoteNum >= 1) CurSpawn = CurPos(PreSpawn, CurSpawn);
                Spawnpos = new Vector3(CurSpawn.x, CurSpawn.y, CurZ);
                SpawnRo = new Vector3(0f, 0f, CurRotat);
                Notepre = Instantiate(Curnote, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                NoteArr[CurNoteNum] = Notepre;
            }

            SelSpawnpos(Dic);


            if (CurNoteNum != NoteArr.Count - 1)//생성할 노트와 다음 노트 연결
            {
                if (ReGeneration(NoteDicArr[CurNoteNum + 1] - Dic)) { }
            }
            else
            {
                Curnote = Note180;
            }

            SpawnRo = new Vector3(0f, 0f, Dic);
            Notepre = Instantiate(Curnote, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
            NoteArr.Insert(CurNoteNum + 1, Notepre);
            NoteDicArr.Insert(CurNoteNum + 1, Dic);
            CurNoteNum++;

            CurZ += 0.01f;
        }
    }

    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.iseditor == true)
        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && Input.GetMouseButtonDown(0))//그룹화
            {
                for(int i = CurNoteNum +1; i < NoteArr.Count; i++)
                {
                    NoteArr[i].transform.parent = null;
                }
                Curnote = hit.collider.gameObject;
                CurNoteNum = NoteArr.IndexOf(Curnote);
                parent.transform.position = NoteArr[CurNoteNum].transform.position;
                for (int i = CurNoteNum + 1; i < NoteArr.Count; i++)
                {
                    NoteArr[i].transform.parent = parent.transform;
                }
            }

            

            Note = NoteArr[CurNoteNum];
            cam.target = Note.transform;
            CurSpawn = new Vector2(Note.transform.position.x, Note.transform.position.y); // 현재 좌표
            CurRotat = NoteDicArr[CurNoteNum];
            PreSpawn = CurSpawn;

            if (CurNoteNum >= 1)
            {
                PreSpawn = new Vector2(NoteArr[CurNoteNum - 1].transform.position.x, NoteArr[CurNoteNum - 1].transform.position.y);
            }

            Vector2 NextSpawn = Vector2.zero;
            


            if (Input.GetKeyDown(KeyCode.W))
            {
                notemanage(90f);
                
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                notemanage(180f);
               
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                notemanage(270f);
               
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                notemanage(360f);
               
            }
        }

        parent.transform.position = NoteArr[CurNoteNum].transform.position; // 그룹화 이동
    }
}
