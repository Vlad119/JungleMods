using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Xml;
using System.IO;

public class SaleScript : MonoBehaviour
{
    public GameObject sale_pref;
    public GameObject day_sale_pref;
    public GameObject TargetContent;
    public static SaleScript fon_sale;

    public List<Sale_pref> Elems = new List<Sale_pref>();
    public GameObject yet;
    [SerializeField] private sales salesData;
    private List<day_sale_pref> day_Sale_Prefs = new List<day_sale_pref>();
    public sales salesTest = new sales();

    private void Awake()
    {
        if (!fon_sale) fon_sale = this;
        else Destroy(gameObject);
        salesTest = new sales();
        salesTest.stocks = new List<Stocks>();
        var st = new Stocks();
        st.title = "Test1";
        st.description = "Тестовая акациЯ";
        st.branches  = new List<int>();
        st.branches.Add(406);
        st.image_url = "https://2x2.su/public/specproject_content/specproject_images/e65ff2d32038ab1e6827ed1be2e49e58bfb2cd00.jpg";
        st.image_preview_url = "https://efrnet.net/media/k2/items/cache/c9b002fe1bb0320831a8ae78670fdb6f_XL.jpg";
        salesTest.stocks.Add(st);
        st = new Stocks();
        st.title = "Test2";
        st.description = "Тестовая акациЯ";
        st.branches = new List<int>();
        st.branches.Add(406);
        st.image_url = "https://2x2.su/public/specproject_content/specproject_images/e65ff2d32038ab1e6827ed1be2e49e58bfb2cd00.jpg";
        st.image_preview_url = "https://efrnet.net/media/k2/items/cache/c9b002fe1bb0320831a8ae78670fdb6f_XL.jpg";
        salesTest.stocks.Add(st);
        st = new Stocks();
        st.title = "Test3";
        st.description = "Тестовая акациЯ";
        st.branches = new List<int>();
        st.branches.Add(406);
        st.image_url = "https://2x2.su/public/specproject_content/specproject_images/e65ff2d32038ab1e6827ed1be2e49e58bfb2cd00.jpg";
        st.image_preview_url = "https://efrnet.net/media/k2/items/cache/c9b002fe1bb0320831a8ae78670fdb6f_XL.jpg";
        salesTest.stocks.Add(st);
    }
    private void Update()
    {
        if (Input.GetKey("f"))
        {
            salesTest.stocks.RemoveAt(salesTest.stocks.Count-1);
        }
        if (Input.GetKey("d"))
        {
            var st = new Stocks();
            st.title = "Test3";
            st.description = "Тестовая акациЯ";
            st.branches = new List<int>();
            st.branches.Add(406);
            st.image_url = "http://kt-russia.ru/wp-content/uploads/2019/02/%D0%90%D0%BA%D1%86%D0%B8%D1%8F-%D1%81%D1%80%D0%B5%D0%B4%D0%B0.jpg";
            st.image_preview_url = "https://efrnet.net/media/k2/items/cache/c9b002fe1bb0320831a8ae78670fdb6f_XL.jpg";
            salesTest.stocks.Add(st);
        }
    }

    public virtual void AllClose<T>(Transform _content) where T : day_sale_pref
    {
        foreach (T val in _content.transform.GetComponentsInChildren<T>())
        {
            Destroy(val.gameObject);
        }
    }

    public void RunLoadSale()
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadSale());
    }

    public bool GetBrabchIs(Stocks stocks)
    {
        bool rez = false;
        foreach (var val in stocks.branches)
        {
            if(MainScript.self.player.branch == val)
            {
                rez = true;
            }
        }
        return rez;
    }

    public IEnumerator LoadSale()
    {
        Request request = new Request(MainScript.self.player);
        string url = MainScript.self.servername + "api/v1/stocks?data=" + JsonUtility.ToJson(request);
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        MainScript.self.Sale_Info = salesData = JsonUtility.FromJson<sales>(www.downloadHandler.text);
        //salesData = salesTest;
        
        int i = 0; int num = 0;
        for (i = 0; i<salesData.stocks.Count; i++)
        {
            if (GetBrabchIs(salesData.stocks[i]))
                {
                try
                {
                    day_Sale_Prefs[num].SetValues(salesData.stocks[i]);
                }
                catch
                {
                    var inst = Instantiate(sale_pref, TargetContent.transform);
                    inst.transform.SetParent(TargetContent.transform);
                    day_Sale_Prefs.Add(inst.GetComponent<day_sale_pref>());
                    day_Sale_Prefs[num].SetValues(salesData.stocks[i]);
                }
                num++;
            }
        }
       
        while  (num < day_Sale_Prefs.Count)
        {
               Destroy(day_Sale_Prefs[day_Sale_Prefs.Count - 1].gameObject);
                day_Sale_Prefs.RemoveAt(day_Sale_Prefs.Count-1);
        }
        // int i = 0;
        //foreach (Stocks n in Sale_Info.stocks)
        //{
        //    int i = 0;

        //    Debug.Log("asd");
        //    if (player.branch != 0)
        //    {
        //        foreach (int m in n.branches)
        //        {
        //            Debug.Log(m);
        //            if (m == player.branch)
        //            {
        //                Debug.Log(n.title);
        //                StartCoroutine(LoadImageSale(n.image_preview_url, n.image_url, n.title, n.description, n.new_price, n.old_price));

        //                break;
        //            }


        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("1");

        //        foreach (int m in n.branches)
        //        {
        //            if (m == 0)
        //            {
        //                StartCoroutine(LoadImageSale(n.image_preview_url, n.image_url, n.title, n.description, n.new_price, n.old_price));


        //                break;
        //            }


        //        }
        //    }

        //}

    }
}
