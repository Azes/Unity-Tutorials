using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AC : MonoBehaviour
{

    /*
     * Would you have wanted to write it all down, I don't think so
     * ;) 
     * written by AzeS 
     * v 1.2
     */
    EZ a;

    //Controller Var´s
    static bool A_Pressed, X_Pressed, B_Pressed, Y_Pressed, LB_Pressed, RB_Pressed, LT_Pressed, RT_Pressed, L3_Pressed, R3_Pressed, Up_Pressed, Down_Pressed, left_Pressed, right_Pressed, select_Pressed, start_Pressed;
    static bool A_Down, X_Down, B_Down, Y_Down, LB_Down, RB_Down, LT_Down, RT_Down, L3_Down, R3_Down, Up_Down, Down_Down, left_Down, right_Down, select_Down, start_Down;
    static bool A_up, X_up, B_up, Y_up, LB_up, RB_up, LT_up, RT_up, L3_up, R3_up, Up_up, Down_up, left_up, right_up, select_up, start_up;
    static float RT_trigger, LT_trigger;
    static Vector2 LS, RS, arrows;

    //Mouse Var´s
    static bool isMouseLeft, isMouseRight, isMouseMiddel, MouseLeft_Down, MouseRight_Down, MouseMiddel_Down, MouseLeft_Up, MouseRight_Up, MouseMiddel_Up;
    static Vector2 Mouse_Position,Mouse_Delta;
    static float Mouse_Wheel;

    //Keyboard
    static Keyboard aKey;


    public bool DebugMouse=false,DebugPosition=false, DebugDelta=false, DebugKeyboard = false ;

    private void Awake()
    {
        a = new EZ();
        a.Enable();
        a.Controller.A.performed += ctx => A_Pressed = true;
        a.Controller.A.canceled += ctx => A_Pressed = false;
        a.Controller.A.started += ctx => A_Down = true;
        a.Controller.A.canceled += ctx => A_up = true;

        a.Controller.X.performed += ctx => X_Pressed = true;
        a.Controller.X.canceled += ctx => X_Pressed = false;
        a.Controller.X.started += ctx => X_Down = true;
        a.Controller.X.canceled += ctx => X_up = true;

        a.Controller.B.performed += ctx => B_Pressed = true;
        a.Controller.B.canceled += ctx => B_Pressed = false;
        a.Controller.B.started += ctx => B_Down = true;
        a.Controller.B.canceled += ctx => B_up = true;

        a.Controller.Y.performed += ctx => Y_Pressed = true;
        a.Controller.Y.canceled += ctx => Y_Pressed = false;
        a.Controller.Y.started += ctx => Y_Down = true;
        a.Controller.Y.canceled += ctx => Y_up = true;

        a.Controller.LB.performed += ctx => LB_Pressed = true;
        a.Controller.LB.canceled += ctx => LB_Pressed = false;
        a.Controller.LB.started += ctx => LB_Down = true;
        a.Controller.LB.canceled += ctx => LB_up = true;
        a.Controller.L3.performed += ctx => L3_Pressed = true;
        a.Controller.L3.canceled += ctx => L3_Pressed = false;
        a.Controller.L3.started += ctx => L3_Down = true;
        a.Controller.L3.canceled += ctx => L3_up = true;
        a.Controller.LT.performed += ctx => LT_Pressed = true;
        a.Controller.LT.canceled += ctx => LT_Pressed = false;
        a.Controller.LT.started += ctx => LT_Down = true;
        a.Controller.LT.canceled += ctx => LT_up = true;

        a.Controller.RB.performed += ctx => RB_Pressed = true;
        a.Controller.RB.canceled += ctx => RB_Pressed = false;
        a.Controller.RB.started += ctx => RB_Down = true;
        a.Controller.RB.canceled += ctx => RB_up = true;
        a.Controller.RT.performed += ctx => RT_Pressed = true;
        a.Controller.RT.canceled += ctx => RT_Pressed = false;
        a.Controller.RT.started += ctx => RT_Down = true;
        a.Controller.RT.canceled += ctx => RT_up = true;
        a.Controller.R3.performed += ctx => R3_Pressed = true;
        a.Controller.R3.canceled += ctx => R3_Pressed = false;
        a.Controller.R3.started += ctx => R3_Down = true;
        a.Controller.R3.canceled += ctx => R3_up = true;

        a.Controller.Select.performed += ctx => select_Pressed = true;
        a.Controller.Select.canceled += ctx => select_Pressed = false;
        a.Controller.Select.started += ctx => select_Down = true;
        a.Controller.Select.canceled += ctx => select_up = true;

        a.Controller.Start.performed += ctx => start_Pressed = true;
        a.Controller.Start.canceled += ctx => start_Pressed = false;
        a.Controller.Start.started += ctx => start_Down = true;
        a.Controller.Start.canceled += ctx => start_up = true;

        a.Controller.Arrows.performed += ctx => Up_Pressed = ctx.ReadValue<Vector2>() == Vector2.up;
        a.Controller.Arrows.canceled += ctx => Up_Pressed = false;
        a.Controller.Arrows.started += ctx => Up_Down = ctx.ReadValue<Vector2>() == Vector2.up;
        a.Controller.Arrows.performed += ctx => Down_Pressed = ctx.ReadValue<Vector2>() == -Vector2.up;
        a.Controller.Arrows.canceled += ctx => Down_Pressed = false;
        a.Controller.Arrows.started += ctx => Down_Down = ctx.ReadValue<Vector2>() == -Vector2.up;
        a.Controller.Arrows.performed += ctx => right_Pressed = ctx.ReadValue<Vector2>() == Vector2.right;
        a.Controller.Arrows.canceled += ctx => right_Pressed = false;
        a.Controller.Arrows.started += ctx => right_Down = ctx.ReadValue<Vector2>() == Vector2.right;
        a.Controller.Arrows.performed += ctx => left_Pressed = ctx.ReadValue<Vector2>() == -Vector2.right;
        a.Controller.Arrows.canceled += ctx => left_Pressed = false;
        a.Controller.Arrows.started += ctx => left_Down = ctx.ReadValue<Vector2>() == -Vector2.right;

        a.Controller.RT.performed += ctx => RT_trigger = ctx.ReadValue<float>();
        a.Controller.LT.performed += ctx => LT_trigger = ctx.ReadValue<float>();
        a.Controller.RT.canceled += ctx => RT_trigger = 0;
        a.Controller.LT.canceled += ctx => LT_trigger = 0;

        a.Controller.RightStick.performed += ctx => RS = ctx.ReadValue<Vector2>();
        a.Controller.RightStick.canceled += ctx => RS = Vector2.zero;
        a.Controller.LeftStick.performed += ctx => LS = ctx.ReadValue<Vector2>();
        a.Controller.LeftStick.canceled += ctx => LS = Vector2.zero;

        a.Controller.Arrows.performed += ctx => arrows = ctx.ReadValue<Vector2>();
        a.Controller.Arrows.canceled += ctx => arrows = Vector2.zero;

        a.Mouse.LeftButton.performed += ctx => isMouseLeft = true;
        a.Mouse.LeftButton.canceled += ctx => isMouseLeft = false;
        a.Mouse.LeftButton.started += ctx => MouseLeft_Down = true;
        a.Mouse.LeftButton.canceled += ctx => MouseLeft_Up = true;

        a.Mouse.RightButton.performed += ctx => isMouseRight = true;
        a.Mouse.RightButton.canceled += ctx => isMouseRight = false;
        a.Mouse.RightButton.started += ctx => MouseRight_Down = true;
        a.Mouse.RightButton.canceled += ctx => MouseRight_Up = true;

        a.Mouse.MiddelButton.performed += ctx => isMouseMiddel = true;
        a.Mouse.MiddelButton.canceled += ctx => isMouseMiddel = false;
        a.Mouse.MiddelButton.started += ctx => MouseMiddel_Down = true;
        a.Mouse.MiddelButton.canceled += ctx => MouseMiddel_Up = true;

        a.Mouse.Position.performed += ctx => Mouse_Position = ctx.ReadValue<Vector2>();
        a.Mouse.Delta.performed += ctx => Mouse_Delta = ctx.ReadValue<Vector2>();

        a.Mouse.Wheel.performed += ctx => Mouse_Wheel = ctx.ReadValue<float>();
        a.Mouse.Wheel.canceled += ctx => Mouse_Wheel = 0;

        a.Keyboard.TastenEingabe.performed += ctx => aKey = Keyboard.current;
    }

    public static bool A_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = A_Down;
            A_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool A_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = A_up;
            A_up = false;
            return buff;
        }
        else return false;
    }
    public static bool A_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return A_Pressed;
        else return false;
    }
    public static bool X_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = X_Down;
            X_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool X_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = X_up;
            X_up = false;
            return buff;
        }
        else return false;
    }
    public static bool X_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return X_Pressed;
        else return false;
    }
    public static bool B_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = B_Down;
            B_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool B_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = B_up;
            B_up = false;
            return buff;
        }else return false;
    }
    public static bool B_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return B_Pressed;
        else return false;
    }
    public static bool Y_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = Y_Down;
            Y_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool Y_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = Y_up;
            Y_up = false;
            return buff;
        }
        else return false;
    }
    public static bool Y_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Y_Pressed;
        else return false;
    }
    public static bool RB_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = RB_Down;
            RB_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool RB_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = RB_up;
            RB_up = false;
            return buff;
        }
        else return false;
    }
    public static bool RB_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return RB_Pressed;
        else return false;
    }
    public static bool LB_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = LB_Down;
            LB_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool LB_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = LB_up;
            LB_up = false;
            return buff;
        }
        else return false;
    }
    public static bool LB_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return LB_Pressed;
        else return false;
    }
    public static bool LT_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = LT_Down;
            LT_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool LT_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = LT_up;
            LT_up = false;
            return buff;
        }
        else return false;
    }
    public static bool LT_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return LT_Pressed;
        else return false;
    }
    public static bool RT_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = RT_Down;
            RT_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool RT_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = RT_up;
            RT_up = false;
            return buff;
        }
        else return false;
    }
    public static bool RT_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return RT_Pressed;
        else return false;
    }
    public static bool R3_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = R3_Down;
            R3_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool R3_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = R3_up;
            R3_up = false;
            return buff;
        }
        else return false;
    }
    public static bool R3_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return R3_Pressed;
        else return false;
    }
    public static bool L3_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = L3_Down;
            L3_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool L3_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = L3_up;
            L3_up = false;
            return buff;
        }
        else return false;
    }
    public static bool L3_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return L3_Pressed;
        else return false;
    }
    public static bool Select_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = select_Down;
            select_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool Select_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = select_up;
            select_up = false;
            return buff;
        }
        else return false;
    }
    public static bool Select_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return select_Pressed;
        else return false;
    }
    public static bool Start_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = start_Down;
            start_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool Start_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = start_up;
            start_up = false;
            return buff;
        }
        else return false;
    }
    public static bool Start_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return start_Pressed;
        else return false;
    }
    public static float LT_RawValue(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return LT_trigger;
        else return 0;
    }
    public static float RT_RawValue(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return RT_trigger;
        else return 0;
    }
    public static float LT_Value(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(LT_trigger * 100f) / 100f;
        else return 0;
    }
    public static float RT_Value(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(RT_trigger * 100f) / 100f;
        else return 0;
    }
    public static float LT_RoundValue(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(LT_trigger * 10f) / 10f;
        else return 0;
    }
    public static float RT_RoundValue(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(RT_trigger * 10f) / 10f;
        else return 0;
    }
    public static Vector2 RightStick(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return RS;
        else return Vector2.zero;
    }
    public static Vector2 LeftStick(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return LS;
        else return Vector2.zero;
    }
    public static float RawXRight(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return RS.x;
        else return 0;
    }
    public static float RawYRight(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return RS.y;
        else return 0;
    }
    public static float RoundXRight(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(RS.x * 10f) / 10f;
        else return 0;
    }
    public static float RoundYRight(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(RS.y * 10f) / 10f;
        else return 0;
    }
    public static float XRight(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(RS.x * 100f) / 100f;
        else return 0;
    }
    public static float YRight(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(RS.y * 100f) / 100f;
        else return 0;
    }
    public static float RawXLeft(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
            return LS.x;
        else
            return 0; 
    }
    public static float RawYLeft(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return LS.y;
        else return 0;
    }
    public static float RoundXLeft(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(LS.x * 10f) / 10f;
        else return 0;
    }
    public static float RoundYLeft(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(LS.y * 10f) / 10f;
        else return 0;
    }
    public static float XLeft(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(LS.x * 100f) / 100f;
        else return 0;
    }
    public static float YLeft(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Mathf.Round(LS.y * 100f) / 100f;
        else return 0;
    }
    public static Vector2 Arrows(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return arrows;
        else return Vector2.zero;
    }
    public static bool Up_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = Up_Down;
            Up_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool Up_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = Up_up;
            Up_up = false;
            return buff;
        }
        else return false;
    }
    public static bool Up_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Up_Pressed;
        else return false;
    }
    public static bool Down_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = Down_Down;
            Down_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool Down_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = Down_up;
            Down_up = false;
            return buff;
        }
        else return false;
    }
    public static bool Down_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return Down_Pressed;
        else return false;
    }
    public static bool Right_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = right_Down;
            right_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool Right_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = right_up;
            right_up = false;
            return buff;
        }
        else return false;
    }
    public static bool Right_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return right_Pressed;
        else return false;
    }
    public static bool Left_ButtonDown(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = left_Down;
            left_Down = false;
            return buff;
        }
        else return false;
    }
    public static bool Left_ButtonUp(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1])
        {
            bool buff = left_up;
            left_up = false;
            return buff;
        }
        else return false;
    }
    public static bool Left_ButtonPressed(int index)
    {
        if (Gamepad.current == Gamepad.all[index - 1]) return left_Pressed;
        else return false;
    }

    /// <summary>
    /// Is mouse button pressed
    /// </summary>
    /// <param name="index">0 = left | 1 = right | 2 = middel</param>
    /// <returns></returns>
    public static bool isMouse(int index)
    {
        if (index == 0) return isMouseLeft;
        else if (index == 1) return isMouseRight;
        else if (index == 2) return isMouseMiddel;
        else return false;
    }
    /// <summary>
    /// is mouse button down
    /// </summary>
    /// <param name="index">0 = left | 1 = right | 2 = middel</param>
    /// <returns></returns>
    public static bool isMouseDown(int index)
    {
        if (index == 0) return MouseLeft_Down;
        else if (index == 1) return MouseRight_Down;
        else if (index == 2) return MouseMiddel_Down;
        else return false;
    }
    /// <summary>
    /// is mouse button up
    /// </summary>
    /// <param name="index">0 = left | 1 = right | 2 = middel</param>
    /// <returns></returns>
    public static bool isMouseUp(int index)
    {
        if (index == 0) return MouseLeft_Up;
        else if (index == 1) return MouseRight_Up;
        else if (index == 2) return MouseMiddel_Up;
        else return false;
    }

    public static Vector2 MousePosition()
    {
        return Mouse_Position;
    }
    public static Vector2 MouseDelta()
    {
        return Mouse_Delta;
    }

    /// <summary>
    /// return the horzontal delta 
    /// </summary>
    /// <returns></returns>
    public static float Mouse_X()
    {
        return Mouse_Delta.x;
    }

    /// <summary>
    /// return the vertical delta
    /// </summary>
    /// <returns></returns>
    public static float Mouse_Y()
    {
        return Mouse_Delta.y;
    }


    public static float MouseWheel()
    {
        return Mouse_Wheel;
    }

    public static float MouseWheelOneValue()
    {
        if (Mouse_Wheel > 0) return 1;
        else if (Mouse_Wheel < 0) return -1;
        else return 0;
    }


    /// <summary>
    /// check for Keyboard key
    /// </summary>
    /// <param name="key">UnityEngine.InputSystem.Key.W</param>
    /// <returns></returns>
    public static bool isKeyboard(Key key)
    {
        if (aKey != null)
        {
            foreach (KeyControl k in aKey.allKeys)
            {
                if (k.wasPressedThisFrame) if (k.keyCode == key) return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Get displayed name of key
    /// </summary>
    /// <returns></returns>
    public static string getKeyboard()
    {
        if (aKey != null)
        {
            foreach (KeyControl k in aKey.allKeys)
            {
                if (k.wasPressedThisFrame) return k.displayName;
            }
        }
        return "null";
    }


    private void Update()
    {
        if (DebugMouse)
        {
            if (isMouse(0)) Debug.Log("leftPressed");
            if (isMouse(1)) Debug.Log("rightPressed");
            if (isMouse(2)) Debug.Log("middelPressed");

            if (DebugPosition)
            {
                Debug.Log("Mouse X = "+ MousePosition().x + "  Mouse Y = "+ MousePosition().y);
            }

            if (DebugDelta)
            {
                Debug.Log("Mouse X = " + MouseDelta().x + "  Mouse Y = " + MouseDelta().y);
            }

            Debug.Log("Wheel Value" + MouseWheel());
        }

        if (DebugKeyboard)
        {
            Debug.Log("Key = " + getKeyboard());

            if(isKeyboard(Key.W)) Debug.Log("W gedrückt");
            
        }
    }



}

#if UNITY_EDITOR
[CustomEditor(typeof(AC))]
public class customInspecterEZ : Editor
{
    Texture2D icon;

    public override void OnInspectorGUI()
    {
        if(icon == null)
        {
            icon = AssetDatabase.LoadAssetAtPath("Assets/EZ-Inputs/Textures/icon.png", typeof(Texture2D)) as Texture2D;
        }

        GUI.DrawTexture(new Rect(0, 0, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight * 6), icon);
        EditorStyles.label.richText = true;
        EditorStyles.label.fontStyle = FontStyle.Bold;
        AC a = (AC)target;

        EditorGUI.DrawRect(new Rect(5, 5, 200, EditorGUIUtility.singleLineHeight * 5), new Color(1,1,1, 0.5f));

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(a.DebugMouse ? "<color=Black> Debug Mouse Inputs</color>" : "<color=grey> Debug Mouse Inputs</color>");
        if(GUILayout.Button(a.DebugMouse ? "Its Active" : "Not Active"))
        {
            a.DebugMouse = !a.DebugMouse;
        }
        EditorGUILayout.EndHorizontal();


        if (a.DebugMouse)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(a.DebugPosition ? "<color=Black> Debug Mouse Position</color>" : "<color=grey> Debug Mouse Position</color>");
            if (GUILayout.Button(a.DebugPosition ? "Its Active" : "Not Active"))
            {
                a.DebugPosition = !a.DebugPosition;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(a.DebugDelta ? "<color=Black> Debug Mouse Delta</color>" : "<color=grey> Debug Mouse Delta</color>");
            if (GUILayout.Button(a.DebugDelta ? "Its Active" : "Not Active"))
            {
                a.DebugDelta = !a.DebugDelta;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(a.DebugKeyboard ? "<color=black> Debug Keyboard Inputs</color>" : "<color=grey> Debug Keyborad Inputs</color>");
        if (GUILayout.Button(a.DebugKeyboard ? "Its Active" : "Not Active"))
        {
            a.DebugKeyboard = !a.DebugKeyboard;
        }
        EditorGUILayout.EndHorizontal();


       if(!a.DebugMouse) GUILayout.Space(60);

        // DrawDefaultInspector();
    }

}
#endif