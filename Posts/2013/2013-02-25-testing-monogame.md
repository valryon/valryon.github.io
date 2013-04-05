As a fellow XNA developer, I've always been interested by [MonoGame](monogame.codeplex.com). Since the 3.0 version came out in January 2013, I thought it was the good timing to give it a try for a brand new game project.


## The context

This new game project will be announced soon, but I can safely say for now that:

- I'm doing it with [Laurent Brossard](http://rednalhgih.com/)
- it will be mobile (iOS first, android and/or windows phone next)
- it will be simple, so no need for complex engines


And for this project, I tried MonoGame. And MonoTouch. Let's see what happened.

## Installing and using MonoGame

Installing MonoGame with the 3.0 installer is a really easy step. Getting it working too, **if you have XNA already installed**. This is a matter of time, but for now you must have a **working Windows XNA dev env** for its content project and pipeline.

Having XNA is simple on Windows 7 but a bit tricky on Windows 8. 

But then it just works. And that's great.

Getting the framework to work on Mac OS X (and Linux, I guess) is not harder than Windows, but you cannot start a project from scratch on those platforms. **For now**. You must apply some tricks to:

- compile the content on your Windows 
- share the generated binaries with your Mac/Linux project

This issue is major to me, but it should be solved in some month thanks to the awesome work on the MonoGame team. 

If you manage to get your compiled content in your Mac/Linux project, then it should be working fine. Sometimes you discover some strange behavior (cannot change the resolution outside the Game.Update method? The 3.0 release was bugged and you have to manually compile the 3.0.1?), but it’s part of the fun.

Another thing is the use of **Portable Library**, which is not compatible with the free Express version of Visual Studio. I cannot afford for now an expensive Pro license so I cannot share easily projects between platforms.

So in my opinion, if you are looking for a C# and .Net game dev environment and you want more control than Unity can offer, **Monogame should be your new favorite framework**.

I’ll definitely use it for a **desktop game project** using Leap Motion (another project to be announced!), but not in a mobile environment.

## MonoGame on mobiles

[MonoTouch](http://xamarin.com/monotouch) allows you to deploy .NET app to iOS and Android. Thanks to Xamarin, you have a nice installer, a nice IDE and accessible licenses.

So at first I thought that would perfectly fits my needs:

- One codebase
- Test on desktop (faster, easier to debug)
- Works on mobiles
- Nice tools (Xamarin Studio for example)

Well all of that is true, except it's full of traps, strange behavior, incompatibility... 
The font you use on desktop? Not working on iOS.
This nice sprite? Not working because of many reasons.

And there's not a nice error message, you have to look at the console to find a raw stack trace, google it and figure out it may be the content.

And then you realize you have **an horrible codebase**, with many projects referencing different assemblies, sharing code full of ``#if PLATFORM`` instructions...

That's where I thought that for mobile game dev, I'd rather stick to the **native frameworks** for now. **Unity** would be a better idea, but the license fees are too expansive for me, for now.

It doesn't mean it doesn't works. Look at Bastion and many App Store games that use MonoGame and MonoTouch. For me it's just not the right solution, and it seems to be not mature enough.
