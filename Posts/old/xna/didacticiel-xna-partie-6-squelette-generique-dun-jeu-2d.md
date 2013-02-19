<p style="text-align: center;"><a href="http://uppix.net/a/0/2/effd4f3f3f3c34fc2c6c6bf462cbb.html"></a><a href="http://uppix.net/4/0/7/ea44aa973d393957e230fc803d20a.html"><img src="http://uppix.net/4/0/7/ea44aa973d393957e230fc803d20a.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<p style="text-align: justify;">Après un temps d'absence je vais essayer de me motiver à terminer ce didacticiel...</p>
<p style="text-align: justify;">Cette partie est totalement subjective (je ne suis qu'un étudiant sans expérience pro dans le domaine du jeu vidéo donc ne prenez pas cet article comme un bout de science exacte) et vous donnera une structure possible pour votre code.</p>

<h3 style="text-align: justify;"><strong>Objectifs :</strong></h3>
<p style="text-align: justify;">Avoir une idée <strong>avant </strong>le développement de la structure de son code pour un jeu quelconque (testé pour de la 2D en tout cas).</p>

<h3><strong>Sommaire :</strong></h3>
<ul>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-1-installation-et-decouverte/">Installation et découverte</a></li>
	<li><a title="Hello World" href="http://www.valryon.fr/didacticiel-xna-partie-2-hello-world">Hello World</a></li>
	<li><a title="Troisième partie : affichage" href="http://www.valryon.fr/didacticiel-xna-partie-3-affichage-dimages-de-sprites-de-backgrounds/">Affichage d'images, de sprites, de backgrounds</a></li>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-4-deplacements-collisions-rotations/">Déplacements, collisions, rotations</a></li>
	<li><a title="Partie 5" href="http://www.valryon.fr/didacticiel-xna-partie-5-–-entreessorties/">Entrées / sorties</a></li>
	<li><a title="Squelette" href="../didacticiel-xna-partie-6-%E2%80%93-squelette-generique-dun-jeu-2d">&gt;&gt;&gt;&gt;&gt;Squelette générique d'un jeu 2D</a></li>
</ul>
<em>Je ne suis pas très satisfait de cet article, aidez-moi à l'améliorer en m'indiquant ce qui cloche... J'ai un peu perdu la motivation d'écrire et c'est sans doute l'un des plus gros problèmes.</em>

<!--more--><img title="Lire la suite…" src="http://www.valryon.fr/wp-includes/js/tinymce/plugins/wordpress/img/trans.gif" alt="" />
<h2 style="text-align: center;">XNA : Squelette générique d'un jeu 2D</h2>
<p style="text-align: justify;">Votre code va comporter plusieurs grandes parties :</p>

<ul style="text-align: justify;">
	<li>Des états : en jeu, en pause, menu.</li>
	<li>Différents moteurs : jeu, physique, graphique, son.</li>
	<li>Des éléments instanciables pour ces moteurs : ennemis, joueurs.</li>
	<li>Du contenu pour le jeu : niveaux, scripts.</li>
</ul>
<p style="text-align: justify;">Chacune de ces parties correspond donc à une pièce maîtresse de votre code, et s'imbrique avec une ou plusieurs autres.</p>

<h3>Une machine à état</h3>
<p style="text-align: justify;">Aussi complexe que puisse être un jeu vidéo, on peut résumer son fonctionnement à une machine à état très simple. Chaque état représente un "écran" à afficher au joueur, comme l'écran titre ou le jeu en lui-même.</p>
Un exemple d'une énumération d'états pour un jeu :
<code lang="C#"> ///
/// State of this program
///
public enum GameState
{
Exit, //Exit program
TitleScreen, //First screen
OptionsScreen, //Options to configure the game
LevelSelectionScreen, //Choose the level to play
Game, //The game
EndLevel //Win or lose
}</code>
<p style="text-align: justify;">Bien sûr les états sont à adapter à chaque jeu. Il nous faut ensuite de stocker l'état du jeu dans une variable (qu'il faut penser à initialiser, par exemple à l'écran titre).
<code lang="C#"> ///
/// Game state, or screen to display
///
private GameState gameState;</code></p>
<p style="text-align: justify;">Ensuite il faut analyser cette variable dans les méthodes<strong> Update()</strong> et <strong>Draw() </strong>.</p>
<p style="text-align: justify;">Une série de if/else ou un bon switch fera l'affaire.<strong> </strong>Selon l'état on va demander au programme d'afficher ce fond et ces éléments, ou de regarder les collisions sur la scène en cours, etc.
<code lang="C#">
public void Draw(...){
if(gameState == GameState.TitleScreen) {
//Afficher écran titre
//Afficher une animation d'intro
}
else if(gameState == GameState.Game) {
//Afficher les backgrounds
//Afficher le joueur, les ennemis, etc
}
}</code></p>
<p style="text-align: justify;">Notez que l'énumération n'est pas une manière très objet de programmer. Il est possible de faire mieux en définissant une classe  abstraite GameState contenant des méthodes Update() / Draw()  / etc et des sous-classes héritant de GameState que la classe principale Game se chargera d'appeler en temps et en heure.</p>
<p style="text-align: center;"><a href="http://uppix.net/b/2/6/58fe6b42480d94751568a7d046b6e.html"><img class="aligncenter" src="http://uppix.net/b/2/6/58fe6b42480d94751568a7d046b6e.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<p style="text-align: justify;">Cela se traduit avec comme code :</p>
<code lang="C#">
private GameState gameState;
private IngameState ingameState;
private TitleScreenState tsState;</code>

public void Initialize(...) {
...
ingameState = new IngameState (...);
tsState = new TitleScreenState (...);
gameState = tsState ;
...
}

public void Update(...) {
...
gameState.Update(...)
...
}

public void Draw(...) {
...
gameState.Draw(...)
...
}
<p style="text-align: justify;">Le principal problème de cette architecture vient du passage de paramètre entre vos états. Mais tout est modulable, à vous de ruser et de trouver ce qui convient le mieux à votre problème.</p>

<h3 style="text-align: justify;">Moteurs de jeu</h3>
<p style="text-align: justify;">Les moteurs de jeux sont la base technique de votre application. C'est tout simplement ce qui va calculer le scrolling de vos décors ou le déplacement du joueur. C'est donc particulièrement vaste. Par défaut la classe Game est un moteur de tout et de rien. Si vous affichez avec Draw () vos éléments, que vous calculez vos collisions ou que vous lancer la lecture d'un son dans Update(), alors Game est à la fois moteur graphique / physique et son.</p>
<p style="text-align: justify;">Selon la taille de votre jeu il peut être intelligent de découper ces tâches dans des classes distinctes. A l'inverse, un petit jeu sera beaucoup moins lisible avec un trop grand découpage en classe. Tout est à réfléchir.</p>
<p style="text-align: justify;">On peut par exemple imaginer le moteur de son / musique, qui possède de nombreuses méthodes statiques pour lancer la lecture ou changer le volume.</p>

<h3 style="text-align: justify;">Éléments instanciables</h3>
<p style="text-align: justify;">Le plus intéressant. Vous remarquerez vite que tout ce qui est à l'écran possède des propriétés communes : position dans le plan / l'espace, texture utilisée, points de vie restants, vecteur vitesse, etc. Il est intéressant de factoriser ces informations dans une super classe (une sorte d'<strong>Object </strong>mais pour votre jeu), classe qui pourra être utilisée dans les paramètres de vos méthodes / moteurs rendant ainsi tout cela très générique et simple à programmer.</p>
<p style="text-align: center;"><a href="http://uppix.net/7/8/c/6cc1a452e5b0466012342d79396ff.html"><img class="aligncenter" src="http://uppix.net/7/8/c/6cc1a452e5b0466012342d79396ff.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<p style="text-align: justify;">Ici un objet Player, Enemy ou BackgroundElement posséde un attribut location, speed, hp et sprite grâce à l'héritage et des attributs propres. L'héritage est ici une technique particulièrement utile.</p>

<h3 style="text-align: justify;">Contenu</h3>
<p style="text-align: justify;">Si votre jeu dépasse le simple stade du prototype, il est probable qu'il contienne plusieurs niveaux. La définition d'un niveau (d'une map) est spécifique à chaque jeu. Un niveau est parfois un tableau rempli de valeurs particulières comme pour le Sudoku, et parfois une structure de données complexes avec les textures pour l'affichage de la scène, le stockage des ennemis à afficher et à venir, les éventuels scripts à déclencher, etc.</p>

<h3 style="text-align: justify;">Conclusion</h3>
<p style="text-align: justify;">Voici pour conclure le résumé en UML de cette partie :</p>
<p style="text-align: center;"><a href="http://uppix.net/3/f/d/f48a78fae51af5f0c8dfb0616cb3b.html"><img class="aligncenter" src="http://uppix.net/3/f/d/f48a78fae51af5f0c8dfb0616cb3b.png" border="0" alt="Image hosted by uppix.net" /></a></p>
<p style="text-align: justify;">Encore une fois c'est une possibilité qui doit être adaptée à chaque jeu. Cette partie étant très vague et très courte, postez vos questions ou vos demandes d'aides pour une architecture en commentaire pour qu'on en discute.</p>