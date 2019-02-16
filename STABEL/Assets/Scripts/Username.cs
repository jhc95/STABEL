using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Text;
using System.IO;

public class Username : MonoBehaviour {
    public InputField field;
    public GameObject entername;
    public static string name;
    public GameObject menu;
    public GameObject usernameMenu;
    public GameObject bg;
    public GameObject health;
    public GameObject time;
    public GameObject score;
    public GameObject play;

    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private String connectionString;

    public void Click_Continue() {
        name = field.text;
        if (name == "")
        {
            StartCoroutine(Message());
        }
        else {
            usernameMenu.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
            bg.SetActive(true);
            health.SetActive(true);
            time.SetActive(true);
            score.SetActive(true);
            play.SetActive(true);
            Save();
        }

    }

    IEnumerator Message() {
        entername.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        entername.gameObject.SetActive(false);
    }

    public void Save()
    {
        Debug.Log(Application.persistentDataPath);
        if (Application.platform != RuntimePlatform.Android)
        {

            connectionString = Application.dataPath + "/DB.s3db";
        }
        else
        {

            connectionString = Application.persistentDataPath + "/DB.s3db";
        }
        IDbConnection dbConnection;
        dbConnection = new SqliteConnection("URI=file:" + connectionString);
        dbConnection.Open();

        using (IDbCommand dbCmD = dbConnection.CreateCommand())
        {
            string sqlQuery = String.Format
                ("INSERT INTO User(username) " +
                "VALUES ('{0}')", name);
            dbCmD.CommandText = sqlQuery;
            dbCmD.ExecuteScalar();
        }
        dbConnection.Close();
       

    }
}
