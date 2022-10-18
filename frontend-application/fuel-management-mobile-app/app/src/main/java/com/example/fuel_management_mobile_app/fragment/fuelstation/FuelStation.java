package com.example.fuel_management_mobile_app.fragment.fuelstation;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.fragment.app.Fragment;

import com.example.fuel_management_mobile_app.R;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link FuelStation#newInstance} factory method to
 * create an instance of this fragment.
 */
public class FuelStation extends Fragment {

    public FuelStation() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_fuel_station, container, false);
    }
}