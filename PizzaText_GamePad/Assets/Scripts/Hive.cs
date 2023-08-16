using UnityEngine;
using TMPro;


public class Hive : MonoBehaviour
{
    public enum Keyname
    {
        KeyA, KeyB, KeyC, KeyD, KeyE, KeyF, KeyG, KeyH, KeyI, KeyJ, KeyK, KeyL, KeyM, KeyN, KeyO, KeyP, KeyQ, KeyR, KeyS, KeyT, KeyU, KeyV, KeyW, KeyX, KeyY, KeyZ, KeySpace1, KeySpace2
    }

    public TMP_InputField inputField;
    public TextMeshProUGUI textField;
    public GameObject[] buttons;

    private const float moveThreshold = 25.0f;
    private const float defaultSelectionTime = 0.2f;

    private float lastSelectionTimeL = defaultSelectionTime;
    private float lastSelectionTimeR = defaultSelectionTime;

    private Vector2 moveL = Vector2.zero;
    private Vector2 moveR = Vector2.zero;

    private Color selectedColor = new Color(0.055f, 0.561f, 0.243f);
    private Color originalColor = new Color(0.1981132f, 0.1981132f, 0.1981132f);

    private Keyname selectedButtonL;
    private Keyname selectedButtonR;

    private float startTime = 0.0f;

    private int currentSentenceIndex = 0;
    private string[] sentences = {
        "the future is here",
        "i love sunshine",
        "more power to you",
        "give me a pen",
        "typing with trackball is easy"
    };


    private void Start()
    {
        selectedButtonL = Keyname.KeyT;
        selectedButtonR = Keyname.KeyE;
        SetButtonColor(buttons[(int)Keyname.KeyT], selectedColor);
        SetButtonColor(buttons[(int)Keyname.KeyE], selectedColor);
        textField.text = sentences[currentSentenceIndex];
        inputField.ActivateInputField();
    }


    public void Update()
    {
        // Update the selection cooldown
        lastSelectionTimeL -= Time.deltaTime;
        lastSelectionTimeR -= Time.deltaTime;

        moveL.x += Input.GetAxis("LeftJoystickHorizontal");
        moveL.y -= Input.GetAxis("LeftJoystickVertical");

        moveR.x += Input.GetAxis("RightJoystickHorizontal");
        moveR.y -= Input.GetAxis("RightJoystickVertical");

        if (lastSelectionTimeL <= 0.0f)
        {
            if (moveL != Vector2.zero)
            {
                float trackballAngle;
                GetTrackBallInfo(out trackballAngle, moveL);

                if (moveL.sqrMagnitude >= moveThreshold)
                {
                    switch (selectedButtonL)
                    {
                        case Keyname.KeyT:
                            SelectionfromT(trackballAngle);
                            break;
                        case Keyname.KeyN:
                            SelectionfromN(trackballAngle);
                            break;
                        case Keyname.KeyD:
                            SelectionfromD(trackballAngle);
                            break;
                        case Keyname.KeyH:
                            SelectionfromH(trackballAngle);
                            break;
                        case Keyname.KeyI:
                            SelectionfromI(trackballAngle);
                            break;
                        case Keyname.KeyC:
                            SelectionfromC(trackballAngle);
                            break;
                        case Keyname.KeyQ:
                            SelectionfromQ(trackballAngle);
                            break;
                        case Keyname.KeyJ:
                            SelectionfromJ(trackballAngle);
                            break;
                        case Keyname.KeyW:
                            SelectionfromW(trackballAngle);
                            break;
                        case Keyname.KeyP:
                            SelectionfromP(trackballAngle);
                            break;
                        case Keyname.KeyZ:
                            SelectionfromZ(trackballAngle);
                            break;
                        case Keyname.KeyY:
                            SelectionfromY(trackballAngle);
                            break;
                        case Keyname.KeySpace1:
                            SelectionfromSpace1(trackballAngle);
                            break;
                        case Keyname.KeyK:
                            SelectionfromK(trackballAngle);
                            break;
                    }

                }
            }

            lastSelectionTimeL = defaultSelectionTime;
            moveL = Vector2.zero;
        }

        if (lastSelectionTimeR <= 0.0f)
        {
            if (moveR != Vector2.zero)
            {
                float trackballAngle;
                GetTrackBallInfo(out trackballAngle, moveR);

                if (moveR.sqrMagnitude >= moveThreshold)
                {
                    switch (selectedButtonR)
                    {
                        case Keyname.KeyE:
                            SelectionfromE(trackballAngle);
                            break;
                        case Keyname.KeyO:
                            SelectionfromO(trackballAngle);
                            break;
                        case Keyname.KeyR:
                            SelectionfromR(trackballAngle);
                            break;
                        case Keyname.KeyS:
                            SelectionfromS(trackballAngle);
                            break;
                        case Keyname.KeyA:
                            SelectionfromA(trackballAngle);
                            break;
                        case Keyname.KeyL:
                            SelectionfromL(trackballAngle);
                            break;
                        case Keyname.KeyX:
                            SelectionfromX(trackballAngle);
                            break;
                        case Keyname.KeyB:
                            SelectionfromB(trackballAngle);
                            break;
                        case Keyname.KeyM:
                            SelectionfromM(trackballAngle);
                            break;
                        case Keyname.KeyG:
                            SelectionfromG(trackballAngle);
                            break;
                        case Keyname.KeySpace2:
                            SelectionfromSpace2(trackballAngle);
                            break;
                        case Keyname.KeyF:
                            SelectionfromF(trackballAngle);
                            break;
                        case Keyname.KeyU:
                            SelectionfromU(trackballAngle);
                            break;
                        case Keyname.KeyV:
                            SelectionfromV(trackballAngle);
                            break;

                    }

                }
            }

            lastSelectionTimeR = defaultSelectionTime;
            moveR = Vector2.zero;
        }
    }

