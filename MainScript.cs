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


[Serializable]
public class Player
{
    public string login;
    public string code;
    public string autorize;
    public string fname;
    public string sname;
    public string birthday;
    public string comment;
    public string mytoken2;
    public int branch;

}

[Serializable]
public class Request
{
    public string api_key;
    //public List<Branch> branch = new List<Branch>();
    public Player player;
    public Bill bill = new Bill();
    public string brand = "cenocity";

    public Request(Player _player)
    {
        api_key = MainScript.api_key;
        player = _player;
        if (MainScript.self.fon_cenopad.GetComponent<FonCenocity_Jungle>().companies == Companies.Cenopad)
        {
            brand = "cenocity";
        }
        else
        {
            brand = "junglemods";

        }
    }
}

[Serializable]
public class Responce
{
    //public List<Branch> branches;
    public Player player;
    public string error = "";
    public Bill bill;
}

[Serializable]
public class Branches
{
    public List<Branch> branches = new List<Branch>();
    // public string brand;
}

[Serializable]
public class Branch
{
    public string title;
    public string nid;
    public string address;
    public string description;
    public string map_name;
    public double map_lat;
    public double map_lon;
    public string brand;
}

[Serializable]
public class Bill
{
    public string login;
    public float sum;
    public int branch_nid;
    public int check_sum;

}

[Serializable]
public class News1
{
    public string body;
    public string title;
    public string date;
}

[Serializable]
public class News
{
    public List<News1> news;
}

[Serializable]
public class sales
{
    public List<Stocks> stocks;
}
[Serializable]
public class Stocks
{
    public int nid;
    public string title;
    public string description;
    public string new_price;
    public string old_price;
    public List<int> branches;
    public string image_url;
    public string image_preview_url;
}

public class MainScript : MonoBehaviour
{

    public GameObject fon_login;
    public GameObject fon_cenopad;
    public GameObject fon_jungle_mods;
    public GameObject fon_nav;
    public GameObject fon_map;
    public GameObject fon_news;
    public GameObject fon_sale;
    public GameObject fon_bonus;
    public GameObject fon_reg;
    public GameObject fon_setting;
    public GameObject fon_report;
    public GameObject fon_bn;
    public Text count_bonus;
    public Bill bonus;
    public Player player;
    public Branches branches;
    public static MainScript self;
    public Canvas canvas;
    public string servername = "https://junglemods.retailbonus.ru/";
    public const string api_key = "fa31b554-0cfc-4cd8-ae9f-06f6db0aa7f0";
    public News news;
    public OnlineMaps mapa;
    public string mytoken;

    public Image qrcode;


    public void StartLoadBanch()
    {
        StartCoroutine(Load_branch());
    }
    public List<string> listShop;
    public Dropdown SelectShop;
    public IEnumerator Load_branch()
    {
        Branch branch = new Branch();
        Request request = new Request(player);
        string Branch = servername + "api/v1/branch?data=" + JsonUtility.ToJson(request);
        Debug.Log(Branch);
        WWW LoadWWWMap = new WWW(Branch);
        yield return LoadWWWMap;
        Debug.Log(LoadWWWMap.text);
        branches = JsonUtility.FromJson<Branches>(LoadWWWMap.text);
        SelectShop.ClearOptions();
        listShop.Clear();
        foreach (Branch n in branches.branches)
        {
            listShop.Add(n.title);
        }
        SelectShop.AddOptions(listShop);
        SelectShop.value = branches.branches
            .IndexOf(branches.branches.Find(x => x.nid == player.branch.ToString()));
        int i = 0;
        //foreach (Dropdown.OptionData n in MainScript.self.fon_setting.transform.Find("Dropdown").GetComponent<Dropdown>().options)
        //{
        //    Debug.Log(fon_setting.transform.Find("Dropdown").GetComponent<Dropdown>().value);
        //    if (player.branch > 0)
        //    {
        //        //if (n.text == branches.branches.Find(x => x.nid == player.branch.ToString()).title)
        //        if (fon_setting.transform.Find("Dropdown").GetComponent<Dropdown>().itemText.text == branches.branches.Find(x => x.nid == player.branch.ToString()).title)
        //        {
        //            MainScript.self.fon_setting.transform.Find("Dropdown").GetComponent<Dropdown>().value = i;
        //            break;
        //        }
        //        i++;
        //    }
        //}
    }

    public IEnumerator CheckToken()
    {
        while (true)
        {
            if (mytoken.Length > 0 &&
                player.mytoken2 != mytoken &&
                player.login.Length > 0 &&
                player.autorize == "1")
            {
                player.mytoken2 = mytoken;

                Request request = new Request(player);

                Debug.Log(MainScript.self.servername + "api/v1/player/update?data=" + JsonUtility.ToJson(request));
                WWW www = new WWW(MainScript.self.servername + "api/v1/player/update?data=" + JsonUtility.ToJson(request));
                yield return www;

                Responce responce = JsonUtility.FromJson<Responce>(www.text);
                Debug.Log(JsonUtility.ToJson(responce));


            }

            yield return new WaitForSeconds(5f);
        }
    }
    
