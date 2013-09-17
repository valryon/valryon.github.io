---
title: The Great Paper Adventure - Post Mortem
layout: post
---

<a title="Using cheat codes - Kawaii - Indie DB" href="http://www.indiedb.com/games/the-great-paper-adventure/images/using-cheat-codes-kawaii" onclick="javascript:_gaq.push(['_trackEvent','outbound-article','http://www.indiedb.com']);" target="_blank"><img class="aligncenter" src="http://media.indiedb.com/cache/images/games/1/13/12847/thumb_620x2000/kawaii_mode.png" alt="Using cheat codes - Kawaii"></a>

Il y a un peu plus de deux ans, aux alentours d’avril 2009, je téléchargeais Visual studio C# 2008 Express et XNA 3.1 dans le but de tester un peu cette technologie et de faire un remake rapide de mon jeu NDS [Shmup](http://www.valryon.fr/shmup-nds/).

Aujourd’hui, en juin 2011, « Shmup XNA » est connu sous le nom de **The Great Paper Adventure**, Shmup 2D coloré, délire régressif 16 bits sur tous les plans. Avec plus de 5000 téléchargements de la version PC, et plus de 2000 de la version Xbox 360 à l’heure actuelle, ce projet d’étudiants sans le sous et sans moyens est un succès à nos yeux.

Mais revenons sur les points forts et faibles du développement du jeu, et essayons d’en tirer quelque chose pour notre prochain jeu.

## Ce qui a marché

### La grande aventure humaine

<img src="http://thegreatpaperadventure.com/static/images/faces/team.jpg" />

Si TGPA est allé aussi loin, pour ne pas dire jusqu’au bout, c’est avant tout grâce à la communauté de gens qui se sont intéressés au jeu.

