using UnityEngine;
using System.Collections;

public class ShooterData : MonoBehaviour {

    public bool already_exist = false;

    public int enemy_killed;
    public int wave_size;

    public int score;
    public int life;

    public bool gameOver;

    void Awake()
    {
            DontDestroyOnLoad(this);   
    }

    public void saveEnemyManager(int em_enemy_killed, int em_wave_size)
    {
        enemy_killed = em_enemy_killed;
        wave_size = em_wave_size;
    }

    public void savePlayer(int pl_score, int pl_life)
    {
        score = pl_score;
        life = pl_life;
    }
}
