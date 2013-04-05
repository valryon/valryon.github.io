<p style="text-align: center;"><a href="http://uppix.net/e/8/7/6ef7c41ab236d93fa753d0e916f23.html"><img src="http://uppix.net/e/8/7/6ef7c41ab236d93fa753d0e916f23.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<span style="color: #008000;"><strong>EDIT : Cet article a été mis à jour pour XNA 4.0</strong></span>
<h3 style="text-align: left;"><strong>Objectifs :</strong></h3>
<p style="text-align: left;">Cette partie est dédiée à tout ce qui peut remplir la méthode <strong>Update()</strong> : collisions et déplacements en particulier.</p>

<h3 style="text-align: left;"><strong>Sommaire :</strong></h3>
<ul>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-1-installation-et-decouverte/">Installation et découverte</a></li>
	<li><a title="Hello World" href="http://www.valryon.fr/didacticiel-xna-partie-2-hello-world">Hello World</a></li>
	<li><a title="Troisième partie : affichage" href="http://www.valryon.fr/didacticiel-xna-partie-3-affichage-dimages-de-sprites-de-backgrounds/">Affichage d'images, de sprites, de backgrounds</a></li>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-4-deplacements-collisions-rotations/">&gt;&gt;&gt;&gt;&gt;Déplacements, collisions, rotations</a></li>
	<li><a href="../didacticiel-xna-partie-5-%E2%80%93-entreessorties/">Entrées / Sorties</a></li>
	<li><a title="Squelette" href="../didacticiel-xna-partie-6-%E2%80%93-squelette-generique-dun-jeu-2d">Squelette générique d'un jeu 2D</a></li>
</ul>
<!--more-->

<img title="Lire la suite…" src="http://www.valryon.fr/wp-includes/js/tinymce/plugins/wordpress/img/trans.gif" alt="" />
<h2 style="text-align: center;"><img title="Lire la suite…" src="http://www.valryon.fr/wp-includes/js/tinymce/plugins/wordpress/img/trans.gif" alt="" />XNA : Déplacements, collisions, rotations</h2>
<p style="text-align: justify;">Maintenant que vous connaissez les principales techniques d'affichages 2D, il est temps de passer au contrôle et à la logique de votre (futur) jeu.</p>
<p style="text-align: justify;">Pour cela il va falloir se demander ce que vous voulez faire. Je vous propose ceci : l'image du cactus (voir partie 2) qui tourne sur elle-même et qui se déplace comme une balle de Pong sur l'écran, en rebondissant sur les côtés.</p>
<p style="text-align: justify;">Réutilisez le code de la partie 2, car l'affichage et le chargement de l'image se fait exactement comme cela a été décrit précédemment. On va s'intéresser presque uniquement au remplissage de la méthode <strong>Update()</strong>.</p>

