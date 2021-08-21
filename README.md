# BarbezDotEu.Sudoku
Yet another Sudoku repo with the description "Yet another Sudoku repo".

## Generating a new game of sudoku

Simply instantiate a new game. From there on out, its 'Solution' property is available.

    var game = new SudokuGame();
    IEnumerable<byte[]> solution = game.Solution;

Then, your own game logic is responsible to omit certain elements from the solution, and to have the gamer guess what they should be.
