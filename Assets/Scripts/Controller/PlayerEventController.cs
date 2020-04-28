using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerEventController : MonoBehaviour
{
    public GameObject playerObject;

    //public DebugUI debugUi;
    public Vector3 movingStartPosition { get; set; } = Vector3.zero;
    public Vector3 movingEndPosition { get; set; } = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EventAction();
    }

    #region Handler
    void EventAction()
    {
        OnMove(playerObject);
    }
    float moveCoef = 1.5f;
    void OnMove(GameObject moveObject)
    {
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickUp))
        {
            moveObject.transform.position += 
                new Vector3(
                    transform.forward.x * Time.deltaTime * OVRInput
                    .Get(OVRInput.Axis2D.PrimaryThumbstick).y * moveCoef,
                    0,
                    transform.forward.z * Time.deltaTime * OVRInput
                    .Get(OVRInput.Axis2D.PrimaryThumbstick).y * moveCoef
                    );
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickDown))
        {
            moveObject.transform.position +=
                new Vector3(
                    transform.forward.x * Time.deltaTime * OVRInput
                    .Get(OVRInput.Axis2D.PrimaryThumbstick).y * moveCoef,
                    0,
                    transform.forward.z * Time.deltaTime * OVRInput
                    .Get(OVRInput.Axis2D.PrimaryThumbstick).y * moveCoef);
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft))
        {
            moveObject.transform.Rotate(0, 50 * Time.deltaTime * OVRInput
                    .Get(OVRInput.Axis2D.PrimaryThumbstick).x * moveCoef, 0);
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight))
        {
            moveObject.transform.Rotate(0, 50 * Time.deltaTime * OVRInput
                    .Get(OVRInput.Axis2D.PrimaryThumbstick).x * moveCoef, 0, 0);
        }
    }
    #endregion
    string CheckAndOutputText(OVRInput.Button data)
    {
        string result = "";
        if (OVRInput.Get(data))
        {
            result += "<color=#ff0000>";
            if (OVRInput.Get(OVRInput.RawButton.LThumbstick)
                || OVRInput.Get(OVRInput.RawButton.LTouchpad)
                || OVRInput.Get(OVRInput.RawButton.LHandTrigger)
                || OVRInput.Get(OVRInput.RawButton.LShoulder)
                || OVRInput.Get(OVRInput.RawButton.LIndexTrigger)
                || OVRInput.Get(OVRInput.RawButton.LThumbstickDown)
                || OVRInput.Get(OVRInput.RawButton.LThumbstickUp)
                || OVRInput.Get(OVRInput.RawButton.LThumbstickLeft)
                || OVRInput.Get(OVRInput.RawButton.LThumbstickRight)
                || OVRInput.Get(OVRInput.RawButton.X)
                || OVRInput.Get(OVRInput.RawButton.Y))
            {
                result += "[LEFT]";
            }else if (OVRInput.Get(OVRInput.RawButton.RThumbstick)
                || OVRInput.Get(OVRInput.RawButton.RTouchpad)
                || OVRInput.Get(OVRInput.RawButton.RHandTrigger)
                || OVRInput.Get(OVRInput.RawButton.RShoulder)
                || OVRInput.Get(OVRInput.RawButton.RShoulder)
                || OVRInput.Get(OVRInput.RawButton.RIndexTrigger)
                || OVRInput.Get(OVRInput.RawButton.RThumbstickDown)
                || OVRInput.Get(OVRInput.RawButton.RThumbstickUp)
                || OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)
                || OVRInput.Get(OVRInput.RawButton.RThumbstickRight)
                || OVRInput.Get(OVRInput.RawButton.A)
                || OVRInput.Get(OVRInput.RawButton.B))
            {
                result += "[RIGHT]";
            }
            else
            {
                result += "[]";
            }
            result += data.ToString() + "：" + OVRInput.Get(data).ToString() + "\n";
            result += "</color>";
        }
        return result;
    }
    string CheckAnalog1DAndOutputText(OVRInput.Axis1D data)
    {
        string result = "";

        if (OVRInput.Get(data) > 0)
        {
            result += "<color=#ff0000>";
        }
        else
        {
            result += "<color=#ffffff>";
        }
        result += data.ToString() + "：" + OVRInput.Get(data).ToString() + "\n";
        result += "</color>";
        return result;
    }
    string CheckAnalog2DAndOutputText(OVRInput.Axis2D data)
    {
        string result = "";

        if (OVRInput.Get(data) != Vector2.zero)
        {
            result += "<color=#ff0000>";
        }
        else
        {
            result += "<color=#ffffff>";
        }
        result += data.ToString() + "：" + OVRInput.Get(data).ToString() + "\n";
        result += "</color>";
        return result;
    }

    string CheckNearTouchAndOutputText(OVRInput.NearTouch data)
    {
        string result = "";

        if (OVRInput.Get(data))
        {
            result += "<color=#ff0000>";
            result += data.ToString() + "：" + OVRInput.Get(data).ToString() + "\n";
            result += "</color>";
        }
        return result;
    }

    string CheckTouchAndOutputText(OVRInput.Touch data)
    {
        string result = "";

        if (OVRInput.Get(data))
        {
            result += "<color=#ff0000>";
            result += data.ToString() + "：" + OVRInput.Get(data).ToString() + "\n";
            result += "</color>";
        }
        return result;
    }

    string CheckTouch()
    {
        string result = "Touch Check\n";
        result += CheckTouchAndOutputText(OVRInput.Touch.Any);
        result += CheckTouchAndOutputText(OVRInput.Touch.Four);
        result += CheckTouchAndOutputText(OVRInput.Touch.None);
        result += CheckTouchAndOutputText(OVRInput.Touch.One);
        result += CheckTouchAndOutputText(OVRInput.Touch.PrimaryIndexTrigger);
        result += CheckTouchAndOutputText(OVRInput.Touch.PrimaryThumbRest);
        result += CheckTouchAndOutputText(OVRInput.Touch.PrimaryThumbstick);
        result += CheckTouchAndOutputText(OVRInput.Touch.PrimaryTouchpad);
        result += CheckTouchAndOutputText(OVRInput.Touch.SecondaryIndexTrigger);
        result += CheckTouchAndOutputText(OVRInput.Touch.SecondaryThumbRest);
        result += CheckTouchAndOutputText(OVRInput.Touch.SecondaryThumbstick);
        result += CheckTouchAndOutputText(OVRInput.Touch.SecondaryTouchpad);
        result += CheckTouchAndOutputText(OVRInput.Touch.Three);
        result += CheckTouchAndOutputText(OVRInput.Touch.Two);
        return result;
    }


    string CheckNearTouch()
    {
        string result = "NearTouch Check\n";
        result += CheckNearTouchAndOutputText(OVRInput.NearTouch.Any);
        result += CheckNearTouchAndOutputText(OVRInput.NearTouch.None);
        result += CheckNearTouchAndOutputText(OVRInput.NearTouch.PrimaryIndexTrigger);
        result += CheckNearTouchAndOutputText(OVRInput.NearTouch.PrimaryThumbButtons);
        result += CheckNearTouchAndOutputText(OVRInput.NearTouch.SecondaryIndexTrigger);
        result += CheckNearTouchAndOutputText(OVRInput.NearTouch.SecondaryThumbButtons);
        return result;
    }

    string CheckAnalog2D()
    {
        string result = "Axis2D Check\n";
        result += CheckAnalog2DAndOutputText(OVRInput.Axis2D.Any);
        result += CheckAnalog2DAndOutputText(OVRInput.Axis2D.None);
        result += CheckAnalog2DAndOutputText(OVRInput.Axis2D.PrimaryThumbstick);
        result += CheckAnalog2DAndOutputText(OVRInput.Axis2D.PrimaryTouchpad);
        result += CheckAnalog2DAndOutputText(OVRInput.Axis2D.SecondaryThumbstick);
        result += CheckAnalog2DAndOutputText(OVRInput.Axis2D.SecondaryTouchpad);
        return result;
    }

    string CheckAnalog1D()
    {
        string result = "Axis1D Check\n";
        result += CheckAnalog1DAndOutputText(OVRInput.Axis1D.Any);
        result += CheckAnalog1DAndOutputText(OVRInput.Axis1D.None);
        result += CheckAnalog1DAndOutputText(OVRInput.Axis1D.PrimaryHandTrigger);
        result += CheckAnalog1DAndOutputText(OVRInput.Axis1D.PrimaryIndexTrigger);
        result += CheckAnalog1DAndOutputText(OVRInput.Axis1D.SecondaryHandTrigger);
        result += CheckAnalog1DAndOutputText(OVRInput.Axis1D.SecondaryIndexTrigger);
        return result;
    }
    string CheckInput()
    {
        string result = "Button Check\n";
        result += CheckAndOutputText(OVRInput.Button.Any);
        result += CheckAndOutputText(OVRInput.Button.Back);
        result += CheckAndOutputText(OVRInput.Button.Down);
        result += CheckAndOutputText(OVRInput.Button.DpadDown);
        result += CheckAndOutputText(OVRInput.Button.DpadLeft);
        result += CheckAndOutputText(OVRInput.Button.DpadRight);
        result += CheckAndOutputText(OVRInput.Button.DpadUp);
        result += CheckAndOutputText(OVRInput.Button.Four);
        result += CheckAndOutputText(OVRInput.Button.Left);
        result += CheckAndOutputText(OVRInput.Button.None);
        result += CheckAndOutputText(OVRInput.Button.One);
        result += CheckAndOutputText(OVRInput.Button.PrimaryHandTrigger);
        result += CheckAndOutputText(OVRInput.Button.PrimaryIndexTrigger);
        result += CheckAndOutputText(OVRInput.Button.PrimaryShoulder);
        result += CheckAndOutputText(OVRInput.Button.PrimaryThumbstick);
        result += CheckAndOutputText(OVRInput.Button.PrimaryThumbstickDown);
        result += CheckAndOutputText(OVRInput.Button.PrimaryThumbstickLeft);
        result += CheckAndOutputText(OVRInput.Button.PrimaryThumbstickRight);
        result += CheckAndOutputText(OVRInput.Button.PrimaryThumbstickUp);
        result += CheckAndOutputText(OVRInput.Button.PrimaryTouchpad);
        result += CheckAndOutputText(OVRInput.Button.Right);
        result += CheckAndOutputText(OVRInput.Button.SecondaryHandTrigger);
        result += CheckAndOutputText(OVRInput.Button.SecondaryIndexTrigger);
        result += CheckAndOutputText(OVRInput.Button.SecondaryShoulder);
        result += CheckAndOutputText(OVRInput.Button.SecondaryThumbstick);
        result += CheckAndOutputText(OVRInput.Button.SecondaryThumbstickDown);
        result += CheckAndOutputText(OVRInput.Button.SecondaryThumbstickLeft);
        result += CheckAndOutputText(OVRInput.Button.SecondaryThumbstickRight);
        result += CheckAndOutputText(OVRInput.Button.SecondaryThumbstickUp);
        result += CheckAndOutputText(OVRInput.Button.SecondaryTouchpad);
        result += CheckAndOutputText(OVRInput.Button.Start);
        result += CheckAndOutputText(OVRInput.Button.Three);
        result += CheckAndOutputText(OVRInput.Button.Two);
        result += CheckAndOutputText(OVRInput.Button.Up);
        return result;
    }
    //#endregion
}
