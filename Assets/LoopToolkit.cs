using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Business_Logic.People;
using HoloToolkit.Unity.InputModule;
using System.Linq;

public class LoopToolkit : MonoBehaviour
{
    public Dictionary<Person, Canvas> people = new Dictionary<Person, Canvas>();
    public HoloToolkit.Unity.InputModule.Cursor cursor;
    public Canvas template;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("ReadQR", 1f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void UpdateValues(Canvas canvas)
    {
        canvas.transform.SetPositionAndRotation(cursor.Position, cursor.Rotation);
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
                                    Person person = JsonUtility.FromJson<Person>(result.Text);
                                    if (person != null && !people.ContainsKey(person))
                                    {
                                        Debug.Log(person);
                                        AssignValues(person, Instantiate(template, transform.parent, true));
                                    }
                                    else if(people.ContainsKey(person))
                                    {
                                        UpdateValues(people[person]);
                                    }                             
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

    void AssignValues(Person person, Canvas canvas)
    {
        foreach (var value in canvas.GetComponentsInChildren<Text>())
            Debug.Log(value.ToString());

        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("CompleteName")).text = person.completeName;
        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("Age")).text = "Age: " + person.age;
        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("Occupation")).text = "Occupation: " + person.occupation;
        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("Comment")).text = person.comment;
        
        UpdateValues(canvas);
        people.Add(person, canvas);
    }
}
