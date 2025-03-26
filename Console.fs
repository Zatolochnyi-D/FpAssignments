module FpAssignments.Console
open System

let hideCursor () =
    Console.CursorVisible <- false

let keyReader () = 
    let mutable key = ConsoleKey.None
    async {
        while true do 
            key <- (Console.ReadKey true).Key
    } |> Async.Start
    fun () -> 
        let result = key
        key <- ConsoleKey.None
        result

let consoleSize () =
    Console.WindowWidth, Console.WindowHeight

let clear () =
    Console.Clear ()

// Handle ArgumentOutOfRange
let writeString x y (text: string) =
    Console.SetCursorPosition (x, y)
    Console.Write text

let writeChar x y (string: char) =
    Console.SetCursorPosition(x, y)
    Console.Write string