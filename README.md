# BarbezDotEu.Sudoku
Yet another Sudoku repo with the description "Yet another Sudoku repo".

## Generating a new game of sudoku

Simply instantiate a new game. From there on out, its 'Solution' property is available. From here on out, your own game logic is responsible to omit certain elements from it, and have the gamer guess what they should be.

    var game = new SudokuGame();
    IEnumerable<byte[]> solution = game.Solution;
