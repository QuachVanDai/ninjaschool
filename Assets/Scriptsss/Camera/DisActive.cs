using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisActive : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam ;
    void Start()
    {
       
        cam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
