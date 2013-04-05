<p style="text-align: center;"><a href="http://uppix.net/9/3/1/8f98e2dcc2bbb0f87f286b706a9f8.html"><img class="aligncenter" src="http://uppix.net/9/3/1/8f98e2dcc2bbb0f87f286b706a9f8.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<strong><span style="color: #008000;">EDIT : Cet article a été mis à jour pour XNA 4.0</span></strong>
<h3 style="text-align: left;"><strong>Objectifs :</strong></h3>
<p style="text-align: left;">Cette partie est dédiée à tout ce qui peut remplir la méthode <strong>Update()</strong> : collisions et déplacements en particulier.</p>

<h3 style="text-align: left;"><strong>Sommaire :</strong></h3>
<ul>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-1-installation-et-decouverte/">Installation et découverte</a></li>
	<li><a title="Hello World" href="http://www.valryon.fr/didacticiel-xna-partie-2-hello-world">Hello World</a></li>
	<li><a title="Troisième partie : affichage" href="http://www.valryon.fr/didacticiel-xna-partie-3-affichage-dimages-de-sprites-de-backgrounds/">Affichage d'images, de sprites, de backgrounds</a></li>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-4-deplacements-collisions-rotations/">Déplacements, collisions, rotations</a></li>
	<li><a title="Partie 5" href="http://www.valryon.fr/didacticiel-xna-partie-5-–-entreessorties/">&gt;&gt;&gt;&gt;&gt;Entrées / sorties</a></li>
	<li><a title="Squelette" href="../didacticiel-xna-partie-6-%E2%80%93-squelette-generique-dun-jeu-2d">Squelette générique d'un jeu 2D</a></li>
</ul>
<!--more-->
<h2 style="text-align: center;">XNA : Gestion des entrées / sorties pour un jeu</h2>
<p style="text-align: justify;">C'est bien beau d'avoir un cactus qui se balade tout seul sur l'écran, mais c'est quand même un jeu qui l'on souhaite produire. Donc quelque chose qui propose un minimum d'interaction avec l'utilisateur.</p>
<p style="text-align: justify;">XNA est prévu pour PC et Xbox 360 et est développé par Microsoft, il n'est donc pas surprenant que le clavier, la souris et le GamePad xbox360 soit supporté nativement (ce dernier pouvant être relié à un PC). <span style="color: #ff0000;"><strong>Nous ne parlerons pas ici de l'utilisation du tactile pour Windows Phone 7 (mais certains principes restent vrais !)</strong></span></p>
<p style="text-align: center;"><a href="http://uppix.net/8/0/f/1d4a5d75a455eb1c09fecca50fb54.html"><img class="aligncenter" src="http://uppix.net/8/0/f/1d4a5d75a455eb1c09fecca50fb54t.jpg" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Si par contre vous voulez gérer des manettes autres, branchées sur le port USB par exemple, là c'est une autre histoire... XNA ne propose rien pour cela.</p>
<p style="text-align: justify;"><span style="color: #3366ff;"><strong>EDIT : Mais ce n'est pas forcément compliqué ! Merci à J-F. Sébum de Canard PC pour son code, disponible ici :
-&gt; h<a title="Gérer joystick joypad XNA" href="http://www.irrlicht.fr/forum/viewtopic.php?id=64">ttp://www.irrlicht.fr/forum/viewtopic.php?id=64</a></strong></span></p>
<p style="text-align: justify;"><span style="color: #3366ff;"><strong>Vous y trouverez un bout de code permettant dé gérer sous Windows toute sorte de "bâtons de joie" (joysticks et joypads). Je peux également vous envoyer un exemple d'utilisation (de TGPA) sur demande.</strong></span></p>
<p style="text-align: justify;">Gardez toujours à l'esprit que nous nous intéressons ici uniquement à ce qu'XNA propose, et donc par extension ce qu'il est possible de faire sur Xbox et Windows Phone. Sur PC vous avez accès à toute l'API .NET et il est donc possible de <em>tout</em> faire.</p>
<p style="text-align: aligncenter;"><strong>Le clavier</strong></p>
La gestion du clavier en XNA est à la fois simple et catastrophique. Simple car il suffit de récupérer un objet pour avoir accès à toutes les touches enfoncées / relevées, etc.