Au début seul, puis très vite aidé par LapinouFou surtout pour les graphismes, notre duo pourtant incroyable n’aurait pas pu terminer ce projet sans l’aide de notre cuisinière, des développeurs additionnels, de Spintronic, et de tous ces gens qui ont eu envie de nous aider. Que ce soit en en parlant autour d’eux, en nous donnant des contacts, en nous soutenant tout du long, en nous aidant techniquement, je tiens à remercier sincèrement toutes celles et ceux qui se sont donnés du mal pour nous. Et un grand merci à tout ceux qui nous ont aidé financièrement via notre collecte de dons sur [Ulule](http://fr.ulule.com/the-great-paper-adventure-1/), nous permettant ainsi de faire une version CD du jeu !

Bien que divisant les joueurs, la musique chiptune du jeu a eu pour but de renforcer le côté loufoque et rétro du titre. Je ne peux donc pas écrire un post-mortem sans parler de notre rencontre avec Spintronic, qui restera définitivement pour nous le point culminant de notre aventure. Sa gentillesse et son sens du partage nous ont permis d’avoir accès à son excellent répertoire et même de partager un bout de scène avec lui lors de son concert au Stunfest 2011.

Et en parlant du Stunfest, comment ne pas remercier la géniale association 3 Hits combo qui organise régulièrement des évènements geeks dans la ville de Rennes ? Leur initiative d’ouvrir leurs festivités à la scène indépendante locale fut une aubaine totalement inattendue pour nous.

Enfin, il paraît qu’on a le public qu’on mérite… dans ce cas on est plus que ravi des reviews de magazines que l’on adore comme [Canard PC (8/10 dans le numéro 226)](http://cpc.cx/1YT) et des avis de joueurs de nombreuses communautés. Ceux-ci ont été indulgents avec les défauts du jeu, et en ont retirés l’essentiel : du fun.

### Les graphismes

<img src="http://thegreatpaperadventure.com/static/images/artworks/art4.png" />

Bien que le jeu pêche du côté de son gameplay (voir après), l’un des points forts du jeu qui aura fait l’unanimité aura été son ambiance complètement barrée, définie par les musiques et les dessins.

On nous a souvent demandé quelles drogues ou substances plus ou moins licites nous utilisions, et voici notre secret : … rien ! Et c’est peut-être pire : toutes les bêtises, références, l’humour absurde proviennent de cerveaux « sains ».

Même s’il ne voudra jamais le reconnaître, il suffit de lire quelques critiques du jeu pour voir que les dessins de LapinouFou plaisent au plus grand nombre. Son trait unique, les couleurs chatoyantes judicieusement choisies par la directrice artistique et le travail qu’il a fourni desserve clairement le jeu. Et heureusement pour lui le pauvre, car il en a bavé ! Au final c’est plus d’une centaine de feuilles A4 qui ont été gribouillées, nettoyées, scannées puis découpées sur Paint .NET (même pas Photoshop).

Quand aux textes entre les niveaux, bien que certains les adorent, je dois avouer que leur écriture à été pour le moins chaotique. Le postulat de base était d’écrire un scénario sur une feuille de papier toilette. Les textes ont clairement été écrit sans trop de logique, plus pour meubler un écran de chargement un peu long que pour réellement raconter une histoire. Mais quelque part, c’est dans le délire TGPA.

### La communication

Une fois le développement terminé, c’est à dire 90% du travail, on dit souvent qu’il reste encore 90%. C’est tellement vrai. Déjà parce que les premiers tests des joueurs révèlent les bugs et les faiblesses du jeu, mais aussi parce que nous avons voulu jouer les indépendants jusqu’au bout. Il a donc fallu commencer à se faire connaître.

Ceci n’est pas forcément compliqué, mais c’est très laborieux. Première étape, l’ouverture de sources d’informations sur le jeu mise à jour régulièrement : notre site vitrine, la page facebook créée et un compte Twitter ouvert. Nous avons aussi une newsletter mais devant le faible nombre d’inscrits (10 personnes), il faut croire que ce mode de publicité est dépassé.

La page [IndieDB](http://www.indiedb.com/games/the-great-paper-adventure) du jeu apporte aussi une crédibilité et un espace de stockage des divers médias qui nous a été très utiles. Surtout avec le compteur de vues / de téléchargements.

Une fois ces divers outils **quantifiables** (fans, nombre de visites, etc) mis en place, nous avons pu contacter les sites concernés par les jeux Indie. Nous avons pour cela suivi [les conseils de Simon, de Pixel Prospector](http://www.pixelprospector.com/indev/2009/12/the-big-list-of-indie-game-sites/). Certains sites permettent aussi de poster ses propres news (Canard PC, dev-fr, PlayerAdvance, IndieDB) et ainsi d’avoir un premier retours de joueurs sur son travail. Car avant de contacter un site comme Destructoid mieux vaut avoir des choses à montrer (trailer, screenshots, beta/démo) et donc être rôdé.

Avec un peu d’acharnement et quelques centaines de mails, nous avons maintenant de bonnes news ou reviews de TGPA sur de nombreux sites, français mais aussi anglophones : Canard PC, GameSideStory, Joystiq, Destructoid, DIYGamer, etc.

Je me dois aussi de citer les autres développeurs XNA et tout particulièrement les petits gars de [SwingSwingSubMarine](http://www.swingswingsubmarine.com/) (Blocks That Matter) ou encore de [Creative Pattern](http://www.google.fr/url?sa=t&source=web&cd=1&ved=0CCkQFjAA&url=http%3A%2F%2Fwww.creative-patterns.com%2F&ei=6ukJToubAYaFhQfjrPmNBg&usg=AFQjCNG9tc2625Bgmrcv2Hyt1G3FQg5boQ&sig2=W-EXZ02DGIaD7-ckQRQwWw) (QuadSmash) qui nous ont fait de la pub, soutenu, bref milles mercis à eux et bonne continuation pour leurs projets.

Grâce à tout ça TGPA a réussi à se faire une place sur le « Meilleur Classement » FR du XBLIG (39e place au 28/06/11)

## Ce qui n’a pas marché

### L’échec de la version Xbox

<img src="http://uppix.net/7/0/f/2aacf1b92525102cab5bc799e95f0tt.jpg" />

Malgré d’excellentes reviews de gros sites anglophones, les ventes n’ont pas décollées. Il n’a jamais été question de devenir millionnaire avec le jeu, mais il faut reconnaître que c’est un échec commercial pour nous.

Nous avons quelques pistes pour l’expliquer :

- **La démo**. Celle-ci commence sur un niveau mou, lent, peu représentatif du produit final. Erreur ! La plupart des joueurs n’ont pas dépassé ce premier niveau (alors que le deuxième de la démo est bien mieux). Je le jugeais important pour présenter les commandes, alors qu’une simple image pour les expliquer aurait clairement été une meilleure idée. Recherchez quelques vidéos de gameplay du jeu, ou lisez le test de Joysick (FR) : beaucoup de gens pensent que le jeu se contente d’être un shooter de vaches.

- **Le prix**. Je ne pense pas que mettre le jeu à 3€ soit une erreur, surtout vu le travail que nous avons fourni. Le problème est que le marché est parasité de titres honteux ou géniaux vendus 1€, mettant les jeux vendus plus chers dans une mauvaise posture. Plus bête encore : la concurrence de la version PC qui elle est gratuite. Les joueurs ont beau être généreux, ils ne sont pas bêtes : pourquoi s’embêter à payer 3€ pour la même chose que sur PC ? D’ailleurs, beaucoup de sites indiquent que nous avons un bouton de donation style « Pay What You Want » pour cette version : nous n’avons jamais rien reçu (et nous n’avons jamais rien voulu recevoir de cette version).

### Les bruitages

Sound Designer, ça ne s’improvise pas. Au cours du développement, plusieurs solutions pour les sons ont été envisagées.

- **Faire les sons nous mêmes.** Mais sans matériel et sans idées, à moins de partir sur des bruits à la bouche qui n’auraient pas plu à tout le monde, cela ne semblait pas la bonne option.

- **Demander à Spintronic** de nous fournir les bruitages. Le pauvre, nous l’avons mis en difficulté avec cette demande. J’ai trouvé très difficile de formuler ce que j’avais besoin. « euh.. un spoutch genre un poulpe qui explose tu vois ? » Non ? Moi non plus à vrai dire…

- **Trouver des sons gratuits.** Avec un peu plus de motivation ç’aurait sûrement été la meilleure option, mais j’ai un peu bâclé cette solution. Du coup il a environ 10 sons dans le jeu, et leur manque se fait parfois cruellement sentir, en particulier pendant les boss.

Définitivement un point à ne pas reproduire pour notre futur prochain jeu ! Surtout qu’avec XNA et ses outils de Sound Design (XACT), c’est très simple et agréable à faire.

### Le gameplay Shmup non assumé

<img src="http://uppix.net/f/0/2/562695f5f8881a33758885b71d823tt.jpg" />

Mon postulat de base était, au grand désespoir de LapinouFou, de faire un Shmup un peu plus accessible et grand public que les cadors du genre. J’ai commencé le développement en connaissant très peu ce genre (j’avais à l’époque terminé uniquement Ikaruga et joué à quelques titres cultes comme R-Type mais c’est tout). j’ai donc cherché à faire un jeu qui me plairait d’abord, et c’est d’ailleurs une idée qui est restée tout au long du développement.

Mais maintenant que j’ai une bonne connaissance des shmups et que j’ai pu discuter avec quelques habitués et superplayers, il est vrai que ce positionnement un peu bâtard (shmup pas vraiment shmup) est plus un problème pour de nombreux joueurs.

D’ailleurs le titre n’est pas en reste : The Great Paper **Adventure**. L’idée est depuis longtemps de proposer un petit voyage au goût d’aventure plutôt qu’un système de scoring poussé, etc. C’est d’ailleurs pourquoi j’ai tenté de concevoir des niveaux un peu originaux : les retournements de scrolling, l’avalanche (copiée de la lave d’Aladdin) , des situations qu’on ne retrouve pas dans un shmup classique.

Mais du coup le gameplay pêche puisqu’il est très classique sans avoir toutes les caractéristiques du genre : hitbox peu compréhensible et « trop grosse », maniement perfectible, patterns des ennemis et des boss peu variés, pas de système de scoring.

Je viens de lire sur [DIYGamer](http://www.diygamer.com/2011/06/free-games-community-reviews/) que TGPA était un peu « un genre à lui tout seul ». Remarque assez amusante et qui montre bien qu’il est plus apprécié quand il n’est pas pris comme un shooter. **PROBLEME** : nous le vendons comme un shmup… Combiné à la mauvaise démo, on peut comprendre la réaction d’une partie des joueurs déçus ou désarçonnés par le jeu après l’avoir essayer sur Xbox 360.

### Conclusion

La version iPad est toujours d’actualité (je vois passer quelques commits), mais je n’en suis pas le responsable ni le développeur, donc je ne fais que la superviser de loin vite fait. Le jeu est donc pour moi terminé, même si une tonne de choses pourrait être ajoutée : scores en ligne, nouveaux niveau/boss (surtout qu’il y en a un fait à 40%), retouches, etc.

Mais nous allons juste nous contenter de considérer le jeu terminé et profiter de ce succès qui nous a motivé à faire d’autres jeux funs et modestes. A bientôt pour de nouvelles aventures, probablement pas en papier mais que l’on espère aussi fantastique !

Damien. The Great Paper Team Leader of The Great Paper Adventure Of The Nameless Hero In A Fantastic And Papered World game.

### A voir aussi

- [The Great Paper Artbook : l’artbook du jeu, les ennemis, les décors, la technique](http://d.pr/f/rLif)
