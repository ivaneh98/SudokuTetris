using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOnClick : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance.PlayClickSound();
        SceneManager.LoadScene(1);

    }
    public void TurnOnSound()
    {
        AudioManager.Instance.TurnOnSound();
        AudioManager.Instance.PlayClickSound();

    }
    public void TurnOffSound()
    {
        AudioManager.Instance.TurnOffSound();

    }
}
