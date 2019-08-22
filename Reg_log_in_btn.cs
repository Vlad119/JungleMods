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

public class Reg_log_in_btn : MonoBehaviour, IPointerClickHandler
{
    public static Reg_log_in_btn self;

    public InputField input_login2;
    public InputField input_pass2;
    public InputField input_code2;
 
    // Use this for initialization

    
    void Awake()
    {
        self = this;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void OnPointerClick(PointerEventData eventData)

    {

        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "reg_log_in_btn":

                {

                    
                    Player player = new Player();

                    player.login = input_login2.text;
                    player.code = input_pass2.text;
                    player.code = input_code2.text;
                    
                    Request request = new Request(player);

                    Debug.Log(MainScript.self.servername + "api/v1/player/register " + JsonUtility.ToJson(request));
                    WWW www = new WWW(MainScript.self.servername + "api/v1/player/register?data=" + JsonUtility.ToJson(request));
                    while (!www.isDone) ;

                    Debug.Log(www.text);
                    Responce responce = JsonUtility.FromJson<Responce>(www.text);
                    Debug.Log(JsonUtility.ToJson(responce));


                    MainScript.self.player = responce.player;
                    Debug.Log(MainScript.self.servername + "register.php?data=" + JsonUtility.ToJson(player));

                    string a = "1";
                    if( responce.player.autorize==a)
                    {
                        MainScript.self.fon_reg.SetActive(false);
                        MainScript.self.fon_cenopad.SetActive(true);
                    }
                    else
                    {
                        fon_reg.self.error.text = "Ошибка регистрации!\n Проверьте введённые данные";
                    }
                    break;
                }
        }




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

