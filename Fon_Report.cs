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


public class Fon_Report : MonoBehaviour, IPointerClickHandler
{

    public static Fon_Report self;
    public InputField comment;


    void Awake()
    {
        self = this;

    }

    public void Send_Report()
    {
        Player player = new Player();
        player.login = PlayerPrefs.GetString("login", player.login);
        player.code = PlayerPrefs.GetString("code", player.code);
        player.comment = Fon_Report.self.comment.text;
        Request request = new Request(player);
        Debug.Log(MainScript.self.servername + "api/v1/player/comment?data=" + JsonUtility.ToJson(request));
        WWW www = new WWW(MainScript.self.servername + "api/v1/player/comment?data=" + JsonUtility.ToJson(request));
        while (!www.isDone) ;
        Debug.Log(www.text);
        Responce responce = JsonUtility.FromJson<Responce>(www.text);
        Debug.Log(JsonUtility.ToJson(responce));
        Fon_Report.self.comment.text = "";
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "close":
                {

                    MainScript.self.fon_report.SetActive(false);
                    MainScript.self.fon_cenopad.SetActive(true);
                    break;
                }
        }
    }
}