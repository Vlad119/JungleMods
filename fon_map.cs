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

public class fon_map : MonoBehaviour {
   
    public static fon_map fon_maps;
    // Use this for initialization
    void Start ()
    {
        fon_maps = this;
    }
    public string adr;
    public OnlineMaps map;
    public Text text;
   //Branch branch = new Branch();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                if (map.GetMarkerFromScreen(Input.mousePosition) != null)
                {
                text.text =  map.GetMarkerFromScreen(Input.mousePosition).address;
                }
          
        }
    }

    public void LoadMap()
    {
        Debug.Log("LoadMap");
        StartCoroutine(MainScript.self.LoadMap(ShowMapcontinue));
    }
    public void ShowMapcontinue(string _brand = "cenocity")
    {

        Debug.Log("lat " + MainScript.self.branches.branches[0].map_lat + ", lon " + MainScript.self.branches.branches[0].map_lon);


    
        MainScript.self.mapa.RemoveAllMarkers();
        for (int i = 0; i < MainScript.self.branches.branches.Count; i++)
        {
           if (MainScript.self.branches.branches[i].brand == _brand || 1 == 1)
            {

                var mar = MainScript.self.mapa.AddMarker(MainScript.self.branches.branches[i].map_lon, MainScript.self.branches.branches[i].map_lat);
                mar.address = MainScript.self.branches.branches[i].address;
            }
        }
        MainScript.self.mapa.SetPositionAndZoom(40.3708839416504f, 56.1207466125488f, 7);
        Debug.Log(56.1207466125488f + " " + MainScript.self.MyPos.y);
    }
}
