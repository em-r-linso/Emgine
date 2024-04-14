using EmgineCore;
using EmgineExperiments;

var game         = new Game(new(800, 600), "Emgine Experiments");
var initialState = new MainState();
game.Start(initialState);