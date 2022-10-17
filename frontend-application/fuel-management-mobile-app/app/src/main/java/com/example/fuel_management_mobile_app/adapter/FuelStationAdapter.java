package com.example.fuel_management_mobile_app.adapter;

import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.example.fuel_management_mobile_app.R;

import androidx.annotation.NonNull;
import androidx.cardview.widget.CardView;
import androidx.recyclerview.widget.RecyclerView;

public class FuelStationAdapter extends RecyclerView.Adapter<FuelStationAdapter.FuelStationViewHolder> {
    @NonNull
    @Override
    public FuelStationAdapter.FuelStationViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        return null;
    }

    @Override
    public void onBindViewHolder(@NonNull FuelStationAdapter.FuelStationViewHolder holder, int position) {

    }

    @Override
    public int getItemCount() {
        return 0;
    }

    public static class FuelStationViewHolder extends RecyclerView.ViewHolder {

        public TextView textView_1, textView_2, textView_3, textView_4, textView_5;
        public Button button;
        public CardView cardView;

        public FuelStationViewHolder(View itemView) {

            super(itemView);
//            this.textView_1 = (TextView) itemView.findViewById(R.id.delivery_ref);
//            this.textView_2 = (TextView) itemView.findViewById(R.id.delivery_material);
//            this.textView_3 = (TextView) itemView.findViewById(R.id.delivery_quantity);
//            this.textView_4 = (TextView) itemView.findViewById(R.id.delivery_status);
//            this.textView_5 = (TextView) itemView.findViewById(R.id.text_vehicle_no);

            cardView = itemView.findViewById(R.id.fuel_station_list_card_view);

        }
    }
}
