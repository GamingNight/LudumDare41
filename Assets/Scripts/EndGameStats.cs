using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EndGameStats {

    public enum GameOverType
    {

        GOOD_GOAL_KEEPER, CATCHED_BY_OPPONENT, TIME_OUT
    }

    public static GameOverType GAME_OVER_TYPE;
}
