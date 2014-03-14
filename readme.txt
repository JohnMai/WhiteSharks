dialoguer_data.xml is what you'll need to put into the main game
at Assets->DialougerOutput->Resources->dialoguer_data.xml

The dialogues are:
0 - intro
1 - Shammy intro
2 - Alexia intro
3 - Noel Case 1
4 - Carlos Case 1
5 - Alexia end Case 1
6 - Pilot Intro (it’s at the end because I forgot about it)

For the global variables:
Booleans:
0 - RightTalker
which side is currently speaking
false or 0 means the left side is talking
true or 1 means the right side is talking

Floats:
0 - LeftSprite
1 - RightSprite
for which sprite to display on which side out of an array of
0 = Boss 1 (caring)
1 = Boss 2 (tough)
2 = Jane Default
3 = Shammy default
4 = Alexia Default
5 = May Default
6 = Noel Default
7 = Carlos Default
8 = Pilot Default

I assume we’ll have the defaults made so for now everything is just set to that. More
will be added when we get the art for them.
