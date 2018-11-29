using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseManager : MonoBehaviour {

    private String connectionString;
    public Text text;

	// Use this for initialization
	void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/DB.s3db";
    }

    public void Save () {
        IDbConnection dbConnection;
        dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        using (IDbCommand dbCmD = dbConnection.CreateCommand()) {
            string sqlQuery = String.Format
                ("INSERT INTO Player_Data(Score,Hit,Missed_Rewards,Average_Velocity,Maximum_Velocity) " +
                "VALUES ({0},{1},{2},{3},{4})",ScoreManager.currentScore, ScoreManager.hit
                , Spawner.totalRewards, ScoreManager.velCounter/ScoreManager.counter, ScoreManager.max);
            dbCmD.CommandText = sqlQuery;
            dbCmD.ExecuteScalar();
            
        }
        dbConnection.Close();
        StartCoroutine(TextShow());

    }

    IEnumerator TextShow() {
        text.text = "Your Data has been saved!";
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        text.gameObject.SetActive(false);
    }
}
