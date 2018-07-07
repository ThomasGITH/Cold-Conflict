using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public int astronautCount, bossCount;

    public Wave(int wave)
    {
        calculateEnemyCount(wave);
    }
    
    public void calculateEnemyCount(int wave)
    {
        switch (wave)
        {
            case 1: astronautCount = 3; bossCount = 0;
                break;
            case 2:
                astronautCount = 6; bossCount = 0;
                break;
            case 3:
                astronautCount = 10; bossCount = 1;
                break;
            case 4:
                astronautCount = 13; bossCount = 1;
                break;
            case 5:
                astronautCount = 16; bossCount = 2;
                break;
            case 6:
                astronautCount = 19; bossCount = 2;
                break;
            case 7:
                astronautCount = 23; bossCount = 2;
                break;
            case 8:
                astronautCount = 27; bossCount = 3;
                break;
            case 9:
                astronautCount = 30; bossCount = 3;
                break;
            case 10:
                astronautCount = 33; bossCount = 3;
                break;
            case 11:
                astronautCount = 37; bossCount = 3;
                break;
            case 12:
                astronautCount = 40; bossCount = 4;
                break;
            case 13:
                astronautCount = 43; bossCount = 4;
                break;
            case 14:
                astronautCount = 46; bossCount = 4;
                break;
            case 15:
                astronautCount = 50; bossCount = 4;
                break;
            case 16:
                astronautCount = 53; bossCount = 4;
                break;
            case 17:
                astronautCount = 57; bossCount = 4;
                break;
            case 18:
                astronautCount = 60; bossCount = 5;
                break;
            case 19:
                astronautCount = 63; bossCount = 5;
                break;
            case 20:
                astronautCount = 67; bossCount = 5;
                break;
            case 21:
                astronautCount = 70; bossCount = 5;
                break;
        }
    }

    public int getAstronautCount()
    {
        return astronautCount;
    }

    public int getBossCount()
    {
        return bossCount;
    }


}
