---
title: Unity - Cheat code
tags: unity
shortdesc: Unity cheat code handler
---

Script that will detect key typed with keyboard and execute a given method is something is recognized.

[See on Gist](https://gist.github.com/valryon/9910583)

```csharp
// Copyright © 2014 Pixelnest Studio
// This file is subject to the terms and conditions defined in
// file 'LICENSE.md', which is part of this source code package.
using UnityEngine;

namespace Pixelnest
{
  /// <summary>
  /// Trigger an event is a cheat code is recognized
  /// </summary>
  /// <remarks>Based on http://blog.remibodin.fr/unity3d-konami-code/ </remarks>
  public class CheatCode : MonoBehaviour
  {
    /// <summary>
    /// Game object contacted if the code is recognized
    /// </summary>
    public GameObject receiver;
    public string message;

    /// <summary>
    /// Time allowed between two valid key code press
    /// </summary>
    public float timeBetweenKeyPress = 1f;

    /// <summary>
    /// Time allowed to input the whole code
    /// </summary>
    public float timeForCode = 5f;

    public string codeName = "Konami Code";

    public KeyCode[] code = new KeyCode[]
                              {
                                KeyCode.UpArrow,
                                KeyCode.UpArrow,
                                KeyCode.DownArrow,
                                KeyCode.DownArrow,
                                KeyCode.LeftArrow,
                                KeyCode.RightArrow,
                                KeyCode.LeftArrow,
                                KeyCode.RightArrow,
                                KeyCode.B,
                                KeyCode.A
                              };

    private float timeSinceStartCode = 0f, timeSinceLastKey = 0f;
    private int index = 0;
    private Vector2 previousPadInput;

    void Update()
    {
      this.timeSinceLastKey += Time.deltaTime;
      this.timeSinceStartCode += Time.deltaTime;

      // Reset
      if (this.timeSinceStartCode >= this.timeForCode || this.timeSinceLastKey >= this.timeBetweenKeyPress)
      {
        this.index = 0;
      }

      KeyCode? currentKey = null;

      // The joystick support
      if (Input.GetJoystickNames().Length > 0)
      {
        currentKey = HandleJoystick();
      }
      else
      {
        // Keyboard, only if no joystick
        currentKey = HandleKeyboard();
      }

      TestNextKey(currentKey);
    }

    /// <summary>
    /// Look for joystick inputs and translate into a keyboard key
    /// </summary>
    /// <returns></returns>
    private KeyCode? HandleJoystick()
    {
      const float deadZone = 0.15f; // A joystick is never fully rested
      KeyCode? currentKey = null;

      // If this is a joystick input, translate it to a keyboard key   

      // Gamepad A or B
      //---------------------------------------------------------------------------
      bool a = false;
      bool b = false;

#if UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
      a = Input.GetKeyDown(KeyCode.Joystick1Button0);
      b = Input.GetKeyDown(KeyCode.Joystick1Button1);
#elif UNITY_STANDALONE_OSX
      a = Input.GetKeyDown(KeyCode.Joystick1Button16);
      b = Input.GetKeyDown(KeyCode.Joystick1Button17);
#endif

      if (a)
      {
        return KeyCode.A;
      }

      if (b)
      {
        return KeyCode.B;
      }

      // Axis
      //---------------------------------------------------------------------------
      float x = Input.GetAxis("Horizontal");
      float y = Input.GetAxis("Vertical");

      // Rest?
      if (Mathf.Abs(x) < deadZone && Mathf.Abs(y) < deadZone)
      {
        x = 0;
        y = 0;
      }

      // Horizontal only
      if (Mathf.Abs(x) > deadZone && Mathf.Abs(y) < deadZone)
      {
        if (previousPadInput.x == 0)
        {
          currentKey = x > 0 ? KeyCode.RightArrow : KeyCode.LeftArrow;
        }
      }
      // Vertical only
      else if (Mathf.Abs(y) > deadZone && Mathf.Abs(x) < deadZone)
      {
        if (previousPadInput.y == 0)
        {
          currentKey = y > 0 ? KeyCode.UpArrow : KeyCode.DownArrow;
        }
      }

      previousPadInput.x = x;
      previousPadInput.y = y;

      return currentKey;
    }

    /// <summary>
    /// Keyboard input
    /// </summary>
    /// <returns></returns>
    private KeyCode? HandleKeyboard()
    {
      if (Input.anyKeyDown)
      {
        if (Input.GetKeyDown(this.code[index]))
        {
          return this.code[index];
        }
      }
      return null;
    }

    /// <summary>
    /// Look at the current keyboard input and the given key and see if it matchs the code
    /// </summary>
    /// <param name="currentKey"></param>
    private void TestNextKey(KeyCode? currentKey)
    {
      // Valid key?
      if (currentKey != null && currentKey.Value == this.code[index])
      {
        // Reset timer is this is the first key
        if (this.index == 0)
        {
          this.timeSinceStartCode = 0f;
        }
        this.timeSinceLastKey = 0f;
        this.index++;

        // Complete?
        if (this.index >= this.code.Length)
        {
          Debug.Log(codeName);

          if (this.receiver != null)
          {
            this.receiver.SendMessage(this.message, SendMessageOptions.DontRequireReceiver);
          }

          this.index = 0;
        }
      }
      // Wrong input (no input is valid)
      else if (currentKey != null)
      {
        // Reset
        this.index = 0;
      }
    }
  }
}
```
