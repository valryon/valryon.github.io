---
title: Using XNA and Monogame today
layout: post
---

**XNA** is special to me. It's a very convenient framework in my favorite language (C#) and I even created a complete game with it (The Great Paper Adventure).

But two points makes me wonder if XNA is still a solution for commercial games.

- 1/ Microsoft has dropped official support to the framework
- 2/ It's Windows and Windows Phone only (as Xbox 306 reign is over)

Fortunately, here comes **Monogame**. The same as XNA (namespaces, behavior, code...) but for multiplatform.

As I finally got a game working on PC and iPad (see picture below) with it, I can provide some tips and tricks for game developers.

<img src="{{site.url}}/static/content/posts/2013-06-18/pon-ipad-pc.jpg" />

## Is Monogame made for you?

Monogame is still very rough. There is few documentation, some bugs, some strange behavior... There is also no doubt that it will be better in the future.

The last release (3.0.1) is quite stable. But what you have to know is that **you must have a fully working XNA dev env** to have a working Monogame game.

And XNA is Windows only. For now you can't make your game from scratch on OS X, Monogame still use the XNA content pipeline but it is changing. 

*It may be possible by compiling undocumented stuff bbut I am too lazy to try that. I like when things simply works.*   

So Monogame is for developer who **can work mostly on Windows**.

Also, I would say that it is for programmers who want a **high level framework for low level game development**.

## Some tips

### 1 - Get your game working on Windows

Related to the previous issue, the easiest way to use Monogame is to have first a fully working game on Windows. 

And as a rule, I would say: **don't use Monogame on Windows**. No more updates doesn't mean the original framework stopped working.

When I tried Monogame 3.0.1 for George on Windows, the game kept freezing some micro seconds and I couldn't play a mp3 song file. Back to XNA, everything worked fine.

### 2 - Use the Monogame release version

This point can be discussed, but I recommend to use the Monogame **installer** (at least on Windows) to get everything working: dlls, templates, dependencies…

You can try to checkout the repository (especially the ``develop`` branch) but it can be a bit tricky to get everything working.


### 3 - Assets must be compiled for target platforms

Assets must be compiled the right way. I lost some time on this point.

The idea is that you must use ``.xnb`` files (compiled content) to load assets.

The easiest way to do is:

- Create a new **Monogame content project**. It's the same then the XNA content project with some multiplatform directive.

<img src="{{site.url}}/static/content/posts/2013-06-18/content_project.png" />


- Add your files to this project. You can use this content project directly with your XNA game.
- You must change the **content processor** for every assets in your content project. They should appear if you installed Monogame correctly)


- Then select the **target platform** in the compilation combobox. Only available if you created a project from a Monogame template.

<img src="{{site.url}}/static/content/posts/2013-06-18/targets.png" />

- **Copy the output files** (``bin/Content/*.xnb``) and transfer them to the project that need them (iOS, Android, OS X...). People made some scripts to automate that part. I just keep copy/pasting and I really wait for the real Monogame multiplatform content pipeline. 

<img src="{{site.url}}/static/content/posts/2013-06-18/content_ios.png" />

### 4 - Organize your projects and solutions

If you target multiple platforms you will obviously have two kind of code: 

- The game core, classes, rules and behavior -> **common**.
- Entry points, references, game center, views -> **specific**.

Organize your project with that in mind.
For example: 

<img src="{{site.url}}/static/content/posts/2013-06-18/projects_and_solutions.png" />

It can be a brain-fuck to some points to have enough abstraction for the common part to have an easier multiplatform integration. 

You still have good old preprocessor directives like ``if WINDOWS``.

**Don't try to have only one big project** file with MSBuild directive symbols, etc. It's a waste of time, it's finally far more easy to sync multiple ``.csproj`` files.

### 5 - Keep yourself up to date 

Check the [official website](http://monogame.net/), the [twitter](https://twitter.com/MonoGameTeam), the [github repository](https://github.com/mono/MonoGame) to know when new things are released.

Keep in minds current limitation, such as:

- Do not use XACT (even if it was quite a good software)
- Assets format per platforms


## So I'll be using it?

I'm not an expert, and I love what is becoming Monogame, those guys are doing a great job. I think it still needs a lot of work and polish, but if you like XNA definitely should give it a try.

As a new indie developer who wants to release many small titles, I must admit I will also look at Unity for productivity / workflow reasons. I often feel like I am reinventing the wheel, which may be very entertaining but not really efficient for a small company.

## See also

- A very good guide to porting games from XNA to Monogame: [Gamasutra - From XNA to MonoGame](http://www.gamasutra.com/view/feature/192209/from_xna_to_monogame.php)

- [Monogame official website](http://monogame.net/) 

- [Projects made with Monogame](http://monogame.net/showcase). Fez, Bastion, Skulls of the Shogun... Big stuff, right?
