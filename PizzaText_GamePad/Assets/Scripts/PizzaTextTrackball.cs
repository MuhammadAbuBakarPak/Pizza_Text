using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Collections.Concurrent;
using MultiInput.Internal.Platforms.Windows;
using MultiInput.Internal.Platforms.Windows.PInvokeNet;

public class PizzaTextTrackball : MonoBehaviour
{
    private MyWMListener listener;

    public enum Keyname
    {
        ABCD, EFGH, IJKL, MNOP, QRST, UVWX, YZ
    }

    public TMP_InputField inputField;
    public TextMeshProUGUI textField;
    public GameObject[] buttons;

    private Dictionary<Keyname, ArrayList> charMap = new Dictionary<Keyname, ArrayList>();

    private readonly ConcurrentQueue<RawInput> inputQueue = new ConcurrentQueue<RawInput>();

    private const int leftTrackballDeviceID = 389154161;
    private const int rightTrackballDeviceID = -25489911;

    private const float moveThreshold = 800.0f;
    private const float defaultSelectionTime = 0.15f;


    private float lastSelectionTimeL = defaultSelectionTime;
    private float lastSelectionTimeR = defaultSelectionTime;

    private Vector2 moveL = Vector2.zero;
    private Vector2 moveR = Vector2.zero;

    private Color selectedColor = new Color(0.02f, 0.8f, 0.07f);
    private Color originalColor1 = new Color(0.2118191f, 0.3773585f, 0.3728844f);
    private Color originalColor2 = new Color(0.2945665f, 0.2675329f, 0.3396226f);
    private Color originalColor3 = new Color(0.1499644f, 0.1294945f, 0.2830189f);

    private Keyname selectedButton;


    private float startTime = 0.0f;

    private int currentSentenceIndex = 0;
    private string[] sentences = {
        "the future is here",
        "i love sunshine",
        "more power to you",
        "give me a pen",
        "typing with trackball is easy"
    };

    private void Awake()
    {
        listener = new MyWMListener(OnInput, OnDeviceAdded, OnDeviceRemoved);
    }

    private bool OnInput(RawInput input)
    {
        inputQueue.Enqueue(input);
        return true;
    }

    private void OnDeviceAdded(RawInputDevicesListItem device)
    {
    }

    private void OnDeviceRemoved(RawInputDevicesListItem device)
    {
    }


    private void Start()
    {
        selectedButton = Keyname.ABCD;
        SetButtonColor(buttons[(int)Keyname.ABCD], selectedColor);

        textField.text = sentences[currentSentenceIndex];
        inputField.ActivateInputField();

        charMap.Add(Keyname.ABCD, new ArrayList() { 'a', 'b', 'c', 'd' });
        charMap.Add(Keyname.EFGH, new ArrayList() { 'e', 'f', 'g', 'h' });
        charMap.Add(Keyname.IJKL, new ArrayList() { 'i', 'j', 'k', 'l' });
        charMap.Add(Keyname.MNOP, new ArrayList() { 'm', 'n', 'o', 'p' });
        charMap.Add(Keyname.QRST, new ArrayList() { 'q', 'r', 's', 't' });
        charMap.Add(Keyname.UVWX, new ArrayList() { 'u', 'v', 'w', 'x' });
        charMap.Add(Keyname.YZ, new ArrayList() { 'y', 'z', ' ', ' ' });
    }


    public void Update()
    {
        // Update the selection cooldown
        lastSelectionTimeL -= Time.deltaTime;
        lastSelectionTimeR -= Time.deltaTime;


        while (inputQueue.TryDequeue(out var val))
        {
            if (val.Header.Type == RawInputType.Mouse && val.Header.Device.ToInt32() == leftTrackballDeviceID)
            {
                moveL.x += val.Data.Mouse.LastX;
                moveL.y -= val.Data.Mouse.LastY;
            }
            else if (val.Header.Type == RawInputType.Mouse && val.Header.Device.ToInt32() == rightTrackballDeviceID)
            {
                moveR.x += val.Data.Mouse.LastX;
                moveR.y -= val.Data.Mouse.LastY;
            }
        }

        if (moveL != Vector2.zero || moveR != Vector2.zero)
        {
            if (moveL != Vector2.zero && lastSelectionTimeL <= 0.0f)
            {
                float trackballAngle;
                GetTrackBallInfo(out trackballAngle, moveL);

                if (moveL.sqrMagnitude >= moveThreshold)
                {
                    SelectSlice(trackballAngle);
                }

                lastSelectionTimeL = defaultSelectionTime;
                moveL = Vector2.zero;
            }

            if (moveR != Vector2.zero && lastSelectionTimeR <= 0.0f)
            {
                float trackballAngle;
                GetTrackBallInfo(out trackballAngle, moveR);

                if (moveR.sqrMagnitude >= moveThreshold)
                {
                    WriteCharacter(ref selectedButton, trackballAngle);
                }
                lastSelectionTimeR = defaultSelectionTime;
                moveR = Vector2.zero;
            }
        }
    }




