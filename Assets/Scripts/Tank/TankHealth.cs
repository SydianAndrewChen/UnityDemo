using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public int HP = 100;
    public GameObject tankExplosionPrefab;
    public AudioClip tankExplosionAudio;
    public Slider slider;
    private float hpTotal = 100;
    void TakeDamage() 
    {
        if (HP < 0) return;
        HP -= Random.Range(0, 20);
        slider.value = HP / hpTotal;
        if (HP <= 0)
        {
            Death();
        }
    }

    void Death() 
    {
        Instantiate(tankExplosionPrefab, transform.position + Vector3.up, transform.rotation);
        AudioSource.PlayClipAtPoint(tankExplosionAudio, transform.position);
        Destroy(gameObject);
    }
}
