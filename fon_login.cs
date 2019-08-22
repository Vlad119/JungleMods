using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Xml;
using System.IO;

public class fon_login : MonoBehaviour
{
    public static fon_login self;
    public InputField login;
    public string logins;
    public InputField code;
    public Text error2;
    public Text lastnews;
    public GameObject LOGO;
    public GameObject LOGO2;
    public GameObject Text;
    public GameObject Text2;


    private void Awake()
    {
        if (!self) self = this;
        else Destroy(gameObject);
    }
    // Use this for initialization
    void Start ()
    {
        
        fon_login.self.error2.text = "";
        //StartCoroutine(MainScript.self.LoadNews(continuelastnews));
        
        if (MainScript.self.player.branch > 0)
        {
            MainScript.self.StartLoadBanch();
            MainScript.self.fon_setting.SetActive(true);
            Debug.Log("Open");
        }

    }

    public void continuelastnews()
    {
        lastnews.text = MainScript.self.news.news[0].body;
    }

    public void BtnLogin()
    {
        MainScript.self.player.login =  fon_login.self.login.text;
        logins = fon_login.self.login.text;
        PlayerPrefs.SetString("login", MainScript.self.player.login);
        MainScript.self.player.code = fon_login.self.code.text;
        PlayerPrefs.SetString("code", MainScript.self.player.code);
        StartCoroutine(MainScript.self.LoadPlayer(MainScript.self.player));
        
        
    }



    // Update is called once per frame
    void Update ()
    {
		
	}
}
