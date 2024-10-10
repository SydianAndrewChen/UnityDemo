using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    private void Start()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particle.main;
        mainModule.loop = false;
        mainModule.stopAction = ParticleSystemStopAction.Callback;
    }
    private void OnParticleSystemStopped()
    {
        Debug.Log("Played");
        Invoke("Push", 0.1f);
    }
    void Push() 
    {
        PoolMgr.GetInstance().PushObj("ShellExplosion", gameObject);
    }
}
