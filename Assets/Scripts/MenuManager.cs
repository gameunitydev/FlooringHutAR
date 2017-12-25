using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region Buttons

    [SerializeField] private Button[] _menuButtons;

    [SerializeField] private GameObject _buttonTry;
    [SerializeField] private GameObject _buttonShop;
    [SerializeField] private GameObject _buttonOffers;
    [SerializeField] private GameObject _buttonInformation;
    [SerializeField] private GameObject _buttonAbout;

    #endregion

    #region URLs

    private string _shopURL = "https://app.flooringhut.co.uk?amp=1";
    private string _offersURL = "https://app.flooringhut.co.uk/offers.html?amp=1";
    private string _informationURL = "https://app.flooringhut.co.uk/information";
    private string _aboutURL = "https://app.flooringhut.co.uk/about-us.html?amp=1";

    #endregion

    [SerializeField] private GameObject _imageFade;
    [SerializeField] private Animation _imageFadeAnimation;

    // Use this for initialization
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        foreach (var button in _menuButtons)
        {
            button.enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // When fade-out animation stops, enables all buttons and destroys unnecessary fade-image
        if (_imageFade != null && !_imageFadeAnimation.isPlaying)
        {
            foreach (var button in _menuButtons)
            {
                button.enabled = true;
            }

            GameObject.Destroy(_imageFade);
        }
    }

    public void ButtonClick(GameObject button)
    {
        // Switch can not be implemented here

        if (button.name == _buttonTry.name)
        {
            TryClick();
        }

        if (button.name == _buttonShop.name)
        {
            Application.OpenURL(_shopURL);
        }

        if (button.name == _buttonOffers.name)
        {
            Application.OpenURL(_offersURL);
        }

        if (button.name == _buttonInformation.name)
        {
            Application.OpenURL(_informationURL);
        }

        if (button.name == _buttonAbout.name)
        {
            Application.OpenURL(_aboutURL);
        }
    }

    // For "Separated" version
    public void TryClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
