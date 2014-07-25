using UnityEngine;
using System.Collections;

public enum GameState {
	Playing,Stop,PrepareStop
}
public class Constant {
	public static float road_lenght = 12f;
	public static float road_weight = 7f;
	public static float road_delta = 5f;
	public static float main_delta = 5f;
	public static float Horizontal_delta = 6f;
	public static float enemy_delta = 3f;
	public static float enemy_updelta = 2f;
	public static float enemy_critical_updelta = 7f;
	public static float enemy_Horizontal_delta = 1f;
	public static float enemy_notfollow_length = 1.5f;

	public static string enemy_prefab_direct = "Prefabs/Enemies/";
	public static string maincar_prefab_direct = "Prefabs/Main/";
	public static string button_direct = "Prefabs/Button/";
	public static string item_direct = "Prefabs/Item/";

	public static float position_random_range_car_people1 = 3.5f;
	public static float position_random_range_car_people2 = 3.5f;
	public static float position_random_range_car_people3 = 3.5f;
	public static float position_random_range_car_people4 = 3.5f;
	public static float position_random_range_car_people5 = 3.5f;
	public static float position_random_range_car_people6 = 3.5f;
	public static float position_random_range_car_people7 = 1.2f;
	public static float position_random_range_gasoline = 3.5f;


	public static float random_distance = 14f;

	public static float car_max_energy = 30f;
	public static float percentofGasoline = 0.5f;
	public static float lost_energy_percent = 01f; // 10% /1s

	public static int maxofRespawn = 4 ; // 4 mạng  



}
