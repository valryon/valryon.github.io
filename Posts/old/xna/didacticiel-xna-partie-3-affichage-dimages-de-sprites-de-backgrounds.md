<p style="text-align: center;"><a href="http://uppix.net/c/9/f/f33995835aa200a492f7315b397fe.html"><img src="http://uppix.net/c/9/f/f33995835aa200a492f7315b397fe.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<span style="color: #008000;"><strong>EDIT : Cet article a été mis à jour pour XNA 4.0</strong></span>
<h3 style="text-align: left;"><strong>Objectifs :</strong></h3>
<p style="text-align: left;">Maintenant que l'on a affiché un magnifique "Hello World", il est temps de s'attaquer à des trucs plus intéressants : le chargement et l'affichage d'images</p>
<!--more-->
<h3 style="text-align: left;"><strong>Sommaire :</strong></h3>
<ul>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-1-installation-et-decouverte/">Installation et découverte</a></li>
	<li><a title="Hello World" href="http://www.valryon.fr/didacticiel-xna-partie-2-hello-world/">Hello World</a></li>
	<li><a title="Troisième partie : affichage" href="didacticiel-xna-partie-4-logique-de-jeu">&gt;&gt;&gt;&gt;&gt;Affichage d'images, de sprites, de backgrounds</a></li>
	<li><a href="../didacticiel-xna-partie-4-deplacements-collisions-rotations/">Déplacements, collisions, rotations</a></li>
	<li><a href="../didacticiel-xna-partie-5-%E2%80%93-entreessorties/">Entrées / Sorties</a></li>
	<li><a title="Squelette" href="http://www.valryon.fr/didacticiel-xna-partie-6-%E2%80%93-squelette-generique-dun-jeu-2d">Squelette générique d’un jeu 2D</a></li>
</ul>
<h2 style="text-align: center;"><img title="Lire la suite…" src="http://www.valryon.fr/wp-includes/js/tinymce/plugins/wordpress/img/trans.gif" alt="" />XNA : Affichage d'images, de sprites, de backgrounds (Draw())</h2>
<p style="text-align: justify;">Sur la base d'un nouveau projet ou sur celui du Hello World (c'est pas pour ce qu'il y avait dedans...), vous allez pouvoir charger n'importe quelle image (ou presque) pour faire ce que vous voulez.</p>

<h3 style="text-align: justify;">Chargement d'images</h3>
<p style="text-align: justify;">Si vous avez suivi la partie 2, vous vous doutez que l'on va utiliser le ContentManager pour faire cela. La première étape consiste donc en l'ajout d'un fichier image à votre projet Content.</p>
<p style="text-align: justify;">Pour faire plus propre vous pouvez créer des sous-dossiers. Créez par exemple un dossier "gfx" et ajoutez-y votre image. Si vous n'avez pas d'image à portée de main,je vous propose celle-ci :</p>
<p style="text-align: center;"><a href="http://uppix.net/b/8/7/255fea26de4cfbf3c0a75ddca4b1e.html"><img src="http://uppix.net/b/8/7/255fea26de4cfbf3c0a75ddca4b1e.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: left;">Ajoutez donc cette image à votre projet :</p>
<p style="text-align: left;"><a href="http://uppix.net/e/7/0/b451d0703b286400ed3bc77ba9ada.html"><img class="aligncenter" src="http://uppix.net/e/7/0/b451d0703b286400ed3bc77ba9adatt.jpg" alt="Image hosted by uppix.net" border="0" /></a>
Vous pouvez importer tous types d'images (PNG, JPG,GIF,...), de textures DirectX (jesaisplusleformat).</p>
<p style="text-align: justify;"><span style="text-decoration: underline;"><strong>Attention à la taille</strong></span> : Ces images sont chargées dans la RAM de votre carte graphique. Celle-ci ne supporte pas forcément de trop grosses images, essayez de vous limiter à moins de 2048 * 2048 (XNA met une erreur de toute façon).</p>
<p style="text-align: justify;"><span style="text-decoration: underline;"><strong>Mais alors comment je fais pour avoir un super long background ???
</strong></span>Eh bien il faut ruser ! Plutôt qu'une seule grande image, il faut gérer plusieurs "petites" images. Je vous invite à voir un lien (en anglais) sur le sujet : <a title="Scrolling, Parallax 2D XNa" href="http://www.xnadevelopment.com/tutorials/scrollinga2dbackground/ScrollingA2DBackground.shtml">http://www.xnadevelopment.com/tutorials/scrollinga2dbackground/ScrollingA2DBackground.shtml</a></p>
<p style="text-align: justify;">Votre image insérée au projet, passons au code.</p>

