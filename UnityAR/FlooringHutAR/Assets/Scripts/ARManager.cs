using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    [SerializeField] private GameObject Dropdown1Object;
    [SerializeField] private Dropdown dropdown1;
    [SerializeField] private Dropdown dropdown2;
    [SerializeField] private Dropdown dropdown3;

    private string _productURL;

    // Use this for initialization
    private void Start()
    {
        API_AR.Instance.FillDropdownCategories(dropdown1);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void OnValueChangedDrop1()
    {
        API_AR.Instance.OnValueChangedDrop1(dropdown1, dropdown2);
        dropdown2.value = 0;
    }

    public void OnValueChangedDrop2()
    {
        API_AR.Instance.OnValueChangedDrop2(dropdown2, dropdown3);
        dropdown3.value = 0;
    }

    public void OnValueChangedDrop3()
    {
        API_AR.Instance.OnValueChangedDrop3(dropdown3);
    }

    public static string GetDownloadFolder()
    {
        string[] temp = (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" }, System.StringSplitOptions.None);

        return (temp[0] + "/Download");
    }

    public void Download()
    {
        //GetDownloadFolder();
        //Download current floor image to Download folder
        StartCoroutine(loadBgImage("img"));
        Debug.Log("Download!");
    }

    public void Share()
    {
        Debug.Log("Share!");
    }

    public void Buy()
    {
        //_productURL = "";
        //Application.OpenURL(_productURL);

        Debug.Log("Buy!");
    }

    IEnumerator loadBgImage(string _imgName)
    {
        string publisherDir = GetDownloadFolder() + "/" + "imgs";
        string bookDir = GetDownloadFolder() + "/" + "imgs" + " / " + "img";

        if (!System.IO.Directory.Exists(publisherDir))
        {
            System.IO.Directory.CreateDirectory(publisherDir);
        }

        if (!System.IO.Directory.Exists(bookDir))
        {
            System.IO.Directory.CreateDirectory(bookDir);
        }

        WWW www = new WWW("http://cdn.edgecast.steamstatic.com/steam/apps/429660/capsule_616x353.jpg?t=1495836396" + "/images/" + _imgName);

        yield return www;

        System.IO.File.WriteAllBytes(bookDir + "/" + _imgName, www.bytes);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
