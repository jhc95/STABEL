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
    private string username;

    // Use this for initialization
    void Start () {
        username = Manager.username;
        if (Application.platform != RuntimePlatform.Android)
        {

            connectionString = Application.dataPath + "/DB.s3db";
        }
        else
        {

            connectionString = Application.persistentDataPath + "/DB.s3db";
        }
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
        dbConnection = new SqliteConnection("URI=file:" + connectionString);
        dbConnection.Open();

        using (IDbCommand dbCmD = dbConnection.CreateCommand()) {
            string sqlQuery = String.Format
                ("INSERT INTO Player_Data(Score,Hit,Missed_Rewards,Average_Velocity,Maximum_Velocity, Maximum_Displacement, Average_Displacement, Floor_Type) " +
                "VALUES ({0},{1},{2},{3},{4},{5},{6},'{7}')", ScoreManager.currentScore, ScoreManager.hit
                , Spawner.totalRewards, ScoreManager.velCounter / ScoreManager.counter, ScoreManager.max,
                ScoreManager.maxDist, ScoreManager.distCounter / ScoreManager.counter, myDropdown.selected);
            dbCmD.CommandText = sqlQuery;
            dbCmD.ExecuteScalar();
        }
        dbConnection.Close();
        Send(); //Send Data to Google Sheets.
        StartCoroutine(TextShow());
    }

    IEnumerator Post()
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.279369748", username);
        form.AddField("entry.668724949", Convert.ToString(ScoreManager.velCounter / ScoreManager.counter));
        form.AddField("entry.1447013133", Convert.ToString(ScoreManager.max));
        form.AddField("entry.1483515053", Convert.ToString(ScoreManager.distCounter / ScoreManager.counter));
        form.AddField("entry.613153977", Convert.ToString(ScoreManager.currentScore));
        form.AddField("entry.2009085010", Convert.ToString(ScoreManager.hit));
        form.AddField("entry.106557325", Convert.ToString(3 - ScoreManager.hit)); //hard coded health point
        form.AddField("entry.999372477", Convert.ToString(Spawner.totalRewards)); 
        form.AddField("entry.1350369698", "Max angle forward"); //hard coded for max_angle_forward

        //Debug.Log("Preparing data to send");
        byte[] ramData = form.data;
        WWW www = new WWW("https://docs.google.com/forms/u/1/d/e/1FAIpQLSfdn08eSFu_fc10372cazqqNd__cyw4ZbvCw0U1vPMJ5eyUUw/formResponse", ramData);
        //Debug.Log("Sending data");
        yield return www;
        //Debug.Log(www.error);
        //Debug.Log(www.url);
        //Debug.Log(www.isDone);
    }


    public void Send()
    {
        //Debug.Log("I got into this");
        StartCoroutine(Post());
    }

    IEnumerator TextShow() {
        text.text = "Your Data has been saved!";
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        text.gameObject.SetActive(false);
    }
}