    public Vector2 MyPos;
    public Coroutine token = null;

    IEnumerator LoadLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            MyPos.x = Input.location.lastData.latitude;
            MyPos.y = Input.location.lastData.longitude;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }



    void Start()
    {

        photoCache = new Dictionary<string, Texture>();
        //StartCoroutine(LoadLocation());
        if (self == null) self = this;
        MainScript.self.fon_login.SetActive(true);
        //  Request request = new Request();
        // request.api_key = "fa31b554-0cfc-4cd8-ae9f-06f6db0aa7f0";

        token = StartCoroutine(CheckToken());

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        if (PlayerPrefs.HasKey("login") && PlayerPrefs.HasKey("code"))
        {

            player.login = PlayerPrefs.GetString("login");
            player.code = PlayerPrefs.GetString("code");

            StartCoroutine(LoadPlayer(player));

        }

        StartLoadBanch();
    }

    public void Exit_app()
    {
        PlayerPrefs.DeleteAll();
        fon_login.SetActive(true);
        fon_nav.SetActive(false);
    }

    public void PlLoad()
    {
        StopCoroutine(token);
        StartCoroutine(LoadPlayer(player));
    }

    public void closeErrorScreen()
    {
        ErrorScreen.SetActive(false);
    }
    public GameObject ReportMessage;
    public void closeRepoerMessage()
    {
        fon_report.SetActive(false);
        fon_nav.SetActive(false);
        ReportMessage.SetActive(false);
        fon_cenopad.SetActive(true);
    }

    public IEnumerator LoadPlayer(Player _player)
    {
        Request request = new Request(_player);
        Debug.Log("Begin load plAYER: " + MainScript.self.servername + "api/v1/player/login?data= " + JsonUtility.ToJson(request));
        WWW www = new WWW(servername + "api/v1/player/login?data=" + JsonUtility.ToJson(request));
        yield return www;
        Debug.Log(www.text);
        Responce responce = JsonUtility.FromJson<Responce>(www.text);



        if (responce.player.autorize == "1")

        {


            player = responce.player;
            player.code = PlayerPrefs.GetString("code");

            PlayerPrefs.SetInt("authorization", 1);
            fon_login.SetActive(false);
            fon_cenopad.SetActive(true);
            fon_login.GetComponent<fon_login>().error2.text = "";
            if (responce.player.fname.Trim() == "")
            {
                fon_setting.SetActive(true);
                fon_settings.self.fname.text = "";
                fon_settings.self.sname.text = "";
                BirthdayScript.self.input.text = "";
                BirthdayScript.self.output.text = "";
                StartLoadBanch();
            }
            else
            {
                fon_setting.SetActive(false);

            }
        }



        else
        {
            //show_message
            ErrorScreen.SetActive(true);
            fon_login.GetComponent<fon_login>().error2.text = "Для продолжения необходимо ввести \n Ваш номер телефона и полученный код";
        }
        QRscript.self.ChangeQrCode();

    }
    public GameObject ErrorScreen;


    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        mytoken = token.Token;
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        DebugLog("Received a new message");
        var notification = e.Message.Notification;
        if (notification != null)
        {
            DebugLog("title: " + notification.Title);
            DebugLog("body: " + notification.Body);
        }
        if (e.Message.From.Length > 0)
            DebugLog("from: " + e.Message.From);
        if (e.Message.Link != null)
        {
            DebugLog("link: " + e.Message.Link.ToString());
        }
        if (e.Message.Data.Count > 0)
        {
            DebugLog("data:");
            foreach (System.Collections.Generic.KeyValuePair<string, string> iter in
                     e.Message.Data)
            {
                DebugLog("  " + iter.Key + ": " + iter.Value);
            }
        }
    }
    private string logText = "";
    public void DebugLog(string s)
    {
        print(s);
        logText += s + "\n";

        while (logText.Length > kMaxLogSize)
        {
            int index = logText.IndexOf("\n");
            logText = logText.Substring(index + 1);
        }

        scrollViewVector.y = int.MaxValue;
    }

    public GUISkin fb_GUISkin;
    private Vector2 controlsScrollViewVector = Vector2.zero;
    private Vector2 scrollViewVector = Vector2.zero;
    const int kMaxLogSize = 16382;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    protected bool isFirebaseInitialized = false;
    private string topic = "TestTopic";
    private bool UIEnabled = true;

    [Serializable]
    public class Gallery
    {
        public List<photoList> photogallery;
    }
    [Serializable]
    public class photoList
    {
        public int id_branch;
        public List<string> images;
    }

    public Coroutine loadSale = null;

    //public void loadsale()
    //{
    //    if (loadSale != null)
    //    {
    //        StopCoroutine(loadSale);
    //    }
    //    loadSale = StartCoroutine(MainScript.self.LoadSale());
    //}


    public sales Sale_Info;

    public GameObject Prefab_sale;
    public GameObject parent_sale;



    //public IEnumerator LoadSale()
    //{
    //    foreach (Transform n in fon_sale.transform.Find("Scroll View/Viewport/Content"))
    //    {
    //        Destroy(n.gameObject);
    //    }
    //    Request request = new Request(player);
    //    string Request = servername + "api/v1/stocks?data=" + JsonUtility.ToJson(request);
    //    WWW LoadWWWNews = new WWW(Request);
    //    yield return LoadWWWNews;
    //    Debug.Log(LoadWWWNews.text);
    //    Sale_Info = JsonUtility.FromJson<sales>(LoadWWWNews.text);
    //    // int i = 0;
    //    foreach (Stocks n in Sale_Info.stocks)
    //    {
    //        int i = 0;

    //        Debug.Log("asd");
    //        if (player.branch != 0)
    //        {
    //            foreach (int m in n.branches)
    //            {
    //                Debug.Log(m);
    //                if (m == player.branch)
    //                {
    //                    Debug.Log(n.title);
    //                    StartCoroutine(LoadImageSale(n.image_preview_url, n.image_url, n.title, n.description, n.new_price, n.old_price));

    //                    break;
    //                }


    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("1");

    //            foreach (int m in n.branches)
    //            {
    //                if (m == 0)
    //                {
    //                    StartCoroutine(LoadImageSale(n.image_preview_url, n.image_url, n.title, n.description, n.new_price, n.old_price));


    //                    break;
    //                }


    //            }
    //        }

    //    }

    //}
    public GameObject ImgSale;
    public void CloseImgSale()
    {
        ImgSale.SetActive(false);
    }

    //public IEnumerator LoadImageSale(string image_prev, string image_url, string title, string desc, string new_price, string old_price)
    //{

    //    var a = Instantiate(Prefab_sale, parent_sale.transform);
    //    WWW www = new WWW(image_prev);
    //    yield return www;
    //    a.GetComponent<ClickSaleItem>().linkImg = image_url;
    //    a.transform.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = www.texture;
    //    a.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = title;
    //    a.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = desc;
    //    a.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>().text = new_price;
    //    a.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = old_price;
    //}

    public Gallery gallery;
    public GameObject ContentImage;
    public GameObject contentGallery;
    public GameObject imgGallery;
    public GameObject qqwe;
    public void startcoront()
    {
        qqwe.SetActive(true);
        MainScript.self.fon_news.SetActive(true);
        StartCoroutine(MainScript.self.LoadNews());
    }

    List<ElemPresentNews> present_elems_news = new List<ElemPresentNews>();

    Vector2 sizeStart;


    [Header("PhotoCache")]
    public Dictionary<string, Texture> photoCache;
    public IEnumerator LoadNews()
    {
        
        MainScript.self.fon_news.SetActive(true);
        //foreach (Transform n in MainScript.self.fon_news.transform)
        //{
        //    if (n.name == "gallery")
        //    {
        //        Destroy(n.gameObject);
        //    }
        //}
        //var a = Instantiate(contentGallery, fon_news.transform);
        //a.name = "gallery";
        //ContentImage = a.transform.GetChild(0).gameObject;

        // string Request = servername + "api/v1/news?data=" + JsonUtility.ToJson(request);
        Request request = new Request(player);
        string Request = servername + "api/v1/photogallery?data=" + JsonUtility.ToJson(request);
        WWW LoadWWWNews = new WWW(Request);
        yield return LoadWWWNews;
        // Debug.Log(Request);
        gallery = JsonUtility.FromJson<Gallery>(LoadWWWNews.text);
        Debug.Log(LoadWWWNews.text);
        int num = 0;
        //факин костыль ибо хз
        //var so = Instantiate(imgGallery, contentGallery.transform);
        //so.transform.SetParent(contentGallery.transform);

        //so.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        //so.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);

        //so.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        //so.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        //so.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);


        sizeStart = contentGallery.GetComponent<RectTransform>().rect.size;
        ////Destroy(so);

        foreach (photoList val in gallery.photogallery)
        {
            Debug.Log("Image recived");
            if (val.id_branch == player.branch)
            {
                Debug.Log("Image matches");
                foreach (string site in val.images)
                {
                        Debug.Log(site);
                        var inst = Instantiate(imgGallery, ContentImage.transform);
                        present_elems_news.Add(inst.GetComponent<ElemPresentNews>());
                        inst.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                        inst.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

                        inst.GetComponent<ElemPresentNews>().SetValues(site, sizeStart);
                    Debug.Log(num + " from " + present_elems_news.Count);
                }
            }
        }
        //while (num < present_elems_news.Count)
        //{
        //    Destroy(present_elems_news[present_elems_news.Count - 1].gameObject);
        //    present_elems_news.RemoveAt(present_elems_news.Count - 1);
        //    ContentImage.GetComponentInParent<ScrollSnapRect>().UpdateData();
        //    print("Delete elem ");
        //}
        //for (int i = num; i< present_elems_news.Count; i++)
        //{
        //    Debug.Log(i + " from " + present_elems_news.Count);
        //    present_elems_news[i].SetValues("", sizeStart);
        //    present_elems_news.RemoveAt(i);
        //}
        ////if (player.branch != 0)
        //{
        //    Debug.Log("11");

        //    if (n.id_branch == player.branch)
        //    {
        //        foreach (string m in n.images)
        //        {
        //            StartCoroutine(loadImagesGallery(imgGallery, ContentImage, m));
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.Log("22");
        //    foreach (string m in n.images)
        //    {
        //        StartCoroutine(loadImagesGallery(imgGallery, ContentImage, m));
        //    }
        //}
        //}

        //   a.GetComponent<ScrollSnapRect>().enabled = true;
        //news = JsonUtility.FromJson<News>(LoadWWWNews.text);
        // action();
    }

    //public IEnumerator loadImagesGallery(GameObject item, GameObject content, string link)
    //{
    //    var a = Instantiate(item, content.transform);
    //    var sizeStart = content.GetComponentInParent<RectTransform>().rect.size;
    //    Debug.Log(sizeStart);

    //    a.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
    //    a.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
    //    a.GetComponent<RectTransform>().sizeDelta = sizeStart;
    //    a.AddComponent<Mask>();
    //    WWW www = new WWW(link);
    //    yield return www;

    //    var img = Instantiate(item, a.transform);
    //    img.GetComponent<RawImage>().texture = www.texture;
    //    var ARF = img.AddComponent<AspectRatioFitter>();
    //    img.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
    //    img.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
    //    ARF.aspectRatio = (img.GetComponent<RawImage>().texture.width/ (float)img.GetComponent<RawImage>().texture.height );
    //    Debug.Log(img.GetComponent<RawImage>().texture.height / img.GetComponent<RawImage>().texture.width + "  AspectGalerelay ");
    //    ARF.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

    //}

    /*
    public IEnumerator ChangeBonus(UnityAction action)
    {
        Request request = new Request(player);
        //   Player player = new Player();
        // Bill bill = new Bill();
        //    request.bill = bill;
        request.player = player;
        request.bill.login = PlayerPrefs.GetString("login", player.login);
        request.bill.sum = 1000;
        request.bill.branch_nid = 0;
        request.bill.check_sum = 34123;
        request.player.login = "21212121212";
        request.player.code = "joker";
        string Bills = servername + "api/v1/bills?data=" + JsonUtility.ToJson(request);
        Debug.Log(Bills);
        WWW LoadWWWChangeBonus = new WWW(Bills);
        yield return LoadWWWChangeBonus;
        Debug.Log(LoadWWWChangeBonus.text);
        bonus = (JsonUtility.FromJson<Responce>(LoadWWWChangeBonus.text)).bill;
        action();
    }
    */
    public IEnumerator LoadBonus(UnityAction action)
    {
        Request request = new Request(player);

        request.player.login = PlayerPrefs.GetString("login");
        string Bills = servername + "api/v1/bills/sum?data=" + JsonUtility.ToJson(request);
        Debug.Log(Bills);
        WWW LoadWWWBonus = new WWW(Bills);
        yield return LoadWWWBonus;
        Debug.Log(LoadWWWBonus.text);
        bonus = JsonUtility.FromJson<Bill>(LoadWWWBonus.text);
        action();
    }

    public IEnumerator LoadMap(UnityAction<string> action)
    {
        Branch branch = new Branch();
        Request request = new Request(player);
        string Branch = servername + "api/v1/branch?data=" + JsonUtility.ToJson(request);
        Debug.Log(Branch);
        WWW LoadWWWMap = new WWW(Branch);
        yield return LoadWWWMap;
        Debug.Log(LoadWWWMap.text);
        branches = JsonUtility.FromJson<Branches>(LoadWWWMap.text);
        Debug.Log("1");
        action((FonCenocity_Jungle.self.companies == Companies.Cenopad) ? "cenocity" : "junglemods");
    }



    void Update()
    {

    }

}
