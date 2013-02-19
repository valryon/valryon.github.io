<p style="text-align: center;"><a href="http://uppix.net/d/c/6/44a05860775df6df3b126d0f13fc3.html"><img class="aligncenter" src="http://uppix.net/d/c/6/44a05860775df6df3b126d0f13fc3.png" alt="Image hosted by uppix.net" border="0" /></a></p>

<h3 style="text-align: left;"><span style="color: #008000;">EDIT : Cet article a été mis à jour pour XNA 4.0</span></h3>
<h3 style="text-align: left;"><strong>Objectifs :</strong></h3>
<p style="text-align: left;">Pour cette deuxième partie nous allons nous intéresser à la création d'un programme affichant "Hello World" à l'écran en utilisant XNA.</p>

<h3 style="text-align: left;"><strong>Sommaire :</strong></h3>
<ul>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-1-installation-et-decouverte/">Installation et découverte</a></li>
	<li><a title="Hello World" href="http://www.valryon.fr/didacticiel-xna-partie-2-hello-world/">&gt;&gt;&gt;&gt;&gt;Hello World</a></li>
	<li><a title="Troisième partie : affichage" href="http://www.valryon.fr/didacticiel-xna-partie-3-affichage-dimages-de-sprites-de-backgrounds/">Affichage d'images, de sprites, de backgrounds</a></li>
	<li><a href="../didacticiel-xna-partie-4-deplacements-collisions-rotations/">Déplacements, collisions, rotations</a></li>
	<li><a href="../didacticiel-xna-partie-5-%E2%80%93-entreessorties/">Entrées / Sorties</a></li>
	<li><a title="Squelette" href="../didacticiel-xna-partie-6-%E2%80%93-squelette-generique-dun-jeu-2d">Squelette générique d'un jeu 2D</a></li>
</ul>
<!--more-->
<h2>XNA : "Hello World"</h2>
<h3 style="text-align: left;">Création d'un nouveau projet</h3>
<p style="text-align: left;">Rentrons dans le vif du sujet. Commencez par créer un nouveau projet sur Visual Studio :</p>
<p style="text-align: center;"><a href="http://uppix.net/2/8/e/562d70577d1ca8d58a6c272fcafd9.html"><img src="http://uppix.net/2/8/e/562d70577d1ca8d58a6c272fcafd9tt.jpg" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Vous vous retrouvez alors avec une classe déjà créée, remplie et ouverte sous vos yeux : Game1.cs. Votre solution contient aussi différents éléments. Assurez-vous déjà que tout marche bien en lançant votre premier jeu. Vous devriez obtenir un écran bleu. Pas le BSOD de Windows heureusement, mais un beau bleu dans une fenêtre :</p>
<p style="text-align: center;"><a href="http://uppix.net/a/9/d/acf4558356beae8d963de528d8591.html"><img class="aligncenter" src="http://uppix.net/a/9/d/acf4558356beae8d963de528d8591tt.jpg" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Tout est prêt pour attaquer.</p>

<h3 style="text-align: justify;">La classe Game</h3>
<p style="text-align: justify;">La base de tout jeu XNA est cette classe. Elle est lancée par la méthode Main contenue dans la classe <strong>Program</strong>, et elle définie un ensemble de méthodes qui simplifie l'organisation de son code.</p>
<p style="text-align: center;"><a href="http://uppix.net/a/2/0/0df81ab3049040fe2573d6686e261.html"><img class="aligncenter" src="http://uppix.net/a/2/0/0df81ab3049040fe2573d6686e261.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Le schéma ci-dessus vous présente l'ordre d'appel des méthodes.</p>

<ul style="text-align: justify;">
	<li>Le constructeur (<strong>Game1()</strong>) est appelé en premier une seule fois.</li>
	<li>La méthode <strong>Initialize()</strong> sert à initialiser les variables du jeu (score, vies, etc). Elle est appelée juste après le constructeur, mais rien ne vous empêche de l'appeler par la suite.</li>
</ul>
<ul style="text-align: justify;">
	<li><strong>LoadContent()</strong> est primordiale. XNA utilise un Pipeline (tunnel) qui permet d'importer n'importe quoi (Sprite, image, modèle 3D, son, etc) dans le jeu très simplement. C'est dans cette méthode que doivent être chargées toutes les images, tous les sons, tous les médias nécessaires à votre jeu. Et tous ces médias doivent se trouver dans le dossier "Content" qui a été créé avec la solution. C'est un dossier spécial qui sera traité par le Pipeline.</li>
</ul>
<ul style="text-align: justify;">
	<li>Un jeu est un programme composé principalement d'une boucle infinie. Cette boucle s'arrête quand l'utilisateur ferme le jeu. Chaque tour de boucle compose une Frame qui est affichée à l'écran. Pour qu'un jeu soit fluide il faut essayer d'avoir 60 Frames par secondes.
