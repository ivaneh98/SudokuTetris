using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
using UnityEngine.SceneManagement;

public class ADSpam : MonoBehaviour
{
    //public string NameText;
    //public TMP_Text Highscore;
    private YandexSDK SDK;

    //public bool isAuth = false;

    //public GameObject ScrollView;
    //public GameObject _fab;
    //public Image[] medals;
    //private string sceneName;
    //[SerializeField] private int highscore;
    private void Start()
    {
    
    
        //sceneName = SceneManager.GetActiveScene().name;
        //if (sceneName == "Menu")
        //{
        //    switch (PlayerPrefs.GetInt("Auth"))
        //    {
        //        case 0:
        //            break;
        //        case 1:
        //            Auth();
        //            GetData();
        //            break;
        //        default:
        //            break;
        //    }
        //}

        SDK = YandexSDK.Instance;
        //SDK.AuthSuccess += SettingName;
        SDK.RewardGet += Rewarded;
        SDK.DataGet += SettingData;
        //SDK.LeaderBoardReady += AddEntri;
    }

    public void UpdateCoinsText(int high)
    {
        //highscore = high;
        //Highscore.text = highscore.ToString();
    }
    public void Auth()
    {
        SDK.Authenticate();
    }
    //public void GetData()
    //{
    //    if (isAuth)
    //        SDK.GettingData();
    //}
    //public void SetData()
    //{
    //    UserGameData UD = new UserGameData(highscore);
    //    if (isAuth)
    //        SDK.SettingData(JsonUtility.ToJson(UD));
    //}
    public void ShowCommon()
    {
        SDK.ShowCommonAdvertisment();
    }
    //public void ShowReward()
    //{
    //    SDK.ShowRewardAdvertisment();
    //}

    //public void GetLeaderBoardEntries()
    //{
    //    if (isAuth)
    //        SDK.getLeaderEntries();
    //    else
    //        SDK.getUnAuthLeaderBoardEntries();
    //}

    //public void SetLeaderBoard(int _highscore)
    //{
    //    if (isAuth)
    //        SDK.setLeaderScore(_highscore);
    //}

    public void AddCoin()
    {
        //highscore++;
        //UpdateCoinsText();
        //SetLeaderBoard();
    }
    private void SettingData()
    {
        //highscore = SDK.GetUserGameData.Highscore;
        //UpdateCoinsText(highscore);
    }
    //private void SettingName()
    //{
    //    NameText = SDK.GetUserData.Name;
    //    isAuth = true;
    //}
    private void Rewarded()
    {
        //highscore++;
        //UpdateCoinsText();
    }

    //private void AddEntri(string _json)
    //{
    //    ClearEntri();
    //    var json = JSON.Parse(_json);
    //    var _count = (int)json["entries"].Count;
    //    Debug.Log(_count);
    //    for (int i = 0; i < _count; i++)
    //    {
    //        var _entries = Instantiate(_fab, ScrollView.transform);
    //        switch (i)
    //        {
    //            case 0:
    //                _entries.transform.GetChild(0).GetComponent<RawImage>().texture = medals[i].mainTexture;

    //                break;
    //            case 1:
    //                _entries.transform.GetChild(0).GetComponent<RawImage>().texture = medals[i].mainTexture;

    //                break;
    //            case 2:
    //                _entries.transform.GetChild(0).GetComponent<RawImage>().texture = medals[i].mainTexture;

    //                break;
    //            default:
    //                _entries.transform.GetChild(0).gameObject.SetActive(false);
    //                break;
    //        }
    //        //StartCoroutine(LoadIMG(url, raw));
    //        _entries.transform.GetChild(1).GetComponent<Text>().text = json["entries"][i]["player"]["publicName"];
    //        _entries.transform.GetChild(2).GetComponent<Text>().text = json["entries"][i]["score"].ToString();
    //    }
    //    if (!isAuth)
    //    {
    //        var _entrie = Instantiate(_fab, ScrollView.transform);
    //        _entrie.transform.GetChild(0).gameObject.SetActive(false);
    //        //var raw = _entrie.transform.GetChild(0).GetComponent<RawImage>();
    //        //StartCoroutine(LoadIMG(url, raw));
    //        _entrie.transform.GetChild(1).GetComponent<Text>().text = "Player";
    //        _entrie.transform.GetChild(2).GetComponent<Text>().text = highscore.ToString();
    //    }
    //}
    //private void ClearEntri()
    //{
    //    if (ScrollView.transform.childCount > 0)
    //    {
    //        foreach (Transform child in ScrollView.transform)
    //        {
    //            Destroy(child.gameObject);
    //        }
    //    }
    //}

    //private IEnumerator LoadIMG(string _url, RawImage _img)
    //{
    //    WWW www = new WWW(_url);
    //    yield return www;
    //    _img.texture = www.texture;
    //}
}
