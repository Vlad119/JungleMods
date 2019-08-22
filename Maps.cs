//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Net;
//using System.Text;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using UnityEngine.EventSystems;
//using UnityEngine.Events;
//using System.Xml;
//using System.IO;

//public class Maps : MonoBehaviour

//{
//    string url = "";
//    public float lat = 24.917828f;
//    public float lon = 67.097096f;
//    LocationInfo li;
//    public int zoom = 14;
//    public int mapWidth = 600;
//    public int mapHeight = 600;
//    public Enum mapType { roadmap,satelite,hybrid, terrain };
//    public mapType mapSelected;
//    public int scale;
//    private bool loadingMap = false;
//    private IEnumerator mapCoroutine;
//    IEnumerator GetGoogleMap(float lat, float lon);
//    HelpURLAttribute = "https://maps.googleapis.com/maps/api/staticmap?center="+lat+","+lon+"&zoom="+zoom+"&size="+mapWidth+"x"+mapHeignt+"&scale"+scale+"maptype="+mapSelected+"&key=AIzaSyD7HelRU9HLJ4pZe4n3ruH3Qi1JeKtiOJs";
//    loadingMap=true;
//    WWW www = new WWW(url);
//    yield return www;
//    loadingMap=false;
//    gameObject.GetComponent<RawImage>().texture=www.texture;
//    stopCoroutine(mapCoroutine);
//}
//void Start()
//{
//    mapCoroutine = GetGoogleMap(lat, lon);
//    StartCoroutine(mapCoroutine);
//}

//void Update()
//{
//    if (Input.GetKeyDown(KeyCode.M))
//    {
//        Debug.Log("New map");
//        lat = 40.6786806f;
//        lon = -0.738644250f;
//        mapCoroutine = GetGoogleMap(lat, lon);
//        StartCoroutine(mapCoroutine);
//    }
//}
