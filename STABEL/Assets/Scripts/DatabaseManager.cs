using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Text;
using System.IO;

public class DatabaseManager : MonoBehaviour {

    private String connectionString;
    public Text text;
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private string strDelimiter = ", ";

    // Use this for initialization
    void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/DB.s3db";
    }

    private void ExportToCSV(String fileName)
    {
    connection = new SqliteConnection(connectionString);
    connection.Open();
        command= connection.CreateCommand();
        command.CommandText="SELECT * FROM Data";
        reader =command.ExecuteReader();
        // Final string to print to text file
        StringBuilder sb = new StringBuilder();
        System.Object[] items = new System.Object[reader.FieldCount];
        while (reader.Read()){
            reader.GetValues(items );
            foreach (var item in items)
            {
                sb.Append(strDelimiter );
                sb.Append(item.ToString( ) );
            }
            sb.Append( "\n" );
        }  

        connection.Close();
        File.WriteAllText(fileName + ".txt", sb.ToString());
    }

    public void Save () {
        IDbConnection dbConnection;
        dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        using (IDbCommand dbCmD = dbConnection.CreateCommand()) {
            string sqlQuery = String.Format
                ("INSERT INTO Data(Score,Hit,Missed_Rewards,Average_Velocity,Maximum_Velocity, Maximum_Displacement, Average_Displacement) " +
                "VALUES ({0},{1},{2},{3},{4},{5},{6})",ScoreManager.currentScore, ScoreManager.hit
                , Spawner.totalRewards, ScoreManager.velCounter/ScoreManager.counter, ScoreManager.max, 
                ScoreManager.maxDist, ScoreManager.distCounter / ScoreManager.counter);
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
