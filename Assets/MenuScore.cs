using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScore : MonoBehaviour
{
    ADSpam InstanceAD;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Score", 0).ToString();
        InstanceAD = GameObject.FindGameObjectWithTag("AD").GetComponent<ADSpam>();
#if !DEBUG
InstanceAD.ShowCommon();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
