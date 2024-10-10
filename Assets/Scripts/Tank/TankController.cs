using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 5;
    public float angularSpeed = 30;
    private Rigidbody rb;

    const string idleAudioName = "Tank/EngineIdle";
    const string driveAudioName = "Tank/EngineDriving";
    private AudioSource idleAudioSource;
    private AudioSource driveAudioSource;

    public float tankDriveVolume = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioMgr.GetInstance().PlaySoundAtObject(gameObject, idleAudioName, false);

        AudioMgr.GetInstance().PlaySoundAtObject(gameObject, driveAudioName, false);
    }
    private void FixedUpdate()
    {

        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalMovement) > 0.1 || Mathf.Abs(horizontalMovement) > 0.1)
        {
            AudioMgr.GetInstance().PlaySoundAtObject(gameObject, driveAudioName);
            AudioMgr.GetInstance().StopSoundAtObject(gameObject, idleAudioName);
        }
        else
        {
            AudioMgr.GetInstance().PlaySoundAtObject(gameObject, idleAudioName);
            AudioMgr.GetInstance().StopSoundAtObject(gameObject, driveAudioName);

        }

        rb.velocity = transform.forward * verticalMovement * speed;
        rb.angularVelocity = transform.up * horizontalMovement * angularSpeed;
    }

}
