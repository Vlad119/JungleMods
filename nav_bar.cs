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


public class nav_bar : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void OnPointerClick(PointerEventData eventData)

    {
       
        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {

            case "news_btn":
                {
                    
                    MainScript.self.fon_news.SetActive(true);

                    MainScript.self.fon_cenopad.SetActive(false);
                    MainScript.self.fon_jungle_mods.SetActive(false);
                    MainScript.self.fon_map.SetActive(false);
                    MainScript.self.fon_sale.SetActive(false);
                    MainScript.self.fon_bonus.SetActive(false);
                    MainScript.self.fon_news.SetActive(true);
                    MainScript.self.startcoront();
                    break;
                }

            case "sale_btn":
                {
                    MainScript.self.ImgSale.SetActive(false);
                    MainScript.self.fon_cenopad.SetActive(false);
                    MainScript.self.fon_jungle_mods.SetActive(false);
                    MainScript.self.fon_map.SetActive(false);
                    MainScript.self.fon_news.SetActive(false);
                    MainScript.self.fon_bonus.SetActive(false);
                    MainScript.self.fon_sale.SetActive(true);
                    MainScript.self.fon_sale.GetComponent<SaleScript>().RunLoadSale();
                    break;
                }

            case "home_btn":
                {
                    
                    MainScript.self.fon_news.SetActive(false);
                    MainScript.self.fon_sale.SetActive(false);
                    MainScript.self.fon_bonus.SetActive(false);
                    MainScript.self.fon_map.SetActive(false);

                    MainScript.self.fon_cenopad.SetActive(true);
                    // MainScript.self.fon_jungle_mods.SetActive(true); 
                    break;
                }
            case "bonus_btn":
                {
                    MainScript.self.count_bonus.text = "";
                    MainScript.self.fon_cenopad.SetActive(false);
                    MainScript.self.fon_jungle_mods.SetActive(false);
                    MainScript.self.fon_map.SetActive(false);
                    MainScript.self.fon_sale.SetActive(false);
                    MainScript.self.fon_news.SetActive(false);
                    MainScript.self.fon_bonus.SetActive(true);

                    //  зачисление или списание бонусов
                   // MainScript.self.fon_bonus.GetComponent<BonusScript>().ChangeBonus();
                  
                    MainScript.self.fon_bonus.GetComponent<BonusScript>().ShowBonus();

                    break;
                }
            case "map_btn":
                {
                    MainScript.self.fon_cenopad.SetActive(false);
                    MainScript.self.fon_jungle_mods.SetActive(false);
                    MainScript.self.fon_news.SetActive(false);
                    MainScript.self.fon_sale.SetActive(false);
                    MainScript.self.fon_bonus.SetActive(false);
                    MainScript.self.fon_map.SetActive(true);
                    MainScript.self.fon_map.GetComponent<fon_map>().LoadMap();

                    break;
                }
        }
    }
}

