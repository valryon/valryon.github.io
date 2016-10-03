---
title: Unity - Paint and blood splatters part 1
redirect_from:
  - /unity-paint-and-blood-splatters-tests/
tags: code unity
excerpt_separator: <!--more-->
---

The concept: trying to paint a level with blood or paint (nearly the same thing, only the color is different) in Unity 3D.

<!--more-->

## First method: decals

Decals are simple sprites, repeated and drawn on top of other objects. They are usually used for visual enhancements of a scene more than for gameplay purposes.

Here's an example of a decal for paint.

[  ![Paint decal][url_img_decal_paint]  ][url_img_decal_paint]

Using an existing script probably made by a Dexter fan ([download and demo here](http://forum.unity3d.com/threads/2562-blood-splatter)) I was able to spawn decals in my level to stick paint everywhere.

[  ![Paint decal test][url_img_decal_paint_test]  ][url_img_decal_paint_test]

The splash algorithm from the original script linked above is quite smart. From a point in space it raycasts in random direction and print a decal when it hits something.

I added some debug features to visualize it.

[  ![Paint decal test visualization][url_img_decal_paint_test_vizualize]  ][url_img_decal_paint_test_vizualize]

To have a proper display of decals, I used a community script called [BlendedDecal](http://wiki.unity3d.com/index.php/BlendedDecal). It removes all glitches due to "Z-fighting".

The decals itself was:

- A quad
- The texture above with alpha as transparency...
- ...texture that was on a BlendedDecal shader material

Nothing else. The fun part is when you add a rigidbody, you can have physics splash.

And here's the script I used for painting. I put it online because the original script was in JavaScript so it may helps someone to have it done in C#.

{TODO gist 6870378 %}

### Advantages:

- Simple
- It manipulates GameObjects
- Nice splash algorithm

### Drawbacks

- What do we do of those game object? Destroy it after x seconds? Let them accumulate until it crashes?
- As you can see of the screenshot above, your decals have to have a size that match where they are applied

# The hard way: painting the texture

Unity allow to directly change the colour of a pixel on a texture.

You just have to use the **[SetPixel(s)](http://docs.unity3d.com/Documentation/ScriptReference/Texture2D.SetPixel.html) method**.

It's a bit slow, but if you batch your modification you can optimize the paint.

Drawing some random pixels is easy, so I thought it could be nice to use the previous decal as a **model**, a shape.

I also added a random roulette for dcals so it's always the same that is used. Here's what I got:

[  ![Texture blood test][url_img_texture_dexter]  ][url_img_texture_dexter]

or the painting version, the same with a color change

[  ![Texture painting test][url_img_texture_colors]  ][url_img_texture_colors]

The wall behind is quite nice.
But the painting on platforms **clearly sucks**. It's all streched and small. I am not applying a ratio, so the texture is stretched but I'm doing as if it was not.

I mixed it with the previous splash algorithm so it's raycasting everywhere to paint things.

### Advantages:

- No objects are created
- Ingame permanent (but reseted at each launch)

### Drawbacks

- Performance: cause a small 0.1sec freeze
- Paintable objects must have a **texture** AND a **mesh collider** (so you can get textureCoords for RaycastHit)
- Complexity
- You must perfectly understand texture. Not like me, trying to hack stuff.

## Work in progress

I'm still working on it, I don't know if I can solve the scale issue but I'll see and update if I find a solution.
Also, I saw some fluid decals but this seems like a preciously kept mystery for now.

**[Take a look at the next article to this the solution I found](http://dmayance.com/unity-paint-part-2/)**

[url_img_decal_paint]: {{site.url}}/static/content/posts/2013-10-08/paint.png

[url_img_decal_paint_test]: {{site.url}}/static/content/posts/2013-10-08/paint_test.png

[url_img_decal_paint_test_vizualize]: {{site.url}}/static/content/posts/2013-10-08/paint_test_visualize.png

[url_img_texture_dexter]: {{site.url}}/static/content/posts/2013-10-08/dexter.png

[url_img_texture_colors]: {{site.url}}/static/content/posts/2013-10-08/colours.png

[url_img_decal_paint_test_vizualize]: {{site.url}}/static/content/posts/2013-10-08/paint_test_visualize.png
