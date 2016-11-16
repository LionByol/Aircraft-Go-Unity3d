using UnityEngine;
using System.Collections;

public class R {

	static public int selectedPlane;		//selected plane - 0~9
	static public int[] availablePlane;		//available plane - 10 planes. 0:unavailable, 1: available
	static public int[] availableLevel;		//available levels - 21 levels. -1:lock, 0:unlock, 123-stars
	static public int selectedLevel;		//selected level
	static public int gameMode;				//0:play mode,  1:rescue mission mode
	static public int gameWorld;			//0:sky, 1:land, 2:sea, 3:ar
	static public int gameDifficulty;		//game difficulty 0:easy 1:normal 2:hard

	static public int[] goal_coin = new int[]{
		100, 145, 120, 135, 170, 150, 55, 90, 125, 250, 200, 180, 145, 250, 180, 120, 199, 145, 230, 255, 450
	};
}
