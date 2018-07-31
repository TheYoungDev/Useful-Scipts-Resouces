using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ConsoleCommandManager : MonoBehaviour {

    public bool CommandWindowOpen = true;
    public KeyCode CommandKeyCode = KeyCode.BackQuote; //KeyCode.tab
    public KeyCode EnterCommandKeyCode = KeyCode.KeypadEnter;


    public delegate void CommandDelegate();
    public CommandDelegate Command;
    public List<CommandDelegate> Functions;
    public string[] ValidCommands;

    public InputField TextField;
    public string[] TextInput;
    public Text InfoBox;


    public Camera MainCam;
    public List<MonoBehaviour> ListOfBehaviours;



    // Use this for initialization
    void Start ()
    {

        CreateFunctionList(0);
        Cursor.lockState = CursorLockMode.None;
    }

   public void CreateFunctionList(int index)
    {
        Functions = new List<CommandDelegate>();
        Functions.Add(help);
        Functions.Add(time);
        Functions.Add(CallFunction);
        Functions.Add(ObjectsDetailsSelect);
        Functions.Add(ObjectsDetails);

    }

    // Update is called once per frame
    void Update () {
        if (CommandWindowOpen)
        {
            if (Input.GetKeyDown(EnterCommandKeyCode))
            {
                CheckCommand();
                TextField.text = "";
                TextField.ActivateInputField();
            }
            if (Input.GetKeyDown(CommandKeyCode))
            {
                CommandWindowOpen = false;
                TextField.enabled = false;
            }
        }
        /*else
        {
            if (Input.GetKeyDown(CommandKeyCode))
            {
                CommandWindowOpen = true;
                TextField.enabled = true;
            }
        }*/
	}
    public void CheckCommand()
    {
        var i = 0;
        var validCmd = false;
        foreach (string _command in ValidCommands)
        {
            if (TextField.text.ToLower().Contains(_command.ToLower()))
            {
                TextInput = (TextField.text.Split(' '));
                Functions[i]();

                validCmd = true;
                break;
            }
            i++;
        }
        if (!validCmd)
            Debug.Log("Invalid Command");

    }
    public void CallFunction()///string GO_Name, string _function
    {
        string GO_Name ="";
        string _function = "";
        string parameterStr;
        
        if (TextInput.Length>=0)
            GO_Name = TextInput[1];
        GameObject GO = GameObject.Find(GO_Name);

        if (TextInput.Length >= 1)
            _function = TextInput[2];

        if (TextInput.Length == 2)
        {
            parameterStr = TextInput[3];
            if (parameterStr.Contains("int"))
            {

                parameterStr.Remove(0, 3);
                var parameter = int.Parse(parameterStr);
                GO.SendMessage(_function, parameter);
                return;
            }
            if (parameterStr.Contains("float"))
            {

                parameterStr.Remove(0, 5);
                var parameter = int.Parse(parameterStr);
                GO.SendMessage(_function, parameter);
                return;
            }
            
        }
        //Debug.Log(GO_Name);
       // Debug.Log(_function);
            if (GO && _function !="")
                GO.SendMessage(_function);


    }
    public void CreateListOfBehaviours(GameObject GO)
    {
        ListOfBehaviours = new List<MonoBehaviour>();
        foreach (MonoBehaviour script in GO.GetComponents<MonoBehaviour>())
        {
            ListOfBehaviours.Add(script);
            Debug.Log(script.name);
        }
    }

    public void ObjectsDetailsSelect()
    {
        RaycastHit hit;
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("Name: " + objectHit.name + " " + "Tag: " + objectHit.tag);
            CreateListOfBehaviours(objectHit.gameObject);
        }

    }
    public void ObjectsDetails()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Transform objectHit = hit.transform;
            Debug.Log("Name: " + objectHit.name+ " " + "Tag: " + objectHit.tag);
            CreateListOfBehaviours(objectHit.gameObject);
        }
       

    }

    public void time()
    {
   
        Debug.Log(Time.timeSinceLevelLoad);
    }

    public void help()
    {
        Debug.Log("List of Commands");
        foreach (string _command in ValidCommands)
        {
            Debug.Log(_command.ToString());
        }
    }


    /* public ListMethods()
 {

 }*/

    public void ListObjectDetails()
    {
        /*foreach (FieldInfo fieldInfo in GetType().GetFields())
            if (fieldInfo.FieldType.IsSubclassOf(typeof(Object)))
                if ((Object)fieldInfo.GetValue(this) == null)
                    Debug.LogError("Field " + fieldInfo.FieldType.Name + " " + fieldInfo.Name + " is null!");*/
    }
}
