using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    public GameObject shellExplosionPrefab;
    public AudioClip shellExplosionAudio;

    const string shellExplosionName = "ShellExplosion";

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tank") 
        {
            other.SendMessage("TakeDamage");
        }
        PoolMgr.GetInstance().GetObj(shellExplosionName, (explosion) =>
        {
            explosion.transform.position = transform.position;
            explosion.transform.rotation = transform.rotation;
            explosion.gameObject.name = shellExplosionName;
        });
        AudioSource.PlayClipAtPoint(shellExplosionAudio, transform.position);
        PoolMgr.GetInstance().PushObj("Shell", this.gameObject);
    }
}
