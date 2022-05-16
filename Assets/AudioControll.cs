using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControll : MonoBehaviour
{
    public AudioSource audio;
    private bool isStarted=false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (AudioManager.Instance.isActive)
        {
            On();
        }   
    }
    void Start()
    {
        if (AudioManager.Instance.isActive)
        {
            On();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!audio.isPlaying&&isStarted)
        //    Destroy(gameObject);
        if (isStarted)
        {
            audio.Play();
            isStarted = false;
        }
    }
    public void Off()
    {
        audio.mute=true;
        
        isStarted = true;
    }
    public void On()
    {
        audio.mute = false;
        isStarted = true;

    }
}
