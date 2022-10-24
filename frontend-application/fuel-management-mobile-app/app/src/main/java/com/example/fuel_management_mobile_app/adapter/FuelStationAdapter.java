package com.example.fuel_management_mobile_app.adapter;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.example.fuel_management_mobile_app.R;
import com.example.fuel_management_mobile_app.fragment.fuelstation.FuelDetails;
import com.example.fuel_management_mobile_app.model.Station;

import androidx.annotation.NonNull;
import androidx.cardview.widget.CardView;
import androidx.fragment.app.FragmentActivity;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

public class FuelStationAdapter extends RecyclerView.Adapter<FuelStationAdapter.FuelStationViewHolder> {

    private final List<Station> fuelStations;
    private final Context context;

    public FuelStationAdapter(List<Station> fuelStations, Context context) {
        this.fuelStations = fuelStations;
        this.context = context;
    }

    @NonNull
    @Override
    public FuelStationAdapter.FuelStationViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater layoutInflater = LayoutInflater.from(parent.getContext());
        View listItem = layoutInflater.inflate(R.layout.single_view_station_layout, parent, false);
        return new FuelStationViewHolder(listItem);
    }

    @Override
    public void onBindViewHolder(@NonNull FuelStationAdapter.FuelStationViewHolder holder, int position) {

        holder.textView_1.setText(fuelStations.get(position).getFuelStationName());
        holder.textView_2.setText(fuelStations.get(position).getLocation());
        holder.textView_3.setText(fuelStations.get(position).getOpentime());
        holder.textView_4.setText(fuelStations.get(position).getClosetime());
        holder.textView_5.setText(this.checkStationStatus());

        holder.cardView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

            }
        });
        holder.dieselButton.setOnClickListener(new View.OnClickListener() {

            final Bundle bundle = new Bundle();
            final FuelDetails fuelDetails = new FuelDetails();

            @Override
            public void onClick(View v) {

                FragmentTransaction mFragmentTransaction = ((FragmentActivity) context).getSupportFragmentManager().beginTransaction();
                mFragmentTransaction.replace(R.id.flFragment, fuelDetails).commit();
            }
        });

        holder.petrolButton.setOnClickListener(new View.OnClickListener() {

            Bundle bundle = new Bundle();
            final FuelDetails fuelDetails = new FuelDetails();

            @Override
            public void onClick(View v) {

                FragmentTransaction mFragmentTransaction = ((FragmentActivity) context).getSupportFragmentManager().beginTransaction();
                mFragmentTransaction.replace(R.id.flFragment, fuelDetails).commit();
            }
        });

    }

    @Override
    public int getItemCount() {
        return fuelStations.size();
    }

    public static class FuelStationViewHolder extends RecyclerView.ViewHolder {

        public TextView textView_1, textView_2, textView_3, textView_4, textView_5;
        public Button petrolButton ,dieselButton;
        public CardView cardView;

        public FuelStationViewHolder(View itemView) {

            super(itemView);
            this.textView_1 = (TextView) itemView.findViewById(R.id.station_name_id);
            this.textView_2 = (TextView) itemView.findViewById(R.id.station_location_id);
            this.textView_3 = (TextView) itemView.findViewById(R.id.station_open_id);
            this.textView_4 = (TextView) itemView.findViewById(R.id.station_close_id);
            this.textView_5 = (TextView) itemView.findViewById(R.id.station_status);
            this.dieselButton = (Button) itemView.findViewById(R.id.fuel_diesel_btn);
            this.petrolButton = (Button) itemView.findViewById(R.id.fuel_petrol_btn);
            cardView = itemView.findViewById(R.id.fuel_station_list_card_view);

        }
    }

    private String checkStationStatus(){

        return "OPEN";
    }
}