<h3 style="text-align: justify;">Rotation du sprite sur lui-même</h3>
<p style="text-align: justify;">Assurez-vous d'avoir un attribut <strong>rotation </strong>de type <em>float </em>et initialisé à 0.0f dans votre code.</p>
<p style="text-align: justify;">Vous vous souvenez du cactus incliné ? Eh bien on va le dessiner pareil, sauf que l'on va incrémenter l'angle de rotation dans la méthode <strong>Update() :
</strong></p>
<p style="text-align: left;"><code lang="C#">protected override void Update(GameTime gameTime)
{
...
rotation += 0.01f;
//0.01f est arbitraire. La méthode étant appelée plusieurs fois par seconde (plus de 60 fois), mieux vaut mettre une valeur pas trop grande pour voir quelque chose...
...
}</code></p>
et pour rappel, dans <strong>Draw() </strong>:
<p style="text-align: left;"><code lang="C#">protected override void Draw(GameTime gameTime)
{
...
spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
spriteBatch.Draw(sprite, dst, src, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 1.0f);
spriteBatch.End();
...
}</code></p>
<p style="text-align: justify;">Générez, lancez, et Tada ! C'est magnifique ! Le cactus tourne... sur le coin en haut à gauche... Eh oui pour rappel la rotation "par défaut" est à cet emplacement (entre guillemets oui car c'est simplement le <strong>Vector.Zero</strong> qui le met ici).</p>
<p style="text-align: center;"><a href="http://uppix.net/7/2/a/1ec47414fccc2ac2580fbc8edb483.html"><img class="aligncenter" src="http://uppix.net/7/2/a/1ec47414fccc2ac2580fbc8edb483.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<p style="text-align: justify;">Or on a dit que l'on voulait un cactus tournant sur lui même. Ok ! Facile !</p>
<p style="text-align: justify;">Quel est le centre du <strong>sprite </strong>? Même si l'image que je vous ai fourni contient du vide, on va considérer le cactus comme centré. Donc le milieu de l'image est</p>
<p style="text-align: left;"><code lang="C#">(sprite.Width/2,sprite.Height/2)</code></p>
<p style="text-align: justify;">où <strong>sprite </strong>est l'objet Texture2D qui contient l'image du cactus en mémoire.</p>
<p style="text-align: justify;">Changer le vecteur <em>origin</em> par le centre de l'image :</p>
<p style="text-align: left;"><code lang="C#">spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
spriteBatch.Draw(sprite, dst, src, Color.White, rotation, <strong>new Vector2(sprite.Width/2,sprite.Height/2)</strong>, SpriteEffects.None, 1.0f);
spriteBatch.End();</code></p>
<p style="text-align: justify;">Ce qui nous donne un truc un peu comme ça (dessin approximatif) :</p>
<p style="text-align: center;"><a href="http://uppix.net/7/3/9/0d7021b2f3f86210a9f4a7da315e0.html"><img class="aligncenter" src="http://uppix.net/7/3/9/0d7021b2f3f86210a9f4a7da315e0.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<p style="text-align: justify;">Voilà pour la rotation ! Ce n'est vraiment pas compliqué, mais ça peut le devenir quand vous voulez une rotation précise. Vous n'avez plus qu'à ressortir vos vieux cours de trigonométrie, ça va servir...</p>

