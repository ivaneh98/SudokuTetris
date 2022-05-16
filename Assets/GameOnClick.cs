using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartGame()
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
    public void Return()
    {
        AudioManager.Instance.PlayClickSound();
        SceneManager.LoadScene(0);
    }
}
