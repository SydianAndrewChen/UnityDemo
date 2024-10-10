using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public float rotationSpeed;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Resources.Load("ArtSrc/Clean Vector Icons/T_1_column");

        rotationSpeed = 5;   

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(Vector3.up, rotationSpeed);
    }
}
