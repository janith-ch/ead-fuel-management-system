package com.example.fuel_management_mobile_app.activity;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.view.MenuItem;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import com.example.fuel_management_mobile_app.R;
import com.example.fuel_management_mobile_app.fragment.dashboard.Dashboard;
import com.example.fuel_management_mobile_app.fragment.fuelstation.FuelStation;
import com.example.fuel_management_mobile_app.fragment.users.Users;
import com.google.android.material.bottomnavigation.BottomNavigationView;

public class MainActivity extends AppCompatActivity implements BottomNavigationView.OnNavigationItemSelectedListener {


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        BottomNavigationView bottomNavigationView;
        bottomNavigationView = findViewById(R.id.bottomNavigationView);
        bottomNavigationView.setOnNavigationItemSelectedListener(this);
        bottomNavigationView.setSelectedItemId(R.id.dashboard);
    }

    Dashboard dashboard = new Dashboard();
    FuelStation fuelStation = new FuelStation();
    Users users = new Users();

    @SuppressLint("NonConstantResourceId")
    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()) {
            case R.id.dashboard:

                getSupportFragmentManager().beginTransaction().replace(R.id.flFragment, dashboard).commit();
                return true;

            case R.id.fuel:
                getSupportFragmentManager().beginTransaction().replace(R.id.flFragment, fuelStation).commit();
                return true;

            case R.id.users:
                getSupportFragmentManager().beginTransaction().replace(R.id.flFragment, users).commit();
                return true;

        }
        return false;
    }

}