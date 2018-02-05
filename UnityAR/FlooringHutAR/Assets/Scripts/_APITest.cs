using System;
using System.Collections;
using SimpleJson;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temboo.Core;
using Temboo.Library;
using UnityEngine;
using UnityEngine.UI;

public class _APITest : MonoBehaviour
{
    private const string Categories_URL = "https://app.flooringhut.co.uk/mapp/categories.php";
    private const string Details_URL = "https://app.flooringhut.co.uk/mapp/categories.php?catid=657";

    [SerializeField] private Text responseText;

    public void RequestCategories()
    {
        var request = new WWW(Categories_URL);

        StartCoroutine(OnResponse(request));
    }

    public void RequestDetails()
    {
        var request = new WWW(Details_URL);

        StartCoroutine(OnResponse(request));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;

        var jsonDictionary = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(req.text);

        //var jsonDictionary = JsonConvert.DeserializeObject<IDictionary<string, object>>(
        //  req.text, new JsonConverter[] { new ConvertedJSON() });

        var allKeys = jsonDictionary.Keys.ToArray();

        /*foreach (var key in allKeys)
        {
            responseText.text += key.ToString() + "\n";
        }*/

        var objects = GetValues(allKeys, jsonDictionary);

        /*for (int i = 0; i < objects.Count; i++)
        {
            responseText.text += objects[i].;
        }*/

        foreach (var obj in objects)
        {
            // Fill first Dropdown
            responseText.text += obj["title"] + "\n";
        }

        /*if (objects != null)
        {
            responseText.text = objects.ToString();
        }*/

        /*responseText.text = jsonDictionary[allKeys[0]]["children"].ToString();
        var next = jsonDictionary[allKeys[0]]["children"];
        responseText.text = next["643"]["title"].ToString();*/

        /*foreach (var key in allKeys)
        {
            responseText.text += key.ToString() + "\n";
        }*/
    }

    /*Swift code
    /*
    var categoryID: Int?
        var title: String?
        var children: [FloorCategory]?

    init(_ dict: [String: Any])
    {
        guard let key: String = dict.keys.first,
        let params: [String: Any] = dict[key] as? [String: Any] else { return }
        
        categoryID = Int(key)
        title = params["title"] as? String

            guard let childrenDict = params["children"] as? [String: Any] else { return }
        
        children = childrenDict.flatMap({ (key, value) in
            return FloorCategory.init([key : value])
        })
    }
    */

    private List<Dictionary<dynamic, dynamic>> GetValues(dynamic[] keys, Dictionary<dynamic, dynamic> jsonDictionary)
    {
        //Dictionary<dynamic, dynamic>[] dicMas = new Dictionary<dynamic, dynamic>[keys.Length];
        //ListDictionary dicMas = new ListDictionary();
        //public List<Dictionary<string,int>> MyList= new List<Dictionary<string,int>>();

        List<Dictionary<dynamic, dynamic>> dicMas = new List<Dictionary<dynamic, dynamic>>();
        var firstKey = keys.First();

        /*if (jsonDictionary[firstKey].GetType() != typeof(JObject))
        {
            UnityEngine.Debug.Log("Not Dictionary");
            return null;
        }*/

        foreach (var key in keys)
        {
            string stringKey = (string)key;
            var value = jsonDictionary[stringKey];

            Dictionary<dynamic, dynamic> categoryDictionary = new Dictionary<dynamic, dynamic>();
            categoryDictionary["categoryId"] = stringKey;

            categoryDictionary["title"] = value["title"];
            //responseText.text += value;
            if (jsonDictionary.ContainsKey("children"))
            {
                Dictionary<dynamic, dynamic> ch = value["children"];
                var chKeys = ch.Keys.ToArray();
                var childrens = GetValues(chKeys, jsonDictionary);
                if (childrens != null)
                {
                    categoryDictionary["children"] = childrens;
                }
            }
            else
            {
                //UnityEngine.Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }

            dicMas.Add(categoryDictionary); //[dicMas.Count] = categoryDictionary;
        }

        return dicMas;
    }
}