<h3 style="text-align: justify;">Affichage</h3>
<p style="text-align: justify;">Pour un objet sur une scène, ici le plan (x,y) affiché à l'écran, nous avons besoin :</p>

<ul style="text-align: justify;">
	<li>De la texture (de l'image)</li>
	<li>De l'emplacement dans la texture (facultatif)</li>
	<li>Des coordonnées où l'afficher sur l'écran</li>
	<li>De la taille de l'image à afficher (qui peut-être différente de l'image source, XNA fait le redimensionnement)</li>
</ul>
<p style="text-align: justify;">On peut y ajouter d'autres paramètres intéressants : rotation de l'image, "flip" (retournement ?), etc.</p>
Il faut donc ajouter quelques attributs à notre classe :

<code lang="C#">
//Rectangle de sélection dans le fichier du sprite (où est l'image à afficher)
Rectangle src;
//Objet texture qui sera chargé
Texture2D sprite;
//Rectangle de destination : où l'image sera affichée sur l'écran et sa taille
Rectangle dst;
//Si on veut faire tourner le sprite
float rotation;</code>

Pour faire propre, on instanciera la texture dans <strong>LoadContent() </strong>et les autres attributs dans le constructeur ou dans <strong>Initialize()</strong>.

<code lang="C#">protected override void Initialize()
{
src = new Rectangle(0, 0, 127, 147);
rotation = 0.0f;
dst = new Rectangle();
dst.X = 300;
dst.Y = 300;
<span style="font-family: monospace;">dst.Width = src.Width;
dst.Height= src.Height;
//dst.Width = 540; //On peut changer manuellement les dimensions du sprite affichée</span>
...
}</code>
<p style="text-align: justify;">On indique donc où se trouve notre image dans le fichier (dans le rectangle <strong>src</strong>) et où on va l'afficher sur l'écran avec  <strong>dst </strong>. Puis on charge l'image avec le ContentManager :
<code lang="C#">
protected override void LoadContent()
{
...
sprite = Content.Load&lt;Texture2D&gt;(@"gfx/cactus");
...
}</code></p>
<p style="text-align: justify;"><span style="color: #0000ff;"><span style="text-decoration: underline;">Remarque</span> : Content.Load est la méthode magique qui vous permettra de charger tous vos médias.</span></p>
Et on affiche, dans <strong>Draw()</strong> :

<code lang="C#">
spriteBatch.Begin();
spriteBatch.Draw(sprite, dst, src, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 1.0f);
spriteBatch.End();
</code>

