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
    public enum Companies {Cenopad, Jungle };

public class FonCenocity_Jungle : MonoBehaviour {

    public GameObject logo;
    public GameObject logo2;
    public GameObject case_btns;
    public GameObject case_btns2;
    public GameObject fon_cenopad;

    public GameObject Text;
    public GameObject Text2;

    public static FonCenocity_Jungle self;
    [SerializeField] public Companies companies;


    // Use this for initialization
    void Start ()
    {
        self = this;
        ChangePage();
	}

    private void OnValidate()
    {
        ChangePage();
    }

    public void ChangePage()
    {
        logo.SetActive(companies == Companies.Cenopad);
        logo2.SetActive(companies == Companies.Jungle);
        case_btns.SetActive(companies == Companies.Cenopad);
        case_btns2.SetActive(companies == Companies.Jungle);
        Camera.main.GetComponent<MainScript>().fon_login.GetComponent<fon_login>().LOGO.SetActive(companies == Companies.Cenopad);
        Camera.main.GetComponent<MainScript>().fon_login.GetComponent<fon_login>().LOGO2.SetActive(companies == Companies.Jungle);
        Camera.main.GetComponent<MainScript>().fon_login.GetComponent<fon_login>().Text.SetActive(companies == Companies.Cenopad);
        Camera.main.GetComponent<MainScript>().fon_login.GetComponent<fon_login>().Text2.SetActive(companies == Companies.Jungle);
       if(fon_map.fon_maps) fon_map.fon_maps.ShowMapcontinue(companies ==  Companies.Cenopad? "cenocity": "junglemods");
    }

    // Update is called once per frame
    void Update ()
    {
        
		
	}
}
