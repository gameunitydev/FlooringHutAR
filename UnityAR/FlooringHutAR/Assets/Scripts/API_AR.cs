using System;
using System.Collections;
using SimpleJson;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temboo.Core;
using Temboo.Library;
using UnityEngine;
using UnityEngine.UI;

public class API_AR : MonoBehaviour
{
    private const string Categories_URL = "https://www.flooringhutapp.co.uk/mapp/categories.php";
    //private const string Details_URL = "https://www.flooringhutapp.co.uk/mapp/categories.php?catid=657";

    public GameObject img1;

    private List<Dictionary<dynamic, dynamic>> objects;
    private List<dynamic> selectedObjects = new List<dynamic>();

    public static API_AR Instance;

    [SerializeField] private GameObject imageScreenError;

    public List<string> categoriesTitlesList = new List<string>();
    public Dictionary<dynamic, dynamic>[][] subcategoryList;

    private void Awake()
    {
        Instance = this;
    }

    public void FillDropdownCategories(Dropdown dropdown1)
    {
        var request = new WWW(Categories_URL);
        StartCoroutine(OnResponse(request, dropdown1));
    }

    /*public void RequestDetails()
    {
        var request = new WWW(Details_URL);

        //StartCoroutine(OnResponse(request));
    }*/

    private IEnumerator OnResponse(WWW req, Dropdown dropdown1)
    {
        yield return req;

        if (req.error != null)
        {
            imageScreenError.SetActive(true);
        }
        else
        {
            var jsonDictionary = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(req.text);

            var allKeys = jsonDictionary.Keys.ToArray();

            objects = GetValues(allKeys, jsonDictionary);

            // Fill first Dropdown
            foreach (var obj in objects)
            {
                categoriesTitlesList.Add(obj["title"].ToString());
            }

            dropdown1.AddOptions(categoriesTitlesList);

        }

    }

    private bool CheckForChildren(Dictionary<dynamic, dynamic> parentDic)
    {
        return parentDic.ContainsKey("children");
    }

    public void OnValueChangedDrop1(Dropdown dropdown1, Dropdown dropdown2)
    {
        if (dropdown1.value != 0)
        {
            Dictionary<dynamic, dynamic> selectedObj = objects[dropdown1.value - 1];
            if (selectedObjects.Count > 0)
            {
                selectedObjects.Clear();
            }

            selectedObjects.Add(dropdown1.value - 1);

            categoriesTitlesList.Clear();

            // Fill second Dropdown
            if (CheckForChildren(selectedObj))
            {
                categoriesTitlesList.Add("All subcategories");

                foreach (var obj in selectedObj["children"])
                {

                    categoriesTitlesList.Add(obj["title"].ToString());
                }
            }
            else
            {
                categoriesTitlesList.Add("No children");
            }

            FillImages(selectedObj);
        }

        dropdown2.ClearOptions();
        dropdown2.AddOptions(categoriesTitlesList);

    }

    public void OnValueChangedDrop2(Dropdown dropdown2, Dropdown dropdown3)
    {

        if (dropdown2.value != 0)
        {
            //var selectedFirstDic = objects[selectedObjects[0]];
            int index = (int)selectedObjects[0];
            var zae = objects[index];
            var selectedSecondDic = zae["children"];
            Dictionary<dynamic, dynamic> selectedObj = selectedSecondDic[dropdown2.value - 1];

            if (selectedObjects.Count == 2)
            {
                selectedObjects.RemoveAt(1);
            }

            selectedObjects.Add(dropdown2.value);

            categoriesTitlesList.Clear();

            if (CheckForChildren(selectedObj))
            {
                categoriesTitlesList.Add("All subcategories");

                foreach (var obj in selectedObj["children"])
                {

                    categoriesTitlesList.Add(obj["title"].ToString());
                }
            }
            else
            {
                categoriesTitlesList.Add("No children");
            }

            FillImages(selectedObj);
        }

        dropdown3.ClearOptions();
        dropdown3.AddOptions(categoriesTitlesList);

    }

    public void OnValueChangedDrop3(Dropdown dropdown3)
    {
        int index = (int)selectedObjects[0];
        int indexFromOne = (int)selectedObjects[1];
        var zae = objects[index];
        var selectedSecondDic = zae["children"];
        var selectedObj = selectedSecondDic[indexFromOne];
        var selectedThirdDic = selectedObj["children"];
        Dictionary<dynamic, dynamic> selectedObjThird = selectedThirdDic[dropdown3.value - 1];

        FillImages(selectedObjThird);
    }

    private void FillImages(Dictionary<dynamic, dynamic> currentDic)
    {
        var categoryId = currentDic["categoryId"];
        var productsURL = Categories_URL + "?catid=" + categoryId.ToString();

        var request = new WWW(productsURL);
        StartCoroutine(RequestProducts(request));
    }

    private IEnumerator RequestProducts(WWW req /*collection*/)
    {
        yield return req;

        var jsonMas = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>[]>(req.text);

        for (int i = 0; i < jsonMas.Length; i++)
        {
            Dictionary<dynamic, dynamic> obj = jsonMas[i];
            if (obj.ContainsKey("smallimage"))
            {
                string imageURL = obj["smallimage"];
                StartCoroutine(DownloadImage(imageURL));
            }
        }
    }

    private IEnumerator DownloadImage(string URL)
    {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        using (WWW www = new WWW(URL))
        {
            yield return www;
            www.LoadImageIntoTexture(tex);
            img1.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }

    private List<Dictionary<dynamic, dynamic>> GetValues(dynamic[] keys, Dictionary<dynamic, dynamic> jsonDictionary)
    {
        List<Dictionary<dynamic, dynamic>> dicMas = new List<Dictionary<dynamic, dynamic>>();

        foreach (var key in keys)
        {
            string stringKey = (string)key;
            var value = jsonDictionary[stringKey];

            Dictionary<dynamic, dynamic> categoryDictionary = new Dictionary<dynamic, dynamic>();
            categoryDictionary["categoryId"] = stringKey;

            categoryDictionary["title"] = value["title"];
            //responseText.text += value;
            if (value["children"] != null/*ContainsKey("children")*/)
            {
                Dictionary<dynamic, dynamic> ch = value["children"].ToObject<Dictionary<dynamic, dynamic>>();
                var chKeys = ch.Keys.ToArray();
                var children = GetValues(chKeys, ch);
                //if (children != null)
                //{
                categoryDictionary["children"] = children;
                //}
            }

            dicMas.Add(categoryDictionary);
        }

        return dicMas;
    }
}