<code lang="C#">
//Entrée joueur : clavier
KeyboardState keyboard = Keyboard.GetState();
if (keyboard.IsKeyDown(Keys.Left))
{
//...
}</code>
<p style="text-align: justify;">L'énumération <strong>Keys </strong>contient toutes les touches que vous pouvez trouver sur un clavier actuel, même les touches de contrôle de médias comme la gestion du volume.</p>
<p style="text-align: justify;">Difficile de s'étendre longtemps sur l'utilisation donc. Par contre si vous voulez faire de la saisie de texte (le nom du joueur par exemple), alors là bon courage ! XNA ne gère pas de composant tout magique comme un champ de texte, il va falloir vous créer le votre, et gérer aussi bien l'ajout de caractères que les retours arrières, etc. J'imagine que c'est trouvable tout fait sur Internet mais c'est quand même dommage de ne rien avoir par défaut.</p>

<h3 style="text-align: justify;">La souris</h3>
<p style="text-align: justify;">Je serai bref : la souris se gère comme le clavier :</p>
<p style="text-align: justify;"><code lang="C#">//Entrée joueur : souris
MouseState mouse = Mouse.GetState()
if (mouse.LeftButton == ButtonState.Released)
{
...
}
</code></p>
<p style="text-align: justify;"><strong>MouseState </strong>possède comme propriétés les différents boutons que vous pouvez espérer trouver pour une souris normale.</p>
<p style="text-align: justify;">Petit truc qui peut servir, il est facile de forcer le curseur de la souris à se placer à une position précise de l'écran grâce à:</p>
<p style="text-align: left;"><code lang="C#">Mouse.SetPosition(x,y)</code>;</p>

<h3 style="text-align: left;">Manette Xbox 360</h3>
<p style="text-align: justify;">Le périphérique le mieux géré à mon avis par XNA est bien la manette de Microsoft. Pas besoin de chercher pourquoi, mais il faut admettre que ce contrôleur est plutôt bien. XNA permet de gérer jusqu'à 4 manettes en même temps : idéal pour un jeu multijoueurs en écran splitté.</p>
<p style="text-align: justify;">Sans surprise la récupération des informations pour une manette est similaire aux autres périphériques :</p>
<code lang="C#">GamePadState pad = GamePad.GetState(PlayerIndex.One);</code>
<p style="text-align: justify;">Notez que <strong>PlayerIndex </strong>est un simple entier valant entre 0 et 3. Il correspond au numéro du joueur qui tient la manette.</p>
<p style="text-align: justify;"><strong>pad </strong>contient :</p>

<ul style="text-align: justify;">
	<li>Les flèches directionnelles : <strong>Dpad</strong></li>
	<li>Les boutons<strong> </strong>de la manette, et des méthodes<strong> isButtonDown/Up() </strong>pour connaître leur état</li>
	<li>Le joystick gauche : <strong>ThumbSticks.Left</strong></li>
	<li>et droite : <strong>ThumbSticks.Right</strong></li>
	<li>Les gachettes : Gauche et droite respectivement <strong>Triggers.Left</strong> et <strong>Triggers.Right</strong></li>
</ul>
<p style="text-align: justify;">Que demander de plus ?</p>
<p style="text-align: justify;">Les vibrations bien sûr !</p>
<p style="text-align: justify;">Elles sont ajustables grâce à :</p>
<code lang="C#">GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);</code>
<p style="text-align: justify;">Où 1.0f est une valeur arbitraire qui peut être ajuster pour correspondre à la sensation souhaitée. Si vous essayez cette méthode, vous remarquerez que les vibrations<span style="text-decoration: underline;"><strong> ne s'arrêtent jamais</strong></span> : c'est normal ! Il faut soi-même la faire décroître en ré-appelant <strong>SetVibration(..)</strong> tout en faisant décroître la valeur de la secousse.</p>

