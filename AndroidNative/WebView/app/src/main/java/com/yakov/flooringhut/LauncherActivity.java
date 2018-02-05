package com.yakov.flooringhut;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import java.util.ArrayList;
import java.util.List;

public class LauncherActivity extends AppCompatActivity {

    public static final String EXTRA_MESSAGE = "info.androidhive.webview.MESSAGE";

    private final String ShopURL = "https://www.flooringhutapp.co.uk?amp=1";
    private final String LoginURL = "https://www.flooringhutapp.co.uk/customer/account/login/?amp=1";
    private final String RegisterNowURL = "https://www.flooringhutapp.co.uk/customer/account/create/?amp=1";
    private final String TradeSignupURL = "https://www.flooringhutapp.co.uk/registertrade.php?amp=1";

    private Button buttonShop;
    private Button buttonLogin;
    private Button buttonRegisterNow;
    private Button buttonTradeSignup;

    List<Button> menuButtons = new ArrayList<Button>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_launcher);

        buttonShop = (Button) findViewById(R.id.buttonShop);
        buttonLogin = (Button) findViewById(R.id.buttonLogin);
        buttonRegisterNow = (Button) findViewById(R.id.buttonRegisterNow);
        buttonTradeSignup = (Button) findViewById(R.id.buttonTradeSignup);

        menuButtons.add(buttonShop);
        menuButtons.add(buttonLogin);
        menuButtons.add(buttonRegisterNow);
        menuButtons.add(buttonTradeSignup);

        for (final Button btn : menuButtons) {
            btn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Redirect(btn.getId());
                }
            });
        }
    }

    private void Redirect(int buttonId)
    {
        if (buttonId == buttonShop.getId())
        {
            GoToWebActivity(ShopURL);
        }

        if (buttonId == buttonLogin.getId())
        {
            GoToWebActivity(LoginURL);
        }

        if (buttonId == buttonRegisterNow.getId())
        {
            GoToWebActivity(RegisterNowURL);
        }

        if (buttonId == buttonTradeSignup.getId())
        {
            GoToWebActivity(TradeSignupURL);
        }
    }

    private void GoToWebActivity(String URL)
    {
        Intent intent = new Intent(this, MainActivity.class);
        intent.putExtra(EXTRA_MESSAGE, URL);
        startActivity(intent);
    }
}
