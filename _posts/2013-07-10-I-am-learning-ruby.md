---
title: I am learning ruby
layout: post
---

The title is pretty clear and seems naive but I never thought I would do it, because my of laziness and of the lack of opportunities to use it in my jobs.

Here is my first experiences with this completely different environement that ruby is.

## From a .NET background

It's been a while since I've using *.NET* for my projects. 

I made several projects using *ASP.NET MVC*, especially a big secured webservice handling thousands of requests each day, and I must admit that this framework is awesome.

But for [Pixelnest](http://pixelnest.io), I'm not sure this is a good choice. It's a bit hard to deploy this kind of app on an Apache server, even if I'm managed to I'm not sure this is reliable, and Windows Server licence are too expensive for us at the moment.

So having a look at modern web technologies, *Ruby* and *Ruby on Rails* seems to be a good choice and it was a good timing for me to learn new things.

## Out of my comfort zone

First thing, besides learning new frameworks and APIs, I didn't learned from scratch something for a while.

And it was hard to make the effort of the first step. Not just installing the tools and displaying a "Hello, world!" but a real first step with writing my own code for a good reason.

It involves thing like reading the documentation from start and fetching newbie questions on Stack Overflow.

Not knowing what to do, how and where to start was really disturbing. 

So I decided to rewrite an existing .NET web project in ruby to have a clear idea of what it should be in the end.

## Starting a little webservice

What I did and re-did was a simple webservice that can fetch a simple database and respond to few REST http requests, with a simple web admin panel.

The webservice have encrypted JSON input / output.

### 1 - The web framework

At first I thought trying *Ruby on Rails* while learning *Ruby* woulnd't be so hard. 

It was a bad idea, obviously, RoR is just an overkilled solution for a newbie that don't even know the language.

I discovered and adopted *Sinatra*, a simple web framework. Few parameters, you link a route to some code and you have a webservice or an admin panel, if you return some views (with *HAML*).


### 2 - The language

Sounds obvious but I discovered many things. Accessors are weird, inheritance is complex (*inherits*?*extends*?*<*?), *symbols* are new for Java/.NET devs…

You use gems and not libs or dlls.

And of course the syntax, the "Duck Typing" philosophy, everyting was a bit uncommon to me.

But you get used to it quickly. 

### 3 - Other frameworks

I used *DataMapper* as ORM to store my class in database easily. Works fine, easy to understand.

The adapters are great, I use *Sqlite3* in dev and *Postgres* in prod without changing anything in my code.

And ruby is full of cool gems to use like *shotgun* that reload your code at each request (very handful in dev).

### 4 - Deployment

This was one of our main concerns with ASP.NET MVC. We wanted to try Heroku.

Well, using the free plan, everything went well. You deploy easily using *git*.

# Will I use Ruby in the future ?

Definitely. I even think that it fits most back-end developers more than ASP .NET (MVC), or Java/PHP/etc.

Visual Studio miss me, but Sublime Text and a terminal is not that bad (except for refactoring), and it works on every platforms (Matthieu told me that you can even code ruby on mobile devices). Here what it looks like for me:

<img src="{{site.url}}/static/content/posts/2013-07-10/my-ruby-dev-env.png" />

All I can do is really recommend you to give it a try for a web project. 

It's a bit hard to dive in, but you get used to it and it became really cool, easy and powerful. 

No surprise so many devs and new services are using it (jekyll, github, etc).