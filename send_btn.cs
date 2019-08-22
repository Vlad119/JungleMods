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


public class send_btn : MonoBehaviour
{
    public static send_btn self;

    public InputField input_login;
    
    



    void Start()
    {
        if (self == null) self = this;
    }
    
    public void Button_send()
    {
        StartCoroutine(Send_Info());
    }
    public int i = 20;
    public GameObject BlockOBJ;
    public IEnumerator Block()
    {
        i = 20;
        BlockOBJ.SetActive(true);
        while (i > 20)
        {
            BlockOBJ.GetComponentInChildren<Text>().text = "" + i;
            i--;
            yield return new WaitForSeconds(1f);

        }
        BlockOBJ.SetActive(false);

    }
    
    public IEnumerator Send_Info()
    {
        StartCoroutine(Block());
        Player player = new Player();
        player.login = send_btn.self.input_login.text;

        Request request = new Request(player);


        Debug.Log(MainScript.self.servername + "api/v1/player/register?data=" + JsonUtility.ToJson(request));
        WWW www = new WWW(MainScript.self.servername + "api/v1/player/register?data=" + JsonUtility.ToJson(request));
        yield return www;

        Debug.Log(www.text);
        if (www.text.Contains("wrong login"))
        {
            MainScript.self.ErrorScreen.SetActive(true);

            MainScript.self.fon_login.GetComponent<fon_login>().error2.text = "Неправильно введен номер телефона";
        }
        else
        {
            MainScript.self.fon_login.GetComponent<fon_login>().error2.text = "";

        }
        Responce responce = JsonUtility.FromJson<Responce>(www.text);
        Debug.Log(JsonUtility.ToJson(responce));

        MainScript.self.player = responce.player;
        Debug.Log(MainScript.self.servername + "register.php?data=" + JsonUtility.ToJson(player));

    }

    IEnumerator Showplangs()
    {
        int i = 0;
        while (i < 5)
        {
            i++;
            yield return new WaitForSeconds(1f);

        }
    }

}



