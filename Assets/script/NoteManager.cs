using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteManager : MonoBehaviour
{
    GameObject parent;
    public GameObject parentprefab;
    public float curNoteNum = 0f;
    public GameObject curNote;
    SpriteRenderer sr;
    public float currentZ = 0f; // ���������� ��Ʈ�� ���ο����ϴ� Z��ǥ
    public bool notetrigger = false;
    public int NoteNum = 0;
    public GameManger gm;
    public float plus = 0.05f;
    float plus2;
    public maincamera cam;
    public GameObject WidthNotePrfeb;
    public GameObject HeightNotePrfeb;
    public GameObject CurvNotePrfeb;
    public GameObject Note;
    public List<GameObject> NoteArr = new List<GameObject>();
    public List<string> NoteDicArr = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        NoteArr[0] = Note;
        NoteDicArr[0] = "D";
    }

    bool isWorS(string a)
    {
        if (a == "W" || a == "S")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool isAorD(string a)
    {
        if (a == "A" || a == "D")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.iseditor == true)
        {


            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && Input.GetMouseButtonDown(0))
            {
                curNote = hit.collider.gameObject;
                NoteNum = NoteArr.IndexOf(curNote);
            }



            Note = NoteArr[NoteNum];
            cam.target = Note.transform;
            float preSpawnX = Note.transform.position.x;//�� �Ҵ� �ȵ� ������
            float preSpawnY = Note.transform.position.y;

            if (NoteNum >= 1)
            {
                preSpawnX = NoteArr[NoteNum - 1].transform.position.x; //  ��Ʈ�����Ҷ� ������ ������Ʈ 90���� �ٽ� �����ϱ����� �� ��Ʈ ��ǥ
                preSpawnY = NoteArr[NoteNum - 1].transform.position.y;
            }

            float SpawnX = Note.transform.position.x; // ���� �����Ҷ� �� ���� ��Ʈ ��ǥ
            float SpawnY = Note.transform.position.y;
            float PX = Note.GetComponent<note>().NoteWidth; // ��Ʈ ���� ��������
            float PY = Note.GetComponent<note>().NoteHeight;
            float P90 = 0.19f * 9f + plus * 9f;


            if (Input.GetKeyDown(KeyCode.D))
            {
                Vector3 Prefabpos = NoteArr[NoteNum].transform.position;
                GameObject notepre;// ��Ʈ ����� ����
                if (NoteDicArr[NoteNum] != "A") // A�� ��Ʈ�� ��������
                {
                  
                    if (NoteNum >= 0 && isWorS(NoteDicArr[NoteNum])) // �̹� ��Ʈ ������ ���̴��� Ȯ�� // ������Ʈ�� 90�� ��Ʈ�� �ٲٷ���
                    {
                        if (NoteNum >= 1)// ��Ʈ ���� 2�� �̻�
                        {
                            Destroy(NoteArr[NoteNum]); //������Ʈ �ı�
                            NoteArr.RemoveAt(NoteNum);//������Ʈ ����Ʈ���� ����
                            --NoteNum;
                            Vector3 Spawnpos; //��Ʈ ���� ��ġ
                            Vector3 SpawnRo; // ��Ʈ ���� ����

                            if (NoteDicArr[NoteNum + 1] == "W")//������Ʈ�� 90�� ��Ʈ�� ����
                            {
                                Spawnpos = new Vector3(preSpawnX, preSpawnY + P90, currentZ);
                                SpawnRo = new Vector3(0f, 0f, -90f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }
                            else if (NoteDicArr[NoteNum + 1] == "S")
                            {
                                Spawnpos = new Vector3(preSpawnX, preSpawnY - P90, currentZ);
                                SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }
                            Note = NoteArr[NoteNum]; // �ٲ��Ʈ ���� ����
                            SpawnX = Note.transform.position.x;
                            SpawnY = Note.transform.position.y;
                            PX = Note.GetComponent<note>().NoteWidth;
                            PY = Note.GetComponent<note>().NoteHeight;


                            if(NoteNum != NoteArr.Count-1)
                            {
                                if (NoteDicArr[NoteNum + 1] == "W")//������Ʈ�� 90�� ��Ʈ�� ����
                                {
                                    Spawnpos = new Vector3(SpawnX + P90, SpawnY, currentZ);
                                    SpawnRo = new Vector3(0f, 0f, 90f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                    NoteArr.Insert(NoteNum + 1, notepre);

                                }
                                else if (NoteDicArr[NoteNum + 1] == "S")
                                {
                                    Spawnpos = new Vector3(SpawnX + P90, SpawnY, currentZ);
                                    SpawnRo = new Vector3(0f, 0f, 180f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                    NoteArr.Insert(NoteNum + 1, notepre);

                                }
                                else
                                {
                                    Spawnpos = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ); //90�� ��Ʈ ������ �� ������Ʈ ������ǥ
                                    SpawnRo = new Vector3(0f, 0f, 0f); // = ��������
                                    notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject; // ������Ʈ ������ ����
                                    NoteArr.Insert(NoteNum + 1, notepre); //��Ʈ ����Ʈ�� ����
                                }
                            }
                            else
                            {
                                Spawnpos = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ); //90�� ��Ʈ ������ �� ������Ʈ ������ǥ
                                SpawnRo = new Vector3(0f, 0f, 0f); // = ��������
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject; // ������Ʈ ������ ����
                                NoteArr.Insert(NoteNum + 1, notepre); //��Ʈ ����Ʈ�� ����
                            }
                        }

                    }
                    else// �Ȳ��̸� �׳� ������Ʈ ����
                    {
                        if (NoteNum != NoteArr.Count - 1)
                        {
                            if (NoteDicArr[NoteNum + 1] == "W")//������Ʈ�� 90�� ��Ʈ�� ����
                            {
                                Vector3 S = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ);
                                Vector3 SR = new Vector3(0f, 0f, 90f);
                                notepre = Instantiate(CurvNotePrfeb, S, Quaternion.Euler(SR)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);

                            }
                            else if (NoteDicArr[NoteNum + 1] == "S")
                            {
                                Vector3 S = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ);
                                Vector3 SR = new Vector3(0f, 0f, 180f);
                                notepre = Instantiate(CurvNotePrfeb, S, Quaternion.Euler(SR)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);

                            }
                            else
                            {
                                Vector3 Spawnpos = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ);
                                Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                            }

                            if (NoteDicArr[NoteNum] == "D")
                            {
                                Destroy(NoteArr[NoteNum ]); 
                                NoteArr.RemoveAt(NoteNum);

                                Vector3 Spawnpos = new Vector3(preSpawnX + P90 + 0.04f, preSpawnY, currentZ);
                                Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum, notepre);
                            }
                        }
                        else
                        {
                            Vector3 Spawnpos = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ);
                            Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                            notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                            NoteArr.Insert(NoteNum + 1, notepre);
                        }


                    }


                    currentZ += 0.1f;
                    NoteDicArr.Insert(NoteNum + 1, "D");//��Ʈ ���� ����
                    NoteNum++;//��Ʈ ���� ����

                }
                else// ��Ʈ�� ���ﶧ
                {
                    
                    if (NoteNum >= 1) // ��Ʈ������ ��� 2�� �̻��϶�
                    {
                        currentZ -= 0.1f;
                        Destroy(NoteArr[NoteNum]);// ��Ʈ �����
                        NoteArr.RemoveAt(NoteNum);//��Ʈ ����Ʈ���� ����
                        NoteDicArr.RemoveAt(NoteNum);//��Ʈ ���⸮��Ʈ���� ����
                        NoteNum--;// ��Ʈ ���� ����
                        if (NoteNum >= 1)// ������ ���� ��Ʈ�� 2�� �̻��� ���
                        {
                            if(NoteNum == NoteArr.Count-1)
                            {
                                if (NoteDicArr[NoteNum] == "W" || NoteDicArr[NoteNum] == "S")//������Ʈ�� 90�� ��Ʈ������ Ȯ�� // 90����Ʈ�� ������Ʈ�� �ٲٷ���
                                {
                                    int dic = 0;// ��Ʈ ���� ����
                                    if (NoteDicArr[NoteNum] == "W")
                                    {
                                        dic = 1;
                                    }
                                    else if (NoteDicArr[NoteNum] == "S")
                                    {
                                        dic = -1;
                                    }

                                    Destroy(NoteArr[NoteNum]); // 90�� ��Ʈ ����
                                    NoteArr.RemoveAt(NoteNum);// ��Ʈ ����Ʈ���� ����

                                    Note = NoteArr[NoteNum - 1];// 90�� ��Ʈ�� ���� ��Ʈ ���� ����
                                    SpawnX = Note.transform.position.x;
                                    SpawnY = Note.transform.position.y;
                                    PX = Note.GetComponent<note>().NoteWidth;
                                    PY = Note.GetComponent<note>().NoteHeight;

                                    Vector3 Spawnpos;

                                    if (NoteDicArr[NoteNum - 1] == "A" || NoteDicArr[NoteNum - 1] == "D")//������Ʈ�� 90�� �ΰ��
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY + (PY + plus * 9) * dic, currentZ); // ���� ��Ʈ ����                               
                                    }
                                    else//�ƴѰ��
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY + PY * dic, currentZ);
                                    }
                                    Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                    notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);
                                }
                            }
                            else
                            {


                                Destroy(NoteArr[NoteNum]); // 90�� ��Ʈ ����
                                NoteArr.RemoveAt(NoteNum);// ��Ʈ ����Ʈ���� ����

                                Note = NoteArr[NoteNum-1];// 90�� ��Ʈ�� ���� ��Ʈ ���� ����
                                SpawnX = Note.transform.position.x;
                                SpawnY = Note.transform.position.y;
                                PX = Note.GetComponent<note>().NoteWidth;
                                PY = Note.GetComponent<note>().NoteHeight;

                                Vector3 Spawnpos = Vector3.zero;
                                Vector3 SR = Vector3.zero;

                                if (isWorS(NoteDicArr[NoteNum+1]) && isWorS(NoteDicArr[NoteNum]))
                                {   
                                    if(NoteDicArr[NoteNum - 1] == "W")
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY + P90 + 0.04f, currentZ);
                                        SR = new Vector3(0f, 0f, 0f);
                                    }
                                    if (NoteDicArr[NoteNum - 1] == "S")
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY - P90 - 0.04f, currentZ);
                                        SR = new Vector3(0f, 0f, 0f);
                                    }

                                    notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SR)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);
                                }
                                else if (NoteDicArr[NoteNum + 1] == "W")//������Ʈ�� 90�� ��Ʈ�� ����
                                {
                                    Spawnpos = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ);
                                    SR = new Vector3(0f, 0f, 0f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SR)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);

                                }
                                else if (NoteDicArr[NoteNum + 1] == "S")
                                {
                                    Spawnpos = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ);
                                    SR = new Vector3(0f, 0f, -90f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SR)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);

                                }
                               

                            }
                        }

                    }

                }
                if (NoteNum != NoteArr.Count - 1 && Prefabpos != Vector3.zero)
                {
                    Vector3 Spawnpos = NoteArr[NoteNum].transform.position;
                    Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                    if (NoteArr[NoteNum].transform.parent == null)
                    {
                        parent = Instantiate(parentprefab, Prefabpos, Quaternion.Euler(SpawnRo)) as GameObject;
                    }

                    for (int i = NoteNum + 1; i < NoteArr.Count; i++)
                    {
                        NoteArr[i].transform.parent = parent.transform;
                    }
                    parent.transform.position = NoteArr[NoteNum].transform.position;

                }

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Vector3 Prefabpos = NoteArr[NoteNum].transform.position;
                GameObject notepre;
                if (NoteDicArr[NoteNum] != "D")
                {
                    
                    if (NoteNum >= 0 && isWorS(NoteDicArr[NoteNum]))
                    {
                        if (NoteNum >= 1)
                        {
                            Destroy(NoteArr[NoteNum]);
                            NoteArr.RemoveAt(NoteNum);
                            --NoteNum;
                            Vector3 Spawnpos;
                            Vector3 SpawnRo;

                            if (NoteDicArr[NoteNum + 1] == "W")
                            {
                                Spawnpos = new Vector3(preSpawnX, preSpawnY + P90, currentZ);
                                SpawnRo = new Vector3(0f, 0f, 180f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }
                            else if (NoteDicArr[NoteNum + 1] == "S")
                            {
                                Spawnpos = new Vector3(preSpawnX, preSpawnY - P90, currentZ);
                                SpawnRo = new Vector3(0f, 0f, 90f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }

                            Note = NoteArr[NoteNum];
                            SpawnX = Note.transform.position.x;
                            SpawnY = Note.transform.position.y;
                            PX = Note.GetComponent<note>().NoteWidth;
                            PY = Note.GetComponent<note>().NoteHeight;

                            if (NoteNum != NoteArr.Count - 1)
                            {
                                if (NoteDicArr[NoteNum + 1] == "W")//������Ʈ�� 90�� ��Ʈ�� ����
                                {
                                    Spawnpos = new Vector3(SpawnX - P90, SpawnY, currentZ);
                                    SpawnRo = new Vector3(0f, 0f, 0f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                    NoteArr.Insert(NoteNum + 1, notepre);

                                }
                                else if (NoteDicArr[NoteNum + 1] == "S")
                                {
                                    Spawnpos = new Vector3(SpawnX - P90, SpawnY, currentZ);
                                    SpawnRo = new Vector3(0f, 0f, -90f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                    NoteArr.Insert(NoteNum + 1, notepre);

                                }
                                else
                                {
                                    Spawnpos = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ); //90�� ��Ʈ ������ �� ������Ʈ ������ǥ
                                    SpawnRo = new Vector3(0f, 0f, 0f); // = ��������
                                    notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject; // ������Ʈ ������ ����
                                    NoteArr.Insert(NoteNum + 1, notepre); //��Ʈ ����Ʈ�� ����
                                }
                            }
                            else
                            {
                                Spawnpos = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ); //90�� ��Ʈ ������ �� ������Ʈ ������ǥ
                                SpawnRo = new Vector3(0f, 0f, 0f); // = ��������
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject; // ������Ʈ ������ ����
                                NoteArr.Insert(NoteNum + 1, notepre); //��Ʈ ����Ʈ�� ����
                            }
                        }

                    }
                    else
                    {
                        if (NoteNum != NoteArr.Count - 1)
                        {
                            if (NoteDicArr[NoteNum + 1] == "W")//������Ʈ�� 90�� ��Ʈ�� ����
                            {
                                Vector3 S = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ);
                                Vector3 SR = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(CurvNotePrfeb, S, Quaternion.Euler(SR)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);

                            }
                            else if (NoteDicArr[NoteNum + 1] == "S")
                            {
                                Vector3 S = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ);
                                Vector3 SR = new Vector3(0f, 0f, -90f);
                                notepre = Instantiate(CurvNotePrfeb, S, Quaternion.Euler(SR)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);

                            }
                            else
                            {
                                Vector3 Spawnpos = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ);
                                Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                            }

                            if (NoteDicArr[NoteNum] == "A")
                            {
                                Destroy(NoteArr[NoteNum]);
                                NoteArr.RemoveAt(NoteNum);

                                Vector3 Spawnpos = new Vector3(preSpawnX - P90 - 0.04f, preSpawnY, currentZ);
                                Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum, notepre);
                            }
                        }
                        else
                        {
                            Vector3 Spawnpos = new Vector3(SpawnX - P90 - 0.04f, SpawnY, currentZ);
                            Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                            notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                            NoteArr.Insert(NoteNum + 1, notepre);
                        }
                    }
                    currentZ += 0.1f;
                    NoteDicArr.Insert(NoteNum + 1, "A");
                    NoteNum++;
                }
                else
                {
                    
                    if (NoteNum >= 1)
                    {
                        currentZ -= 0.1f;
                        Destroy(NoteArr[NoteNum]);// ��Ʈ �����
                        NoteArr.RemoveAt(NoteNum);//��Ʈ ����Ʈ���� ����
                        NoteDicArr.RemoveAt(NoteNum);//��Ʈ ���⸮��Ʈ���� ����
                        NoteNum--;// ��Ʈ ���� ����
                        if (NoteNum >= 1)
                        {
                            if(NoteNum == NoteArr.Count-1)
                            {
                                if (isWorS(NoteDicArr[NoteNum]))
                                {
                                    int dic = 0;
                                    if (NoteDicArr[NoteNum] == "W")
                                    {
                                        dic = 1;
                                    }
                                    else if (NoteDicArr[NoteNum] == "S")
                                    {
                                        dic = -1;
                                    }

                                    Destroy(NoteArr[NoteNum]);
                                    NoteArr.RemoveAt(NoteNum);

                                    Note = NoteArr[NoteNum - 1];
                                    SpawnX = Note.transform.position.x;
                                    SpawnY = Note.transform.position.y;
                                    PX = Note.GetComponent<note>().NoteWidth;
                                    PY = Note.GetComponent<note>().NoteHeight;

                                    Vector3 Spawnpos;
                                    if (NoteDicArr[NoteNum - 1] == "A" || NoteDicArr[NoteNum - 1] == "D")
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY + (PY + plus * 9) * dic, currentZ);
                                    }
                                    else
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY + PY * dic, currentZ);
                                    }
                                    Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                    notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);
                                }
                            }
                            else
                            {
                                Destroy(NoteArr[NoteNum]); // 90�� ��Ʈ ����
                                NoteArr.RemoveAt(NoteNum);// ��Ʈ ����Ʈ���� ����

                                Note = NoteArr[NoteNum - 1];// 90�� ��Ʈ�� ���� ��Ʈ ���� ����
                                SpawnX = Note.transform.position.x;
                                SpawnY = Note.transform.position.y;
                                PX = Note.GetComponent<note>().NoteWidth;
                                PY = Note.GetComponent<note>().NoteHeight;

                                Vector3 Spawnpos = Vector3.zero;
                                Vector3 SR = Vector3.zero;

                                if (isWorS(NoteDicArr[NoteNum + 1]) && isWorS(NoteDicArr[NoteNum]))
                                {
                                    if (NoteDicArr[NoteNum - 1] == "W")
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY + P90 + 0.04f, currentZ);
                                        SR = new Vector3(0f, 0f, 0f);
                                    }
                                    if (NoteDicArr[NoteNum - 1] == "S")
                                    {
                                        Spawnpos = new Vector3(SpawnX, SpawnY - P90 - 0.04f, currentZ);
                                        SR = new Vector3(0f, 0f, 0f);
                                    }

                                    notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SR)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);
                                }
                                else if (NoteDicArr[NoteNum + 1] == "W")//������Ʈ�� 90�� ��Ʈ�� ����
                                {
                                    Spawnpos = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ);
                                    SR = new Vector3(0f, 0f, 90f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SR)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);

                                }
                                else if (NoteDicArr[NoteNum + 1] == "S")
                                {
                                    Spawnpos = new Vector3(SpawnX + P90 + 0.04f, SpawnY, currentZ);
                                    SR = new Vector3(0f, 0f, 180f);
                                    notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SR)) as GameObject;
                                    NoteArr.Insert(NoteNum, notepre);

                                }
                            }
                        }

                    }

                }
                if (NoteNum != NoteArr.Count - 1 && Prefabpos != Vector3.zero)
                {
                    Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                    if (NoteArr[NoteNum].transform.parent == null)
                    {
                        parent = Instantiate(parentprefab, Prefabpos, Quaternion.Euler(SpawnRo)) as GameObject;
                    }

                    for (int i = NoteNum + 1; i < NoteArr.Count; i++)
                    {
                        NoteArr[i].transform.parent = parent.transform;
                    }
                    parent.transform.position = NoteArr[NoteNum].transform.position;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Vector3 Prefabpos = NoteArr[NoteNum].transform.position;
                GameObject notepre;
                if (NoteDicArr[NoteNum] != "S")
                {
                    
                    if (NoteNum >= 0 && isAorD(NoteDicArr[NoteNum]))
                    {
                        if (NoteNum >= 1)
                        {
                            Destroy(NoteArr[NoteNum]); //������Ʈ �ı�
                            NoteArr.RemoveAt(NoteNum);//������Ʈ ����Ʈ���� ����
                            --NoteNum;
                            Vector3 Spawnpos;
                            Vector3 SpawnRo;

                            if (NoteDicArr[NoteNum + 1] == "D")
                            {
                                Spawnpos = new Vector3(preSpawnX + P90, preSpawnY, currentZ);
                                SpawnRo = new Vector3(0f, 0f, 90f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }
                            else if (NoteDicArr[NoteNum + 1] == "A")
                            {
                                Spawnpos = new Vector3(preSpawnX - P90, preSpawnY, currentZ);
                                SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }

                            Note = NoteArr[NoteNum];
                            SpawnX = Note.transform.position.x;
                            SpawnY = Note.transform.position.y;
                            PX = Note.GetComponent<note>().NoteWidth;
                            PY = Note.GetComponent<note>().NoteHeight;
                            Spawnpos = new Vector3(SpawnX, SpawnY + P90 + 0.04f, currentZ);
                            SpawnRo = new Vector3(0f, 0f, 0f);
                            notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                            NoteArr.Insert(NoteNum + 1, notepre);

                        }
                    }
                    else
                    {
                        Vector3 Spawnpos = new Vector3(SpawnX, SpawnY + PY, currentZ);
                        Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                        notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                        NoteArr.Insert(NoteNum + 1, notepre);
                    }
                    currentZ += 0.1f;
                    NoteDicArr.Insert(NoteNum + 1, "W");//��Ʈ ���� ����
                    NoteNum++;//��Ʈ ���� ����
                }
                else
                {
                    
                    if (NoteNum >= 1)
                    {
                        currentZ -= 0.1f;
                        Destroy(NoteArr[NoteNum]);// ��Ʈ �����
                        NoteArr.RemoveAt(NoteNum);//��Ʈ ����Ʈ���� ����
                        NoteDicArr.RemoveAt(NoteNum);//��Ʈ ���⸮��Ʈ���� ����
                        NoteNum--;// ��Ʈ ���� ����

                        if (NoteNum >= 1)
                        {
                            if (isAorD(NoteDicArr[NoteNum]))
                            {
                                int dic = 0;
                                if (NoteDicArr[NoteNum] == "D")
                                {
                                    dic = 1;
                                }
                                else if (NoteDicArr[NoteNum] == "A")
                                {
                                    dic = -1;
                                }

                                Destroy(NoteArr[NoteNum]); // 90�� ��Ʈ ����
                                NoteArr.RemoveAt(NoteNum);// ��Ʈ ����Ʈ���� ����

                                Note = NoteArr[NoteNum - 1];
                                SpawnX = Note.transform.position.x;
                                SpawnY = Note.transform.position.y;
                                PX = Note.GetComponent<note>().NoteWidth;
                                PY = Note.GetComponent<note>().NoteHeight;

                                Vector3 Spawnpos;
                                if (NoteDicArr[NoteNum - 1] == "S" || NoteDicArr[NoteNum - 1] == "W")
                                {
                                    Spawnpos = new Vector3(SpawnX + (PX + plus * 9) * dic, SpawnY, currentZ);
                                }
                                else
                                {
                                    Spawnpos = new Vector3(SpawnX + PX * dic, SpawnY, currentZ);
                                }
                                Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum, notepre);
                            }
                        }

                    }

                }
                if (NoteNum != NoteArr.Count - 1 && Prefabpos != Vector3.zero)
                {
                    Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                    if (NoteArr[NoteNum].transform.parent == null)
                    {
                        parent = Instantiate(parentprefab, Prefabpos, Quaternion.Euler(SpawnRo)) as GameObject;
                    }

                    for (int i = NoteNum + 1; i < NoteArr.Count; i++)
                    {
                        NoteArr[i].transform.parent = parent.transform;
                    }
                    parent.transform.position = NoteArr[NoteNum].transform.position;
                }

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Vector3 Prefabpos = NoteArr[NoteNum].transform.position;
                GameObject notepre;
                if (NoteDicArr[NoteNum] != "W")
                {
                    
                    if (NoteNum >= 0 && isAorD(NoteDicArr[NoteNum]))
                    {
                        if (NoteNum >= 1)
                        {
                            Destroy(NoteArr[NoteNum]);
                            NoteArr.RemoveAt(NoteNum);
                            NoteDicArr.RemoveAt(NoteNum);//������Ʈ ����Ʈ���� ����
                            --NoteNum;
                            Vector3 Spawnpos;
                            Vector3 SpawnRo;

                            if (NoteDicArr[NoteNum + 1] == "D")
                            {
                                Spawnpos = new Vector3(preSpawnX + P90, preSpawnY, currentZ);
                                SpawnRo = new Vector3(0f, 0f, 180f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }
                            else if (NoteDicArr[NoteNum + 1] == "A")
                            {
                                Spawnpos = new Vector3(preSpawnX - P90, preSpawnY, currentZ);
                                SpawnRo = new Vector3(0f, 0f, -90f);
                                notepre = Instantiate(CurvNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum + 1, notepre);
                                ++NoteNum;
                            }

                            Note = NoteArr[NoteNum];
                            SpawnX = Note.transform.position.x;
                            SpawnY = Note.transform.position.y;
                            PX = Note.GetComponent<note>().NoteWidth;
                            PY = Note.GetComponent<note>().NoteHeight;

                            Spawnpos = new Vector3(SpawnX, SpawnY - P90 - 0.04f, currentZ);
                            SpawnRo = new Vector3(0f, 0f, 0f);
                            notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                            NoteArr.Insert(NoteNum + 1, notepre);
                        }

                    }
                    else
                    {
                        Vector3 Spawnpos = new Vector3(SpawnX, SpawnY - PY, currentZ);
                        Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                        notepre = Instantiate(HeightNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                        NoteArr.Insert(NoteNum + 1, notepre);
                    }
                    currentZ += 0.1f;
                    NoteDicArr.Insert(NoteNum + 1, "S");//��Ʈ ���� ����
                    NoteNum++;//��Ʈ ���� ����
                }
                else
                {
                    
                    if (NoteNum >= 1)
                    {
                        currentZ -= 0.1f;
                        Destroy(NoteArr[NoteNum]);// ��Ʈ �����
                        NoteArr.RemoveAt(NoteNum);//��Ʈ ����Ʈ���� ����
                        NoteDicArr.RemoveAt(NoteNum);//��Ʈ ���⸮��Ʈ���� ����
                        NoteNum--;
                        if (NoteNum >= 1)
                        {
                            if (isAorD(NoteDicArr[NoteNum]))
                            {
                                int dic = 0;
                                if (NoteDicArr[NoteNum] == "D")
                                {
                                    dic = 1;
                                }
                                else if (NoteDicArr[NoteNum] == "A")
                                {
                                    dic = -1;
                                }

                                Destroy(NoteArr[NoteNum]); // 90�� ��Ʈ ����
                                NoteArr.RemoveAt(NoteNum);// ��Ʈ ����Ʈ���� ����

                                Note = NoteArr[NoteNum - 1];
                                SpawnX = Note.transform.position.x;
                                SpawnY = Note.transform.position.y;
                                PX = Note.GetComponent<note>().NoteWidth;
                                PY = Note.GetComponent<note>().NoteHeight;

                                Vector3 Spawnpos;
                                if (NoteDicArr[NoteNum - 1] == "S" || NoteDicArr[NoteNum - 1] == "W")
                                {
                                    Spawnpos = new Vector3(SpawnX + (PX + plus * 9) * dic, SpawnY, currentZ);
                                }
                                else
                                {
                                    Spawnpos = new Vector3(SpawnX + PX * dic, SpawnY, currentZ);
                                }
                                Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                                notepre = Instantiate(WidthNotePrfeb, Spawnpos, Quaternion.Euler(SpawnRo)) as GameObject;
                                NoteArr.Insert(NoteNum, notepre);
                            }
                        }
                    }
                }
                if (NoteNum != NoteArr.Count - 1 && Prefabpos != Vector3.zero)
                {
                    
                    Vector3 SpawnRo = new Vector3(0f, 0f, 0f);
                    if (NoteArr[NoteNum].transform.parent == null)
                    {
                        parent = Instantiate(parentprefab, Prefabpos, Quaternion.Euler(SpawnRo)) as GameObject;
                    }

                    for (int i = NoteNum + 1; i < NoteArr.Count; i++)
                    {
                        NoteArr[i].transform.parent = parent.transform;
                    }
                    parent.transform.position = NoteArr[NoteNum].transform.position;
                }
            }
        }
    }
}

