using EmgineCore;
using TomsHouseFMV;

var game = new Game(new(800, 600), "Tom's House");

//TODO: DebugState if in debug mode, otherwise start with the first state of the game
game.Start(new DebugState());