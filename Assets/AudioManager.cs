using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : Manager<AudioManager>
{
    [SerializeField]
    private GameObject PutObjectSound;
    [SerializeField]
    private GameObject ButtonTurnOn;
    [SerializeField]
    private GameObject ButtonTurnOff;
    [SerializeField]
    private GameObject ClickSound;
    public bool isActive;
    public override void Init()
    {
        isActive=PlayerPrefs.GetInt("isSoundOn",1)==1;
        if (isActive)
        {
            ButtonTurnOff.SetActive(true);
            ButtonTurnOn.SetActive(false);
        }
        else
        {
            ButtonTurnOff.SetActive(false);
            ButtonTurnOn.SetActive(true);
        }
    }

    public void PlayPutSound()
    {
        Instantiate(PutObjectSound);
    }
    public void PlayClickSound()
    {
        Instantiate(ClickSound);
    }
    public void TurnOnSound()
    {
        PlayerPrefs.SetInt("isSoundOn", 1);
        isActive = true;
        ButtonTurnOff.SetActive(true);
        ButtonTurnOn.SetActive(false);
    }
    public void TurnOffSound()
    {
        PlayerPrefs.SetInt("isSoundOn", 0);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Sound"))
        {
            obj.GetComponent<AudioControll>().Off();
        }
        isActive = false;
        ButtonTurnOff.SetActive(false);
        ButtonTurnOn.SetActive(true);
    }
}
