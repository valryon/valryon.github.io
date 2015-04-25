---
title: 2D water line in Unity
layout: post
tags: code, unity
---

I just managed to have a 2d water line in Unity 4.3.

I'd like to share a good working base with you, it's not a ready to use solution but it can be a good start for a more complex water line.

It's not that hard to make, but it lacks of buoyancy, droplets, reflection... like I said, it's a start.

Thanks to [http://forum.unity3d.com/threads/141925-2d-Water](http://forum.unity3d.com/threads/141925-2d-Water) and [http://games.deozaan.com/unity/MeshTutorial.pdf](http://games.deozaan.com/unity/MeshTutorial.pdf)

# The idea

The line is a list of dots connected by springs.

Then we create a simple mesh (few triangles) between each pair of dots.

Finally, we update the mesh vertices at each update using the new dots positions.

# The result

Here's the result with the ``Sprite-default`` material:

[ ![wave][wave]][wave]

[ ![editor][editor]][editor]

[ ![wireframe][wireframe]][wireframe]

You can probably apply some shader on it.

# The script

{% gist 7927741 %}

Enjoy! ;)


[editor]: {{site.url}}/static/content/posts/2013-12-12/editor.png

[wireframe]: {{site.url}}/static/content/posts/2013-12-12/wireframe.png

[wave]: {{site.url}}/static/content/posts/2013-12-12/wave.gif
