using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maincamera : MonoBehaviour
{
    public GameManger gm;
    public Transform target;
    [SerializeField]
    public float speed = 3f;
    public Vector3 targethead;
    [SerializeField]
    private float pluscamera = 1f;

    [SerializeField]
    public Vector2 center;
    public Vector2 size;
    float height;
    float width;


    void Start()
    {
        //height = Camera.main.orthographicSize;
        //width = height * Screen.width / Screen.height;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(center, size);
    //}

    void FixedUpdate()
    {
        if(target != null)
        {
            
            targethead = new Vector3(target.position.x, target.position.y + pluscamera, target.position.z);
        }
        
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(target.position.x, target.position.y + pluscamera , -10f);
        transform.position = Vector3.Lerp(transform.position, targethead, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        //float lx = size.x * 0.5f - width;
        //float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        //float ly = size.x * 0.5f - height;
        //float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        //transform.position = new Vector3(clampX, clampY, -10f);
    }
}
