---
title: Unity - Timer using co-routine
layout: post
---

Hello folks,

It is time to share some useful code I wrote for some Unity projects.

Here is a simple and powerful **Timer class**. 

It features:

- A duration (it will wait n seconds then execute the piece of code)
- Repeat this timer until the object is destroyed or stopped
- The callback, a ``System.Action`` delegate to execute some code with a very simple syntax

Enjoy :).

{% gist 9915075 %}
