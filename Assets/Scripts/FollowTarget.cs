using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject player1;
    private Vector3 offsetTrans = new Vector3(0, 30, -30);
    private Vector3 offsetAngle = new Vector3(45, 0, 0);
    private Vector3 cameraPos;
    private Vector3 cameraSpeed = Vector3.zero;


    private bool isSingleMode = true;
    private bool isLoaded = false;
    private bool isFollowing = false;

    float fovRatio;
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - (player1.position + player2.position) / 2;
        //fovRatio = GetComponent<Camera>().fieldOfView / Vector3.Distance(player1.position, player2.position);
        EventCenter.GetInstance().AddEventListener<GameObject>("TankLoadComplete", (obj) => {
            Debug.Log("Loaded!");
            this.isLoaded = true;
            this.player1 = obj;
            this.transform.parent = player1.transform;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLoaded) return;

        //this.cameraPos = player1.transform.position +
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, offsetTrans, ref cameraSpeed, 2, 100);
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, offsetAngle, 0.01f);
        if (isFollowing && isSingleMode) 
        {

        }
        
    }
    /*
    void MultiplePlayer() 
    {
        Vector3 center = new Vector3();
        float distance = 5;
        if (player1 != null && player2 != null)
        {
            center = (player1.position + player2.position) / 2;
            distance = Vector3.Distance(player1.position, player2.position);
        }
        else if (player1 != null)
        {
            center = player1.position;
        }
        else if (player2 != null)
        {
            center = player2.position;
        }
        transform.position = center + offset;

        GetComponent<Camera>().fieldOfView = fovRatio * distance;
    }
    */
}
