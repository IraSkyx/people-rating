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
    public HoloToolkit.Unity.InputModule.InteractiveMeshCursor cursor;
    public Canvas template;
    public Camera camera;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("ReadQR", 1f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cursor.transform.localToWorldMatrix.lossyScale);  
    }

    private void UpdateValues(Canvas canvas)
    {
        canvas.transform.SetPositionAndRotation(camera.ViewportToWorldPoint(camera.cameraToWorldMatrix.lossyScale), Quaternion.identity);
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
                                        AssignValues(person, Instantiate(template, cursor.transform.position, Quaternion.identity));
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
        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("CompleteName")).text = person.completeName;
        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("Age")).text = "Age: " + person.age;
        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("Occupation")).text = "Occupation: " + person.occupation;
        canvas.GetComponentsInChildren<Text>().First(x => x.gameObject.name.Equals("Comment")).text = person.comment;
        
        UpdateValues(canvas);
        people.Add(person, canvas);
    }
}
