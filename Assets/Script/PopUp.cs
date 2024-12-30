using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    float PopTime = 2f;
    Vector3 move = new Vector3(0, 2f, 0);
    void Start()
    {
        Destroy(gameObject, PopTime);
        transform.localPosition = new Vector3(0, 1, 0);            
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += move * Time.deltaTime;
    }
}
