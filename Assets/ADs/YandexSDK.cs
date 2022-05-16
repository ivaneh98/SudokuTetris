using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;



public class YandexSDK : MonoBehaviour
{
    // Создание SINGLETON
    public static YandexSDK Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private string lang;
    public string Lang 
    { 
        get 
        {
            return lang;
        }
        private set 
        {
            switch (value)
            {
                case "ru":
                    lang = "ru";
                    break;
                case "be":
                    lang = "ru";
                    break;
                case "kk":
                    lang = "ru";
                    break;
                case "uk":
                    lang = "ru";
                    break;
                case "uz":
                    lang = "ru";
                    break;
                default:
                    lang = "en";
                    break;
            }
        } 
    }

    UserGameData UGD;
    private UserData UD;

    public UserGameData GetUserGameData => UGD;

    public UserData GetUserData => UD;
    
    
    [DllImport("__Internal")]
    private static extern void Auth();    // Авторизация - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void ShowCommonADV();    // Показ обычной рекламы - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void GetData();    // Получение данных - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void SetData(string data);    // Отправка данных - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void ShowRewardADV();    // Показ рекламы с наградой - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void GetLeaderBoardEntries();
    [DllImport("__Internal")]
    private static extern void GetUnAuthLeaderBoardEntries();
    [DllImport("__Internal")]
    private static extern void SetLeaderBoard(int score);
    [DllImport("__Internal")]
    private static extern void GetLang();


    public event Action AuthSuccess;    //События
    public event Action DataGet;    //События
    public event Action RewardGet;  //События
    public event Action<string> LangSuccess;  //События
    public event Action<string> LeaderBoardReady; 


    public void Authenticate()    //    Авторизация
    {
        Auth();
    }

    public void GettingData()    // Получение данных
    {
        GetData();
    }
    public void GettingLang()    // Получение данных
    {
        GetLang();
    }
    public void SettingData(string data)    // Сохранение данных
    {
        SetData(data);
    }

    public void getLeaderEntries()
    {
        GetLeaderBoardEntries();
    }
    public void getUnAuthLeaderBoardEntries()
    {
        GetUnAuthLeaderBoardEntries();
    }
    public void setLeaderScore(int score)
    {
        SetLeaderBoard(score);
    }

    public void BoardEntriesReady(string _data)
    {
        LeaderBoardReady?.Invoke(_data);
    }

    public void ShowCommonAdvertisment()    // Показ обычной рекламы
    {
        ShowCommonADV();
    }

    public void ShowRewardAdvertisment()    // Показ рекламы с наградой
    {
        ShowRewardADV();
    }

    
    public void AuthenticateSuccess(string data)    // Авторизация успешно пройдена
    {
        UD.Name = data;
        AuthSuccess?.Invoke();
    }
    
    public void DataGetting(string data) // Данные получены
    {
        UserDataSaving UDS = new UserDataSaving();
        UDS = JsonUtility.FromJson<UserDataSaving>(data);
        UGD = JsonUtility.FromJson<UserGameData>(UDS.data);
        DataGet?.Invoke();
    }
    
    public void RewardGetting() // Реклама просмотрена
    {
        RewardGet?.Invoke();
    }

    public void LangReady(string data)    // Авторизация успешно пройдена
    {
        Lang = data;
        LangSuccess?.Invoke(Lang);
    }

}

[Serializable]
public class UserData
{
    public string Name;
    public string image;
}

[Serializable]
public class UserGameData
{
    public UserGameData(int highscore)
    {
        Highscore = highscore;
    }
    public int Highscore;
}
[Serializable]
public class UserDataSaving
{
    public string data;
}

