using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Business_Logic.People;


public class Loop : MonoBehaviour
{
    public Text completeName;
    public Text age;
    public Text occupation;
    public Text comment;
    public Text ageLabel;
    public Text occupationLabel;

    public Person currentPerson;

    // Use this for initialization
    void Start()
    {
        UnassignValues();
        InvokeRepeating("ReadQR", 1f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ReadQR()
    {
#if !UNITY_EDITOR
        try 
        {
            MediaFrameQrProcessing.Wrappers.ZXingQrCodeScanner.ScanFirstCameraForQrCode(
                       result =>
                       {
                           UnityEngine.WSA.Application.InvokeOnAppThread(() =>
                           {                               
                               if (result != null)
                               {
                                    currentPerson = JsonUtility.FromJson<Person>(result.Text);
                                    AssignValues();
                               }
                               else
                               {
                                    UnassignValues();
                               }
                           },
                           false);
                       },
                       TimeSpan.FromSeconds(1.4));
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
#endif
    }

    public void AssignValues()
    {
        if (currentPerson != null)
        {
            completeName.text = currentPerson.completeName;
            age.text = currentPerson.age;
            occupation.text = currentPerson.occupation;
            comment.text = currentPerson.comment;
            ageLabel.text = "Age:";
            occupationLabel.text = "Occupation:";
        }
    }

    private void UnassignValues()
    {
        completeName.text = String.Empty;
        age.text = String.Empty;
        occupation.text = String.Empty;
        comment.text = String.Empty;
        ageLabel.text = String.Empty;
        occupationLabel.text = String.Empty;
    }
}