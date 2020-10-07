using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class addCommandClass : Attribute
{
    public addCommandClass()
    {
       
    }
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class addCommand : Attribute
{
    private string name;

    public virtual string callName
    {
        get
        {
            return name;
        }
    }

    public addCommand(string call)
    {
        name = call;
       
    }



}

public class ConsoleSystem : MonoBehaviour
{
    public GameObject[] CommandObject; 
   
   
    public FirstPersonController fps;
    public InputField input;
    public GameObject voschau;
    public GameObject interactfield;
    public Text interactText;
    public RectTransform vorschauView;
    public Text vorschauText;
    public static bool menuAktive;
    private bool cheatOn = false,commandAktiv;
    float timer;
    bool ist;

    string comS = "<b><color=green>", ckE = "</color></b>", keyS = "<b><color=red>";
    float offmin;
    
    List<string> commands = new List<string>()
    {
        "//cheat"
    };

    public class commandList
    {

        public string name;
        public object target;
        public MethodInfo mi;

        public commandList(string name, MethodInfo mi, object target)
        {
            this.target = target;
            this.name = name;
            this.mi = mi;
        }
    }

    public List<commandList> cList = new List<commandList>();

    // Start is called before the first frame update
    void Start()
    {
        
        input = GetComponent<InputField>();
        offmin = vorschauView.sizeDelta.y;
        getConsolMethods();
    }

    void getConsolMethods()
    {
        List<string> tp = new List<string>();
        List<MonoBehaviour> mon = new List<MonoBehaviour>();
        for (int x = 0; x < CommandObject.Length; x++)
        {
            for (int y = 0; y < CommandObject[x].GetComponents<MonoBehaviour>().Length; y++)
            {
                if (CommandObject[x].GetComponents<MonoBehaviour>()[y].GetType().GetCustomAttribute<addCommandClass>() != null)
                {
                    mon.Add(CommandObject[x].GetComponents<MonoBehaviour>()[y]);
                    tp.Add(CommandObject[x].GetComponents<MonoBehaviour>()[y].GetType().Name);
                }
            }
        }

        for (int i = 0; i < mon.Count; i++)
        {
            MethodInfo[] m = mon[i].GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            
            for (int j = 0; j < m.Length; j++)
            {

                if (m[j].GetCustomAttribute<addCommand>() != null)
                {
                    string s = m[j].GetCustomAttribute<addCommand>().callName;

                    for (int v = 0; v < CommandObject.Length; v++) {
                        if (CommandObject[v].GetComponent(tp[i])) {
                            cList.Add(new commandList(s, m[j], CommandObject[v].GetComponent(tp[i])));
                            Debug.Log("add command " + s);
                            commands.Add("//" + s);
                        }
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        
            if (input.text.Length > 0 && !voschau.activeInHierarchy) voschau.SetActive(true);
            string s = "";

        if (input.text.Contains("//"))
        {
            for (int i = 0; i < commands.Count; i++) if (commands[i].Contains(input.text)) s += commands[i] + "\n";
            vorschauText.text = s;
        }
        else
        {
            vorschauText.text = "";
            if(vorschauView.gameObject.activeInHierarchy) voschau.SetActive(false);
        }
        
    }

    void drawInteract(string text)
    {
        if (!interactfield.activeInHierarchy)
        {
           
            interactfield.SetActive(true);
            interactText.text = text;
            timer = 0;
            ist = true;
        }
           
        

    }

    void drawingInter()
    {
        timer += Time.deltaTime;

        if (timer % 60 >= 5)
        {
            ist = false;
            interactfield.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ist) drawingInter();

        if (menuAktive)
        {
            if (fps.enabled)
            {
                fps.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }


            foreach (string com in commands)
            {
                if (input.text.Equals(com))
                {
                    input.text = input.text.Replace(com, comS + com + ckE);
                    input.caretPosition = input.text.Length;
                    commandAktiv = true;
                }

                if (input.text.Equals((comS + com + ckE).Remove((comS + com + ckE).Length - 1)))
                {
                    input.text = input.text.Replace((comS + com + ckE).Remove((comS + com + ckE).Length - 1), com.Remove(com.Length - 1));
                    input.caretPosition = input.text.Length;
                    commandAktiv = true;
                }

                if (commandAktiv)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (!cheatOn)
                        {
                            if (input.text.ToLower().Equals((comS + "//cheat" + ckE + " On").ToLower()))
                            {
                                cheatOn = true;
                                drawInteract("Cheats Activate");
                            }
                        }
                        else
                        {
                            if (input.text.ToLower().Equals((comS + "//cheat" + ckE + " Off").ToLower()))
                            {
                                cheatOn = false;
                                drawInteract("Cheats Deactivate");
                            }
                        }

                        if (cheatOn)
                        {
                            for (int i = 0; i < commands.Count; i++)
                            {
                                if (input.text.ToLower().Contains((comS + commands[i] + ckE).ToLower()))
                                {
                                   
                                    for (int j = 0; j < cList.Count; j++)
                                    {
                                        if (cList[j].name.Contains(commands[i].Substring(2)))
                                        {
                                          
                                            int para = cList[j].mi.GetParameters().Length;

                                            string[] t = input.text.Split(' ');

                                            if (t.Length - 1 == para)
                                            {
                                                
                                                List<object> o = new List<object>();

                                                for (int x = 1; x < t.Length; x++)
                                                {

                                                    int ii;
                                                    float ff;
                                                    bool bb;

                                                    if (int.TryParse(t[x], out ii)) o.Add(int.Parse(t[x]));
                                                    else if (float.TryParse(t[x], out ff)) o.Add(float.Parse(t[x]));
                                                    else if (bool.TryParse(t[x], out bb)) o.Add(bool.Parse(t[x]));
                                                    else o.Add(t[x]);

                                                }

                                                cList[j].mi.Invoke(cList[j].target, o.ToArray());
                                                drawInteract(commands[i].Substring(2) + "  Used");

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            drawInteract("Cheats not Active");
                        }

                        input.text = "";
                        input.Select();
                        input.ActivateInputField();
                    }
                }
            }
        }
        else
        {
            if (!fps.enabled) fps.enabled = true;
        }


        

    }
}