XNA synchronise deux threads en parallèle : la mise à jour du modèle (<strong>Update()</strong>) et de la vue (<strong>Draw()</strong>)<strong> </strong>. Ils tournent donc à la même vitesse et sont appelés une fois par frame (donc en moyenne 60 fois par secondes, excepté sur Windows Phone 7 qui est à ~30 frames par secondes).</li>
</ul>
<ul style="text-align: justify;">
	<li><strong>UnloadContent() </strong>peut servir à effectuer des actions particulières lors de la fermeture du jeu. Par défaut, les objets en mémoire sont désalloués.</li>
</ul>
<p style="text-align: justify;">Cette classe possède également deux attributs :</p>

<ul style="text-align: justify;">
	<li><em>GraphicsDeviceManager</em> graphics</li>
	<li><em>SpriteBatch</em> spriteBatch</li>
</ul>
<p style="text-align: justify;">On ne s'intéressera pour l'instant qu'au deuxième qui permet d'afficher à l'écran des textures (un sprite étant une texture appliqué sur un plan).</p>

<h3>Afficher du texte</h3>
<p style="text-align: justify;">Autant vous prévenir tout de suite, XNA gère mal l'affichage du texte à l'écran. Cela n'empêche pas qu'il y a quelques méthodes existantes pour le faire.</p>
<p style="text-align: justify;">La première chose à faire est de créer une police d'écriture qui sera chargée et utilisée par le jeu.</p>
<p style="text-align: center;"><a href="http://uppix.net/8/f/2/3e865dec347381bd06ecee30d23d2.html"><img src="http://uppix.net/8/f/2/3e865dec347381bd06ecee30d23d2tt.jpg" alt="Image hosted by uppix.net" border="0" /></a></p>

<ul>
	<li>Dans le dossier <strong>Content</strong>, clic droit</li>
	<li>Ajouter un nouveau élément</li>
	<li>Créer une nouvelle police : "<strong>Sprite Font</strong>"</li>
</ul>
<p style="text-align: justify;">Visual Studio vous ouvre alors le fichier spriteFont (XML), qui définit la taille, la police utilisée et d'autres informations modifiables si besoin.</p>
<p style="text-align: justify;">Ajoutez  un attribut SpriteFont à votre classe Game1.</p>
<p style="text-align: left;"><code lang="C#">GraphicsDeviceManager graphics;
SpriteBatch spriteBatch;
SpriteFont font;</code></p>
<p style="text-align: left;">Il faut ensuite charger la police avec le Pipeline (aussi appelé ContentManager). Dans LoadContent, ajoutez :</p>
<p style="text-align: left;"><code lang="C#">font = Content.Load&lt;SpriteFont&gt;(@"SpriteFont1");</code></p>
<p style="text-align: justify;">Vous remarquerez :</p>

<ul style="text-align: justify;">
	<li>Le "@" permet d'entrer dans la chaîne des caractères spéciaux sans qu'il ne soient interprétés (les "\" par exemple)</li>
	<li>L'absence d'extension pour le fichier à charger. En effet le ContentManager transforme tout et l'accès aux fichiers transformés se fait sur le nom (<em>asset name</em> dans les propriétés) simplement.</li>
</ul>
<p style="text-align: justify;">Enfin, il faut ajouter au programme l'affichage d'un texte avec cette police.</p>
<p style="text-align: justify;">Dans la méthode<strong> Draw()</strong>, ajoutez :
<code lang="C#">
spriteBatch.Begin();
spriteBatch.DrawString(font, "Hello World !", new Vector2(), Color.Yellow);
spriteBatch.End();</code></p>
<p style="text-align: justify;">Le <strong>spriteBatch </strong>est donc l'élément clé de tout affichage. Avant toute utilisation, il doit être initialisé avec <strong>Begin() </strong>(nous verrons plus tard les paramètres optionnels), et il doit être clos avec la méthode <strong>End()</strong>.</p>
<p style="text-align: justify;">Entre ces deux appels, vous pouvez faire différents affichages (qui utiliseront les paramètres spécifiés dans <strong>Begin()</strong>, mais ici, on s'en fiche :) ) de textes avec <strong>DrawString()</strong> ou de textures avec <strong>Draw()</strong>.</p>
<p style="text-align: justify;">Pour afficher notre "Hello World" nous utiliserons donc la première méthode, en lui passant en paramètre :</p>

<ul style="text-align: justify;">
	<li>La police (SpriteFont) à utiliser</li>
	<li>Le texte</li>
	<li>La position du texte, sous la forme d'un Vector2. Ici j'utilise un vecteur par défaut, à (0,0) mais vous pouvez changer ça dans les paramètres du constructeur.</li>
	<li>La couleur du texte. XNA définit une palette de couleurs conséquente dans la classe Color.</li>
</ul>
<p style="text-align: justify;">Vous obtiendrez un magnifique (tout est relatif) texte :</p>
<p style="text-align: center;"><a href="http://uppix.net/d/a/c/09d38169a506a2fe94d0a64088656.html"><img class="aligncenter" src="http://uppix.net/d/a/c/09d38169a506a2fe94d0a64088656tt.jpg" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Nous allons voir par la suite comment afficher des images quelconques, des sprites ou des fonds d'écrans par exemple :)</p>