using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Temboo.Core;
using Temboo.Library;
using Temboo.Library.YouTube.Search;
using UnityEngine;
using UnityEngine.UI;

public class API : MonoBehaviour
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

        responseText.text = req.text;
    }

    private void Test()
    {

    }
}
