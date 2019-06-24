using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

public class LevelSelector : MonoBehaviour
{
    static string[] Scopes = { SheetsService.Scope.Spreadsheets };
    static string ApplicationName = "STABEL";
    static String spreadsheetId = "1o6IDUyYbyHCUiUcv5Rl7r7AHV6ES6KC6WCf5R7RrZ4Q";
    UserCredential credential;
    SheetsService service;

    public PlayerBehavior player;
    public Spawner spawner;
    public Manager manager;
    public Dropdown platformDropDown;

    int numLevels = 0;
    public Dropdown dropdown;
    public GameObject panel;

    String range = "";
    SpreadsheetsResource.ValuesResource.GetRequest request;
    ValueRange returnData;
    IList<IList<System.Object>> values;

    // Start is called before the first frame update
    void Start()
    {
        using (var stream = new FileStream("client_secret_810592468800-53mbfahniql4q09ladv4ifq7j2o9gch0.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
        {
            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "STABEL",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            //Console.WriteLine("Credential file saved to: " + credPath);

            //Creating Google Sheets API service
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        UpdateList();
        SetLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateList()
    {
        range = "Levels!B1";

        request = service.Spreadsheets.Values.Get(spreadsheetId, range);
        returnData = request.Execute();
        values = returnData.Values;
        numLevels = Int32.Parse(values[0][0].ToString());

        dropdown.ClearOptions();
        if (numLevels == 1)
        {
            range = "Levels!A" + 3;
            String optionName = "";
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            returnData = request.Execute();
            values = returnData.Values;
            optionName = (String)values[0][0];
            Dropdown.OptionData defaultOption = new Dropdown.OptionData();
            defaultOption.text = optionName;
            dropdown.options.Add(defaultOption);
        }
        else
        {
            int endCol = 2 + numLevels;
            range = "Levels!A3:A" + endCol;
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            returnData = request.Execute();
            values = returnData.Values;
            foreach (var row in values)
            {
                foreach (var column in row)
                {
                    String optionName = column.ToString();
                    Dropdown.OptionData option = new Dropdown.OptionData();
                    option.text = optionName;
                    dropdown.options.Add(option);
                }
            }
        }
        SetLevel();
    }

    public void SetLevel()
    {
        int rowIndex = 0;
        if (numLevels == 1)
        {
            range = "Levels!A3:G3";
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            returnData = request.Execute();
            values = returnData.Values;

            manager.SetSensitivityLimit(values[0][1].ToString());
            manager.SetObstacleSpawnLimit(values[0][2].ToString());
            manager.SetTimeLimit(values[0][3].ToString());
            platformDropDown.value = int.Parse(values[0][4].ToString());
            manager.bindex = int.Parse(values[0][5].ToString());
            manager.UpdateBackground();
            int toggle = int.Parse(values[0][6].ToString());
            if(toggle == 0)
            {
                manager.movingToggle.isOn = true;
            }
            else
            {
                manager.movingToggle.isOn = false;
            }
        }
        else if (numLevels > 1)
        {
            int endCol = 2 + numLevels;
            range = "Levels!A3:G" + endCol;
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            returnData = request.Execute();
            values = returnData.Values;

            foreach(var row in values)
            {
                if(rowIndex == dropdown.value)
                {
                    manager.SetSensitivityLimit(values[rowIndex][1].ToString());
                    manager.SetObstacleSpawnLimit(values[rowIndex][2].ToString());
                    manager.SetTimeLimit(values[rowIndex][3].ToString());
                    platformDropDown.value = int.Parse(values[rowIndex][4].ToString());
                    manager.bindex = int.Parse(values[rowIndex][5].ToString());
                    manager.UpdateBackground();
                    int toggle = int.Parse(values[rowIndex][6].ToString());
                    if (toggle == 0)
                    {
                        manager.movingToggle.isOn = true;
                    }
                    else
                    {
                        manager.movingToggle.isOn = false;
                    }
                }
                rowIndex++;
            }
        }
    }
}
