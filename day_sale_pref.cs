using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class day_sale_pref : MonoBehaviour
{
    public Text day_sale_name;
    public Text about_day_product;
    public Text day_product_cost_sale;
    public Text cost_product;
    public RawImage day_sale_product_img;
    private Stocks data;
    public LayoutElement layout;
    public Texture texture;
    [SerializeField] private Texture defTex;

    public void SetValues(Stocks _data)
    {
        data = _data;
        day_sale_name.text = data.title;
        about_day_product.text = data.description;
        day_product_cost_sale.text = data.new_price;
        cost_product.text = data.old_price;
        StartCoroutine(LoadImageR());
        if (!day_sale_product_img.texture)
        {
            StartCoroutine(LoadImagesPrev());
        }
        else
        {
            if (day_sale_product_img.texture.name != data.image_url)
            {
                StartCoroutine(LoadImagesPrev());
            }
        }
    }

    IEnumerator LoadImagesPrev()
    {

        var www = UnityWebRequestTexture.GetTexture(data.image_preview_url);
        yield return www.SendWebRequest();
        try { 
            day_sale_product_img.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            day_sale_product_img.texture.name = data.image_preview_url;
        }
        catch
        {
            day_sale_product_img.texture = defTex;
        }


    }
    RawImage ImgObj;
    public void ShowImg()
    {
        ImgObj = MainScript.self.ImgSale.GetComponentInChildren<RawImage>();
        if (!texture)
        {
            StartCoroutine(LoadImageR(()=> {
                MainScript.self.ImgSale.GetComponentInChildren<AspectRatioFitter>().aspectRatio = (texture.width / (float)texture.height);
                MainScript.self.ImgSale.SetActive(true);
            }));
        }
        else
        {
            MainScript.self.ImgSale.GetComponentInChildren<RawImage>().texture = texture;
            MainScript.self.ImgSale.GetComponentInChildren<AspectRatioFitter>().aspectRatio = (texture.width / (float)texture.height);
            MainScript.self.ImgSale.SetActive(true);
        }
    }

    IEnumerator LoadImageR(UnityEngine.Events.UnityAction _callback = null)
    {
        var www = UnityWebRequestTexture.GetTexture(data.image_url);
        yield return www.SendWebRequest();
        texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _callback?.Invoke();

    }
}
