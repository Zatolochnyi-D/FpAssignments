module FpAssignments.Console
open System

let hideCursor () =
    Console.CursorVisible <- false

let preventInputPrinting () = 
    async {
        while true do 
            Console.ReadKey true |> ignore
    } |> Async.Start

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