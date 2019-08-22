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


public class TopPanel : MonoBehaviour, IPointerClickHandler
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void OnPointerClick(PointerEventData eventData)

    {

        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {

            case "burger_btn":
                {
                    MainScript.self.fon_cenopad.SetActive(false);
                    MainScript.self.fon_jungle_mods.SetActive(false);
                    MainScript.self.fon_news.SetActive(false);
                    MainScript.self.fon_sale.SetActive(false);
                    MainScript.self.fon_bonus.SetActive(false);
                    MainScript.self.fon_map.SetActive(false);

                    MainScript.self.fon_setting.transform.Find("fname").GetComponent<InputField>().text = "";
                    MainScript.self.fon_setting.transform.Find("sname").GetComponent<InputField>().text = "";
                    MainScript.self.fon_setting.transform.Find("birthday").GetComponent<InputField>().text = "";

                    if (MainScript.self.player.branch > 0)
                    {
                        MainScript.self.fon_setting.transform.Find("Text").GetComponent<Text>().text = "Редактирование Вашего профиля";
                        MainScript.self.fon_setting.transform.Find("save_changes_btn/Text").GetComponent<Text>().text = "Применить";
                        MainScript.self.fon_nav.transform.Find("button_bonus").GetComponent<Text>().text = "Редактировать";
                        MainScript.self.fon_setting.transform.Find("fname").GetComponent<InputField>().text = MainScript.self.player.fname;
                        MainScript.self.fon_setting.transform.Find("sname").GetComponent<InputField>().text = MainScript.self.player.sname;
                        MainScript.self.fon_setting.transform.Find("birthday").GetComponent<InputField>().text = MainScript.self.player.birthday.Replace(".","");
                       

                    }
                    else
                    {
                        MainScript.self.fon_setting.transform.Find("Text").GetComponent<Text>().text = "Заполните все поля \n получите 200 бонусов!";
                        MainScript.self.fon_setting.transform.Find("save_changes_btn/Text").GetComponent<Text>().text = "Получить бонусы";
                        MainScript.self.fon_nav.transform.Find("button_bonus").GetComponent<Text>().text = "БОНУС";

                    }
                    MainScript.self.fon_nav.SetActive(true);
                    
                    break;
                }

            case "settings_btn":
                {
                    MainScript.self.fon_setting.SetActive(true);
                    fon_settings.self.Get_Settings();
                    break;
                }

        }
    }
}
