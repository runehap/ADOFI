using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayManager : MonoBehaviour
{
    public SpriteRenderer NoteSr;
    public float judg1 = 0f;
    public float judg2 = 0f;
    public float JudgeAngle = 0f;
    public bool isSound = false;
    public Transform centerpos;
    public maincamera cam;
    public bool istriger = false;
    public int NoteNum2 = 0;
    public float cycledic = -1f;
    public NoteManager2 nm;
    public GameManger gm;
    public GameObject[] FireAndIce;
    public GameObject center;
    public GameObject side;
    public float radius = 3.0f;
    public float speed = 5f;
    public float angle = 0f;
    Vector3 judgedirection = new Vector3 (0f,0f,0f);

    // Start is called before the first frame update
    void Start()
    {
        center = FireAndIce[0];
        side = FireAndIce[1]; 
        centerpos = nm.NoteArr[NoteNum2].transform;
        FireAndIce[1].SetActive(false);
        FireAndIce[0].transform.position = nm.NoteArr[NoteNum2].transform.position;

    }

    bool judgment(float a)
    {
        if(a == 90f)
        { 
            judg1 = 60f;
            judg2 = 120f;
            if (JudgeAngle <= judg2 && JudgeAngle >= judg1)
                return true;
            else if (JudgeAngle <= judg2 + 15f && JudgeAngle >= judg1 - 15f)
                return true;
            else if (JudgeAngle <= judg2 + 30f && JudgeAngle >= judg1 - 30f)
                return true;
            else
                return false;
            
        }
        else if(a == 360f)
        {
            judg1 = -30f;
            judg2 = 30f;
            if (JudgeAngle <= judg2 && JudgeAngle >= judg1)
                return true;
            else if (JudgeAngle <= judg2 + 15f && JudgeAngle >= judg1 - 15f)
                return true;
            else if (JudgeAngle <= judg2 + 30f && JudgeAngle >= judg1 - 30f)
                return true;
            else
                return false;

        }
        else if (a == 270f)
        {
            judg1 = -120f;
            judg2 = -60f;
            if (JudgeAngle <= judg2 && JudgeAngle >= judg1)
                return true;
            else if (JudgeAngle <= judg2 + 15f && JudgeAngle >= judg1 - 15f)
                return true;
            else if (JudgeAngle <= judg2 + 30f && JudgeAngle >= judg1 - 30f)
                return true;
            else
                return false;
        }
        else if(a == 180f)
        {
            judg1 = 150f;
            judg2 = -150f;
            if (JudgeAngle <= judg2 && JudgeAngle >= -180f)
                return true;
            else if (JudgeAngle >= judg1 && JudgeAngle <= 180f)
                return true;
            else if (JudgeAngle <= judg2+15f && JudgeAngle >= -180f)
                return true;
            else if (JudgeAngle >= judg1-15f && JudgeAngle <= 180f)
                return true;
            else if (JudgeAngle <= judg2 + 30f && JudgeAngle >= -180f)
                return true;
            else if (JudgeAngle >= judg1 - 30f && JudgeAngle <= 180f)
                return true;
            else return false;
        }
        else
        {
            return false;
        }
    }

   


    // Update is called once per frame
    void Update()
    {

        if (gm.isplay == true)
        {
            if (NoteNum2 != nm.NoteArr.Count)
            {
                centerpos = nm.NoteArr[NoteNum2].transform;
            }
            center.transform.position = new Vector3(centerpos.transform.position.x, centerpos.transform.position.y, -1f);



            cam.target = centerpos;
            FireAndIce[1].SetActive(true);
            center.GetComponent<CircleCollider2D>().enabled = false;
            side.GetComponent<CircleCollider2D>().enabled = true;



            if (JudgeAngle <= 30)
            {
                if (isSound == false && NoteNum2 != 0)
                {
                    isSound = true;
                    center.GetComponent<AudioSource>().Play();
                }
            }

            if (Input.anyKeyDown)
            {
                if (isSound == false && NoteNum2 == 0)
                {
                    isSound = true;
                    center.GetComponent<AudioSource>().Play();
                }
                judgedirection = side.transform.position - center.transform.position;
                JudgeAngle = Mathf.Atan2(judgedirection.y, judgedirection.x) * Mathf.Rad2Deg;

                
                //Debug.Log(JudgeAngle);

               if (NoteNum2+1 != nm.NoteArr.Count && judgment(nm.NoteDicArr[NoteNum2 + 1]))
               {
                    JudgeAngle = 0f;
                    NoteNum2++;
                    NoteSr = nm.NoteArr[NoteNum2].GetComponent<SpriteRenderer>();
                    NoteSr.color = new Color(0.58f, 0.98f, 0.62f,1f);
                    GameObject temp = center;
                     center = side;
                    side = temp;

                    side.transform.position = new Vector3(nm.NoteArr[NoteNum2 - 1].transform.position.x, nm.NoteArr[NoteNum2 - 1].transform.position.y, -1f);
                    Vector3 direction = nm.NoteArr[NoteNum2 - 1].transform.position - nm.NoteArr[NoteNum2].transform.position;
                    angle = Mathf.Atan2(direction.y, direction.x);
                    isSound = false;
                    istriger = false;
               }
            }      
            

            //if(istriger == true)
            //{
            //    if (isSound == false)
            //    {
            //        isSound = true; 
            //        center.GetComponent<AudioSource>().Play();
            //    }

            //    if (Input.anyKeyDown)
            //    {
            //        if (NoteNum2+1 != nm.NoteArr.Count)
            //        {

            //            NoteNum2++;
            //            GameObject temp = center;
            //            center = side;
            //            side = temp;

            //            side.transform.position = new Vector3(nm.NoteArr[NoteNum2 - 1].transform.position.x , nm.NoteArr[NoteNum2 - 1].transform.position.y , -1f);
            //            Vector3 direction = nm.NoteArr[NoteNum2-1].transform.position - nm.NoteArr[NoteNum2].transform.position;
            //            angle = Mathf.Atan2(direction.y, direction.x);
            //            isSound = false;
            //            istriger = false;
            //        }
            //    }

            angle += cycledic * speed * Time.deltaTime;
            side.transform.position = center.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), -1f) * radius;
        }
        else
        {
            NoteNum2 = 0;
            center = FireAndIce[0];
            side = FireAndIce[1];
            FireAndIce[1].SetActive(false);
            FireAndIce[0].transform.position = nm.NoteArr[NoteNum2].transform.position;
        }
    }
}