    private void GetTrackBallInfo(out float angle, Vector2 move)
    {
        angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;
    }

    private void LateUpdate()
    {
        ProcessKeyPress();
        inputField.MoveToEndOfLine(false, false);
    }

    private void ProcessKeyPress()
    {
        int T = inputField.text.Length;
        if (Input.anyKeyDown && T == 0 && startTime == 0.0f)
        {
            startTime = Time.time;
        }

        // Handle backspace key
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (inputField != null && T > 0)
            {
                inputField.text = inputField.text.Remove(T - 1);
            }
        }

        // Handle the "Enter" key press
        if (Input.GetKeyDown(KeyCode.F4))
        {
            EnterKeyFunctionality();

            float elapsedTime = Time.time - startTime;
            float wordsPerMinute = (T - 1) / elapsedTime * 60.0f * 0.2f;
            Debug.LogFormat("Text Entry Speed (Sentence {0}): {1} WPM", currentSentenceIndex, wordsPerMinute);

            // Reset start time
            startTime = 0.0f;
        }

    }

    //Perform Enter Key Functionality
    private void EnterKeyFunctionality()
    {
        if (textField != null)
        {
            currentSentenceIndex++;
            // Update the text field with the next sentence
            if (currentSentenceIndex < sentences.Length)
            {
                textField.text = sentences[currentSentenceIndex];
                inputField.text = string.Empty;     // Clear the input field
            }
            else
            {
                // Reset sentence index and display a message
                currentSentenceIndex = 0;
                textField.text = "Done";
            }
        }
    }

    private void WriteCharacter(ref Keyname button, float angle)
    {
        ArrayList charArray = charMap[button];

        if (charArray != null)
        {
            if (angle > 45.0f && angle <= 135.0f)
            {
                inputField.text += charArray[0].ToString();
            }
            else if (angle > 135.0f && angle <= 225.0f)
            {
                inputField.text += charArray[2].ToString();
            }
            else if (angle > 225.0f && angle <= 315.0f)
            {
                inputField.text += charArray[1].ToString();
            }
            else //if ((angle > 0.0f && angle <= 45.0f) || (angle > 315.0f && angle <= 360.0f))
            {
                inputField.text += charArray[3].ToString();
            }
        }
    }



    private void SetButtonColor(GameObject button, Color color)
    {
        MeshRenderer[] renderers = button.GetComponents<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }

    private void SelectSlice(float angle)
    {
        if (angle > 12.86f && angle <= 64.29f)
        {
            selectedButton = Keyname.EFGH;
        }
        else if (angle > 64.29f && angle <= 115.72f)
        {
            selectedButton = Keyname.ABCD;
        }
        else if (angle > 115.72f && angle <= 167.15f)
        {
            selectedButton = Keyname.YZ;
        }
        else if (angle > 167.15f && angle <= 218.58f)
        {
            selectedButton = Keyname.UVWX;
        }
        else if (angle > 218.58f && angle <= 270.01f)
        {
            selectedButton = Keyname.QRST;
        }
        else if (angle > 270.01f && angle <= 321.44f)
        {
            selectedButton = Keyname.MNOP;
        }
        else //if ((angle > 0.0f &&  angle <= 12.86f) || (angle > 321.44f &&  angle <= 360.0f))
        {
            selectedButton = Keyname.IJKL;
        }

        SetButtonColor(buttons[(int)Keyname.ABCD], originalColor1);
        SetButtonColor(buttons[(int)Keyname.EFGH], originalColor2);
        SetButtonColor(buttons[(int)Keyname.IJKL], originalColor3);
        SetButtonColor(buttons[(int)Keyname.MNOP], originalColor2);
        SetButtonColor(buttons[(int)Keyname.QRST], originalColor3);
        SetButtonColor(buttons[(int)Keyname.UVWX], originalColor2);
        SetButtonColor(buttons[(int)Keyname.YZ], originalColor3);

        SetButtonColor(buttons[(int)selectedButton], selectedColor);
    }


    private void OnDestroy()
    {
        if (listener != null)
        {
            listener.Dispose();
            listener = null;
        }
    }


}