<h3 style="text-align: justify;">Déplacement d'un objet</h3>
<p style="text-align: justify;">Faisons mieux qu'une simple rotation : le déplacement.</p>
<p style="text-align: justify;">Pour éclaircir le code nous allons utiliser deux nouvelles variable sous forme de vecteurs : la position actuelle et la vitesse de l'objet.</p>
<code lang="C#">Vector2 location,speed;</code>
<p style="text-align: justify;">La vitesse sera une sorte de coefficient multiplicateur pour faire varier la direction. Vous comprendrez plus en détails par la suite.</p>
<p style="text-align: justify;">Initialisons celles-ci à coté des autres, avec une valeur de (100,100) par exemple pour <strong>location </strong>et (150,150) pour <strong>speed </strong>:</p>
<code lang="C#">location = new Vector2(100, 100);
speed = new Vector2(150, 150);
</code>
<p style="text-align: justify;">Vous vous souvenez de la variable <strong>dst </strong>? Jusqu'à présent on forçait son X et son Y à 300. Supprimez ces lignes (et vite !), et dans la méthode <strong>Update()</strong> ajoutez celle-ci :</p>
<code lang="C#">protected override void Update(GameTime gameTime)
{
... //Rotation avant
dst.X = (int)location.X;
dst.Y = (int)location.Y;
...
}</code>
<p style="text-align: justify;">Le langage C# est<strong> fortement typé</strong> : toute conversion (<em>cast</em>) doit être explicite. En cas de problème utilisez la classe <strong>Convert </strong>qui dispose de nombreuses méthode statiques pour faire du bon travail. Par exemple <code lang="C#">Convert.ToInt32("10")</code> transformera (String) "10" en (int) 10.</p>
<p style="text-align: justify;">Revenons à nos vecteurs. Comme la position va être mise à jour dynamiquement, à chaque passage d'<strong>Update()</strong>, il faut mettre à jour <strong>dst </strong>puisque c'est là que sera affichée l'image. Bien sûr pour l'instant rien ne change, mais quand <strong>location </strong>changera, l'image se synchronisera avec les nouvelles coordonnées.</p>
<p style="text-align: justify;">Bon c'est bien beau mais comment on peut faire que le sprite se déplace ?</p>
<p style="text-align: justify;">La méthode <strong>Update()</strong> possède un paramètre extrêmement intéressant : le <strong>GameTime</strong>. Le GameTime est une horloge (<em>timer</em>) qui est mise à jour automatiquement par XNA. Elle connaît : la durée totale du jeu, le temps écoulé sur votre PC (si la fenêtre est en arrière plan le timer du jeu s'arrête mais pas celui du PC), le temps écoulé depuis le dernier appel à <strong>Update()</strong>, et tout ça dans toutes les unités possibles...</p>
<p style="text-align: justify;">Bref l'outil rêvé pour avoir des déplacements synchronisés entre la vitesse de la machine et le jeu. En effet, si nos déplacements dépendent du timer, alors si le PC ralentit le jeu ralentira uniformément aussi.</p>
<p style="text-align: justify;">Ce qui va nous intéresser est le temps écoulé (en secondes) depuis le dernier appel à la méthode <strong>Update()</strong>. Nous allons le récupérer sous forme de <em>float </em>dans une variable :</p>
<p style="text-align: left;"><code lang="C#">float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;</code></p>
<p style="text-align: justify;">Vous aurez constaté en tapant ces lignes qu'il y a moult attributs / méthodes disponibles qui pourront vous servir. Consultez la documentation (Touche F1 dans Visual Studio sur un élément) pour bien connaître le fonctionnement de chaque élément.</p>
<p style="text-align: justify;">Nous avons toutes les cartes en main pour mettre à jour les coordonnées de l'image (Pour mieux vous situez, j'ai remis la mise à jour de l'angle et de <strong>dst</strong>) :</p>
<p style="text-align: left;"><code lang="C#"> protected override void Update(GameTime gameTime)
{
...
rotation += 0.01f;
...
float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
int deplacementX = (int)(speed.X * elapsedTime);
int deplacementY = (int)(speed.Y * elapsedTime);
location.X += deplacementX;
location.Y += deplacementY;
dst.X = (int)location.X;
dst.Y = (int)location.Y;
...
}</code></p>
Essayez !
<p style="text-align: center;"><a href="http://uppix.net/9/c/c/742ded1c5f72fff71727a9f01547d.html"><img src="http://uppix.net/9/c/c/742ded1c5f72fff71727a9f01547d.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<p style="text-align: justify;">Le cactus se fait la malle ! Logique : les vitesses sont positives et nous n'avons mis aucune notion de collision.</p>
<p style="text-align: justify;">En tout cas, vous en conviendrez : votre cactus bouge. Essayez de modifier les valeurs du vecteur vitesse pour comprendre son impact.</p>

<h3 style="text-align: justify;">Collisions avec l'écran</h3>
<p style="text-align: justify;">Faisons maintenant rebondir notre cactus sur le bord de l'écran.</p>
<p style="text-align: justify;">Quand notre image arrive à un bord, il faut le détecter et inverser une de ses trajectoires (ici, sa vitesse).Rien de bien compliqué :</p>
<p style="text-align: left;"><code lang="C#">if ((location.X &lt; 0) || (location.X &gt; 800))
{
speed.X = -speed.X;
}
if ((location.Y &lt; 0) || (location.Y &gt; 600))
{
speed.Y = -speed.Y;
}</code></p>
<p style="text-align: justify;">L'écran de base ayant une résolution de 800*600, vous devriez voir votre cactus rebondir :)</p>
<p style="text-align: justify;">Vous pouvez récupérer la valeur exact de la résolution grâce à l'attribut <strong>graphics </strong>et à ses propriétés <strong>PreferredBackBufferWidth </strong>/ <strong>PreferredBackBufferHeight </strong>(entre autre moyen)</p>
<p style="text-align: justify;">Ici on regarde la collision pour le point (0,0) de l'image, mais rien de vous empêche d'utiliser la taille de l'image pour faire en sorte qu'elle ne sorte jamais de l'écran.</p>
<p style="text-align: justify;">Dans la prochaine partie on utilisera les entrées utilisateur (claviers et manettes) pour améliorer notre jeu, et je vous présenterai les quelques sorties possibles brièvement.</p>