using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip put;
    [SerializeField] AudioClip dash;
    [SerializeField] AudioClip fetch;
    [SerializeField] AudioClip scoreUp;
    [SerializeField] AudioClip scoreDown;
    AudioSource audioSource;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(string type)
    {
        switch (type)
        {
            case "Dash": audioSource.PlayOneShot(dash); break;
            case "Put": audioSource.PlayOneShot(put); break;
            case "Fetch": audioSource.PlayOneShot(fetch); break;
            case "ScoreUp": audioSource.PlayOneShot(scoreUp); break;
            case "ScoreDown":audioSource.PlayOneShot(scoreDown); break;
        }
    }
}
