package com.example.fuel_management_mobile_app.model;

import androidx.annotation.NonNull;

public class Station {

    private int fuelStationId;
    private String fuelStationName;
    private String location;
    private String opentime;
    private String closetime;

    public Station(){

    }

    public int getFuelStationId() {
        return fuelStationId;
    }

    public void setFuelStationId(int fuelStationId) {
        this.fuelStationId = fuelStationId;
    }

    public String getFuelStationName() {
        return fuelStationName;
    }

    public void setFuelStationName(String fuelStationName) {
        this.fuelStationName = fuelStationName;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getOpentime() {
        return opentime;
    }

    public void setOpentime(String opentime) {
        this.opentime = opentime;
    }

    public String getClosetime() {
        return closetime;
    }

    public void setClosetime(String closetime) {
        this.closetime = closetime;
    }

    @NonNull
    @Override
    public String toString() {
        return "Station{" +
                "fuelStationId=" + fuelStationId +
                ", fuelStationName='" + fuelStationName + '\'' +
                ", location='" + location + '\'' +
                ", opentime='" + opentime + '\'' +
                ", closetime='" + closetime + '\'' +
                '}';
    }
}
