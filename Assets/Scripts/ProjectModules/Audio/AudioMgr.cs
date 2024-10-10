using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AudioMgr : BaseManager<AudioMgr>
{
    // 物体以及对应的音效管理物体
    private Dictionary<string, List<AudioSource>> soundObjDic = new Dictionary<string, List<AudioSource>>();

    private AudioSource BGMSource;
    private float BGMVolume = 1;
    private float soundVolume = 1;
    private float overallVolume = 1;

    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();

    public AudioMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }
    void Update() 
    {
        for (int i = soundList.Count - 1; i >= 0; --i) 
        {
            if (!soundList[i].isPlaying) 
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }
    public void PlayBGM(string name)
    {
        if (BGMSource == null) 
        {
            GameObject obj = new GameObject();
            obj.name = "BGM";
            BGMSource = obj.AddComponent<AudioSource>();
        }

        //异步加载背景音乐
        ResourceMgr.GetInstance().LoadAsync<AudioClip>("Audio/Music/" + name, (clip) => {
            if (clip == null) 
            {
                Debug.LogError("Wrong path: Audio/Music/" + name);
                return;
            }
            BGMSource.clip = clip;
            BGMSource.Play();
            BGMSource.loop = true;
        });
    }

    public void SetBGMVolume(float vol)
    {
        if (BGMSource == null || BGMSource.clip == null) 
        {
            Debug.Log("BGM is not instantiated or loaded yet!");
            return;
        }
        BGMVolume = vol;
        BGMSource.volume = overallVolume * BGMVolume;
    }

    public void PauseBGM()
    {
        if (BGMSource ==null || BGMSource.clip == null)
        {
            Debug.Log("BGM is not instantiated or loaded yet!");
            return;
        }
        BGMSource.Pause();
    }

    public void StopBGM()
    {
        if (BGMSource == null || BGMSource.clip == null)
        {
            Debug.Log("BGM is not instantiated or loaded yet!");
            return;
        }
        BGMSource.Stop();
    }

    public void PlaySound(string name, bool playOnAwake = false, bool isLoop = false, UnityAction<AudioSource> callBack = null)
    {
        if (soundObj == null) 
        {
            soundObj = new GameObject();
            soundObj.name = "Sounds";
        }

        AudioSource soundSource = FindSound(name);
        if (soundSource != null)
        {
            if (!soundSource.isPlaying)
                soundSource.Play();
        }
        else 
        {
            ResourceMgr.GetInstance().LoadAsync<AudioClip>("Audio/Sound/" + name, (clip) => {
                soundSource = soundObj.AddComponent<AudioSource>();
                soundSource.clip = clip;
                soundSource.clip.name = name;
                //AudioSource.PlayClipAtPoint(clip, position, soundVolume * overallVolume);
                soundSource.loop = isLoop;
                soundSource.volume = soundVolume;
                soundSource.playOnAwake = playOnAwake;
                if (playOnAwake) soundSource.Play();
                soundList.Add(soundSource);
                if (callBack != null)
                    callBack(soundSource);
            }, true);
        }
    }

    // 需要传入特定的物体以找到对应的声效管理，在这个物体上添加以及管理声效
    public void PlaySoundAtObject(GameObject obj, string soundName, bool playOnAwake = false, bool isLoop = false, UnityAction<AudioSource> callBack = null) 
    {
        GameObject sO = null;
        string sOName = obj.name + " Sounds Group";
        if (!soundObjDic.ContainsKey(obj.name))
        {
            sO = new GameObject();
            sO.name = sOName;
            sO.transform.parent = obj.transform;
            sO.transform.localPosition = Vector3.zero;
            sO.transform.localEulerAngles = Vector3.zero;
            List<AudioSource> sL = new List<AudioSource>();
            soundObjDic.Add(obj.name, sL);
        }
        else
        {
            sO = obj.transform.Find(sOName).gameObject;
            if (sO == null) 
            {
                Debug.LogError("Something wrong with sound!");
                return;
            }
        }

        AudioSource soundSource = FindSound(soundName, obj.name);
        if (soundSource != null)
        {
            if (!soundSource.isPlaying)
                soundSource.Play();
        }
        else
        {
            ResourceMgr.GetInstance().LoadAsync<AudioClip>("Audio/Sound/" + soundName, (clip) => {
                soundSource = sO.AddComponent<AudioSource>();
                soundSource.clip = clip;
                soundSource.clip.name = soundName;
                soundSource.loop = isLoop;
                soundSource.volume = soundVolume;
                soundSource.playOnAwake = playOnAwake;
                if (playOnAwake) soundSource.Play();
                soundObjDic[obj.name].Add(soundSource);
                if (callBack != null)
                    callBack(soundSource);
            }, true);
        }
    }

    public void SetSoundVolume(float vol) 
    {
        soundVolume = vol;
        for(int i = 0; i < soundList.Count; ++i)
        {
            soundList[i].volume = overallVolume * soundVolume;
        }
    }

    public void StopSound(AudioSource audioSource)
    {
        if (soundList.Contains(audioSource))
        {
            soundList.Remove(audioSource);
            audioSource.Stop();
            GameObject.Destroy(audioSource);
        }
    }

    public void StopSound(string clipName)
    {
        AudioSource source = FindSound(clipName);
        if (source == null)
        {
            Debug.LogWarning("Stopping a non-existing sound! ClipName : " + clipName);
            return;
        }
        else 
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
    public void StopSoundAtObject(GameObject obj, string clipName)
    {
        AudioSource source = FindSound(clipName, obj.name);
        if (source == null)
            return;

        soundList.Remove(source);
        source.Stop();
        GameObject.Destroy(source);
    }

    public void SetOverallVolume(float vol) 
    {
        for (int i = 0; i < soundList.Count; ++i)
        {
            soundList[i].volume = soundVolume * vol;
        }
        if (BGMSource == null || BGMSource.clip == null)
        {
            Debug.LogWarning("BGM is not instantiated or loaded yet!");
            return;
        }
        BGMSource.volume = BGMVolume * vol;
    }

    private AudioSource FindSound(string soundName, string objName = null) 
    {
        List<AudioSource> sL = null;
        sL = (objName == null) ? soundList : soundObjDic[objName];
        foreach (var sS in sL)
        {
            if (sS != null && sS.clip.name == soundName)
                return sS;
        }
        return null;
    }
}
