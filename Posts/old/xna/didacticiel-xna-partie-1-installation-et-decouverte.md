<p style="text-align: center;"><a href="http://uppix.net/b/8/2/3c8e64650b9c76eb2003532e65c20.html"><img src="http://uppix.net/b/8/2/3c8e64650b9c76eb2003532e65c20.png" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: left;"><strong><span style="color: #008000;">EDIT : Cet article a été mis à jour pour XNA 4.0</span></strong></p>
Partant d'une idée soumise par un membre actif de <a href="http://www.dev-fr.org">dev-fr</a>, je vous propose une série d'articles permettant de s'initier au framework XNA. N'hésitez pas à poser vos questions, remarques éventuelles ou critiques sur ces articles pour que je les améliore ;).
<h3 style="text-align: left;"><strong>Objectifs :</strong></h3>
<p style="text-align: justify;">Au bout de ce didacticiel/tutoriel, vous devriez être capable de manipuler les principaux mécanismes qui rendent XNA intéressants. Ce cours ne s"intéresse qu'à des mécanismes et outils pour des <span style="text-decoration: underline;"><strong>jeux 2D</strong></span>, donc n'espérer pas recréer Team Fortress 2 avec moi.</p>
<p style="text-align: justify;">Je suis en effet plus spécialisé 2D que 3D mais rien ne vous empêche de bûcher d'autres didacticiels (voir la partie "Références") pour apprendre de nouvelles techniques :).</p>
<p style="text-align: justify;">Cette première partie va servir à installer et configurer votre machine pour pouvoir utiliser XNA.</p>
<!--more-->
<h3 style="text-align: left;"><strong>Sommaire :</strong></h3>
<ul>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-1-installation-et-decouverte/">&gt;&gt;&gt;&gt;&gt;Installation et découverte</a></li>
	<li><a title="Hello World" href="http://www.valryon.fr/didacticiel-xna-partie-2-hello-world/">Hello World</a></li>
	<li><a title="Troisième partie : affichage" href="http://www.valryon.fr/didacticiel-xna-partie-3-affichage-dimages-de-sprites-de-backgrounds/">Affichage d'images, de sprites, de backgrounds</a></li>
	<li><a href="../didacticiel-xna-partie-4-deplacements-collisions-rotations/">Déplacements, collisions, rotations</a></li>
	<li><a href="http://www.valryon.fr/didacticiel-xna-partie-5-%E2%80%93-entreessorties/">Entrées / Sorties</a></li>
	<li>Parties plus subjectives : structure du code pour un jeu, techniques diverses (animations, ..., suggérez !)</li>
	<li>Compatibilité Xbox 360</li>
	<li>Pour aller plus loin</li>
	<li>Références</li>
