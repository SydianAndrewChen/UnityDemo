using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire : MonoBehaviour
{
    public GameObject shellPrefab;
    private KeyCode fireKey;
    public Transform firePosition;

    public float shellSpeed;

    public AudioClip fireAudio;
    const string fireAudioName = "Tank/ShotFiring";
    public float fireVolume = 2f;

    // Start is called before the first frame update
    void Start()
    {
        fireKey = KeyCode.Space;
        shellSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            //Instantiate
            AudioMgr.GetInstance().PlaySoundAtObject(firePosition.gameObject, fireAudioName);
            //AudioSource.PlayClipAtPoint(fireAudio, transform.position, fireVolume);
            PoolMgr.GetInstance().GetObj("Shell", (shell) =>
            {
                AudioMgr.GetInstance().PlaySound(fireAudioName);
                shell.transform.parent = this.transform;
                shell.transform.position = firePosition.position;
                shell.transform.rotation = firePosition.rotation;
                shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * shellSpeed;
            });

        }
    }
}
