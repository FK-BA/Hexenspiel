using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UIElements;
using System.Globalization;
using Valve.VR;
using Valve.VR.InteractionSystem;
using TMPro;

public class Output : MonoBehaviour
{
    public GameObject Camera = null;
    public GameObject Controller = null;
    public TMP_InputField TMP_InputField;
    public GameObject kristalleV1;


    private SteamVR_Action_Boolean buttonPressed; // flying
    private SteamVR_Action_Boolean buttonPressedHolding; // holding

    private String Name;
    private List<string[]> rowData = new List<string[]>();
    private int counter;
    public int Kristall_remaining;

    private void Awake()
    {
        DateTime localDate = DateTime.Now;
        Name = System.DateTime.Now.ToString("HH-mm-ss");
        buttonPressed = SteamVR_Input.GetBooleanAction("TouchpadPress"); //flying
        buttonPressedHolding = SteamVR_Input.GetBooleanAction("GrabPinch"); //holding
    }

    void Start()
    {
        Kristall_remaining = 0;
        PotionTrigger[] Kristall = FindObjectsOfType<PotionTrigger>();
        Kristall_remaining = Kristall.Length;
        Debug.Log(Kristall.Length);
        TMP_InputField.gameObject.SetActive(true);

        string[] rowDataTemp = new string[16]; // Adjust the array length to accommodate the new data
        rowDataTemp[0] = "Timestamp";
        rowDataTemp[1] = "Crystals"; //collected crystals
        rowDataTemp[2] = "X_Head_Position";
        rowDataTemp[3] = "Y_Head_Position";
        rowDataTemp[4] = "Z_Head_Position";
        rowDataTemp[5] = "X_Head_Rotation";
        rowDataTemp[6] = "Y_Head_Rotation";
        rowDataTemp[7] = "Z_Head_Rotation";
        rowDataTemp[8] = "X_Controller_Position";
        rowDataTemp[9] = "Y_Controller_Position";
        rowDataTemp[10] = "Z_Controller_Position";
        rowDataTemp[11] = "X_Controller_Rotation";
        rowDataTemp[12] = "Y_Controller_Rotation";
        rowDataTemp[13] = "Z_Controller_Rotation";
        rowDataTemp[14] = "FlyingButton"; //force button for flying
        rowDataTemp[15] = "HoldButton"; //pick up objects

        rowData.Add(rowDataTemp);
    }

    void Update()
    {
        if (TMP_InputField.interactable == false)
        {
            Save();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Application.Quit();
        }


        UpdateCrystalCount();


    }

    void UpdateCrystalCount()
    {
        if (TMP_InputField.interactable)
        {
            // If the input field is interactable, perform this block of code
            PotionTrigger[] Kristall = FindObjectsOfType<PotionTrigger>();
            Kristall_remaining = Kristall.Length;
        }
        else
        {
            // If the input field is not interactable, perform this block of code
            PotionTrigger[] Kristall = FindObjectsOfType<PotionTrigger>();
            if (Kristall.Length != Kristall_remaining)
            {
                counter++;
                Debug.Log(counter);
                Kristall_remaining = Kristall.Length;
                Debug.Log(Kristall.Length);
            }
        }
    }
    void Save()
    {
        string[] rowDataTemp = new string[16]; // Adjust the array length
        rowDataTemp[0] = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fffff");
        rowDataTemp[1] = counter.ToString(); //Crystal counter
        rowDataTemp[2] = Camera.transform.position.x.ToString();
        rowDataTemp[3] = Camera.transform.position.y.ToString();
        rowDataTemp[4] = Camera.transform.position.z.ToString();
        rowDataTemp[5] = Camera.transform.eulerAngles.x.ToString();
        rowDataTemp[6] = Camera.transform.eulerAngles.y.ToString();
        rowDataTemp[7] = Camera.transform.eulerAngles.z.ToString();
        rowDataTemp[8] = Controller.transform.position.x.ToString();
        rowDataTemp[9] = Controller.transform.position.y.ToString();
        rowDataTemp[10] = Controller.transform.position.z.ToString();
        rowDataTemp[11] = Controller.transform.eulerAngles.x.ToString(); //Controller rotation
        rowDataTemp[12] = Controller.transform.eulerAngles.y.ToString(); //Controller rotation
        rowDataTemp[13] = Controller.transform.eulerAngles.z.ToString(); //Controller rotation
        rowDataTemp[14] = buttonPressed.GetState(SteamVR_Input_Sources.Any) ? "pressed" : (buttonPressed.GetStateUp(SteamVR_Input_Sources.Any) ? "released" : "");
        rowDataTemp[15] = buttonPressedHolding.GetState(SteamVR_Input_Sources.Any) ? "pressed" : (buttonPressedHolding.GetStateUp(SteamVR_Input_Sources.Any) ? "released" : "");

        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = "/";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = GetPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    string GetPath()
    {
        string kristalleVersion = kristalleV1.activeSelf ? "V1" : "V2"; // Determine the version

        // Find the "HighScoreWater" GameObject
        GameObject highScoreWater = GameObject.Find("HighScoreWater");

        // Get the HighScoreIndicator component attached to it
        HighScoreIndicator highScoreIndicator = highScoreWater.GetComponent<HighScoreIndicator>();

        // Get the highScoreThreshold value
        int highScoreThreshold = highScoreIndicator.highScoreThreshold;

#if UNITY_EDITOR
    return Application.dataPath + "/VR 1 Witch Game/" + DateTime.Now.ToLongDateString() + "_" + Name + "_" + TMP_InputField.text.ToString() + "_" + kristalleVersion + "_" + highScoreThreshold + ".csv";
#else
        return Application.dataPath + "/" + DateTime.Now.ToLongDateString() + "_" + Name + "_" + TMP_InputField.text.ToString() + "_" + kristalleVersion + "_" + highScoreThreshold + ".csv";
#endif
    }
}