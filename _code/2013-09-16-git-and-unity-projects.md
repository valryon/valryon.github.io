---
title: Git and Unity projects
tags: unity
shortdesc: Using git version control with Unity.
---

I know this has been covered a hundred times over the Internet, but I need to centralized all the useful information for my future projects.

By default, it's not that easy to have a clean Unity project under source control.

**Remark:** this post has been updated for **Unity 4.3**.

**Remember:** you need to do this for each new project.

## Enable external source control

In Unity, go to *Edit* -> *Project Settings* -> *Editor*

<img src="{{site.url}}/static/content/posts/2013-09-16/editor.png" />

In the inspector window, select *Visible Meta Files* mode under Version Control and *Force Text* under Assets Serialization.

<img src="{{site.url}}/static/content/posts/2013-09-16/inspector.png" />

The first option will allow a project to be commited without the huge *Library* folder, containing the asset database.

Forcing asset to text is better when you work in teams because you can use a diff tool.

## The .gitignore

This is a nice standard .gitignore file for Unity projects.

{TODO gist 6581543 %}

Few comments:

- the *Library* folder is created **automatically** the first time you open the project
- the *.csproj* and *.sln* files can be recreated in one clic (Assets -> Sync Monodevelop projects) so you shoud not commit them (and they should not be modified manually)

## Applying this to an existing project

Sometimes you want to add version control to an existing project. Be careful, you can lose all your data, so make sure to have a **backup**.

- Create a new git repository
- Copy all your Unity files in it
- Open your project
- Change the editor settings
- Save
- (Save)
- (Save)
- Check that each file as an *.meta* in the *Assets* folder
- Manually delete the Library, obj and Temp folder
- Save
- Restart the project
- Makes sure everything is working (you will know that very, very fast)
- Commit
- Enjoy!

This works for SVN too, just adapt the ignore rules.