    //    private void GetTrackBallInfo(out float sqrLength, out float angle, Vector2 move)
    private void GetTrackBallInfo(out float angle, Vector2 move)
    {
        //        float X = move.x;
        //        float Y = move.y;
        //        sqrLength = X * X + Y * Y;

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
            startTime = Time.time;  // Start the timer for text entry
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            TextMeshProUGUI buttonText = buttons[(int)selectedButtonL].GetComponentInChildren<TextMeshProUGUI>();
            string character = buttonText.text;
            inputField.text += character.ToString();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            TextMeshProUGUI buttonText = buttons[(int)selectedButtonR].GetComponentInChildren<TextMeshProUGUI>();
            string character = buttonText.text;
            inputField.text += character.ToString();
        }

        // Handle backspace key
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (inputField != null && T > 0)
            {
                inputField.text = inputField.text.Remove(T - 1);
            }
        }

        // Handle space key
        if (Input.GetKeyDown(KeyCode.F3))
        {
            inputField.text += ' ';
        }

        // Handle the "Enter" key press
        if (Input.GetKeyDown(KeyCode.F4))
        {
            EnterKeyFunctionality();

            //WPM Calculation
            //float endTime = Time.time;
            // Calculate the text entry speed for the current sentence
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

    private void SetButtonColor(GameObject button, Color color)
    {
        MeshRenderer[] renderers = button.GetComponents<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }




    // Selection for neighbours of T
    public void SelectionfromT(float angle)
    {
        if (angle > 20.0f && angle <= 90.0f)
        {
            selectedButtonL = Keyname.KeyH;
        }
        else if (angle > 90.0f && angle <= 160.0f)
        {
            selectedButtonL = Keyname.KeyD;
        }
        else if (angle > 160.0f && angle <= 215.0f)
        {
            selectedButtonL = Keyname.KeyN;
        }
        else if (angle > 215.0f && angle <= 315.0f)
        {
            selectedButtonL = Keyname.KeyC;
        }
        else //if ((angle > 0.0f &&  angle <= 20.0f) || (angle > 315.0f &&  angle <= 360.0f))
        {
            selectedButtonL = Keyname.KeyI;
        }
        SetButtonColor(buttons[(int)Keyname.KeyT], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of N
    public void SelectionfromN(float angle)
    {
        if (angle > 0.0f && angle <= 50.0f)
        {
            selectedButtonL = Keyname.KeyT;
        }
        else if (angle > 50.0f && angle <= 95.0f)
        {
            selectedButtonL = Keyname.KeyD;
        }
        else if (angle > 95.0f && angle <= 150.0f)
        {
            selectedButtonL = Keyname.KeyP;
        }
        else if (angle > 150.0f && angle <= 200.0f)
        {
            selectedButtonL = Keyname.KeyW;
        }
        else if (angle > 200.0f && angle <= 275.0f)
        {
            selectedButtonL = Keyname.KeyJ;
        }
        else// if (angle > 275.0f && angle <= 360.0f)
        {
            selectedButtonL = Keyname.KeyC;
        }
        SetButtonColor(buttons[(int)Keyname.KeyN], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of D
    public void SelectionfromD(float angle)
    {
        if (angle > 25.0f && angle <= 100.0f)
        {
            selectedButtonL = Keyname.KeyZ;
        }
        else if (angle > 100.0f && angle <= 160)
        {
            selectedButtonL = Keyname.KeyP;
        }
        else if (angle > 160.0f && angle <= 180)
        {
            selectedButtonL = Keyname.KeyW;
        }
        else if (angle > 180.0f && angle <= 270.0f)
        {
            selectedButtonL = Keyname.KeyN;
        }
        else if (angle > 270.0f && angle <= 340.0f)
        {
            selectedButtonL = Keyname.KeyT;
        }
        else//if ((angle > 0.0f && angle <= 25.0f) || (angle > 340.0f && angle <= 360.0f)) 
        {
            selectedButtonL = Keyname.KeyH;
        }
        SetButtonColor(buttons[(int)Keyname.KeyD], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of H
    public void SelectionfromH(float angle)
    {
        if (angle > 0.0f && angle <= 65.0f)
        {
            selectedButtonL = Keyname.KeyY;
        }
        else if (angle > 65.0f && angle <= 135.0f)
        {
            selectedButtonL = Keyname.KeyZ;
        }
        else if (angle > 135.0f && angle <= 205.0f)
        {
            selectedButtonL = Keyname.KeyD;
        }
        else if (angle > 205.0f && angle <= 270.0f)
        {
            selectedButtonL = Keyname.KeyT;
        }
        else if (angle > 270.0f && angle <= 330.0f)
        {
            selectedButtonL = Keyname.KeyI;
        }
        else// if (angle > 330.0f &&  angle <= 360.0f)
        {
            selectedButtonL = Keyname.KeySpace1;
        }
        SetButtonColor(buttons[(int)Keyname.KeyH], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of I
    public void SelectionfromI(float angle)
    {
        if (angle > 30.0f && angle <= 75.0f)
        {
            selectedButtonL = Keyname.KeyY;
        }
        else if (angle > 75.0f && angle <= 140.0f)
        {
            selectedButtonL = Keyname.KeyH;
        }
        else if (angle > 140.0f && angle <= 185.0f)
        {
            selectedButtonL = Keyname.KeyT;
        }
        else if (angle > 185.0f && angle <= 255.0f)
        {
            selectedButtonL = Keyname.KeyC;
        }
        else if (angle > 255.0f && angle <= 340.0f)
        {
            selectedButtonL = Keyname.KeyK;
        }
        else//if ((angle > 0.0f && angle <= 30.0f) ||  (angle > 340.0f && angle <= 360.0f))
        {
            selectedButtonL = Keyname.KeySpace1;
        }
        SetButtonColor(buttons[(int)Keyname.KeyI], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }


    // Selection for neighbours of C
    public void SelectionfromC(float angle)
    {
        if (angle > 0.0f && angle <= 60.0f)
        {
            selectedButtonL = Keyname.KeyI;
        }
        else if (angle > 60.0f && angle <= 120.0f)
        {
            selectedButtonL = Keyname.KeyT;
        }
        else if (angle > 120.0f && angle <= 180.0f)
        {
            selectedButtonL = Keyname.KeyN;
        }
        else if (angle > 180.0f && angle <= 220.0f)
        {
            selectedButtonL = Keyname.KeyJ;
        }
        else if (angle > 220.0f && angle <= 315.0f)
        {
            selectedButtonL = Keyname.KeyQ;
        }
        else// if (angle > 315.0f && angle <= 360.0f)
        {
            selectedButtonL = Keyname.KeyK;
        }
        SetButtonColor(buttons[(int)Keyname.KeyC], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of Q
    public void SelectionfromQ(float angle)
    {
        if (angle > 0.0f && angle <= 60.0f)
        {
            selectedButtonL = Keyname.KeyK;
        }
        else if (angle > 60.0f && angle <= 120.0f)
        {
            selectedButtonL = Keyname.KeyC;
        }
        else if (angle > 120.0f && angle <= 180.0f)
        {
            selectedButtonL = Keyname.KeyJ;
        }
        else if (angle > 180.0f && angle <= 230.0f)
        {
            selectedButtonL = Keyname.KeySpace1;
        }
        else if (angle > 230.0f && angle <= 300.0f)
        {
            selectedButtonL = Keyname.KeyZ;
        }
        else// if (angle > 300.0f && angle <= 360.0f)
        {
            selectedButtonL = Keyname.KeyW;
        }

        SetButtonColor(buttons[(int)Keyname.KeyQ], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of J
    public void SelectionfromJ(float angle)
    {
        if (angle > 0.0f && angle <= 35.0f)
        {
            selectedButtonL = Keyname.KeyC;
        }
        else if (angle > 35.0f && angle <= 90.0f)
        {
            selectedButtonL = Keyname.KeyN;
        }
        else if (angle > 90.0f && angle <= 180.0f)
        {
            selectedButtonL = Keyname.KeyW;
        }
        else if (angle > 180.0f && angle <= 270.0f)
        {
            selectedButtonL = Keyname.KeyY;
        }
        else// if (angle > 270.0f && angle <= 360.0f)
        {
            selectedButtonL = Keyname.KeyQ;
        }

        SetButtonColor(buttons[(int)Keyname.KeyJ], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of W
    public void SelectionfromW(float angle)
    {
        if (angle > 30.0f && angle <= 90.0f)
        {
            selectedButtonL = Keyname.KeyP;
        }
        else if (angle > 90.0f && angle <= 145.0f)
        {
            selectedButtonL = Keyname.KeyQ;
        }
        else if (angle > 145.0f && angle <= 200.0f)
        {
            selectedButtonL = Keyname.KeySpace1;
        }
        else if (angle > 200.0f && angle <= 270.0f)
        {
            selectedButtonL = Keyname.KeyZ;
        }
        else if (angle > 270.0f && angle <= 340.0f)
        {
            selectedButtonL = Keyname.KeyJ;
        }
        else
        {
            selectedButtonL = Keyname.KeyN;
        }

        SetButtonColor(buttons[(int)Keyname.KeyW], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of P
    public void SelectionfromP(float angle)
    {
        if (angle > 0.0f && angle <= 90.0f)
        {
            selectedButtonL = Keyname.KeyZ;
        }
        else if (angle > 90.0f && angle <= 180.0f)
        {
            selectedButtonL = Keyname.KeyK;
        }
        else if (angle > 180.0f && angle <= 240.0f)
        {
            selectedButtonL = Keyname.KeyW;
        }
        else if (angle > 240.0f && angle <= 300.0f)
        {
            selectedButtonL = Keyname.KeyN;
        }
        else// if (angle > 300.0f && angle <= 360.0f)
        {
            selectedButtonL = Keyname.KeyD;
        }
        SetButtonColor(buttons[(int)Keyname.KeyP], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of Z
    public void SelectionfromZ(float angle)
    {
        if (angle > 0.0f && angle <= 60.0f)
        {
            selectedButtonL = Keyname.KeyW;
        }
        else if (angle > 60.0f && angle <= 120.0f)
        {
            selectedButtonL = Keyname.KeyQ;
        }
        else if (angle > 120.0f && angle <= 180.0f)
        {
            selectedButtonL = Keyname.KeySpace1;
        }
        else if (angle > 180.0f && angle <= 230.0f)
        {
            selectedButtonL = Keyname.KeyP;
        }
        else if (angle > 230.0f && angle <= 270.0f)
        {
            selectedButtonL = Keyname.KeyD;
        }
        else if (angle > 270.0f && angle <= 310.0f)
        {
            selectedButtonL = Keyname.KeyH;
        }
        else
        {
            selectedButtonL = Keyname.KeyY;
        }

        SetButtonColor(buttons[(int)Keyname.KeyZ], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }


    // Selection for neighbours of Y
    public void SelectionfromY(float angle)
    {
        if (angle > 0.0f && angle <= 90.0f)
        {
            selectedButtonL = Keyname.KeyJ;
        }
        else if (angle > 90.0f && angle <= 180.0f)
        {
            selectedButtonL = Keyname.KeyZ;
        }
        else if (angle > 180.0f && angle <= 230.0f)
        {
            selectedButtonL = Keyname.KeyH;
        }
        else if (angle > 230.0f && angle <= 280.0f)
        {
            selectedButtonL = Keyname.KeyI;
        }
        else
        {
            selectedButtonL = Keyname.KeySpace1;
        }
        SetButtonColor(buttons[(int)Keyname.KeyY], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }


    // Selection for neighbours of Space1
    public void SelectionfromSpace1(float angle)
    {
        if (angle > 20.0f && angle <= 90.0f)
        {
            selectedButtonL = Keyname.KeyQ;
        }
        else if (angle > 90.0f && angle <= 140.0f)
        {
            selectedButtonL = Keyname.KeyY;
        }
        else if (angle > 140.0f && angle <= 165.0f)
        {
            selectedButtonL = Keyname.KeyH;
        }
        else if (angle > 165.0f && angle <= 210.0f)
        {
            selectedButtonL = Keyname.KeyI;
        }
        else if (angle > 210.0f && angle <= 270.0f)
        {
            selectedButtonL = Keyname.KeyK;
        }
        else if (angle > 270.0f && angle <= 340.0f)
        {
            selectedButtonL = Keyname.KeyZ;
        }
        else
        {
            selectedButtonL = Keyname.KeyW;
        }

        SetButtonColor(buttons[(int)Keyname.KeySpace1], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }

    // Selection for neighbours of K
    public void SelectionfromK(float angle)
    {
        if (angle > 0.0f && angle <= 90.0f)
        {
            selectedButtonL = Keyname.KeySpace1;
        }
        else if (angle > 90.0f && angle <= 150.0f)
        {
            selectedButtonL = Keyname.KeyI;
        }
        else if (angle > 150.0f && angle <= 190.0f)
        {
            selectedButtonL = Keyname.KeyC;
        }
        else if (angle > 190.0f && angle <= 270.0f)
        {
            selectedButtonL = Keyname.KeyQ;
        }
        else
        {
            selectedButtonL = Keyname.KeyP;
        }

        SetButtonColor(buttons[(int)Keyname.KeyK], originalColor);
        SetButtonColor(buttons[(int)selectedButtonL], selectedColor);
    }



























































    // Selection for neighbours of E
    public void SelectionfromE(float angle)
    {
        if (angle > 20.0f && angle <= 90.0f)
        {
            selectedButtonR = Keyname.KeyS;
        }
        else if (angle > 90.0f && angle <= 160.0f)
        {
            selectedButtonR = Keyname.KeyR;
        }
        else if (angle > 160.0f && angle <= 215.0f)
        {
            selectedButtonR = Keyname.KeyO;
        }
        else if (angle > 215.0f && angle <= 315.0f)
        {
            selectedButtonR = Keyname.KeyL;
        }
        else //if ((angle > 0.0f &&  angle <= 20.0f) || (angle > 315.0f &&  angle <= 360.0f))
        {
            selectedButtonR = Keyname.KeyA;
        }

        SetButtonColor(buttons[(int)Keyname.KeyE], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }


    // Selection for neighbours of O
    public void SelectionfromO(float angle)
    {
        if (angle > 0.0f && angle <= 50.0f)
        {
            selectedButtonR = Keyname.KeyE;
        }
        else if (angle > 50.0f && angle <= 95.0f)
        {
            selectedButtonR = Keyname.KeyR;
        }
        else if (angle > 95.0f && angle <= 150.0f)
        {
            selectedButtonR = Keyname.KeyG;
        }
        else if (angle > 150.0f && angle <= 200.0f)
        {
            selectedButtonR = Keyname.KeyM;
        }
        else if (angle > 200.0f && angle <= 275.0f)
        {
            selectedButtonR = Keyname.KeyB;
        }
        else// if (angle > 275.0f && angle <= 360.0f)
        {
            selectedButtonR = Keyname.KeyL;
        }
        SetButtonColor(buttons[(int)Keyname.KeyO], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of R
    public void SelectionfromR(float angle)
    {
        if (angle > 25.0f && angle <= 100.0f)
        {
            selectedButtonR = Keyname.KeySpace2;
        }
        else if (angle > 100.0f && angle <= 160)
        {
            selectedButtonR = Keyname.KeyG;
        }
        else if (angle > 160.0f && angle <= 180)
        {
            selectedButtonR = Keyname.KeyM;
        }
        else if (angle > 180.0f && angle <= 270.0f)
        {
            selectedButtonR = Keyname.KeyO;
        }
        else if (angle > 270.0f && angle <= 340.0f)
        {
            selectedButtonR = Keyname.KeyE;
        }
        else//if ((angle > 0.0f && angle <= 25.0f) || (angle > 340.0f && angle <= 360.0f)) 
        {
            selectedButtonR = Keyname.KeyS;
        }
        SetButtonColor(buttons[(int)Keyname.KeyR], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of S
    public void SelectionfromS(float angle)
    {
        if (angle > 0.0f && angle <= 65.0f)
        {
            selectedButtonR = Keyname.KeyF;
        }
        else if (angle > 65.0f && angle <= 135.0f)
        {
            selectedButtonR = Keyname.KeySpace2;
        }
        else if (angle > 135.0f && angle <= 205.0f)
        {
            selectedButtonR = Keyname.KeyR;
        }
        else if (angle > 205.0f && angle <= 270.0f)
        {
            selectedButtonR = Keyname.KeyE;
        }
        else if (angle > 270.0f && angle <= 330.0f)
        {
            selectedButtonR = Keyname.KeyA;
        }
        else// if (angle > 330.0f &&  angle <= 360.0f)
        {
            selectedButtonR = Keyname.KeyU;
        }
        SetButtonColor(buttons[(int)Keyname.KeyS], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of A
    public void SelectionfromA(float angle)
    {
        if (angle > 30.0f && angle <= 75.0f)
        {
            selectedButtonR = Keyname.KeyF;
        }
        else if (angle > 75.0f && angle <= 140.0f)
        {
            selectedButtonR = Keyname.KeyS;
        }
        else if (angle > 140.0f && angle <= 185.0f)
        {
            selectedButtonR = Keyname.KeyE;
        }
        else if (angle > 185.0f && angle <= 255.0f)
        {
            selectedButtonR = Keyname.KeyL;
        }
        else if (angle > 255.0f && angle <= 340.0f)
        {
            selectedButtonR = Keyname.KeyV;
        }
        else//if ((angle > 0.0f && angle <= 30.0f) ||  (angle > 340.0f && angle <= 360.0f))
        {
            selectedButtonR = Keyname.KeyU;
        }
        SetButtonColor(buttons[(int)Keyname.KeyA], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }


    // Selection for neighbours of L
    public void SelectionfromL(float angle)
    {
        if (angle > 0.0f && angle <= 60.0f)
        {
            selectedButtonR = Keyname.KeyA;
        }
        else if (angle > 60.0f && angle <= 120.0f)
        {
            selectedButtonR = Keyname.KeyE;
        }
        else if (angle > 120.0f && angle <= 180.0f)
        {
            selectedButtonR = Keyname.KeyO;
        }
        else if (angle > 180.0f && angle <= 220.0f)
        {
            selectedButtonR = Keyname.KeyB;
        }
        else if (angle > 220.0f && angle <= 315.0f)
        {
            selectedButtonR = Keyname.KeyX;
        }
        else// if (angle > 315.0f && angle <= 360.0f)
        {
            selectedButtonR = Keyname.KeyV;
        }
        SetButtonColor(buttons[(int)Keyname.KeyL], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of X
    public void SelectionfromX(float angle)
    {
        if (angle > 0.0f && angle <= 60.0f)
        {
            selectedButtonR = Keyname.KeyV;
        }
        else if (angle > 60.0f && angle <= 120.0f)
        {
            selectedButtonR = Keyname.KeyL;
        }
        else if (angle > 120.0f && angle <= 180.0f)
        {
            selectedButtonR = Keyname.KeyB;
        }
        else if (angle > 180.0f && angle <= 230.0f)
        {
            selectedButtonR = Keyname.KeyU;
        }
        else if (angle > 230.0f && angle <= 300.0f)
        {
            selectedButtonR = Keyname.KeySpace2;
        }
        else// if (angle > 300.0f && angle <= 360.0f)
        {
            selectedButtonR = Keyname.KeyM;
        }

        SetButtonColor(buttons[(int)Keyname.KeyX], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of B
    public void SelectionfromB(float angle)
    {
        if (angle > 0.0f && angle <= 35.0f)
        {
            selectedButtonR = Keyname.KeyL;
        }
        else if (angle > 35.0f && angle <= 90.0f)
        {
            selectedButtonR = Keyname.KeyO;
        }
        else if (angle > 90.0f && angle <= 180.0f)
        {
            selectedButtonR = Keyname.KeyM;
        }
        else if (angle > 180.0f && angle <= 270.0f)
        {
            selectedButtonR = Keyname.KeyF;
        }
        else// if (angle > 270.0f && angle <= 360.0f)
        {
            selectedButtonR = Keyname.KeyX;
        }

        SetButtonColor(buttons[(int)Keyname.KeyB], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of M
    public void SelectionfromM(float angle)
    {
        if (angle > 30.0f && angle <= 90.0f)
        {
            selectedButtonR = Keyname.KeyG;
        }
        else if (angle > 90.0f && angle <= 145.0f)
        {
            selectedButtonR = Keyname.KeyX;
        }
        else if (angle > 145.0f && angle <= 200.0f)
        {
            selectedButtonR = Keyname.KeyU;
        }
        else if (angle > 200.0f && angle <= 270.0f)
        {
            selectedButtonR = Keyname.KeySpace2;
        }
        else if (angle > 270.0f && angle <= 340.0f)
        {
            selectedButtonR = Keyname.KeyB;
        }
        else
        {
            selectedButtonR = Keyname.KeyO;
        }

        SetButtonColor(buttons[(int)Keyname.KeyM], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of G
    public void SelectionfromG(float angle)
    {
        if (angle > 0.0f && angle <= 90.0f)
        {
            selectedButtonR = Keyname.KeySpace2;
        }
        else if (angle > 90.0f && angle <= 180.0f)
        {
            selectedButtonR = Keyname.KeyV;
        }
        else if (angle > 180.0f && angle <= 240.0f)
        {
            selectedButtonR = Keyname.KeyM;
        }
        else if (angle > 240.0f && angle <= 300.0f)
        {
            selectedButtonR = Keyname.KeyO;
        }
        else// if (angle > 300.0f && angle <= 360.0f)
        {
            selectedButtonR = Keyname.KeyR;
        }
        SetButtonColor(buttons[(int)Keyname.KeyG], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of Space2
    public void SelectionfromSpace2(float angle)
    {
        if (angle > 0.0f && angle <= 60.0f)
        {
            selectedButtonR = Keyname.KeyM;
        }
        else if (angle > 60.0f && angle <= 120.0f)
        {
            selectedButtonR = Keyname.KeyX;
        }
        else if (angle > 120.0f && angle <= 180.0f)
        {
            selectedButtonR = Keyname.KeyU;
        }
        else if (angle > 180.0f && angle <= 230.0f)
        {
            selectedButtonR = Keyname.KeyG;
        }
        else if (angle > 230.0f && angle <= 270.0f)
        {
            selectedButtonR = Keyname.KeyR;
        }
        else if (angle > 270.0f && angle <= 310.0f)
        {
            selectedButtonR = Keyname.KeyS;
        }
        else
        {
            selectedButtonR = Keyname.KeyF;
        }

        SetButtonColor(buttons[(int)Keyname.KeySpace2], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of F
    public void SelectionfromF(float angle)
    {
        if (angle > 0.0f && angle <= 90.0f)
        {
            selectedButtonR = Keyname.KeyB;
        }
        else if (angle > 90.0f && angle <= 180.0f)
        {
            selectedButtonR = Keyname.KeySpace2;
        }
        else if (angle > 180.0f && angle <= 230.0f)
        {
            selectedButtonR = Keyname.KeyS;
        }
        else if (angle > 230.0f && angle <= 280.0f)
        {
            selectedButtonR = Keyname.KeyA;
        }
        else
        {
            selectedButtonR = Keyname.KeyU;
        }
        SetButtonColor(buttons[(int)Keyname.KeyF], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of U
    public void SelectionfromU(float angle)
    {
        if (angle > 20.0f && angle <= 90.0f)
        {
            selectedButtonR = Keyname.KeyX;
        }
        else if (angle > 90.0f && angle <= 140.0f)
        {
            selectedButtonR = Keyname.KeyF;
        }
        else if (angle > 140.0f && angle <= 165.0f)
        {
            selectedButtonR = Keyname.KeyS;
        }
        else if (angle > 165.0f && angle <= 210.0f)
        {
            selectedButtonR = Keyname.KeyA;
        }
        else if (angle > 210.0f && angle <= 270.0f)
        {
            selectedButtonR = Keyname.KeyV;
        }
        else if (angle > 270.0f && angle <= 340.0f)
        {
            selectedButtonR = Keyname.KeySpace2;
        }
        else
        {
            selectedButtonR = Keyname.KeyM;
        }

        SetButtonColor(buttons[(int)Keyname.KeyU], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }

    // Selection for neighbours of V
    public void SelectionfromV(float angle)
    {
        if (angle > 0.0f && angle <= 90.0f)
        {
            selectedButtonR = Keyname.KeyU;
        }
        else if (angle > 90.0f && angle <= 150.0f)
        {
            selectedButtonR = Keyname.KeyA;
        }
        else if (angle > 150.0f && angle <= 190.0f)
        {
            selectedButtonR = Keyname.KeyL;
        }
        else if (angle > 190.0f && angle <= 270.0f)
        {
            selectedButtonR = Keyname.KeyX;
        }
        else
        {
            selectedButtonR = Keyname.KeyG;
        }

        SetButtonColor(buttons[(int)Keyname.KeyV], originalColor);
        SetButtonColor(buttons[(int)selectedButtonR], selectedColor);
    }


}