<h3>Une bonne manière de gérer plusieurs périphériques</h3>
<p style="text-align: justify;">Laisser le choix des armes à son joueur vous fera toujours gagner quelque points de sympathie avec ce dernier (Même si ce n'est applicable que sur PC). Une bonne manière de gérer plusieurs entrées de manière très simple est la suivante :</p>

<ul style="text-align: justify;">
	<li>Vous contrôlez les différents états des périphériques</li>
	<li>En fonction des boutons / directions, vous mettez des variables booléennes à vrai</li>
	<li>En fonction de ces variables, vous effectuez des actions</li>
</ul>
<p style="text-align: justify;">Soit, pour mieux comprendre :</p>
<code lang="C#">KeyboardState keyboard = Keyboard.GetState();
MouseState mouse = Mouse.GetState();
GamePadState pad1 = GamePad.GetState(PlayerIndex.One);</code>

<code lang="C#">bool tirer = false;</code>

if (keyboard.IsKeyDown(Keys.Space)) tirer = true;
if (mouse.LeftButton == ButtonState.Pressed) tirer = true;
if (pad1.IsButtonDown(Buttons.A)) tirer = true;

&gt;if (tirer)
{
///...
}
<p style="text-align: justify;">On peut encore factoriser ça mais c'est pour que le code reste lisible. Vous séparez ainsi l'acquisition de l'action du joueur par un contrôleur et le traitement de cet action.</p>
<p style="text-align: justify;">Mais il y a un problème avec cette solution : tant que le bouton est appuyé, la condition est vraie. Donc l'action <em>tirer</em> sera ici vraie pendant plusieurs secondes, ce qui signifie plusieurs centaines de frames !</p>
<p style="text-align: justify;">Autrement dit le chargeur de votre arme va se vider bien vite... il va falloir ajouter un délai et/ou affiner la détection d'appui des touches.</p>

<h4>Première idée : le délai</h4>
<p style="text-align: justify;">Déclarez dans votre classe (pas dans la méthode donc) :</p>
<span style="font-family: monospace;">float tirerAttente = 0f;</span>

&nbsp;

Dans <strong>Update()</strong> :
<p style="text-align: justify;">...</p>
<code lang="C#">
if(tirerAttente &gt; 0f) tirerAttente -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
if(tirer &amp;&amp; tirerAttente &lt;= 0f) {
tirerAttente = 1500f; //1,5 sec d'attente entre deux tirs
...
}</code>
<p style="text-align: justify;">Le tir n'est donc possible au mieux que toutes les 1500ms, peut importe ce que le joueur fait avec sa manette / son clavier / son téléphone.</p>

<h4>Deuxième idée : affiner la détection</h4>
<p style="text-align: justify;">Le plus simple (quoique jugé non professionnel, ne me demandez pas pourquoi c'est ce que l'on m'a dit pour TGPA) est de détecter le relâchement d'une touche.</p>
<p style="text-align: justify;">Il suffit à chaque frame de sauver l'état précédent des périphériques.</p>
<p style="text-align: justify;">Déclarez de nouvelle variable dans votre classe :</p>
<p style="text-align: justify;"><code lang="C#">KeyboardState precKeyboard;
MouseState precMouse;
GamePadState precPad1;</code></p>
<p style="text-align: justify;">Et ajouter à la fin de votre méthode <strong>Update()</strong> :</p>
<p style="text-align: justify;"><code lang="C#">...
precKeyboard = keyboard;
precMouse = mouse;
precPad1 = pad1 ;</code></p>
&nbsp;

Grâce à cela, vous pouvez désormais tester l'appui puis le relâchement d'une touche, et pas seulement l'appui.

<code lang="C#">if ((precKeyboard.IsKeyDown(Keys.Space)) &amp;&amp; (keyboard.IsKeyUp(Keys.Space))) tirer = true;
if ((precMouse.LeftButton == ButtonState.Pressed) &amp;&amp; (mouse.LeftButton == ButtonState.Released)) tirer = true;
if ((pad1.IsButtonDown(Buttons.A)) &amp;&amp; (pad1.IsButtonUp(Buttons.A))) tirer = true;</code>

Bien sur cela est à adapter à vos besoins.
<h3>Les sorties</h3>
<p style="text-align: justify;">Les sorties possibles ne sont pas l'intérêt principal d'un jeu vidéo. Sauf pour le partage de score, et de quelques autres données par Internet, on ne peut pas dire que cet élément soit crucial pour le jeu.</p>
Outre l'écran, il est possible (même sur Xbox) de lire/écrire des fichiers (texte, binaires ou XML).

Il est aussi possible d'envoyer / recevoir des données par Internet, mais n'ayant jamais traité cette partie je vous invite à chercher par vous-mêmes.

<strong>Les fichiers</strong>

Important pour la Xbox/WP7 : pour accéder aux fichiers stockés dans votre solution, comme vous ne pouvez pas savoir où ils sont stockés, il vous faut utiliser une classe qui le sait :

<code lang="C#">System.IO.Stream stream = TitleContainer.OpenStream("Nom de fichier");</code>

Une fois ce flux ouvert vous pouvez le lire, par exemple avec le parseur XML détaillé plus loin.

Pour gérer vos sauvegardes, je vous conseille la librairie <a href="http://easystorage.codeplex.com/">EasyStorage</a>. Une discussion sur ce sujet vous permettra de mieux comprendre comment l'utiliser, ici :
<a href="http://www.dev-fr.org/index.php/topic,5227.msg49119/topicseen.html">http://www.dev-fr.org/index.php/topic,5227.msg49119/topicseen.html</a>
<h4>Les fichiers textes</h4>
<p style="text-align: justify;">Idéal pour lire des fichiers contenant du texte (sous-titres, descriptions, format de niveau).</p>
<p style="text-align: justify;">On utilisera les classes <strong>StreamReader </strong>/ <strong>StreamWriter </strong>de l'assembly System.IO.</p>
Exemple pour la lecture :

<code lang="C#">StreamReader sr = new StreamReader(chemin);
while(String ligne = sr.ReadLine()) {
//Traitement de la ligne
};
</code>
<h4>Les fichiers binaires</h4>
Pour lire et sauver des objets instanciés, il est possible de les stocker dans un fichier.

Il suffit d'utiliser les classes <strong>BinaryReader </strong>/ <strong>Binary</strong><strong>Writer </strong>de l'assembly <strong>System.IO</strong>.

Même principe que pour le fichier texte.
<h4>Les fichiers XML</h4>
<p style="text-align: justify;">Le framework 4.0 fournit une nouvelle classe pour le XML qui rend très facile le parsage.</p>
<p style="text-align: justify;">Pour lire un fichier XML :</p>
<code lang="C#">XElement element = XElement.Load(TitleContainer.OpenStream(filename));</code>
<p style="text-align: justify;">Vous pouvez ensuite rechercher des éléments très simplements, un peu comme avec XPath :</p>
<code lang="C#">XElement e = element.Element("monElement");</code>

L'élément récupéré peut lui aussi être exploré de la même manière, et ainsi de suite. Pour accéder à un attribut (ici sa valeur en plus) :

<code lang="C#">e.Attribute("monAttribut").Value</code>
<strong>Conclusion</strong>
<p style="text-align: justify;">Vous avez peut-être l'impression que je suis allé trop vite, et, oui, je pense aussi que je ne me suis pas attardé. Il existe une tonne d'aide pour la gestion des fichiers, qui est déjà très simplifiée en .NET, donc plutôt que de vous expliquer la roue je préfère me contenter de vous la montrer.</p>
<p style="text-align: justify;">Vous aurez aussi compris que la gestion des contrôles en XNA est très simple et agréable, même si cela pourrait être encore mieux (gestion d'un délai entre deux appuis de touches, gestion automatique de l'atténuation des vibrations, etc).</p>
A la demande de <a href="http://lapinoufou.olympe-network.com/">LapinouFou </a>le prochain article devrait être sur la structure du code pour un petit jeu telle que je la conçois, même si ce n'est qu'une possibilité parmi une infinité.
<h3>Annexe</h3>
<p style="text-align: justify;">Le code source de cette partie combiné à la partie précédente est disponible ici :
=&gt; <a title="http://nopaste.info/6d57f48de4.html" href="http://nopaste.info/6d57f48de4.html">http://nopaste.info/6d57f48de4.html</a></p>