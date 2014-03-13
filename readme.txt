dialoguer_data.xml is what you'll need to put into the main game
at Assets->DialougerOutput->Resources->dialoguer_data.xml

There are no current global variables due to the lack of 
knowing how much character art we’ll even have. We should list these
soon and figure out how to sort them, but when global variables are made
they will be 2 floats, each one telling dialoguer what sprite to draw
for either the left or right side. Then one boolean to tell which side is talking.

The dialogues are:
0 - intro
1 - Shammy intro
2 - Alexia intro
3 - Noel Case 1
4 - Carlos Case 1
5 - Alexia end Case 1
