---
title: Unity - Paint and blood splatters part 2
tags: unity
shortdesc: Painting a level with blood or paint in Unity (R&D).
---

Advanced research on painting a level with decals in Unity 3D.

<!--more-->

After another day of research, I am quite satisfied with the result:

[  ![Paint decal][url_img_result]  ][url_img_result]

Finally, I used:

- Decals
- Batching (thanks to built-in Unity optimization)
- Recycling

What I didn't achieve:
- Permanent paint, even with a giant pool the time will come where some paintings will be removed
- Fluids

## Explanation

I used the script I published before and added some new feature.

### Recycling pool

Decals are stored in an array. If we reach the limit, we delete the oldest decal to recreate a new one.

If the pool is large enough, you'll have some time before the player see some splash are missing.

Maybe this can be optimized at splash time: if the raycast already return a splash, maybe we don't need to create a new one.

### Dynamic Batching

I discovered that Unity will automatically optimize draw calls when it can. [See the documentation about Draw Call Batching](http://docs.unity3d.com/Documentation/Manual/DrawCallBatching.html).

However, it is not that simple in our case: we want multicolor paintings. But if you assign a new color to a shared material, either all decals using it will change color or you will create a new material and lose the shared connection.

So I made a simple list-based system where we create a material only when we want to use a decal/color we never used before and we register it in the list.

Unity will batch all calls of the same decals. If you open the _Stats_ window, you can see this optimization.

[  ![Batching draw calls][url_img_batching]  ][url_img_batching]

You can also mark your decals as "Static" if you don't plan to move them.

If you have a lot of colors or decals, you may have to find another solution to preload your materials.

## The script

Here is the script. Provided "as it', "no warranty", "works for me".

{TODO gist 6886871 %}

Feel free to contact me for any question.

## Interesting resources

If you are interested on the subject:

- [How to project decal](http://blog.wolfire.com/2009/06/how-to-project-decals/): what we would like to do but it's way too complex for us
- [Portal 2 painting](http://www.valvesoftware.com/publications/2011/gdc_2011_grimes_nonstandard_textures.pdf): some hints about how Portal 2 paint is made (skip to page 65)

[url_img_result]: {{site.url}}/static/content/posts/2013-10-09/paint.png

[url_img_batching]: {{site.url}}/static/content/posts/2013-10-09/batching.png
