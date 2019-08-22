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



public class NewsScript : MonoBehaviour
{

    public GameObject news_pref;
    public GameObject TargetContent;
    public static NewsScript fon_news;
    public List <news_pref_script> Elems = new List<news_pref_script>();
    public GameObject yet;

    [SerializeField] private Transform PhotoContent;
    


    void Start()
    {
        fon_news = this;
       // gameObject.SetActive(false);
    }

    void Update()
    {

    }
    void AllClose()
    {
        foreach (news_pref_script val in Elems)
        {
            Destroy(val.gameObject);
        }
        Elems.Clear();
    }
    public void RunReit()
    {
      //  StartCoroutine(MainScript.self.LoadNews(Loadcontinue));
    }

    private void OnDisable()
    {
        foreach(Transform photo in PhotoContent)
        {
            Destroy(photo.gameObject);
        }
    }

    public void Loadcontinue()
    {
        //AllClose();
        //if (MainScript.self.news != null)
        //{
        //    for (int j = 0; j < MainScript.self.news.news.ToArray().Length; j++)
        //    {
        //        GameObject InstMis = Instantiate(news_pref);
        //        InstMis.transform.SetParent(TargetContent.transform, false);
        //        Elems.Add(InstMis.GetComponent<news_pref_script>());
        //        InstMis.GetComponent<news_pref_script>().SetValue(j);
        //        yet.transform.SetAsLastSibling();
        //    }
        //}
    }
}
