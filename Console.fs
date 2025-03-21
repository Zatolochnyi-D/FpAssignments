module FpAssignments.Console

open System

let consoleSize () =
    Console.WindowWidth, Console.WindowHeight

let clear () =
    Console.Clear ()

// Handle ArgumentOutOfRange
let writeText x y (text: string) =
    if x > -1 && y > -1 && x < Console.BufferWidth && y < Console.BufferHeight then
        Console.SetCursorPosition (x, y)
        Console.Write text
    else 
        Console.SetCursorPosition (0, 0)
        let width, height = consoleSize ()
        for _ = 0 to width * height do
            Console.Write "X"