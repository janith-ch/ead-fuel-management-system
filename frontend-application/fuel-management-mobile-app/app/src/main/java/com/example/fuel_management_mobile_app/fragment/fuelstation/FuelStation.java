package com.example.fuel_management_mobile_app.fragment.fuelstation;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.fuel_management_mobile_app.R;
import com.example.fuel_management_mobile_app.adapter.FuelStationAdapter;
import com.example.fuel_management_mobile_app.constant.CommonConstant;
import com.example.fuel_management_mobile_app.model.Station;

import org.json.JSONArray;

import java.util.ArrayList;
import java.util.List;

import lombok.SneakyThrows;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link FuelStation} factory method to
 * create an instance of this fragment.
 */
public class FuelStation extends Fragment {

    FuelStationAdapter fuelStationAdapter;
    private final List<Station> fuelStations = new ArrayList<>();


    public FuelStation() {
        // Required empty public constructor
    }
    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        getStations();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {


        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_fuel_station, container, false);
        RecyclerView recyclerView = view.findViewById(R.id.fuel_stations_recycle_view);
        fuelStationAdapter = new FuelStationAdapter(fuelStations, getContext());

        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        recyclerView.setAdapter(fuelStationAdapter);

        return view;
    }

    private void getStations() {

        //RequestQueue initialized
        RequestQueue requestQueue = Volley.newRequestQueue(getContext());

       JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(

                Request.Method.GET,
                CommonConstant.BASE_URL.concat("FuelStation"),
                null,

                new Response.Listener<JSONArray>() {

                    @SuppressLint({"LongLogTag", "NotifyDataSetChanged"})
                    @SneakyThrows
                    @Override
                    public void onResponse(JSONArray stations) {

                        Log.i("Response {}", stations.toString());

                        fuelStations.clear();

                        for (int i = 0; i < stations.length(); i++) {

                            try {

                             //   Station station= new Station();
                                Station station = (Station) stations.get(i);

                                station.setFuelStationId(station.getFuelStationId());
                                station.setLocation(station.getLocation());
                                station.setOpentime(station.getOpentime());
                                station.setClosetime(station.getClosetime());

                                fuelStations.add(station);

                            } catch (Exception exception) {

                                exception.printStackTrace();
                            }

                        }
                        Log.i("filtered station response {}", fuelStations.toString());

                        fuelStationAdapter.notifyDataSetChanged();
                    }

                }, (Response.ErrorListener) error -> Log.e("Response", error.toString())

        );
        requestQueue.add(jsonArrayRequest);

    }
}