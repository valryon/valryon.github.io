---
title: Unity - Timer using co-routine
tags: unity
shortdesc: Advanced Timer usable as a coroutine for Unity.
---

Here is a simple and powerful **Timer class**.

Features:

- A duration (it will wait n seconds then execute the piece of code)
- Repeat this timer until the object is destroyed or stopped
- The callback, a ``System.Action`` delegate to execute some code with a very simple syntax

Enjoy :).

[See on Gist](https://gist.github.com/valryon/9915075)

```csharp
// 2014 - Pixelnest Studio
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Ready to use timers for coroutines
/// </summary>
/// <summary>
  /// Ready to use timers for coroutines
  /// </summary>
  public class Timer
  {
    /// <summary>
    /// Simple timer, no reference, wait and then execute something
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator Start(float duration, Action callback)
    {
      return Start(duration, false, callback);
    }


    /// <summary>
    /// Simple timer, no reference, wait and then execute something
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="repeat"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator Start(float duration, bool repeat, Action callback)
    {
      do
      {
        yield return new WaitForSeconds(duration);

        if (callback != null)
          callback();

      } while (repeat);
    }

    public static IEnumerator StartRealtime(float time, System.Action callback)
    {
      float start = Time.realtimeSinceStartup;
      while (Time.realtimeSinceStartup < start + time)
      {
        yield return null;
      }

      if (callback != null) callback();
    }

    public static IEnumerator NextFrame(Action callback)
    {
      yield return new WaitForEndOfFrame();

      if (callback != null)
        callback();
    }
  }
```

Exemple

```csharp
const float duration = 3f;

// Simple creation: can't be stopped even if lopping
//--------------------------------------------
StartCoroutine(Timer.Start(duration, true, () =>
{
  // Do something at the end of the 3 seconds (duration)
  //...
}));

// Launch the timer
StartCoroutine(t.Start());

// Ask to stop it next frame
t.Stop();
```
