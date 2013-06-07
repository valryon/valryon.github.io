---
title: Blogging with Jekyll and GitHub pages
layout: post
---
Last Monday I completely broke up my blog. 

It was a custom homemade engine using ASP.NET MVC hosted on an Apache server, parsing markdown files to serve Razor powered views. It was nice, but I updated the server and it just stopped working. The site was up but no posts were found. I think it was the signal: having a custom engine is fun and challenging, but **for a blog does it worth reinventing the wheel again?** (You can still the .NET engine on [github](https://github.com/Valryon/valryon.github.io/tree/asp.net-mvc)).

So I just switch to **Jekyll** for the server and **GitHub Pages** for hosting.

### Write in markdown

As I already did with my homemade engine, I am now writing posts in markdown files stored in a GitHub repo. [See this post as markdown](https://github.com/Valryon/valryon.github.io/blob/master/_posts/2013-04-08-blogging-with-jekyll-and-github-pages.md).

Markdown is great, simple, easy to parse and even to read. And you can still add some HTML in it for images or videos. You can find some WISYWIG editors as [MarkdownPad](http://markdownpad.com/) on Windows or [Mou](http://mouapp.com/) for OS.X.

### Layouts, feeds, etc

Jekyll is smart. It will look at each files you feed him with and try to transform it with rules.
Posts, for example, have a simple header like this:

<script src="https://gist.github.com/Valryon/5335230.js"></script>

The `layout` item tells the engine the layout to use. This layout is stored in the ``_layouts`` folder (what a surprise).

<img src="{{site.url}}/static/content/posts/2013-04-08/blog_layouts.png" />

You should have a file named just as the desired layout (``post.html`` here). Those layouts can inherit between them. For my blog I have a ``default.html`` layout and two children layouts.

Children will fill the ``content`` layout section with some new things you write. This is very similar to some other template system, such as the Razor one in ASP.NET MVC.

If you want a sitemap, it's the same idea. You add a ``sitemap.xml`` to your repository containing some code:

<script src="https://gist.github.com/Valryon/5335214.js"></script>

Here it just shows every post, but you can manually specify any URL you want. Same goes for the RSS feed.

### GitHub Pages hosting

GitHub now provides a simple way to host a static website on their server for free.

You can have user/organization pages or per-project pages. The rules differs a bit:

- For a user or an organization, you must have a ``name.github.io`` repository. The master branch will be served at [name.github.io](name.github.io)

- For a project ``name.github.com/project``, you must have a ``gh-pages`` branch containing your pages. It will be served at [name.github.io/project](name.github.io/project).

A bit tricky, but nothing to configure and it only has two states : working or not.

### Deployment

To deploy new posts, new layouts, config files, etc, you just have to commit and then...

``git push``.

And your site will be updated in few seconds. Kind of magical.

If you have your own Jekyll server, you can enable auto-refresh to the server will check for file modification. For now a config change still requires a jekyll restart, but posts and layouts can be updated on the fly.

### My 2 cents

This combination of markdown + Jekyll is so great and easy that I will probably reuse it for every static HTML content I want to serve. 

GitHub Pages is the bonus for a free and easy hosting system, but having my own Jekyll server would still have been really simple.

Combined with Javascript you can do nearly everything that don't directly require a database connection (you can still have a webservice and JS calls).

### Try it!

Feel free to fork me or many other blogs using this tech, like [this one I used](https://github.com/FlorianWolters/florianwolters.github.com).

Already having markdown content, it took me about 4 hours to create again my complete blog. Using the default Twitter bootstrap CSS for now, I have a responsive and readable website at no costs.
