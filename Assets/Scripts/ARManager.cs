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

    public void Download()
    {
        Debug.Log("Download!");
    }

    public void Share()
    {
        Debug.Log("Share!");
    }

    public void Buy()
    {
        Debug.Log("Buy!");
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
