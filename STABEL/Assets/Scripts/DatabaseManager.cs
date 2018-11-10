using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseManager : MonoBehaviour {

    private String connectionString;

	// Use this for initialization
	void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/DB.s3db";
    }

    void Update()
    {
        if (ScoreManager.dead)
        {
            int score = ScoreManager.currentScore;
            int hit = ScoreManager.hit;
            int totalRewards = Spawner.totalRewards;
            float maxVel = ScoreManager.max;
            float avgVel = ScoreManager.velCounter / ScoreManager.counter;
            Insert(score, hit, totalRewards - score, avgVel, maxVel);
            ScoreManager.dead = false;
        }
    }

    private void Insert (int score, int hit, int totalRewards, float avgVel, float maxVel) {
        IDbConnection dbConnection;
        dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        using (IDbCommand dbCmD = dbConnection.CreateCommand()) {
            string sqlQuery = String.Format
                ("INSERT INTO Player_Data(Score,Hit,Missed_Rewards,Average_Velocity,Maximum_Velocity) " +
                "VALUES ({0},{1},{2},{3},{4})",score, hit, totalRewards, avgVel, maxVel);
            dbCmD.CommandText = sqlQuery;
            dbCmD.ExecuteScalar();
            dbConnection.Close();
            
        }

	}
}