</ul>
<h2 style="text-align: center;">XNA : Installation et découverte</h2>
<p style="text-align: justify;">XNA est un framework, donc un ensemble d'outils mis à la disposition de développeur, basé sur la plate-forme .NET et qui sur-couche DirectX. On a donc la possibilité d'utiliser un langage moderne et simple (C#), d'exploiter simplement l'API de DirectX même en y connaissant rien et d'avoir à notre disposition les mêmes outils que .NET, comme par exemple Visual Studio. L'autre atout d'XNA est la compatibilité du code sur quatre plate-formes : PC, Xbox 360, Windows Phone 7 et Zune. Pour les deux premières, il est possible de développer un seul et même code et de le déployer sur ces deux machines. Les deux dernières sont un peu différentes par leur capacités et leur interface.</p>
<p style="text-align: justify;">Avant de pouvoir s'amuser, il va falloir installer pléthore de composants pour que tout marche bien. Et  il faut également s'assurer que son PC supporte les caractéristiques minimales.</p>

<h3>Configuration requise</h3>
<p style="text-align: justify;">Voici la configuration minimale recommandée par Microsoft :</p>

<ul style="text-align: justify;">
	<li><strong>Système d'exploitation : </strong>Windows Vista Service Pack 1; Windows XP Service Pack 3; Windows 7</li>
	<li><strong>Carte graphique :</strong> DirectX 9.0c et Shader 1.1 (Shader 2.0 recommandé pour certains kits de démarrage).</li>
</ul>
<p style="text-align: justify;">Donc un PC pas trop vieux, mais je confirme que tout ça tourne pas trop mal sur un Netbook (Compaq mini 311c). J'ajouterai qu'il vaut mieux avoir ses pilotes de carte graphique à jour.</p>
<p style="text-align: justify;">L'émulateur Windows Phone 7 est par contre beaucoup plus lourd et vous demandera une machine puissante pour émuler correctement votre programme.</p>

<h3 style="text-align: justify;">Installation</h3>
<p style="text-align: justify;">Trois exécutables sont à récupérer pour un total d'1 Go environ.</p>

<ul style="text-align: justify;">
	<li>Visual Studio C# Express 2010: L'IDE de Microsoft dans sa version gratuite, limitée (pas de support SVN par exemple) mais quand même très puissante et pratique. Téléchargement par <a title="Visual Studio Express 2010" href="http://msdn.microsoft.com/fr-fr/express/aa975050">ICI
</a><span style="color: #0000ff;">Remarque : prenez la version qui vous convient : Windows Phone Developper ou C#</span></li>
	<li>DirectX SDK : Librairies de développement DirectX. Téléchargement <a title="DirectX SDK (en anglais uniquement)" href="http://www.microsoft.com/downloads/details.aspx?FamilyID=24a541d6-0486-4453-8641-1eee9e21b282&amp;displayLang=en">ICI</a></li>
	<li>Le framework XNA : Nécessite que les deux composants précédents soient installés. Téléchargement <a title="XNA" href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=04704acf-a63a-4f97-952c-8b51b34b00ce&amp;displaylang=en">ICI</a></li>
</ul>
<p style="text-align: justify;">Installez ensuite ces trois composants dans l'ordre où je les ai cités. En cas d'erreur, regardez du côté de la documentation Microsoft qui se veut plutôt complète à ce sujet.</p>
<p style="text-align: justify;">Une fois cette étape terminée, vous devriez être prêt à vous lancer dans le développement XNA.</p>

<h3>Découverte</h3>
<div style="text-align: justify;">La version 4.0 d'XNA ne propose plus de StarterKit, à mon grand regret... Je vous en propose donc un petit maison, qui n'est pas d'un niveau incroyable puisque fait en une après-midi mais qui vous montrera quand même un peu de code et un exemple pour PC et Xbox 360.</div>
<div style="text-align: justify;"><em>AnotherTetris</em>, est donc un "jeu" de Tetris codé rapidement. Vous pouvez aligner des pièces, les empiler, les faire tourner. Il y a quelques bugs et la maniabilité n'est pas top (flèches + espace au clavier ou Gamepad).</div>
<div style="text-align: justify;">La musique est de Spintronic.</div>
<ul style="text-align: justify;">
<ul style="text-align: justify;">
	<li>Télécharger les sources : <a title="http://www.valryon.fr/telechargements/?did=2" href="http://www.valryon.fr/telechargements/?did=2">http://www.valryon.fr/telechargements/?did=2</a> (3 Mo)</li>
	<li>Générez et lancez avec <a href="http://uppix.net/3/3/4/0dc75fba84df17c897ac2f2a6aae9.html"><img src="http://uppix.net/3/3/4/0dc75fba84df17c897ac2f2a6aae9.png" alt="Image hosted by uppix.net" border="0" /></a></li>
	<li>Jouez ;)</li>
</ul>
</ul>
<p style="text-align: center;"><a href="http://uppix.net/a/4/6/061683cf315ad56097c4d4ee19be0.html"><img class="aligncenter" src="http://uppix.net/a/4/6/061683cf315ad56097c4d4ee19be0tt.jpg" alt="Image hosted by uppix.net" border="0" /></a></p>
<p style="text-align: justify;">Sauf erreur, vous avez sur votre machine tout ce qu'il faut pour commencer à développer. Mon petit projet vous aura je l'espère motivé(e)(s), et je vous invite à survoler le code pour vous familiariser avec le C# et XNA. Si vous n'avez jamais programmé en objet, il va falloir aller voir du côté de developpez.net / site du zéro pour prendre quelques cours car je ne m'attarderai pas sur ces notions.</p>

<h3 style="text-align: justify;">Quelques conseils</h3>
<p style="text-align: justify;">Une partie un peu plus subjective : les quelques conseils que j'aimerai donner à celui ou celle qui se lance dans l'aventure XNA.</p>

<ol style="text-align: justify;">
	<li>Bien gérer son code. Et pour cela, utiliser un gestionnaire de source comme SVN. Il existe des dépôts gratuits (<a title="Assembla, serveurs SVN" href="http://www.assembla.com/">Assembla </a>en cherchant un peu dans les offres propose plusieurs centaines de mégas et une interface sympa) et de très bon clients (comme <a title="Tortoise, client SVN" href="http://tortoisesvn.net/">Tortoise</a>) qui vous éviteront de perdre des fichiers et qui facilitent énormément le travail en équipe.</li>
	<li>Accrochez-vous : Quand on crée à un jeu on arrive toujours à un point où l'on a terminé ce qui nous intéressait et où il reste des parties moins marrantes à faire (personnellement, autant j'adore créer un moteur de jeu autant j'ai horreur de créer du contenu derrière...). Même s'il ne faut pas que cela devienne une corvée, il faut parfois savoir se pousser un peu pour terminer son jeu (ou une partie du jeu).</li>
	<li>Informez-vous. Et de plusieurs façons : en regardant la concurrence (<a title="TIG" href="http://www.tigsource.com/">tigsource</a>), les infos (<a title="Gamasutra" href="http://www.gamasutra.com/">Gamasutra</a>) et surtout en jouant.</li>
</ol>
<p style="text-align: justify;">Dans la prochaine partie on s'attaquera au code, à la structure qui est définie par XNA et on compilera notre premier Hello World. Wouhou !</p>