using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ElemPresentNews : MonoBehaviour
{
    private string link = "";
    [SerializeField] private RawImage img;
    [SerializeField] private AspectRatioFitter ARF;
    private Vector2 size;

    public bool GetStatus(string val)
    {
        return val == link;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    

    public void SetValues(string _link, Vector2 _size)
    {
        Debug.Log("Values Setting");
        size = _size;
        if (MainScript.self.photoCache.ContainsKey(_link))
        {
            Debug.Log("Image cached");
            img.texture = MainScript.self.photoCache[_link];
            ARF.aspectRatio = (float)img.texture.width / (float)img.texture.height;
            GetComponent<RectTransform>().sizeDelta = size;
            GetComponentInParent<ScrollSnapRect>().UpdateData();
        }
        else
        {
            StartCoroutine(SetvaluesLoad(_link));
        }
    }

    public IEnumerator SetvaluesLoad( string url)
    {
        Debug.Log("Loading picture!");
        link = url;
        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        img.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        ARF.aspectRatio = (img.GetComponent<RawImage>().texture.width / (float)img.GetComponent<RawImage>().texture.height);
        //ARF.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
        GetComponentInParent<ScrollSnapRect>().UpdateData();
        GetComponent<RectTransform>().sizeDelta = size;

        MainScript.self.photoCache.Add(url, img.texture);
    }

}
