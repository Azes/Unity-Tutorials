using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class createCubeMapFromCameraWizzard : ScriptableWizard
{
    public string selectedFolderPath = "";
    public string CubemapName;
    public int cubemapSize;
    public Camera cam;
    private int[] availableSizes = new int[] { 16, 32, 64, 128, 256, 512, 1024, 2048 };
    private int selectedSizeIndex = 0;

    private void OnWizardUpdate()
    {
        minSize = new Vector2(300, 170);
        maxSize = new Vector2 (300, 170);
        helpString = "Select Camera to create CubeMap";
        isValid = (cam != null);
    }

    private void OnWizardCreate()
    {
        Cubemap map = new Cubemap(cubemapSize, TextureFormat.ARGB32, false);
        cam.RenderToCubemap(map);
        var n = Path.Combine(selectedFolderPath, CubemapName + ".cubemap");
        n = n.Substring(Application.dataPath.Length - 6);
        AssetDatabase.CreateAsset(map, n);
    }

    [MenuItem("AzeS/EZ-CubeMap")]
    static void createMap()
    {
        DisplayWizard<createCubeMapFromCameraWizzard>("EZ-CubeMap", "Render");
    }

    void OnGUI()
    {
        EditorGUI.DrawRect(new Rect(0, 0, 300, 143), new Color32(22, 22, 22, 255));

        var h = new GUIStyle(GUI.skin.label);
        var h2 = new GUIStyle(EditorStyles.textField);
        h.richText = true;
        h.alignment = TextAnchor.MiddleCenter;
        h2.fontStyle = FontStyle.Bold;
        var c = new Color();
        ColorUtility.TryParseHtmlString("#03fcf0", out c);
        h2.normal.textColor = c;

        GUILayout.Label("<color=#03fcf0><b><u><size=26>EZ-CubeMap</size></u></b></color>", h);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField((selectedFolderPath == string.Empty ? "Cubemap save Path" : selectedFolderPath.ToString()), h2, GUILayout.Width(140));
        if (GUILayout.Button(new GUIContent("Browse", "Select a save Path"), GUILayout.Width(160)))
        {
            selectedFolderPath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Cubemap Name:", "That will obviously be the name of the cubemap"), h2);
        GUILayout.Space(30);
        CubemapName = EditorGUILayout.TextField(CubemapName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        var h1 = new GUIStyle(EditorStyles.foldoutHeader);
        h1.richText = true;
        h1.alignment = TextAnchor.MiddleCenter;
       
        cubemapSize = availableSizes[selectedSizeIndex];
        GUILayout.Label(new GUIContent("Cubemap Size: ", "The Resolution of the Cubemap"), h2);
        GUILayout.Space(40);
        selectedSizeIndex = EditorGUILayout.Popup(selectedSizeIndex, GetSizeOptions(), h1);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Camera:", "The Render Camera"), h2);
        GUILayout.Space(80);
        cam = EditorGUILayout.ObjectField("", cam, typeof(Camera), true, GUILayout.Width(160)) as Camera;
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        
        bool allselc = selectedFolderPath != string.Empty && CubemapName != string.Empty && cubemapSize != 0 && cam != null;

        var style = new GUIStyle(GUI.skin.box);
        style.hover.background = (!allselc ? MakeTex(10, 10, new Color(1f, 0f, 0f)) : MakeTex(10, 10, new Color(1, 1, 1, 0.5f)));
        style.onHover.background = (!allselc ? MakeTex(10, 10, new Color(1f, 0f, 0f)) : MakeTex(10, 10, new Color(1, 1, 1, 0.5f)));
        style.normal.background = (allselc ? MakeTex(10, 10, new Color(1f, 0f, 0f)) : MakeTex(10, 10, Color.white));
        style.hover.textColor = new Color(0f, 1f, 1f);
        style.normal.textColor = (!allselc  ? new Color(1f, .5f, .5f) : Color.white);

        if (GUILayout.Button("Render", style, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
        {
            if (selectedFolderPath != string.Empty)
            {
                if(CubemapName != string.Empty)
                {
                    if (cubemapSize >= 16)
                    {
                        if(cam != null)
                        {
                            OnWizardCreate();
                            if (EditorUtility.DisplayDialog("Succses", $"The Cubemap {CubemapName} wase create at the path {selectedFolderPath}", "end")) Close();
                        }
                        else EditorUtility.DisplayDialog("Error", "There is no Camera selected", "Ok");
                    }
                    else EditorUtility.DisplayDialog("Error", "The Resolution Size of the cube map is to small. the minimum value start  by 32p", "Ok");
                }
                else EditorUtility.DisplayDialog("Error", "You haven't chosen a cubemap name", "Ok");
            }
            else EditorUtility.DisplayDialog("Error", "There is no selected Asset Folder", "Ok");
        }

        Repaint();
    }
    private string[] GetSizeOptions()
    {
        string[] sizeOptions = new string[availableSizes.Length];
        for (int i = 0; i < availableSizes.Length; i++)
        {
            sizeOptions[i] = availableSizes[i].ToString();
        }
        return sizeOptions;
    }
    private Texture2D MakeTex(int width, int height, Color color)
    {
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        Texture2D backgroundTexture = new Texture2D(width, height);

        backgroundTexture.SetPixels(pixels);
        backgroundTexture.Apply();

        return backgroundTexture;
    }
}
