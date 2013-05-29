---
title: GPUImage Monotouch binding
layout: post
---

For our Pixelnest projects we are currently using Xamarin products to create iOS apps. 

As we needed to use the awesome **[GPUImage lib](https://github.com/BradLarson/GPUImage)**, we had to look for an existing **C# Monotouch binding**.
This lib allow you to apply effects to your UIImage, such as a Pixelate effect like this:

<img src="{{site.url}}/static/content/posts/2013-05-31/gpuimage.png" />


Fortunately, someone dropped it to me on Stack Overflow and I now trying to add this working binding to the official [monotouch-bindings repository](https://github.com/mono/monotouch-bindings).

As the PR is still waiting for a merge, if you need the binding you can find it **[on our monotouch-bindings fork](https://github.com/pixelnest/monotouch-bindings)**. It includes a ready-to-use sample.

Please feel free to use it, fork it, enhance it!
