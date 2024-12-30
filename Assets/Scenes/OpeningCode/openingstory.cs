using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingstory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject mainmenu;
    [SerializeField] AudioSource audio;
    [SerializeField] GameData gameData;
    void Start()
    {
        if (gameData.isStart)
        {
            mainmenu.SetActive(true);
            gameObject.SetActive(false);
            audio.Play();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            mainmenu.SetActive(true);
            gameObject.SetActive(false);
            audio.Play();
        }
    }
}