Ce qui donne :
<p style="text-align: center;"><a href="http://uppix.net/6/5/0/c39449cbe5803d8aad318b1c02176.html"><img src="http://uppix.net/6/5/0/c39449cbe5803d8aad318b1c02176tt.jpg" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">C'est beau, non ? Avouez qu'afficher une image est extrêmement simple... Si ça peut vous rassurer, tout est de la même difficulté :)</p>
<p style="text-align: justify;">Ces lignes sont équivalents à :</p>
<code lang="C#">
spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
spriteBatch.Draw(sprite, dst, Color.White);
spriteBatch.End();
</code>
<p style="text-align: justify;">En effet, on utilise tout le fichier donc pas besoin de spécifier l'image à choisir. Mais si vous avez une planche de sprites, alors il faut jouer avec le <strong>src </strong>pour avoir le bon sprite affiché.</p>
<p style="text-align: justify;">Le SpriteSortMode permet d'indiquer quand et comment les images doivent être affichées. Avec Deferred vous vous assurez que les images ne seront affichées que quand End() sera appelé. Cela vous permet d'optimiser vos appels au spriteBatch et d'avoir de meilleures performances.</p>
<p style="text-align: justify;">La transparence est activée par défaut (AlphaBlend). Elle peut être désactivée :</p>
<p style="text-align: left;"><code lang="C#">spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.Opaque);</code>
<a href="http://uppix.net/b/d/8/6124a4ad38e0e0df3f11f413b44f2.html"><img class="aligncenter" src="http://uppix.net/b/d/8/6124a4ad38e0e0df3f11f413b44f2.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Un autre BlendState est disponible : Additive. Il permet d'additionner les couleurs quand plusieurs images sont superposées. Cela peut être utile pour les effets de particules. Mais pas ici :</p>
<p style="text-align: center;"><a href="http://uppix.net/e/f/3/1647276d562122c1579678b3a4e54.html"><img src="http://uppix.net/e/f/3/1647276d562122c1579678b3a4e54.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Le dernier BlendState (NonPremultiplied) est à oublier. Avec XNA 4.0, toutes les fonctionnalités d'AlphaBlending (gestion de la transparence) ont été changées pour être plus performante et plus logique. Le NonPremultiplied permet d'avoir l'ancien AlphaBlending et sert donc simplement à la rétro-compatibilité.</p>
<p style="text-align: justify;">Voyons les autres paramètres de la méthode <strong>Draw()</strong>. Celle-ci est tellement puissante qu'elle s'occupe à elle seule de tous les rendus sans problème. Affectez maintenant une valeur différente de 0 à la rotation. 1.7f par exemple (Pi/2, c'est en radians, soit un quart de tour). N'oubliez pas d'utiliser la surcharge qui prend en compte la rotation. Vous obtenez :</p>
<p style="text-align: justify;"><code lang="C#">rotation = (float)Math.PI/2;
...
spriteBatch.Draw(sprite, dst, src, Color.White, rotation, Vector2.Zeo, SpriteEffects.None, 1.0f);</code></p>
<p style="text-align: center;"><a href="http://uppix.net/5/1/4/789148db1c3a23fcdfceba3ea3831.html"><img src="http://uppix.net/5/1/4/789148db1c3a23fcdfceba3ea3831.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Magnifique. Attention, détail qui peut provoquer des bugs : la rotation s'effectue ici sur le coin supérieur gauche (coordonnées (0,0)). C'est le 6e paramètre de notre appel à la fonction Draw().</p>
<p style="text-align: center;"><a href="http://uppix.net/c/7/f/414165d6c73053f53d3f4d1146980.html"><img src="http://uppix.net/c/7/f/414165d6c73053f53d3f4d1146980.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Il est possible de changer en donnant une valeur différente au paramètre <strong>origin</strong>. Ici j'ai mis <strong>Vector2.Zero</strong> mais vous pouvez mettre (en calculant les valeurs) <strong>new Vector2(milieux, milieuy)</strong> pour faire tourner le sprite sur lui-même par exemple. Nous le ferons dans la prochaine partie.</p>
<p style="text-align: justify;">Enfin, il est possible d'utiliser les <strong>SpriteEffects </strong>pour de la symétrie horizontale ou verticale (<em>flip</em>) :</p>
<p style="text-align: justify;">SpriteEffect.None :
<a href="http://uppix.net/f/b/2/5fc3d4f3015361581a256cc6fdb00.html"><img src="http://uppix.net/f/b/2/5fc3d4f3015361581a256cc6fdb00.png" alt="Image hosted by uppix.net" border="0" /></a></p>
SpriteEffect.FlipHorizontally :
<a href="http://uppix.net/2/9/8/c0f0a4f0b7f7f1adc4002f1394f72.html"><img src="http://uppix.net/2/9/8/c0f0a4f0b7f7f1adc4002f1394f72.png" alt="Image hosted by uppix.net" border="0" /></a>

SpriteEffect.FlipVertically :
<a href="http://uppix.net/4/3/7/5fbdd94d2623a9c6a755d727e730c.html"><img src="http://uppix.net/4/3/7/5fbdd94d2623a9c6a755d727e730c.png" alt="Image hosted by uppix.net" border="0" /></a>
<p style="text-align: justify;">Nous avons fait le tour des possibilités d'affichage 2D avec XNA. Retenez que c'est simple, puissant et complet. Les images se superposent dans l'ordre où vous les affichez, donc on affiche un fond en premier, ensuite les éléments dans l'ordre de profondeur.</p>
<p style="text-align: justify;">Prochain chapitre : tout ce qu'on peut mettre dans <strong>Update()</strong> =)</p>