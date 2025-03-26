module FpAssignments.Program

open System
open System.Collections.Generic
open Window
open GUI
open MazeGenerator

[<EntryPoint>]
let main args = 
    let window = create 120 45 30

    let exitBind = { key = ConsoleKey.Escape; func = fun () -> Environment.Exit 0 }
    addBinding window exitBind

    mainLoop window
    // addContent window (generateMaze window 40 15 100000)
    0